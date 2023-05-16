using CommunityToolkit.Maui.Converters;
using System.Globalization;
using Color = Microsoft.Maui.Graphics.Color;

namespace ICSProj.App.Converters;

public class HexStringToColorConverter: BaseConverterOneWay<string, Color>
{
    public override Color ConvertFrom(string value, CultureInfo culture)
        => value != null ?  Color.FromArgb(value) : Colors.Transparent;
    public override Color DefaultConvertReturnValue { get; set; } = Colors.Transparent;
}
