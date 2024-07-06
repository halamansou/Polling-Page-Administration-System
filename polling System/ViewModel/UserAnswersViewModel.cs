namespace Polling_System.Models
{
    public class UserAnswersViewModel
    {
        public string UserName { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
