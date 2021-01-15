﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Additional
    {
        public int IdAddit { get; set; }
        public int? IdContract { get; set; }
        public ushort? IdPredost { get; set; }
        public string Osnovanie { get; set; } // WTF?
        public DateTime? DateDogPor { get; set; }
        public string DogPor { get; set; }
        public ushort? IdCopy { get; set; }
        public string Ogranichenie { get; set; }
        public string InfoUvedom { get; set; } // DateUvedom
        public string Primechanie { get; set; }
        public string PhoneCivil { get; set; }
        public string InfoSnyatUchet { get; set; }
        public DateTime? DateEvualation { get; set; }
        public string InfoEvualation { get; set; }
        public DateTime? DateEvualDone { get; set; }
        public DateTime? DateZaselenie { get; set; }

        public Contract Contract { get; set; }
        public Predost Predost { get; set; }
        public CopyKgc CopyKgc { get; set; }
    }
}