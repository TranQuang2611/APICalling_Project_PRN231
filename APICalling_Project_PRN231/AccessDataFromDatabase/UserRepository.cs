using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class UserRepository
    {
        private readonly ReviewStoreContext _context;

        public UserRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public UserDTO GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);  
            UserDTO result = new UserDTO();
            result.UserId = user.UserId;
            result.Username = user.Username;
            return result;
        }

        public User GetUser(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == model.UserName && x.Password == model.Password);
            return user;
        }

    }
}
