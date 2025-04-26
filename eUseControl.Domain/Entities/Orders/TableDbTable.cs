using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
namespace eUseControl.Web.Models
{
    [Table("Tables")]

    public class TableDbTable : BaseDbItem
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TStatus Status { get; set; } // "Free", "Occupied", "Reserved"
        public TZone Zone { get; set; }
        public DateTime? ReservationTime { get; set; }
    }


}