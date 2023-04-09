using System.Collections.Generic;
using System.Collections.ObjectModel;
using ICSProj.BL.Models;

namespace ICSProj.BL;

public static class EnumerableExtension
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> values)
        => new(values);
}
