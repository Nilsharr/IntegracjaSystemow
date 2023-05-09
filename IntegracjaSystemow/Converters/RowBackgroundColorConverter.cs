using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using IntegracjaSystemow.Models;

namespace IntegracjaSystemow.Converters;

public class RowBackgroundColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return new SolidColorBrush(Colors.Gray);
        }

        var status = (RowStatus) value;
        return status switch
        {
            RowStatus.Duplicated => new SolidColorBrush(Colors.Red),
            RowStatus.Edited => new SolidColorBrush(Colors.White),
            RowStatus.NotEdited => new SolidColorBrush(Colors.Gray),
            _ => throw new ArgumentOutOfRangeException(nameof(status), "Invalid row status")
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}