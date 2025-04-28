using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ITable
    {
        AdminResp AddTable(TableDTO ingrid);
        AdminResp EditTable(TableDTO ingrid);
        AdminResp DeleteTable(int Id);

        List<TableDTO> GetAllTables();


        TableDTO GetTableById(int TableId);
    }
}
