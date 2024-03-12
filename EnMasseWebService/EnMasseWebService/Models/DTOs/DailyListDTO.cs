namespace EnMasseWebService.Models.DTOs
{
    public class DailyListDTO
    {
        public Guid UserId { get; set; }

        public DateTime? LastTime { get; set; }

        public Guid? LastDailyId { get; set; }
    }
}
