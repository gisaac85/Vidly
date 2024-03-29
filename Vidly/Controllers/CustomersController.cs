﻿using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customers = customer.Customers,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }



            if (customer.Customers.Id == 0)
                _context.Customers.Add(customer.Customers);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Customers.Id);
                customerInDb.Name = customer.Customers.Name;
                customerInDb.BirthDate = customer.Customers.BirthDate;
                customerInDb.MembershipTypeId = customer.Customers.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.Customers.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
            //catch (DbEntityValidationException ex)
            //{
            //    string errorMessage = "";
            //    foreach (var errors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in errors.ValidationErrors)
            //        {

            //            errorMessage = validationError.ErrorMessage;
            //        }

            //        return Content(errorMessage);
            //    }
            //}
        }



        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customers = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}