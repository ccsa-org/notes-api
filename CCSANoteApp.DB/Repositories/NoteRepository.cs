using CCSANoteApp.Domain;

namespace CCSANoteApp.DB.Repositories
{
    public class NoteRepository : Repository<Note>
    {
        public NoteRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public List<Note> FetchUserNotesByGroup(Guid userId, GroupName groupName)
        {
            var notes = _session.Query<Note>().Where(x => x.NoteCreator.Id == userId && x.GroupName == groupName);
            return notes.ToList();
        }

        public List<Note> FetchNotesByGroup(GroupName groupName)
        {
            var notes = _session.Query<Note>().Where(x => x.GroupName == groupName);
            return notes.ToList();
        }

        public List<Note> FetchUserNotes(Guid userId)
        {
            var notes = _session.Query<Note>().Where(x => x.NoteCreator.Id == userId);
            return notes.ToList();
        }
    }
}
