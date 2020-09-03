using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }       
}
