namespace Advert.Domain.Constants;

public static class AdvertStatus
{
    public const string Draft = "draft";
    public const string WaitingForPayment = "waiting_for_payment";
    public const string Active = "active";
    public const string PreModeration = "premoderation";
    public const string Rejected = "rejected";
    public const string Paused = "paused";
    public const string Removed = "removed";
    public const string Archived = "archived";
    public const string Purged = "purged";
}
