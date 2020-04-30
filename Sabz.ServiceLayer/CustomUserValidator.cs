using Microsoft.AspNet.Identity;
using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer
{
    public class CustomUserValidator<TUser> : UserValidator<TUser, int>
     where TUser : ApplicationUser
    {

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="manager"></param>
        public CustomUserValidator(UserManager<TUser, int> manager) : base(manager)
        {
            this.Manager = manager;
        }

        private UserManager<TUser, int> Manager { get; set; }

        /// <summary>
        ///     Validates a user before saving
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors);
            //if (RequireUniqueEmail)
            //{
            //    await ValidateEmail(item, errors);
            //}
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }
            return IdentityResult.Success;
        }

        private async Task ValidateUserName(TUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture,"از نام کاربری با کاراکتر بیشتر استفاده کنید.", "Name"));//Resources.PropertyTooShort, 
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
            {
                // If any characters are not letters or digits, its an illegal user name
                errors.Add(String.Format(CultureInfo.CurrentCulture, "از کاراکتر معتبر استفاده کنید.",user.UserName));//Resources.InvalidUserName, 
            }
            else
            {
                var owner = await Manager.FindByNameAsync(user.UserName);
                if (owner != null && !EqualityComparer<int>.Default.Equals(owner.Id, user.Id))
                {
                    errors.Add(String.Format(CultureInfo.CurrentCulture,"این نام کاربری قبلا ثبت شده است.", user.UserName));//Resources.DuplicateName, 
                }
            }
        }

        // make sure email is not empty, valid, and unique
        private async Task ValidateEmail(TUser user, List<string> errors)
        {
            //if (!user.Email.IsNullOrWhiteSpace())
            //{
                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    errors.Add(String.Format(CultureInfo.CurrentCulture, "از ایمیل با کاراکتر بیشتر استفاده کنید.", "Email"));//Resources.PropertyTooShort,
                return;
                }
                try
                {
                    var m = new MailAddress(user.Email);
                }
                catch (FormatException)
                {
                    errors.Add(String.Format(CultureInfo.CurrentCulture,"ایمیل نامعتبر است.", user.Email));//Resources.InvalidEmail, 
                return;
                }
            //}
            var owner = await Manager.FindByEmailAsync(user.Email);
            if (owner != null && !EqualityComparer<int>.Default.Equals(owner.Id, user.Id))
            {
                errors.Add(String.Format(CultureInfo.CurrentCulture,"این ایمیل قبلا ثبت شده است",user.Email));//Resources.DuplicateEmail,
            }
        }
    }
}
