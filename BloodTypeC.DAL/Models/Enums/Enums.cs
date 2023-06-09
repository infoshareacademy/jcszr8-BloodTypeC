using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Models.Enums
{
    public class Enums
    {
        public enum UserActions { Unknown, RegisterAccount, ConfirmEmail, ForgotPassword, ResendActivationLink,
            LogIn, LogOut, ViewBeer, AddBeer, EditBeer, RemoveBeer, AddBeerToFavorites, RemoveBeerFromFavorites}
    }
}
