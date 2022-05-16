using System;
using System.ComponentModel.DataAnnotations;

namespace FundStarDemoAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string CustumerName { get; set; }
        public string CustumerDocument { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public string ProductsList { get; set; }


    }
}