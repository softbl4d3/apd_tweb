namespace eUseControl.Web.Models
{
    public class ChefOrderItemViewModel
    {
        public int Id { get; set; } // ID OrderItem
        public string DishName { get; set; } // Название блюда
        public int Amount { get; set; } // Количество
        public string Note { get; set; } // Примечание
        public string Status { get; set; } // Статус (InProgress, Ready, etc.)
    }
}
