using practise_app_first_code.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace practise_app_first_code
{
    public class ServiceShop
    {
        private readonly ShopDbContext _context;

        public ServiceShop(ShopDbContext context)
        {
            _context = context;
        }

        // Создание нового продукта
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Получение всех продуктов
        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        // Создание нового заказа
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        // Получение всех заказов
        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        // Создание нового клиента
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        // Получение всех клиентов
        public IQueryable<Client> GetAllClients()
        {
            return _context.Clients;
        }

        // Получение всех продуктов для определенного заказа
        public IQueryable<Product> GetProductsForOrder(int orderId)
        {
            return _context.Orders
                           .Where(o => o.Id == orderId)
                           .SelectMany(o => o.Products);
        }

        // Получение всех заказов с информацией о продуктах и клиентах
        public IQueryable<Order> GetAllOrdersWithDetails()
        {
            return _context.Orders
                           .Include(o => o.Products)
                           .Include(o => o.Client);
        }
        public Client GetClientWithHighestOrderSum()
        {
            return _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Products)
                .GroupBy(o => o.Client)
                .Select(g => new
                {
                    Client = g.Key,
                    TotalOrderSum = g.Sum(o => o.Products.Sum(p => p.Price))
                })
                .OrderByDescending(g => g.TotalOrderSum)
                .FirstOrDefault()?.Client;
        }
        public Product GetProductWithHighestPrice()
        {
            return _context.Products
                .OrderByDescending(p => p.Price)
                .FirstOrDefault();
        }

        // dz
        public IQueryable<Client> GetClientsByEmailDomain(string domain)
        {
            return _context.Clients
                           .Where(c => c.Email.Contains(domain));
        }
        public IQueryable<Product> GetProductsCheaperThanAverage()
        {
            var averagePrice = _context.Products.Average(p => p.Price);
            return _context.Products
                           .Where(p => p.Price < averagePrice);
        }
        public Client GetClientWithLongestName()
        {
            return _context.Clients
                           .OrderByDescending(c => c.Name.Length)
                           .FirstOrDefault();
        }
        public IQueryable<Order> GetOrdersByClientWithShortestName()
        {
            var clientWithShortestName = _context.Clients
                                                 .OrderBy(c => c.Name.Length)
                                                 .FirstOrDefault();

            if (clientWithShortestName == null)
                return Enumerable.Empty<Order>().AsQueryable();

            return _context.Orders
                           .Where(o => o.ClientId == clientWithShortestName.Id);

        }
        public Product GetProductWithLowestPrice()
        {
            return _context.Products
                           .OrderBy(p => p.Price)
                           .FirstOrDefault();
        }
        public IQueryable<Client> GetClientsWithAboveAverageOrders()
        {
            var averageOrderCount = _context.Orders
                                            .GroupBy(o => o.ClientId)
                                            .Average(g => g.Count());

            return _context.Clients
                           .Where(c => _context.Orders
                                               .Count(o => o.ClientId == c.Id) > averageOrderCount);
        }
        public Client GetClientWithMostExpensiveProductOrder()
        {
            var orderWithMostExpensiveProduct = _context.Orders
                                                        .Include(o => o.Client)
                                                        .Include(o => o.Products)
                                                        .OrderByDescending(o => o.Products.Max(p => p.Price))
                                                        .FirstOrDefault();

            return orderWithMostExpensiveProduct?.Client;
        }
        public decimal GetAverageOrderPrice()
        {
            return _context.Orders
                           .Select(o => o.Products.Sum(p => p.Price))
                           .Average();
        }
        public Client GetClientWithLatestOrder()
        {
            var latestOrder = _context.Orders
                                      .Include(o => o.Client)
                                      .OrderByDescending(o => o.Id) // Assuming Id is auto-incremented and represents order time
                                      .FirstOrDefault();

            return latestOrder?.Client;
        }
        public IQueryable<Order> GetOrdersWithProductsPricedOver(decimal price)
        {
            return _context.Orders
                           .Include(o => o.Products)
                           .Where(o => o.Products.Any(p => p.Price > price));
        }
    }
}
