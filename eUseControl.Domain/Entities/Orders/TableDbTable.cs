using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
namespace eUseControl.Domain.Entities.Orders
{
    [Table("Tables")]

    public class TableDbTable : BaseDbItem
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TStatus Status { get; set; } 
        public TZone Zone { get; set; }
    }


}