using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        AdminResp CreateOrder(OrderDTO order);
        List<OrderItemDTO> GetAllOrderItems();
        //AdminResp DeleteOrder(int Id);
        List<OrderDTO> GetAllOrders();
        //OrderDTO GetOrderById(int orderId);
        //List<OrderDTO> GetOrdersByTableId(int tableId);
    }
}
