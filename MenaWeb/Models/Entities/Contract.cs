﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Contract
    {
        public int IdContract { get; set; }
        public short IdDelegate { get; set; }
        public short IdExecutor { get; set; }
        public int? IdApartmentSide1 { get; set; }
        public int? IdApartmentSide2 { get; set; }
        public int? IdApartmentSide12 { get; set; }
        public DateTime? PreContractDate { get; set; }
        public short IdPreContractIssued { get; set; }
        public int IdContractReason { get; set; }
        public DateTime? ContractRegistrationDate { get; set; }
        public DateTime? AgreementRegistrationDate { get; set; }
        public int? IdAgreementRepresent { get; set; }  // Empty
        public string PreContractNumber { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime LastChangeDate { get; set; }
        public string LastChangeUser { get; set; }
        public DateTime? FillingDate { get; set; }
        public bool Deleted { get; set; }
        public bool EvictionRequired { get; set; }
        public Delegate Delegate { get; set; }
        public Signer Signer { get; set; }
        public Apartment ApartmentSide1 { get; set; }
        public Apartment ApartmentSide2 { get; set; }
        public Apartment ApartmentSide12 { get; set; }
        public PreContractIssued PreContractIssued { get; set; }
        public ContractReason ContractReason { get; set; }
        public virtual List<ContractStatusHistory> ContractStatusHistory { get; set; }
        public virtual List<Additional> Additionals { get; set; }

        public Contract()
        {
            ApartmentSide1 = new Apartment();
            ApartmentSide12 = new Apartment();
            ApartmentSide2 = new Apartment();
            ContractStatusHistory = new List<ContractStatusHistory>();
            Additionals = new List<Additional> ();
        }

    }
}
