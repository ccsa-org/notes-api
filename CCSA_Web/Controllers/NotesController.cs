using CCSANoteApp.Domain;
using CCSANoteApp.Domain.DTOs;
using CCSANoteApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCSA_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        public INoteService NoteService { get; }
        public IUserService UserService { get; }
        public NotesController(INoteService databaseService, IUserService userService)
        {
            NoteService = databaseService;
            UserService = userService;
        }

        [HttpPost("create-note")]
        public IActionResult CreateNote([FromBody] NoteDto note)
        {
            var useid = UserService.GetUser(note.CreatorUserId);
            if (useid==null)
            {
                return NotFound();
            }
            NoteService.CreateNote(note.CreatorUserId,note.Title, note.Content, note.GroupName);
            
            return Ok("Created Successfully");
        }

        
        [HttpDelete]
        public IActionResult DeleteNote(Guid id)
        {
            NoteService.DeleteNote(id);
            return Ok("Deleted Successfully");
        }

        [HttpDelete("multiple")]
        public IActionResult DeleteNote([FromBody]List<Guid> noteIds)
        {
            NoteService.DeleteNote(noteIds);
            return Ok("Deleted Successfully");
        }
        [HttpGet]
        public IActionResult FetchNote()
        {
            return Ok(NoteService.FetchNote());
        }
        [HttpGet("user-notes-group")]
        public IActionResult FetchUserNotesByGroup(Guid userId, GroupName groupName)
        {
            return Ok(NoteService.FetchUserNotesByGroup(userId,groupName));
        }

        [HttpGet("notes-bygroup")]
        public IActionResult FetchNotesByGroup(GroupName groupName)
        {
            return Ok(NoteService.FetchNotesByGroup(groupName));
        }

        [HttpGet("by-id/{noteId}")]
        public IActionResult FetchNoteById(Guid noteId)
        {
            FetchNoteDto note = NoteService.FetchNoteById(noteId);
            if(note.NoteId == Guid.Empty)
            {
                return NotFound();
            }
            return Ok(note);
        }


        [HttpGet("by-user/{userId}")]
        public IActionResult FetchNoteByUser(Guid userId)
        {
            var user = UserService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            var notes = NoteService.FetchNoteByUser(userId);
            return Ok(notes);
        }

        [HttpPut("title")]
        public IActionResult UpdateNoteTitle(Guid id, string title)
        {
            NoteService.UpdateNoteTitle(id, title);
            return Ok("Updated Successfully");
        }
    }
}
