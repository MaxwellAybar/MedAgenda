namespace MedAgenda.Application.Dtos.SystemHistory
{
    public class CreateSystemHistoryDto
    {
        public int UserId { get; set; }     
        public string Action { get; set; }
    }
}