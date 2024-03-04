namespace EnMasseWebService.Models.DTOs
{
    public class DailyDTO
    {
        public Guid UserId { get; set; }

        public string? Caption { get; set; }

        public DateTime? Created { get; set; }

        public List<ImageIncomingDTO>? Images { get; set; }
    }
}
