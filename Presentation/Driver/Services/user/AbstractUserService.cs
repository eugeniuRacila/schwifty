using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Models;


namespace Driver.Services.user
{
    public abstract class AbstractUserService : IMutualService
    {
        public abstract Task<string> RegisterUser(User user);
        public abstract Task LoginUser(string email, string password);
    }
}