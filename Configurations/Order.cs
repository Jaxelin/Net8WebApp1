namespace DBEntityModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // 导航属性：一个订单可以有多个订单项
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // 外键属性（可选但推荐显式定义）
        public int OrderId { get; set; }

        // 导航属性：指向主表
        public Order Order { get; set; } = null!;
    }
}
