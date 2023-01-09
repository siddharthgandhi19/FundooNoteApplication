using CommonLayer.ModelClass;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRL: IUserRL
    {
        FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public UserEntity Registration (UserRegistation userRegistation)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName= userRegistation.FirstName;
                userEntity.LastName= userRegistation.LastName;
                userEntity.Email= userRegistation.Email;
                userEntity.Password= userRegistation.Password;
                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result>0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
