﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.ViewModels
{
    public class BookVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string CoverUrl { get; set; }


        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
