using CCSANoteApp.DB.Repositories;
using CCSANoteApp.Domain;
using CCSANoteApp.Domain.DTOs;

namespace CCSANoteApp.Infrastructure
{
    public class NoteService : INoteService
    {
        private readonly NoteRepository _noteRepository;
        private readonly UserRepository _userRepository;

        public NoteService(NoteRepository noteRepository, UserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void CreateNote(Note note)
        {
            _noteRepository.Add(note);
        }

        public void CreateNote(Guid creatorUserId, string title, string content, GroupName groupName)
        {
            var creator = _userRepository.GetById(creatorUserId);
            var note = new Note
            {
                Title = title,
                Content = content,
                NoteCreator = creator,
                GroupName = groupName
            };
            _noteRepository.Add(note);
        }

        public void DeleteNote(Guid id)
        {
            var note = _noteRepository.GetById(id);
            if (note != null)
            {
                _noteRepository.Delete(note);
            }
        }

        public void DeleteNote(List<Guid> noteIds)
        {
            foreach (var id in noteIds)
            {
                DeleteNote(id);
            }
        }

        public List<FetchNoteDto> FetchNote()
        {
            var notes = _noteRepository.GetAll();
            var result = new List<FetchNoteDto>();
            foreach (var note in notes)
            {
                result.Add(new FetchNoteDto
                {
                    Content = note.Content,
                    CreatedDate = note.CreatedDate,
                    NoteCreatorUserName = note.NoteCreator.Username,
                    GroupName = note.GroupName,
                    Title = note.Title,
                    UpdatedDate = note.UpdatedDate,
                    NoteId = note.Id
                });
            }
            return result;
        }

        public List<FetchNoteDto> FetchUserNotesByGroup(Guid userId, GroupName groupName)
        {
            var _notes = _noteRepository.FetchUserNotesByGroup(userId, groupName);
            var result = new List<FetchNoteDto>();
            foreach (var note in _notes)
            {
                result.Add(new FetchNoteDto
                {
                    Content = note.Content,
                    CreatedDate = note.CreatedDate,
                    NoteCreatorUserName = note.NoteCreator.Username,
                    GroupName = note.GroupName,
                    Title = note.Title,
                    UpdatedDate = note.UpdatedDate,
                    NoteId = note.Id
                });
            }
            return result;
        }

        public FetchNoteDto FetchNoteById(Guid id)
        {
            var note = _noteRepository.GetById(id);
            if (note is not null)
            {
                var result = new FetchNoteDto
                {
                    Content = note.Content,
                    CreatedDate = note.CreatedDate,
                    NoteCreatorUserName = note.NoteCreator.Username,
                    GroupName = note.GroupName,
                    Title = note.Title,
                    UpdatedDate = note.UpdatedDate,
                    NoteId = note.Id
                };
                return result;
            }
            return new FetchNoteDto();
        }

        public List<FetchNoteDto> FetchNoteByUser(Guid creatorId)
        {
            var notes = _noteRepository.FetchUserNotes(creatorId);
            var result = new List<FetchNoteDto>();
            foreach (var note in notes)
            {
               result.Add(new FetchNoteDto
               {
                        Content = note.Content,
                        CreatedDate = note.CreatedDate,
                        NoteCreatorUserName = note.NoteCreator.Username,
                        GroupName = note.GroupName,
                        Title = note.Title,
                        UpdatedDate = note.UpdatedDate,
                        NoteId = note.Id
               });
            }
           return result;         
        }

        public void UpdateNote(Guid id, string title, string content, GroupName group)
        {
            var _note = _noteRepository.GetById(id);
            if (_note != null)
            {
                _note.Title = title;
                _note.Content = content;
                _note.GroupName = group;
            _noteRepository.Update(_note);
            }
        }

        public void UpdateNoteTitle(Guid id, string title)
        {
            var _note = _noteRepository.GetById(id);
            if (_note != null)
            {
                _note.Title = title;
            _noteRepository.Update(_note);
            }
        }

        public List<FetchNoteDto> FetchNotesByGroup(GroupName groupName)
        {
            var _notes = _noteRepository.FetchNotesByGroup(groupName);
            var result = new List<FetchNoteDto>();
            foreach (var note in _notes)
            {
                result.Add(new FetchNoteDto
                {
                    Content = note.Content,
                    CreatedDate = note.CreatedDate,
                    NoteCreatorUserName = note.NoteCreator.Username,
                    GroupName = note.GroupName,
                    Title = note.Title,
                    UpdatedDate = note.UpdatedDate,
                    NoteId = note.Id
                });
            }
            return result;
        }
    }
}
