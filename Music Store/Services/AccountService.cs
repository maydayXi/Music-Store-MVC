using System.Collections.Generic;
using System.Security.Claims;
using Music_Store.Models;
using Music_Store.Repository;
using Music_Store.ViewModels;
using Music_Store.Security;
using System;
using WebGrease.Extensions;

namespace Music_Store.Services
{
    public class AccountService
    {
        private readonly EFRepository<ShopUser> _shopUserRepository;
        private readonly Fortify fortify;

        public AccountService()
        {
            _shopUserRepository = new EFRepository<ShopUser>(new MusicShopEntities());
            fortify = new Fortify();
        }

        /// <summary>
        /// Resister user
        /// </summary>
        /// <param name="vmLogon"> register info </param>
        public void RegisterUser(VmLogon vmLogon)
        {
            _shopUserRepository.Create(new ShopUser()
            {
                UserName = vmLogon.Username,
                UserPassword = fortify.Encryt(vmLogon.Password),
                UserRole = vmLogon.IsAdmin ? 0 : 4
            });
            _shopUserRepository.SaveChanges();
        }

        /// <summary>
        /// Get user claims
        /// </summary>
        /// <param name="vmLogon"> login information </param>
        /// <returns> User claims </returns>
        public ClaimsPrincipal GetUserClaims(VmLogon vmLogon)
        {
            ShopUser user = _shopUserRepository.Read(u => u.UserName ==  vmLogon.Username);
            string p = new Fortify().Decrypt(user.UserPassword);
            user = p == vmLogon.Password ? user : null;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user == null ? string.Empty : vmLogon.Username),
                new Claim(
                    ClaimTypes.Role, user == null 
                        ? string.Empty 
                        : Enum.GetName(typeof(UserRole), user.UserRole)
                )
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "ApplicationCookie"));
        }
    }
}