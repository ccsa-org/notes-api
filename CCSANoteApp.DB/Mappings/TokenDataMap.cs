using CCSANoteApp.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSANoteApp.DB.Mappings
{
    public class TokenDataMap:ClassMap<TokenData>
    {
        public TokenDataMap()
        {
            Table("TokenData");
            Id(user => user.Id);
            Map(user => user.RefreshToken);
            Map(user => user.TokenExpiry);
            Map(user => user.Name);
            Map(user => user.Email);
            Map(user => user.Identifier);
            
        }
    }
}
