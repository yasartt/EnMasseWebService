namespace EnMasseWebService.Models.DTOs
{
    public class ContactRequestDTO
    {
        public int SenderId {  get; set; }
        public int ReceiverId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
