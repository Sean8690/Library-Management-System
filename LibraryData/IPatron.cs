using System;
using System.Collections.Generic;
using System.Text;
using LibraryData.Models;

namespace LibraryData
{
    public interface IPatron
    {
        Patrons Get(int id);
        IEnumerable<Patrons> GetAll();
        void Add(Patrons newPatron);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId);
        IEnumerable<Hold> GetHolds(int patronId);
        IEnumerable<Checkout> GetCheckouts(int patronId);
    }
}
