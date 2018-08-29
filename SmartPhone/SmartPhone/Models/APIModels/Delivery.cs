using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class Delivery
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public bool? Delivered { get; set; }
        public string SalesReferenceNo { get; set; }
        public string CourierName { get; set; }
        public string DeliveryNumber { get; set; }
        public string ReceiptNumber { get; set; }
        public string CourierTrackingNumber { get; set; }
        public string DeliveryStaffName { get; set; }
    }
}
