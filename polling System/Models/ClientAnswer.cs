namespace Polling_System.Models
{
    public class ClientAnswer
    {

        public int Id { get; set; }
        public int ClientId { get; set; }

        //public client Client { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
