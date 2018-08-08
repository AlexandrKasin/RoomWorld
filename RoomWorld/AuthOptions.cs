using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace RoomWorld
{
    public class AuthOptions
    {
        public const string ISSUER = "RoomWorldServer"; 
        public const string AUDIENCE = "https://localhost:44394"; 
        const string KEY = "keyertyertyet5345345fgf4";   
        public const int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
