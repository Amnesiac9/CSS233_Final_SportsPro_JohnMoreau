namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Registration
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public Registration()
        {

        }

        public Registration(Customer customer,Product product)
        {
            CustomerId = customer.Id;
            Customer = customer;
            ProductId = product.Id;
            Product = product;
        }
    }
}
