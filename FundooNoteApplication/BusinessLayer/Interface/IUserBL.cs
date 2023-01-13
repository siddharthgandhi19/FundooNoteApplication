﻿using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistration userRegistation);
        public string Login(UserLogin userLogin);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
