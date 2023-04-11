namespace Srtream.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetFormattedDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}
