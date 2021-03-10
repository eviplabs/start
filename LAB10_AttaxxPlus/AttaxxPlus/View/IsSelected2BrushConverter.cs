using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AttaxxPlus.View
{
    public class IsSelected2BrushConverter : IValueConverter
    {
        // EVIP: reusing brushes, named constants
        readonly private static SolidColorBrush snow = new SolidColorBrush(Colors.Snow);    //border color if selected
        readonly private static SolidColorBrush gray = new SolidColorBrush(Colors.Gray);        //border color if not selected

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? snow : gray;   //border color based on selection
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
