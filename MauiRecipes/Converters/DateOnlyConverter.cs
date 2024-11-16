namespace MauiRecipes.Converters
{
    public class DateOnlyConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("yyyy-MM-dd");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException(); // Optional: Implement this if you need two-way binding.
        }

    }
}
