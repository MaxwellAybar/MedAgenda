namespace MedAgenda.WebMVC.Models
{
    public class SystemHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}