using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OilStationMVC.Models;

namespace OilStationMVC
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
public class Response
{
    public string message_id { get; set; }
    public int message_count { get; set; }
    public double price { get; set; }
}

public class RootObject
{
    public Response Response { get; set; }
    public string ErrorMessage { get; set; }
    public int Status { get; set; }
}
public class SmsService : IIdentityMessageService
{
    public Task SendAsync(IdentityMessage message)
    {
        string sURL = "https://www.thetexting.com/rest/sms/json/message/send?api_key=b7lhxiit17fyij1&api_secret=4zra1b3hgcveax5&to=" + message.Destination + "&text=" + message.Body;
        try
        {
            using (WebClient client = new WebClient())
            {
                
                string s = client.DownloadString(sURL);
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(s);
                int n = responseObject.Status;
                if (n == 3)
                {

                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return Task.FromResult(0);
    }
}

// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
    {
    }

    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {
        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        // Configure validation logic for usernames
        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };

        // Configure validation logic for passwords
        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 1,
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
        manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
        {
            //MessageFormat = "Your security code is {0}",
            MessageFormat = "Your security code is 230289",


        });

        //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
        //{
        //    Subject = "Security Code",
        //    BodyFormat = "Your security code is {0}"
        //});
        //manager.EmailService = new EmailService();
        manager.SmsService = new SmsService();
        var dataProtectionProvider = options.DataProtectionProvider;
        if (dataProtectionProvider != null)
        {
            manager.UserTokenProvider =
                new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
        return manager;
    }
}

// Configure the application sign-in manager which is used in this application.
public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
{
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    {
        return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    }

    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
}

// Configure the application Role manager used in this application. RoleMananger is defined in ASP.NET Identity and is used by the application.
public class ApplicationRoleManager : RoleManager<IdentityRole>
{
    public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore) { }
    public static ApplicationRoleManager Create(
     IdentityFactoryOptions<ApplicationRoleManager> options,
     IOwinContext context)
    {
        var manager = new ApplicationRoleManager(
            new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        return manager;
    }
}



