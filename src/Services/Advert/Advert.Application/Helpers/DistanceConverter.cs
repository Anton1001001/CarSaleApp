namespace Advert.Application.Helpers;

public static class DistanceConverter
{
    private const double Factor = 1.60934d;
    private const int MaxMileage = 999999;
    
    public static int MilesToKilometers(int miles)
    {
        var kilometers = (int)(miles * Factor);
        if (kilometers > MaxMileage)
        {
            kilometers = MaxMileage;
        }

        return kilometers;
    }
}