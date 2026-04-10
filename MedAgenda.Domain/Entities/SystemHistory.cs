namespace MedAgenda.Domain.Entities
{
    public class SystemHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }       
        public string Action { get; set; }     
        public System.DateTime Date { get; set; }  
    }
}