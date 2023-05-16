using ICSProj.BL.Enums;

namespace ICSProj.BL.Models;

public record ActivityFilterModel()
{
    public DateTime Start { get; set; }
    public DateTime End  { get; set; }
    public TagListModel Tag { get; set; }
    public ProjectAssignListModel Project  { get; set; }

    public TimePeriod? TimePeriod { get; set; }

    public static ActivityFilterModel Empty => new()
    {
        Start = DateTime.Today,
        End = DateTime.Today,
        Tag = null,
        Project = null,
        TimePeriod = null
    };

    public void AdjustDatesBasedOnTimePeriod(in ActivityFilterModel model)
    {
        if (model.TimePeriod.HasValue)
        {
            switch (model.TimePeriod.Value)
            {
                case Enums.TimePeriod.None:
                    Start = DateTime.Today;
                    End = DateTime.Today;
                    break;
                case Enums.TimePeriod.LastWeek:
                    model.Start = DateTime.Today.AddDays(-7);
                    model.End = DateTime.Today;
                    break;
                case Enums.TimePeriod.LastMonth:
                    model.Start = DateTime.Today.AddMonths(-1);
                    model.End = DateTime.Today;
                    break;
                case Enums.TimePeriod.PreviousMonth:
                    model.Start = DateTime.Today.AddMonths(-2);
                    model.End = DateTime.Today.AddMonths(-1);
                    break;
                case Enums.TimePeriod.LastYear:
                    model.Start = DateTime.Today.AddYears(-1);
                    model.End = DateTime.Today;
                    break;
            }
        }
    }
}
