namespace EnMasseWebService.Models.DTOs
{
    public class DailyView
    {
        public int DailyId { get; set; }
        public int UserId { get; set; }
        public string? Caption { get; set; }
        public DateTime? Created { get; set; }
        public List<ImageDTO>? Images { get; set; }
        public string UserName { get; set; }
        public string? UserPhotoId { get; set; }
    }
}
