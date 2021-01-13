﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Person
    {
        public int IdPerson { get; set; }
        public uint? IdApartment { get; set; }
        public ushort IdPersonStatus { get; set; }
        public ushort IdContractor { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public DateTime? Birth { get; set; }
        public short IdDocument { get; set; }
        public string DocumentSeria { get; set; }
        public string DocumentNumber { get; set; }
        public int? IdDocumentIssued { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string BornPlace { get; set; }
        public int? IdTemplate { get; set; } // TODO: Warrant relation
        public string Portion { get; set; }
        public string Phone { get; set; }
        public byte Sex { get; set; }
        public string LastChangeUser { get; set; }
        public DateTime LastChangeDate { get; set; }
        public bool Deleted { get; set; }
        public Apartment Apartment { get; set; }
        public Contractor Contractor { get; set; }
        public PersonStatus PersonStatus { get; set; }
        public DocumentIssued DocumentIssued { get; set; }
        public Document Document { get; set; }

    }
}
