using CommunityToolkit.Maui.Converters;
using ICSProj.BL.Models;
using System.Globalization;

namespace ICSProj.App.Converters;

public class ProjectNameToStringConverter : BaseConverterOneWay<ProjectAssignListModel, string>
{
    public override string ConvertFrom(ProjectAssignListModel value, CultureInfo? culture)
        => value.ProjectName;
    public override string DefaultConvertReturnValue { get; set; } = string.Empty;
}
