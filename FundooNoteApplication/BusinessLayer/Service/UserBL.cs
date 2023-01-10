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
        IUserRL iUserRL;
        public UserBL(IUserRL iUserRL)
        {
            this.iUserRL = iUserRL;
        }

        public string Login(UserLogin userLogin)
        {
            try
            {
                return iUserRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity Registration(UserRegistration userRegistation)
        {
            try
            {
                return iUserRL.Registration(userRegistation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
