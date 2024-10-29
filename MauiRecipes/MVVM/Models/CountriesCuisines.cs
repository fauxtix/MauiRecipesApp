namespace MauiRecipes.MVVM.Models
{
    public class CountriesCuisines
    {
        public class Result
        {
            public int id { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public string imageType { get; set; }
        }

        public class Root
        {
            public List<Result> results { get; set; }
            public int offset { get; set; }
            public int number { get; set; }
            public int totalResults { get; set; }
        }
    }
}
