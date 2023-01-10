using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Registration(UserRegistration userRegistation);
        public string Login(UserLogin userLogin);
    }
}
