﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.ViewModels
{
    public class AuthorVM
    {
        [Required]
        public string Name { get; set; }
    }
}
