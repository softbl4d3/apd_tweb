using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic
{
    public class TableBL : TableApi, ITable
    {
        public AdminResp AddTable(TableDTO ingrid)
        {
            return AddTableAction(ingrid);
        }
        public AdminResp EditTable(TableDTO ingrid)
        {
            return EditTableAction(ingrid);
        }
        public AdminResp DeleteTable(int Id)
        {
            return DeleteTableAction(Id);
        }
        public List<TableDTO> GetAllTables()
        {
            return GetAllTablesAction();
        }
        public TableDTO GetTableById(int TableId)
        {
            return GetTableByIdAction(TableId);
        }
    }
}
