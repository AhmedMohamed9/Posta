//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Posta.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int id { get; set; }
        public int Cus_ID { get; set; }
        public System.DateTime date { get; set; }
        public decimal price { get; set; }
        public decimal Deliver_Price { get; set; }
        public string Note { get; set; }
        public string place { get; set; }
        public string recived_Name { get; set; }
        public string recived_phone1 { get; set; }
        public string recived_phone2 { get; set; }
        public int City_id { get; set; }
    
        public virtual Citiezen Citiezen { get; set; }
        public virtual custimer custimer { get; set; }
    }
}
