using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Services.Interfaces;
using Auth.Shared;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Advert.Application.CQRS.Queries.PrintAdvert;

public class PrintAdvertHandler(IAdvertService advertService, IOptions<JwtSettings> options)
    : IRequestHandler<PrintAdvertQuery, Result<PrintAdvertResponse>>
{
    private readonly JwtSettings _jwtSettings = options.Value;
    
    public async Task<Result<PrintAdvertResponse>> Handle(PrintAdvertQuery request, CancellationToken cancellationToken)
    {
        var advertResult = await advertService.GetAdvertByIdAsync(request.Id, cancellationToken);

        if (advertResult.IsFailed)
            return Result.Fail(advertResult.Errors);

        var advert = advertResult.Value;

        var photoUrl = advert.Photos.FirstOrDefault(p => p.Main)?.Big.Url
                       ?? advert.Photos.FirstOrDefault()?.Big.Url;

        var pdfBytes = GenerateAdvertPdf(advert, photoUrl);

        // var filePath = Path.Combine(Directory.GetCurrentDirectory(),
        //     $"C:\\Users\\ryan_gosling\\Desktop\\Folder\\Advert_{advert.Id}.pdf");
        // await File.WriteAllBytesAsync(filePath, pdfBytes, cancellationToken);

        return new PrintAdvertResponse(pdfBytes);
    }

    private byte[] GenerateAdvertPdf(AdvertResponse advert, string? photoUrl)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        if (advert.Parameters is not CarParametersResponse parameters)
            throw new Exception("Invalid parameters type");

        var advertUrl = $"{_jwtSettings.ValidAudience}/advert-details/{advert.Id}";
        var qrBytes = GenerateQrCode(advertUrl);

        byte[]? imageBytes = null;
        if (!string.IsNullOrEmpty(photoUrl))
        {
            try
            {
                imageBytes = DownloadImage(photoUrl);
            }
            catch
            {
                imageBytes = null;
            }
        }

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(26));

                page.Header()
                    .Column(col =>
                    {
                        col.Item()
                            .PaddingBottom(24)
                            .Text("Продаю на clutch.by")
                            .FontSize(80)
                            .ExtraBold();
                    });
                page.Content()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Column(col =>
                            {
                                col.Item().PaddingBottom(12)
                                    .Text($"{parameters.Brand} {parameters.Model} {parameters.Generation} {parameters.Modification}");
                                col.Item().Text($"{parameters.Year} г.").Bold();
                                col.Item().Text($"{parameters.TransmissionType}".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.EngineType}, {GetFormattedEngineCapacity(parameters.EngineCapacity)} л".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.DriveType}".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.BodyType}".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.MileageKm} км".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.Color}".CapitalizeFirstLetter()).Bold();
                                col.Item().Text($"{parameters.EnginePower} л.c".CapitalizeFirstLetter()).Bold();
                            });

                        if (imageBytes != null)
                        {
                            row.ConstantItem(280)
                                .AlignTop()
                                .AlignRight()
                                .Image(imageBytes)
                                .FitArea();
                        }
                    });
                page.Footer()
                    .Column(col =>
                    {
                        col.Item()
                            .PaddingTop(12)
                            .AlignCenter()
                            .Width(180)
                            .Text(text =>
                            {
                                text.Span("Отсканируйте QR-код, чтобы открыть объявление")
                                    .FontSize(12)
                                    .FontColor("#757575");
                            });
                        col.Item()
                            .AlignCenter()
                            .PaddingTop(4)
                            .PaddingBottom(12)
                            .Border(0.5f)
                            .Width(180)
                            .Height(180)
                            .Image(qrBytes)
                            .FitArea();
                    });
            });
        }).GeneratePdf();

        return document;
    }

    private byte[] DownloadImage(string url)
    {
        using var client = new HttpClient();
        return client.GetByteArrayAsync(url).Result;
    }

    private byte[] GenerateQrCode(string url)
    {
        using var qrGenerator = new QRCoder.QRCodeGenerator();
        using var qrData = qrGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new QRCoder.PngByteQRCode(qrData);
        return qrCode.GetGraphic(20);
    }

    private static string? GetFormattedEngineCapacity(int? value)
    {
        var liters = value / 1000.0;
        return liters?.ToString("0.0").Replace('.', ',');
    }
}

public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str[1..];
    }
}
