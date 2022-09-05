using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.Domain
{
    public class TokenData:BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Identifier { get; set; }
        public virtual string Email { get; set; }
        public virtual string RefreshToken { get; set; }
        public virtual DateTime TokenExpiry { get; set; }
    }
}
