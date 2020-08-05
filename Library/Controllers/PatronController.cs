using Library.ViewModels.Patron;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        private IPatron _patron;

        public PatronController(IPatron patrons)
        {
            _patron = patrons;    
        }

        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();

            var ViewModel = allPatrons
                .Select(p => new PatronViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    LibraryCardId = p.LibraryCard.Id,
                    OverdueFees = p.LibraryCard.Fees, 
                    HomeLibraryBranch = p.HomeLibraryBranch.Name
                }).ToList();

            return View(ViewModel);
        }

        public IActionResult Detail(int id)
        {
            var patron = _patron.Get(id);

            if (patron == null)
            {
                return NotFound();
            }

            var model = new PatronViewModel
            {
                LastName = patron.LastName,
                FirstName = patron.FirstName,
                Address = patron.Address, 
                HomeLibraryBranch = patron.HomeLibraryBranch.Name, 
                MemberSince = patron.LibraryCard.Created, 
                OverdueFees = patron.LibraryCard.Fees,
                LibraryCardId = patron.LibraryCard.Id,
                Telephone = patron.TelephoneNumber, 
                AssetsCheckOut = _patron.GetCheckouts(id).ToList() ?? new List<LibraryData.Models.Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistory(id),
                Holds = _patron.GetHolds(id)
            };

            return View(model);
        }
    }
}
