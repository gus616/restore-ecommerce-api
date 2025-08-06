
namespace Application.Products.DTOs
{
    public class FilterDto
    {
        public List<string> Brands { get; set; }

        public List<string> Types { get; set; }

        public decimal MinPrice { get; set; } = 0;

        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}
