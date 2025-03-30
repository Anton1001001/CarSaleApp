using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;

namespace File.Core.Extensions;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> ForCtorParam<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression,
        Expression<Func<TDestination, TMember>> destinationMember,
        Action<ICtorParamConfigurationExpression<TSource>> paramOptions)
    {
        var memberInfo = FindProperty(destinationMember);
        var memberName = memberInfo.Name;
        
        return mappingExpression
            .ForCtorParam(memberName, paramOptions);
    }

    private static MemberInfo FindProperty(LambdaExpression lambdaExpression)
    {
        var expressionToCheck = lambdaExpression.Body;
        while (true)
        {
            switch (expressionToCheck)
            {
                case MemberExpression
                {
                    Member: var member, Expression: { NodeType: ExpressionType.Parameter or ExpressionType.Convert }
                }:
                    return member;
                case UnaryExpression { Operand: var operand }:
                    expressionToCheck = operand;
                    break;
                default:
                    throw new ArgumentException(
                        $"Expression '{lambdaExpression}' must resolve to top-level member and not any child object's properties. You can use ForPath, a custom resolver on the child type or the AfterMap option instead.",
                        nameof(lambdaExpression));
            }
        }
    }
}