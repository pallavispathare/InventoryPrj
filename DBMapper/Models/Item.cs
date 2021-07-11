using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DBMapper.Models
{
    public class Item
    {
       
        public int ID { get; set; }
        public string ItemName {get;set;}
        public string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }
    }
}