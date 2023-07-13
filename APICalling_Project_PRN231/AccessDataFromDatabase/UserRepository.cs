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
            result.Image = user.Image;
            return result;
        }

        public User GetUser(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == model.UserName && x.Password == model.Password);
            return user;
        }

        public User CheckExist(RegisterModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username.Trim() == model.UserName);
            return user;
        }

        public string Register(RegisterModel model)
        {
            string mess = "Tên tài khoản đã tồn tại";
            var u = _context.Users.FirstOrDefault(x => x.Username == model.UserName.Trim());
            if(u == null)
            {
                mess = "";
                User user = new User();
                user.Username = model.UserName.Trim();
                user.Password = model.Password.Trim();
                user.Role = "reviewer";
                user.Image = "/assets/images/review/author-4.jpg";
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return mess;
        }

    }
}
