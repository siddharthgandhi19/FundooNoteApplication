using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL: IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity Registration(UserRegistration userRegistation)
        {
            try
            {
                return userRL.Registration(userRegistation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
