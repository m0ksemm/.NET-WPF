using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace EvernoteClone.ViewModel.Converters
{
    public class FontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Якщо прийшов FontFamily, повертаємо його як є
            if (value is FontFamily fontFamily)
                return fontFamily;

            // Якщо прийшов рядок – створюємо FontFamily
            if (value is string fontName)
                return new FontFamily(fontName);

            return new FontFamily("Segoe UI"); // дефолт
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Якщо користувач вибрав FontFamily – повертаємо його назву
            if (value is FontFamily fontFamily)
                return fontFamily.Source;

            return value?.ToString() ?? string.Empty;
        }
    }
}
