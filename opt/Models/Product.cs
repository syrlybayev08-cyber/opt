namespace SpectrAgency.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
    }
}