namespace LimehouseStudios.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public int PostCount
        {
            get
            {
                if (Posts != null)
                {
                    return Posts.Count();
                }
                return 0;
            }
        }
    }
}