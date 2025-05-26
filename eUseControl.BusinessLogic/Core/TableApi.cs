using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Core
{
    public class TableApi
    {
        public AdminResp AddTableAction(TableDTO tableDto)
        {
            TableDbTable tableDb = new TableDbTable
            {
                TableNumber = tableDto.TableNumber,
                Status = Domain.Enums.TStatus.Free,
                Capacity = tableDto.Capacity,
                Zone = tableDto.Zone


            };
            using (var context = new OrderContext())
            {
                bool tableExists = context.Tables
                    .Any(t => t.TableNumber == tableDb.TableNumber);

                if (tableExists)
                {
                    return new AdminResp
                    {
                        Status = false,
                        Message = "table exist"
                    };
                }

                context.Tables.Add(tableDb);
                context.SaveChanges();
            }

            return new AdminResp
            {
                Status = true,
                Message = "Стол успешно добавлен."
            };

        }
        public AdminResp EditTableAction(TableDTO tableDto)
        {
            TableDbTable tableDb = new TableDbTable
            {
                TableNumber = tableDto.TableNumber,
                Status = tableDto.Status,
                Capacity = tableDto.Capacity,
                Zone = tableDto.Zone


            };

            try {
                using (var context = new OrderContext())
                {
                    var existTable = context.Tables.First(t => t.TableNumber == tableDb.TableNumber);

                    if (existTable != null)
                    {
                        tableDb.Id = existTable.Id;
                        tableDb.UpdatedAt = DateTime.Now;
                        context.Tables.AddOrUpdate(tableDb);
                        context.SaveChanges();
                    }

                }

            }
            catch(Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"table exist{ex.Message}"
                };
            }
            return new AdminResp
            {
                Status = true,
            };

        }
        public AdminResp DeleteTableAction(int Id)
        {
            try
            {
                using (var context = new OrderContext())
                {
                    var existTable = context.Tables
                        .FirstOrDefault(t => t.TableNumber == Id);

                    var orders = context.Orders
                        .Where(o => o.TableId.TableNumber == Id)
                        .ToList();

                    var orderItems = context.OrderItems
                        .Include("OrderId.TableId")
                        .Where(oi => oi.OrderId.TableId.TableNumber == Id)
                        .ToList();
                    if (existTable != null)
                    {
                        context.Orders.RemoveRange(orders);
                        context.OrderItems.RemoveRange(orderItems);
                        context.Tables.Remove(existTable);
                        context.SaveChanges();

                        return new AdminResp
                        {
                            Status = true,
                        };
                    }
                    else
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "table no exist"
                        };
                    }

                }

            }
            catch(Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"err : {ex.Message}"
                };
            }

        }
        public List<TableDTO> GetAllTablesAction()
        {
            List<TableDbTable> tableDb;
            try
            {
                using (var context = new OrderContext())
                {
                    tableDb = context.Tables.ToList();
                }
            }
            catch(Exception)
            {
                return new List<TableDTO>();
            }

            List<TableDTO> tableDto = tableDb.Select(t => new TableDTO
            {
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                Status = t.Status,
                Zone = t.Zone,
            }).ToList();

            return tableDto;

        }
        public TableDTO GetTableByIdAction(int Id)
        {
            TableDbTable tableDb;
            try
            {
                using (var context = new OrderContext())
                {
                    tableDb = context.Tables.First(t => t.TableNumber == Id);
                }
            }
            catch(Exception)
            {
                return new TableDTO();
            }
            TableDTO tableDto = new TableDTO
            {
                TableNumber = tableDb.TableNumber,
                Capacity = tableDb.Capacity,
                Status = tableDb.Status,
                Zone = tableDb.Zone
            };
            return tableDto;
        }
    }
}
