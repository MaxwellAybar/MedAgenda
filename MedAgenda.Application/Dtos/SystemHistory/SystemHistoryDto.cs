namespace MedAgenda.Application.Dtos.SystemHistory
{
    public class SystemHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }       
        public string Action { get; set; }
        public System.DateTime Date { get; set; } 
    }
}