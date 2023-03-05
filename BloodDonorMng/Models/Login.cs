namespace AuthProject.Models
{
    public class Login
    {
        public int id { get; set; }
        public string Email { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public bool KeepLoggedIn { get; set; }


    }
}
