using System;
using System.Collections.Generic;
using SBEISK.SGM.Presentation.API.ViewModels.Material;

namespace SBEISK.SGM.Presentation.API.ViewModels.Receivement
{
    public class ReceivementInvoiceWithoutOrderViewModel 
    {
        public string Provider { get; set; }
        //public string Reason { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Complement { get; set; }
        public int ReceiverUser { get; set; }
        public DateTime ReceivementDate { get; set; }
        public List<WithoutOrder> Materials { get; set; } = new List<WithoutOrder>();
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string[] Emails { get; set; }
    }
}