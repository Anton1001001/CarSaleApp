namespace Advert.Domain.Constants;

public static class AdvertPrivateStatus
{
    public const string Active = "active";
    public const string PostModeration = "postmoderation";
    public const string PreModeration = "premoderation";
    public const string Rejected = "rejected";
    public const string Paused = "paused";
    public const string Sold = "sold";
    public const string Removed = "removed";
    public const string WaitingForPayment = "waiting_for_payment";
    public const string PaidPublicationExpired = "paid_publication_expired";
}