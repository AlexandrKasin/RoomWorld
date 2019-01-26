using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Service.DTO
{
    public class RegistrationApartmentViewModel
    {
        public IFormCollection FormFile { get; set; }


        public string HeadTitle { get; set; }
        public string Description { get; set; }
        public int Accommodates { get; set; }
        public double ApartmentSize { get; set; }


        public double Cost { get; set; }



      

        public int CountBathroom { get; set; }


        public int CountBedroom { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}