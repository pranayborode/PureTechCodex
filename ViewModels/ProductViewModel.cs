namespace PureTechCodex.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public IFormFile? Image { get; set; }
    }
}
