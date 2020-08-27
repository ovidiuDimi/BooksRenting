using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksRenting.Models
{
    public class Renting
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
