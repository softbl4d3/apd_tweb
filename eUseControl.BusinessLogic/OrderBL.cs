using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic
{
    public class OrderBL : OrderApi, IOrder
    {
        public AdminResp CreateOrder(OrderDTO order) => CreateOrderAction(order);

        public List<OrderDTO> GetAllOrders() => GetAllOrdersAction();

        public List<OrderItemDTO> GetAllOrderItems() => GetAllOrderItemsAction();

        public AdminResp ChangeOrderItemStatus(int id, DStatus status) => ChangeOrderItemStatusAction(id, status);
    }
}
