using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Helper;
using OilStationMVC.Models;
using OilStationMVC.ViewModels;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get

            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var User = await UserManager.FindByEmailAsync(model.Email);
            if (User != null && !User.EmailConfirmed && await UserManager.CheckPasswordAsync(User, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Email Not Confirmed yet");
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            if (User != null && !User.PhoneNumberConfirmed && await UserManager.CheckPasswordAsync(User, model.Password))
            {
                var Phonecode = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Id, User.PhoneNumber);
                //Phonecode = "230289";
                if (UserManager.SmsService != null)
                {
                    var message = new IdentityMessage
                    {
                        Destination = User.PhoneNumber,
                        Body = "Your security code is: " + Phonecode
                    };
                    await UserManager.SmsService.SendAsync(message);
                }
                return RedirectToAction("VerifyPhoneNumber", "Manage", new { PhoneNumber = User.PhoneNumber, ReturnUrl = "/Account/Login" });
                //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                //ModelState.AddModelError(string.Empty, "Phone Number Not Confirmed yet");
                //return View(model);
            }
            if (!User.Active)
            {
                ModelState.AddModelError(string.Empty, "Sorry, this user not active . please contact with admin");
                return View(model);
            }
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe = false)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(OilStationMVC.Models.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNo };
                var result = await UserManager.CreateAsync(user, model.Password);
                await UserManager.SetTwoFactorEnabledAsync(user.Id, true);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var ConfirmationLinke = Url.Action("ConfirmEmail", "Account", new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    ViewBag.ErrorTitle = " Registeration Successfuly";
                    ViewBag.ErrorMessage = " Before You Can Login You Must Confirm Your Email  By Clicking On Confirmation Email We Sent To You";
                    EmailClass.SendEmailMessage("Registeration Successfuly", ConfirmationLinke, user.Email);
                    return View("RegistrationSucess");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                EmailClass.SendEmailMessage("Reset Password", callbackUrl, user.Email);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        public async Task<ActionResult> SendCode(string returnUrl = null)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            //return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });

        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            //return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });

        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });


                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    var result = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {

                        //await SignInManager.RefreshSignInAsync(user);
                        return View("ChangePasswordConfirmation");
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err);
                        return View();
                    }
                }
                return RedirectToAction("Login");
            }
            return View();
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }


        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }



        public ActionResult Roles()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles);
        }

        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    model.IsSaved = true;
                    return RedirectToAction("Roles");
                }
                else
                {
                    model.IsSaved = false;
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditRole(string Id)
        {
            ViewBag.RoleId = Id;
            var role = await RoleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            RoleViewModel roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(string Id, RoleViewModel roleViewModel)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                role.Name = roleViewModel.RoleName;
                IdentityResult result = await RoleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach (string err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err);
                    }
                    return View(roleViewModel);
                }
            }
            else
            {
                return View(roleViewModel);
            }

        }

        [HttpGet]
        public ActionResult UsersList()
        {
            var lst = UserManager.Users.ToList();
            return View(lst);
        }

        [HttpGet]
        public async Task<ActionResult> EditUsersRole(string Id)
        {
            ViewBag.RoleId = Id;
            var role = await RoleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            ViewBag.RoleName = role.Name;
            var model = new List<UserRoleViewModel>();
            var Users = UserManager.Users;

            if (Users.Count() > 0)
            {
                foreach (var user in Users)
                {
                    UserRoleViewModel userRoleViewModel = new UserRoleViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName

                    };
                    userRoleViewModel.IsSelected = await UserManager.IsInRoleAsync(user.Id, role.Name);

                    model.Add(userRoleViewModel);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditUsersRole(List<Models.UserRoleViewModel> model, string Id)
        {

            var role = await RoleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            if (model.Count > 0)
            {
                foreach (UserRoleViewModel userRoleViewModel in model)
                {
                    ApplicationUser user = await UserManager.FindByIdAsync(userRoleViewModel.UserId);
                    IdentityResult result = null;
                    if (!await UserManager.IsInRoleAsync(user.Id, role.Name) && userRoleViewModel.IsSelected)
                    {
                        result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    else if (await UserManager.IsInRoleAsync(user.Id, role.Name) && !userRoleViewModel.IsSelected)
                    {
                        result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                    }
                }
            }
            return RedirectToAction("Roles");
        }


        [HttpGet]
        public async Task<ActionResult> ManageUserClaims(string Id)
        {
            var User = await UserManager.FindByIdAsync(Id);
            if (User == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            var UserClaims = await UserManager.GetClaimsAsync(User.Id);

            var model = new UserClaimsViewModel
            {
                UserId = Id
            };

            foreach (Claim item in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = item.Type
                };
                if (UserClaims.Any(c => c.Type == item.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ManageUserClaims(string Id, UserClaimsViewModel userClaimsViewModel)
        {
            var User = await UserManager.FindByIdAsync(Id);
            if (User == null)
            {
                ViewBag.ErrorMessage = $" the role with id {Id} not found";
                return View("NotFound");
            }
            var UserClaims = await UserManager.GetClaimsAsync(User.Id);
            IdentityResult result = null;
            if (UserClaims.Count > 0)
            {
                foreach (var item in UserClaims)
                {
                    result = await UserManager.RemoveClaimAsync(User.Id, item);
                }
            }
            if (result != null)
            {
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can Not Delete User Claims");
                    return View(userClaimsViewModel);
                }
            }
            if (userClaimsViewModel.Claims.Count > 0)
            {
                foreach (var newclaim in userClaimsViewModel.Claims)
                {
                    if (newclaim.IsSelected)
                    {
                        Claim claim = new Claim(newclaim.ClaimType, newclaim.ClaimType);
                        result = await UserManager.AddClaimAsync(User.Id, claim);
                    }

                }
            }


            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can Not Add User Claims");
                return View(userClaimsViewModel);
            }

            return View(userClaimsViewModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccessDeinied()
        {
            return View("AccessDeinied");
        }

        public async Task<ActionResult> ChangeState(string Id, bool NewState)
        {
            if (Id != null)
            {
                var User = await UserManager.FindByIdAsync(Id);
                if (User == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                User.Active = NewState;
                await UserManager.UpdateAsync(User);
                return RedirectToAction("UsersList");
            }
            else
            {
                return View(nameof(UsersList));
            }
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}