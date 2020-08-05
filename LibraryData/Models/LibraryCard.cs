using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryData.Models
{
    public class LibraryCard
    {
        public int Id { get; set; }

        [Display(Name = "Overdue Fees")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fees { get; set; }

        [Display(Name = "Card Issued Date")] 
        public DateTime Created { get; set; }

        [Display(Name = "Materials on Loan")] 
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}