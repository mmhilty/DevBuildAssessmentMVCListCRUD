using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StillWorkingThatList.Models;

namespace StillWorkingThatList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAllGuests()
        {
            PartyDBEntities ORM = new PartyDBEntities();

            ViewBag.GuestList = ORM.Guests.ToList();

            return View();
        }

        public ActionResult ViewAllDishes()
        {
            PartyDBEntities ORM = new PartyDBEntities();

            ViewBag.DishList = ORM.Dishes.ToList();
            return View();
        }

        public ActionResult EditDish(int dishID)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            Dish dishToEdit = ORM.Dishes.Find(dishID);
            return View(dishToEdit);
        }

        public ActionResult SaveDishEdits(Dish updatedDish)
        {
            PartyDBEntities ORM = new PartyDBEntities();

            Dish oldDish = ORM.Dishes.Find(updatedDish.DishID);

            oldDish.FirstName = updatedDish.FirstName;
            oldDish.LastName = updatedDish.LastName;
            oldDish.PhoneNumber = updatedDish.PhoneNumber;
            oldDish.DishName = updatedDish.DishName;
            oldDish.DishDescription = updatedDish.DishDescription;

            ORM.Entry(oldDish).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("ViewAllDishes");

        }

        public ActionResult DeleteDish(int dishID)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            ORM.Dishes.Remove(ORM.Dishes.Find(dishID));
            ORM.SaveChanges();

            return RedirectToAction("ViewAllDishes");
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult EditRegistration(int guestID)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            Guest guestToEdit = ORM.Guests.Find(guestID);
            return View(guestToEdit);
        }

        public ActionResult SaveRegistrationEdits(Guest updatedGuest)
        {
            PartyDBEntities ORM = new PartyDBEntities();

            Guest oldGuest = ORM.Guests.Find(updatedGuest.GuestID);

            oldGuest.FirstName = updatedGuest.FirstName;
            oldGuest.LastName = updatedGuest.LastName;
            //oldGuest.Email = updatedGuest.Email;
            oldGuest.Attending = updatedGuest.Attending;
            oldGuest.Date = updatedGuest.Date;
            oldGuest.GuestName = updatedGuest.GuestName;
            
            ORM.Entry(oldGuest).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("ViewAllGuests");
        }

        public ActionResult DeleteGuest(int guestID)
        {

            PartyDBEntities ORM = new PartyDBEntities();
            ORM.Guests.Remove(ORM.Guests.Find(guestID));
            ORM.SaveChanges();

            return RedirectToAction("ViewAllGuests");
        }

        public ActionResult AddRegistration(Guest newGuest)
        {
            PartyDBEntities ORM = new PartyDBEntities();

            if (newGuest != null)
            {
                ORM.Guests.Add(newGuest);
                ORM.SaveChanges();
            }

            return RedirectToAction("ViewAllGuests");
        }


        public ActionResult DishSignup()
        {
            return View();
        }

        public ActionResult AddDish(Dish newDish)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            
            if (newDish != null)
            {
                ORM.Dishes.Add(newDish);
                ORM.SaveChanges();
            }

            return RedirectToAction("ViewAllDishes");
        }




    }
}