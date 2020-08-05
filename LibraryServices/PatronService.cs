using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
    public class PatronService : IPatron
    {
        private LibraryDbContext _context;

        public PatronService(LibraryDbContext context)
        {
            _context = context;
        }

        public void Add(Patrons newPatron)
        {
            _context.Add(newPatron);
            _context.SaveChanges();
        }

        public Patrons Get(int id)
        {
            return GetAll()
                .FirstOrDefault(patron => patron.Id == id);
        }

        public IEnumerable<Patrons> GetAll()
        {
            return _context.Patrons
                .Include(pr => pr.LibraryCard)
                .Include(patron => patron.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id;

            return _context.CheckoutHistories
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id;
                
            return _context.Checkouts
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId);
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            var cardId = Get(patronId).LibraryCard.Id;

            return _context.Holds
                .Include(l => l.LibraryCard)
                .Include(l => l.LibraryAsset)
                .Where(l => l.LibraryCard.Id == cardId)
                .OrderByDescending(h => h.HoldPlaced);
        }
    }
}
