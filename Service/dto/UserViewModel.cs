using System.Collections.Generic;

namespace Service.dto
{
    public class UserViewModel
    {
        public  string Name { get; set; }

        public  string Surname { get; set; }

        public  string Role { get; set; }

        public string Email { get; set; }

        public  string Password { get; set; }

        public  string PhoneNumber { get; set; }

        public ICollection<FlatViewModel> Flats { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }

    }
}
