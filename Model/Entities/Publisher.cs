﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
         
        public List<Book> Books { get; set; }
    }
}
