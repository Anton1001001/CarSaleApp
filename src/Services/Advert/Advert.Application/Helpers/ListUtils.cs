namespace Advert.Application.Helpers;

public static class ListUtils
{
    public static List<T> GetDistinctValues<T, TModel, TKey>(
        List<TModel> modifications,
        Func<TModel, T> selector,
        Func<T, TKey> keySelector)
        => modifications.Select(selector).DistinctBy(keySelector).ToList();

    public static List<T> FilterList<T, TValue>(
        List<T> items,
        TValue? filterValue,
        Func<T, TValue> selector
    ) where TValue : struct, IEquatable<TValue>
    {
        return filterValue is not null
            ? items.Where(item => selector(item).Equals(filterValue.Value)).ToList()
            : items;
    }
}