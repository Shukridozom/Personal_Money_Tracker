using PersonalMoneyTracker.Core.Models;

namespace PersonalMoneyTracker.Dtos
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
