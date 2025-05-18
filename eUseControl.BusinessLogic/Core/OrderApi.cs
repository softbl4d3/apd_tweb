using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Core
{
    public class OrderApi
    {
        public List<OrderDTO> GetAllOrdersAction()
        {
            try
            {
                using (var context = new OrderContext())
                {
                    var orders = context.Orders
                        .Include("TableId")
                        .Include("WaiterId")
                        .Include("OrderItems.DishId")
                        .ToList();
                    return orders.Select(o => new OrderDTO
                    {
                        TableNumber = o.TableId.TableNumber,
                        WaiterName = o.WaiterId.UserName,
                        OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                        {
                            DishName = oi.DishId.Name,
                            Amount = oi.Amount,
                            Note = oi.Note,
                            Status = oi.Status,
                            TableNumber = oi.OrderId.TableId.TableNumber,
                        }).ToList(),
                        Note = o.Note,
                        TotalAmount = o.TotalAmount,
                        Status = o.Status
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }
            return new List<OrderDTO>();
        }

        internal List<OrderItemDTO> GetAllOrderItemsAction()
        {
            try
            {
                List<OrderItemDbTable> orderItems;
                using (var context = new OrderContext())
                {
                    orderItems = context.OrderItems
                        .Include("OrderId.TableId")
                        .Include("OrderId.WaiterId")
                        .Include("DishId")
                        .ToList();
                }
                var orderitemsDto = orderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    DishName = oi.DishId.Name,
                    Amount = oi.Amount,
                    Note = oi.Note,
                    Status = oi.Status,
                    TableNumber = oi.OrderId.TableId.TableNumber,
                    WaiterName = oi.OrderId.WaiterId.UserName,

                }).ToList();

                return orderitemsDto;
            }
            catch (Exception)
            {
                return new List<OrderItemDTO>();   
            }
        }

        internal AdminResp CreateOrderAction(OrderDTO order)
        {
            try
            {
                var TableDb = new TableDbTable();
                var WaiterDb = new UserDbTable();
                List<DishDbTable> dishesDb;

                using (var context = new UserContext())
                {
                    WaiterDb = context.Users.FirstOrDefault(u => u.UserName == order.WaiterName);
                }
                if (WaiterDb == null)
                {
                    return new AdminResp
                    {
                        Status = false,
                        Message = "Waiter not found"
                    };
                }
                using (var context = new OrderContext())
                {
                    var dishNames = order.OrderItems.Select(i => i.DishName).ToList();

                    dishesDb = context.Dishes
                        .Where(d => dishNames.Contains(d.Name))
                        .ToList();
                }


                using (var context = new OrderContext())
                {
                    TableDb = context.Tables.FirstOrDefault(t => t.TableNumber == order.TableNumber);

                    var orderItems = order.OrderItems
                        .Select(item =>
                        {
                            var dish = dishesDb.FirstOrDefault(d => d.Name == item.DishName);
                            return new OrderItemDbTable
                            {
                                DishId = dish,
                                Amount = item.Amount,
                                Price = (dish?.Price ?? 0) * item.Amount,
                                Note = item.Note,
                                Status = Domain.Enums.DStatus.InProgress
                            };
                        })
                        .ToList();

                    var totalAmount = orderItems.Sum(item => item.Price);

                    var orderDbTable = new OrderDbTable
                    {
                        TableId = TableDb,
                        WaiterId = WaiterDb,
                        CompletedTime = DateTime.MaxValue,
                        Status = Domain.Enums.DStatus.InProgress,
                        TotalAmount = totalAmount,
                        Note = order.Note,
                        OrderItems = orderItems
                    };

                    foreach (var item in orderItems)
                    {
                        item.OrderId = orderDbTable;
                    }
                    foreach (var dish in dishesDb)
                    {
                        context.Dishes.Attach(dish);
                    }
                    context.Orders.Add(orderDbTable);

                    TableDb.Status = TStatus.Occupied;
                    context.SaveChanges();
                }
                return new AdminResp
                {
                    Status = true,
                    Message = "Order created successfully"
                };
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Error creating order: {ex.Message}"
                };
            }
        }

        internal AdminResp ChangeOrderItemStatusAction(int Id, DStatus status)
        {
            OrderItemDbTable orderItemDb;
            int OrderId;
            try
            {
                using (var context = new OrderContext())
                {
                    orderItemDb = context.OrderItems
                        .Include("OrderId")
                        .FirstOrDefault(item => item.Id == Id);

                    if (orderItemDb == null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = $"Ошибка: заказ с Id = {Id} не найден."
                        };
                    }
                    OrderId = orderItemDb.OrderId.Id;
                    orderItemDb.Status = status;


                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return new AdminResp { Status = false ,Message = $"err : {ex.Message}"};

            }

            try 
            {
                List<OrderItemDbTable> orderItems;
                using (var context = new OrderContext())
                {
                    orderItems = context.OrderItems
                        .Include("OrderId")
                        .Where(item => item.OrderId.Id == OrderId)
                        .ToList();
                }
                
                foreach (var orderItem in orderItems) 
                {
                    if(orderItem.Status != status)
                    {
                        return new AdminResp { Status = true };
                    }

                    using (var context = new OrderContext())
                    {
                        var ord = context.Orders
                            .Include("TableId")
                            .FirstOrDefault(o => o.Id == OrderId);

                        ord.Status = status;

                        if (status == DStatus.Completed)
                        {
                            var table = context.Tables.FirstOrDefault(t => t.Id == ord.TableId.Id);
                            ord.CompletedTime = DateTime.Now;
                            table.Status = TStatus.Free;

                        }

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex) 
            {
                return new AdminResp { Status = false, Message = $"err : {ex.Message}" };
            }


            return new AdminResp { Status = true };
        }

    }
}
