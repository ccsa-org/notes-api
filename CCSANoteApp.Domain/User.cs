namespace CCSANoteApp.Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            Notes = new List<Note>();
        }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<Note> Notes { get; set; }

    }
}
