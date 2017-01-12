namespace JustOrderIt.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using Common.GlobalConstants;
    using Data.DbAccessConfig.Contexts;
    using Data.DbAccessConfig.IdentityStores;
    using Data.Models;
    using Data.Models.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Services.Web;

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser, string>
    {
        public ApplicationUserManager(
            IUserStore<ApplicationUser, string> store,
            IEmailService emailService,
            ISmsService smsService)
            : base(store)
        {
            this.EmailService = emailService;
            this.SmsService = smsService;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<JustOrderItDbContext>()), new EmailService(), new SmsService());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = ValidationConstants.ApplicationUserPasswordMinLength,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider(
                "Phone Code",
                new PhoneNumberTokenProvider<ApplicationUser> { MessageFormat = "Your security code is {0}" });
            manager.RegisterTwoFactorProvider(
                "Email Code", new EmailTokenProvider<ApplicationUser> { Subject = "Security Code", BodyFormat = "Your security code is {0}" });

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
