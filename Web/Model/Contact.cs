namespace Web.Model
{
    public class Contact : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}