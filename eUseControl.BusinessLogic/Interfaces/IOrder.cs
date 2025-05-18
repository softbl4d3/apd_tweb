using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        AdminResp CreateOrder(OrderDTO order);
        List<OrderItemDTO> GetAllOrderItems();
        //AdminResp DeleteOrder(int Id);
        List<OrderDTO> GetAllOrders();
        AdminResp ChangeOrderItemStatus(int id, DStatus status);
        //OrderDTO GetOrderById(int orderId);
        //List<OrderDTO> GetOrdersByTableId(int tableId);
    }
}
