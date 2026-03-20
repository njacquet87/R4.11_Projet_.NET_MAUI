namespace Jacquet_Valet_App.ViewModels
{
    public class BeerItem
    {
        public string ImagePath { get; set; }
    }

    public class BeerViewModel
    {
        public List<BeerItem> Beers { get; set; }

        public BeerViewModel()
        {
            Beers = new List<BeerItem>
            {
                // images provisoires
                new BeerItem { ImagePath = "dotnet_bot.png" },
                new BeerItem { ImagePath = "dotnet_bot.png" },
                new BeerItem { ImagePath = "dotnet_bot.png" }
            };
        }
    }
}