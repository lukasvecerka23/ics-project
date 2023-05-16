using CommunityToolkit.Maui.Converters;
using ICSProj.BL.Enums;
using System.Globalization;

namespace ICSProj.App.Converters;

public class TimePeriodToStringConverter: BaseConverterOneWay<TimePeriod, string>
{
    public override string ConvertFrom(TimePeriod value, CultureInfo? culture)
        => value switch
        {
            TimePeriod.None => "Žádné",
            TimePeriod.LastWeek => "Poslední týden",
            TimePeriod.LastMonth => "Poslední měsíc",
            TimePeriod.PreviousMonth => "Předchozí měsíc",
            TimePeriod.LastYear => "Poslední rok",
            _ => string.Empty
        };

    public override string DefaultConvertReturnValue { get; set; } = string.Empty;
}
