using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service
{
    public class AuthOptions
    {
        public const string Issuer = "RoomWorldServer"; 
        public const string Audience = "https://localhost:44394"; 
        const string Key = "keyertyertyet5345345fgf4";   
        public const int Lifetime = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
