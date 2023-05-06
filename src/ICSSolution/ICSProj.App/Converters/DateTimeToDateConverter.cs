using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace ICSProj.App.Converters;

public class DateTimeToDateConverter : BaseConverterOneWay<DateTime, DateOnly>
{
    public override DateOnly ConvertFrom(DateTime value, CultureInfo? culture)
        => DateOnly.FromDateTime(value);
    public override DateOnly DefaultConvertReturnValue { get; set; } = DateOnly.MinValue;
}
