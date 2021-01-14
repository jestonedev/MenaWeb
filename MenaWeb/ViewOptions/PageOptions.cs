﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewOptions
{
    public class PageOptions
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int SizePage { get; set; }

        public int TotalRows { get; set; }
        public int Rows { get; set; }

        public PageOptions()
        {
            CurrentPage = 1;
            SizePage = 25;
        }
    }
}
