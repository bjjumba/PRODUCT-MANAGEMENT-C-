using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application
{
    public  interface IUserLogin
    {
        Task<UserAuth> GetCredentialAsync(string userName, string password);
        Task<int> AddUserAsync(UserAuth userAuth);

    }
}
