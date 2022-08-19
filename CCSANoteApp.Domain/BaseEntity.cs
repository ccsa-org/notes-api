using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Domain
{
    public class BaseEntity
    {
        public virtual Guid Id { get; protected set; }
    }
}
