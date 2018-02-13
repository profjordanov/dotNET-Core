using System.Collections.Generic;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> GetProductsByCategory(string category);

    IEnumerable<Order> GetAllOrders(bool includeItems);
    IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
    Order GetOrderById(string username, int id);

    bool SaveAll();
    void AddEntity(object model);
  }
}