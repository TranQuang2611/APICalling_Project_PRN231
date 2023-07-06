using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; }
        public bool IsAdmin
        {
            get
            {
                if (Role == "admin")
                {
                    return true;
                }
                return false;
            }
        }
    }
}
