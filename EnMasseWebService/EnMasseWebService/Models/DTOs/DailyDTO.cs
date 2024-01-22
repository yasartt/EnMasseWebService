namespace EnMasseWebService.Models.DTOs
{
    public class DailyDTO
    {
        public int DailyTypeId { get; set; }
        public int UserId { get; set; }

        public int PhotoNumber { get; set; }

        public int VideoNumber { get; set; }

        public string? Caption { get; set; }
    }
}
