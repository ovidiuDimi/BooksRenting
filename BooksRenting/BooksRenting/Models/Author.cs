﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksRenting.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        internal void Add(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
