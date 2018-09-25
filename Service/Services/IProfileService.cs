using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IProfileService
    {
        Task<User> GetProflieByEmail(string email);
    }
}
