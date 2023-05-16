using CommunityToolkit.Maui.Converters;
using ICSProj.BL.Models;
using System.Globalization;

namespace ICSProj.App.Converters;

public class TagNameToStringConverter : BaseConverterOneWay<TagListModel, string>
{
    public override string ConvertFrom(TagListModel value, CultureInfo culture)
        => value.Name;
    public override string DefaultConvertReturnValue { get; set; } = string.Empty;
}
