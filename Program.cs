using practise_app_first_code.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practise_app_first_code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopDbContext())
            {
                // Создаем сервис магазина
                var service = new ServiceShop(context);

                //dz
                var clientsWithExampleDomain = service.GetClientsByEmailDomain("example.com");
                var cheapProducts = service.GetProductsCheaperThanAverage();
                var longestNameClient = service.GetClientWithLongestName();
                var ordersByShortestNameClient = service.GetOrdersByClientWithShortestName();
                var lowestPriceProduct = service.GetProductWithLowestPrice();
                var clientsWithManyOrders = service.GetClientsWithAboveAverageOrders();
                var clientWithExpensiveProductOrder = service.GetClientWithMostExpensiveProductOrder();
                var averageOrderPrice = service.GetAverageOrderPrice();
                var latestOrderClient = service.GetClientWithLatestOrder();
                var expensiveProductOrders = service.GetOrdersWithProductsPricedOver(800);
                Console.ReadKey();
            }
        }
        
        }
    }