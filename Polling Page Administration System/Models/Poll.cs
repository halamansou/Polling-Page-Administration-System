namespace Polling_Page_Administration_System.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
