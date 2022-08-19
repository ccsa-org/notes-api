using CCSANoteApp.Domain;
using CCSANoteApp.Domain.DTOs;

namespace CCSANoteApp.Infrastructure
{
    public interface INoteService
    {
        void CreateNote(Note note);
        void CreateNote(Guid creatorUserId, string title, string content,GroupName groupName);
        void UpdateNoteTitle(Guid id, string title);
        void DeleteNote(Guid id);
        void DeleteNote(List<Guid> notes);
        List<FetchNoteDto> FetchNote();
        List<FetchNoteDto> FetchNoteByUser(Guid id);
        FetchNoteDto FetchNoteById(Guid id);
        List<FetchNoteDto> FetchUserNotesByGroup(Guid userId, GroupName groupName);
        public List<FetchNoteDto> FetchNotesByGroup(GroupName groupName);
    }
}
