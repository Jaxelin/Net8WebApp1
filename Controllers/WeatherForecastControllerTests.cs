using DBEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1.Controllers.Tests
{
    [TestClass()]
    public class WeatherForecastControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;

            using var context = new AppDbContext(options);

            // Act: 添加数据

            var order1 = new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Laptop", Price = 1200m },
                    new OrderItem { ProductName = "Mouse", Price = 25m }
                }
            };

            var order2 = new Order
            {
                Id = 2,
                OrderDate = DateTime.UtcNow.AddDays(-1),
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Keyboard", Price = 80m }
                }
            };

            context.Orders.AddRange(order1, order2);
            context.SaveChanges();

            // 查询所有订单及其明细
            var orders = context.Orders
                .Include(o => o.OrderItems) // 必须 Include 才能加载子项
                .ToList();


            var orders1 = context.Orders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                Items = o.OrderItems.Select(i=>i.ProductName)

            }).ToList();

            var orders2 = context.Orders.ToList();

            // 验证数量
            //Assert.Equal(2, orders.Count);

            //var ord001 = orders.First(o => o.OrderNo == "ORD001");
            //Assert.Equal(2, ord001.OrderItems.Count);
            //Assert.Contains(ord001.OrderItems, item => item.ProductName == "Laptop");
            //Assert.Equal(1200m, ord001.OrderItems.First(i => i.ProductName == "Laptop").Price);

            //var ord002 = orders.First(o => o.OrderNo == "ORD002");
            //Assert.Single(ord002.OrderItems);
            //Assert.Equal("Keyboard", ord002.OrderItems[0].ProductName);
            Assert.Fail();
        }
    }
}