namespace LimehouseStudios.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
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