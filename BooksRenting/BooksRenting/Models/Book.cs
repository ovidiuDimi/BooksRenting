using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksRenting.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }


    }
}
