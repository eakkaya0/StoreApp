using System;
using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IOrderRepository
{
    IQueryable<Order> Orders {get;}

    public void SaveOrder(Order order);

}
