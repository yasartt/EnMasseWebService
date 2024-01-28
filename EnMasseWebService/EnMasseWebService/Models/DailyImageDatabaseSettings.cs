namespace EnMasseWebService.Models
{
    public class DailyImageDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DailyImagesCollectionName { get; set; } = null!;
    }
}

