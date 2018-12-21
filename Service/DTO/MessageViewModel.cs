using System;
using System.Collections.Generic;
using System.Text;
using Service.dto;

namespace Service.DTO
{
    public class MessageViewModel
    {
        public string Text { get; set; }

        public UserViewModel UserFrom { get; set; }
        public string UsernameTo { get; set; }
    }
}
