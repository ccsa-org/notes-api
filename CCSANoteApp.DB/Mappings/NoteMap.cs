using CCSANoteApp.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.DB.Mappings
{
    public class NoteMap:ClassMap<Note>
    {
        public NoteMap()
        {
            Table("Notes");
            Id(note => note.Id);
            Map(note => note.Content);
            Map(note=>note.Title);
            Map(note=>note.CreatedDate);
            Map(note=>note.UpdatedDate);
            Map(note=>note.GroupName);
            References(note=>note.NoteCreator).Not.Nullable();
        }
    }
}
