using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoomWorld.Models;

namespace Service.iServices
{
    public interface IRegistrationService
    {
        Task RegistrateUserAsunc(User user);
    }
}
