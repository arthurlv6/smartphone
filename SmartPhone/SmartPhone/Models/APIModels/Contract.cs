﻿using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class Contract
    {
        public Contract()
        {
            ContractProduct = new HashSet<ContractProduct>();
        }
        #region new fields
        public string DeliveryCompany { get; set; }
        #endregion
        public long Id { get; set; }
        public string Sid { get; set; }
        public string RefType { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime? ContractSignDate { get; set; }
        public int? SalesId { get; set; }
        public decimal BookingFee { get; set; }
        public decimal OthersFee { get; set; }
        public int? DeliveryAfterPayment { get; set; }
        public string BankInfo { get; set; }
        public string Status { get; set; }
        public DateTime? DebitStartDate { get; set; }
        public decimal? DebitPerAmount { get; set; }
        public string DebitFrequency { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Note { get; set; }
        public int? DeliveryId { get; set; }
        public int? AgreeId { get; set; }
        public DateTime? DeliverDate { get; set; }
        public string DeliverComment { get; set; }
        public decimal AccountFee { get; set; }
        public decimal DeliveryFee { get; set; }
        public int SettingId { get; set; }
        public int WarehouseId { get; set; }
        public bool IsContract { get; set; }
        public string CompleteStatus { get; set; }
        public int? ChannelId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public string Channel { get; set; }
        public string SalesMan { get; set; }
        //
        public string Warehouse { get; set; } = "Main";
        public string QuoteNumber { get; set; } = "1122";
        public string TaxRate { get; set; } = "15%";
        public string TrackNumber { get; set; } = "";
        public ICollection<ContractProduct> ContractProduct { get; set; }
    }
}