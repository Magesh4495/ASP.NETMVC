using Entities.Entity;
using Entities.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Helper;
using WebMatrix.WebData;
using WebRegister.Filters.MVC;
using System.Net.Mail;

namespace WebRegister.Controllers
{
    //[AuthorizeUserAttribute]
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        public LoginController()
        {
        }
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }
        // GET: Login
        [HttpPost]
        public ActionResult CreateLogin(UserProfile profile)
        {
            bool IsLoggedIn = false;
            //isCreated = loginService.CreateLogin(profile);
            string GeneratedPassword = System.Web.Security.Membership.GeneratePassword(8, 3);
            try
            {
                WebSecurity.CreateUserAndAccount(profile.UserEmailAddress, GeneratedPassword);
                Emailer.Send(profile.UserEmailAddress, string.Empty, string.Empty, "Greetings from Get Set Technology", BODY, false, false, null, null, MailPriority.High);
            }
            catch(Exception ex)
            {

            }
            IsLoggedIn = WebSecurity.Login(profile.UserEmailAddress, GeneratedPassword);
            //if (isCreated)
            //{
            //    profile = loginService.fetchLogin(profile);
            //    String BODY =
            //   "<h1>Greetings " + profile.UserFirstName + "</h1>" +
            //   "<p>User Name   : " + profile.UserEmailAddress + "</p>" +
            //   "<p>Password   : " + profile.LoginPassword + "</p>" +
            //   "<p>This email was sent through the " +
            //   "using the .NET System.Net.Mail library.</p>";
            //    //Emailer.SendEmail(profile.UserEmailAddress, "Welcome CMS", BODY);
            //    Emailer.Send(profile.UserEmailAddress, string.Empty, string.Empty, "Greetings from Get Set Technology", BODY, false, false,null, null, MailPriority.High);
            //}
            if (IsLoggedIn)
                return RedirectToAction("DashBoard", "Home", null);
            else
                return RedirectToAction("LogIn", "Home", null);
        }
        [HttpPost]
        public ActionResult LoginVerify(UserProfile profile)
        {
            //WebSecurity.Login(profile.UserEmailAddress, profile.LoginPassword, true);
            if(IsValidLogin(profile))
            {
                return View();
            }
            else
            {
                return View();
            }
        }
        public bool IsValidLogin(UserProfile profile)
        {
            bool isValid = false;
            if(profile!=null)
            {
                UserProfile fetchedProfile = new UserProfile();
                fetchedProfile = loginService.fetchLogin(profile);
                WebSecurity.ConfirmAccount("");
                if(fetchedProfile.UserEmailAddress == profile.UserEmailAddress && fetchedProfile.LoginPassword == profile.LoginPassword)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public void SendMessage(UserProfile profile)
        {
            using (var web = new System.Net.WebClient())
            {
                try
                {
                    string userName = "testingforemail12@gmail.com";
                    string userPassword = "Q1w2e3r4t5y6!";
                    string msgRecepient = profile.UserPhoneNumber.ToString();
                    string msgText = "Welcome " + profile.UserFirstName + ",\nThank you for Creating an account with Magesh Application, \n Your User name is :" + profile.UserEmailAddress + " \n Password: " + profile.LoginPassword + "";
                    string url = "http://smsc.vianett.no/v3/send.ashx?" +
                        "src=" + msgRecepient +
                        "&dst=" + msgRecepient +
                        "&msg=" + System.Web.HttpUtility.UrlEncode(msgText, System.Text.Encoding.GetEncoding("ISO-8859-1")) +
                        "&username=" + System.Web.HttpUtility.UrlEncode(userName) +
                        "&password=" + userPassword;
                    string result = web.DownloadString(url);
                    //if (result.Contains("OK"))
                    //{
                    //    ("Sms sent successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Some issue delivering", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                catch (Exception ex)
                {
                    //Catch and show the exception if needed. Donot supress. :)  

                }
            }

        }
        
    }
}