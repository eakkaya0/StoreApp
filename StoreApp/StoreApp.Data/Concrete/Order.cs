namespace StoreApp.Data.Concrete
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }=null!;
        public string CustomerPhone { get; set; }=null!;

         public string CustomerCity { get; set; }=null!;

          public string CustomerAdressLine { get; set; }=null!;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
       
    }

    public class OrderItem
    {
         public int Id { get; set; }
          public int OrderId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }=null!;

        public Order Order  { get; set; }=null!;
        
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

