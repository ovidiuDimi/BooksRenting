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
        internal object NewCategory;
        private int newCategoryId;

        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [NotMapped]
        public List<Author> AvailableAuthors { get; set; } = new List<Author>();

        [NotMapped]
        public List<Category> AvailableCategories { get; set; } = new List<Category>();

        [Required]
        [NotMapped]
        [Display(Name = "Select Author")]
        public int SelectedAuthorId { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Select Category")]
        public int SelectedCategoryId { get; set; }

        [NotMapped]
        public List<Author> NewAuthors { get; set; } = new List<Author>();

        [Required]
        [NotMapped]
        [Display(Name = "New Author")]
        public int NewAuthorId { get; set; }

        [NotMapped]
        public List<Category> NewCategories { get; set; } = new List<Category>();

        [Required]
        [NotMapped]
        [Display(Name = "New Category")]
        public int NewCategoryId { get; set; }
    }
}
