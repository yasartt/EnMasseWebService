namespace EnMasseWebService.Models
{
    public class EnteractMongoDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DailyImagesCollectionName { get; set; } = null!;
        public string SessionMessagesCollectionName { get; set; } = null!;
    }
}

