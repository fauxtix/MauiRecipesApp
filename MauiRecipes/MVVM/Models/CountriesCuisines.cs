using Newtonsoft.Json;

namespace MauiRecipes.MVVM.Models
{
    public class CountriesCuisines
    {
        public class Result
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("image")]
            public string Image { get; set; }
            [JsonProperty("imageType")]
            public string ImageType { get; set; }
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
