using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AttaxxPlus.View
{
    // EVIP: IValueConverter
    public class OwnerIndex2BrushConverter : IValueConverter
    {
        // Note: reusing brushes for all calls,
        //  using array instead of switch in Convert.
        readonly private static SolidColorBrush red = new SolidColorBrush(Colors.Coral);
        readonly private static SolidColorBrush blue = new SolidColorBrush(Colors.CornflowerBlue);
        readonly private static SolidColorBrush green = new SolidColorBrush(Colors.LightSeaGreen);
        readonly private static SolidColorBrush gray = new SolidColorBrush(Colors.LightGray);
        readonly private static SolidColorBrush[] burshes = new [] { gray, red, blue, green};

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // EVIP: avoiding switch with an array: field owner (int) -> Brush
            return burshes[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
