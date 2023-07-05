namespace APICalling_Project_PRN231.DTO
{
    public class ApiRespond
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
