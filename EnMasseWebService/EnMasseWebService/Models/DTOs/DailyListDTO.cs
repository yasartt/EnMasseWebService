namespace EnMasseWebService.Models.DTOs
{
    public class DailyListDTO
    {
        public int UserId { get; set; }

        public DateTime? LastTime { get; set; }

        public int? LastDailyId { get; set; }
    }
}
