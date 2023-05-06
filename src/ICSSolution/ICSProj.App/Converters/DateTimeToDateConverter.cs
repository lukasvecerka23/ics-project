using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace ICSProj.App.Converters;

public class DateTimeToDateConverter : BaseConverterOneWay<DateTime, DateTime>
{
    public override DateTime ConvertFrom(DateTime value, CultureInfo? culture)
        => value.Date;
    public override DateTime DefaultConvertReturnValue { get; set; } = DateTime.MinValue;
}
