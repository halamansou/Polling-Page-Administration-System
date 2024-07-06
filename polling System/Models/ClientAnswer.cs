using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polling_System.Models
{
    public class ClientAnswer
    {

        public int Id { get; set; }


        [ForeignKey("user")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
