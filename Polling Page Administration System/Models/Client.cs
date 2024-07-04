namespace Polling_Page_Administration_System.Models
{
    public class Client
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClientAnswer> ClientAnswers { get; set; }
    }
}
