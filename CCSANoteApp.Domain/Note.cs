using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Domain
{
    public class Note : BaseEntity
    {
        public Note()
        {
            Id = Guid.NewGuid();
        }
        public virtual User NoteCreator { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual GroupName GroupName { get; set; }
        public virtual DateTime CreatedDate { get; protected set; } = DateTime.Now;
        public virtual DateTime UpdatedDate { get; protected set; } = DateTime.Now;

    }
}
