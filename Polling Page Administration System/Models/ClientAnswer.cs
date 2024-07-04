namespace Polling_Page_Administration_System.Models
{
    public class ClientAnswer
    {

        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
