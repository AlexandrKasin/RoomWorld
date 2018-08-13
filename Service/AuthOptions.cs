using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service
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
