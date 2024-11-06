namespace MauiRecipes.Converters
{
    public class IngredientImageToFullUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string ingredientimage)
            {
                if (!string.IsNullOrEmpty(ingredientimage))
                    return $"https://img.spoonacular.com/ingredients_100x100/{ingredientimage}";
                else
                    return "Ingredients.png";
            }
            return "ingredients.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
