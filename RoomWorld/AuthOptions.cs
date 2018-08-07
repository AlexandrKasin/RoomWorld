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
        public const string ISSUER = "RoomWorldServer"; // издатель токена
        public const string AUDIENCE = "https://localhost:44394"; // потребитель токена
        const string KEY = "keyertyertyet5345345fgf4";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
