using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.DTO
{
    public class TableDTO
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TZone Zone { get; set; }
    }
}
