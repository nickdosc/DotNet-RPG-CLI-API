namespace Project_CL.Data.user
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;

        public List<Character> Characters { get; set; } = new List<Character>();

    }
}
