using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using StillWorkingThatList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Linq;

namespace StillWorkingThatList.Controllers
{
    public class IdentityController : Controller
    {

        public UserManager<IdentityUser> userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();


        // GET: Identity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel newRegistration)
        {
            if (newRegistration != null)
            {

                var authManager = HttpContext.GetOwinContext().Authentication;
                var addIdent = userManager.Create(new IdentityUser(newRegistration.Email), newRegistration.Password);

                if (addIdent.Succeeded)
                {
                    PartyDBEntities ORM = new PartyDBEntities();

                    var newCharacter = new GoTCharacter();
                    var characterData = new APIController().GetCharacter(newRegistration.CharacterName);
                    newCharacter.Name = characterData.name;
                    newCharacter.House = characterData.allegiances[0];
                    newCharacter.Allegiance = characterData.allegiances[0];
                    newCharacter.Book = characterData.books[0].ToString();

                    ORM.GoTCharacters.Add(newCharacter);

                    var newGuest = new Guest();

                    newGuest.FirstName = newRegistration.FirstName;
                    newGuest.LastName = newRegistration.LastName;
                    newGuest.Email = newRegistration.Email;
                    newGuest.Attending = newRegistration.Attending;
                    newGuest.Date = newRegistration.Date;
                    newGuest.GuestName = newRegistration.GuestName;
                    newGuest.GoTCharacter = newCharacter;

                    ORM.Guests.Add(newGuest);
                    ORM.SaveChanges();

                    

                    return RedirectToAction("ViewAllGuests", "Home");
                }

            }

            return RedirectToAction("Registration", newRegistration);


        }
        

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {

            var authManager = HttpContext.GetOwinContext().Authentication;

            var user = userManager.Find(login.Email, login.Password);

            if (user != null)
            {
                var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false }, ident);

                return RedirectToAction("Index", "Home");

            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(login);

        }

    }
}