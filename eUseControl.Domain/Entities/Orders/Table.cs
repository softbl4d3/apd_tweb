using System;
using System.Collections.Generic;
using System.Linq;
using eUseControl.Domain.Entities;
namespace eUseControl.Web.Models
{
    public class Table : BaseDbItem
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } // "Free", "Occupied", "Reserved"
        public string Zone { get; set; }
        public DateTime? ReservationTime { get; set; }
    }


}