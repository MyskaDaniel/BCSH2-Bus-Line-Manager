using System;
using Avalonia.Controls;
using Avalonia.Controls.Selection;

namespace BusLineManager.Core;

public class ViewModelGetter
{
    public static T? GetViewModel<T, E>(SelectionModelSelectionChangedEventArgs args) where E : UserControl
    {
        var result = default(T);
        var a = (SelectionModelSelectionChangedEventArgs<E>) args;
        try
        {
            if (args.SelectedItems.Count > 0)
            {
                result = (T?) a.SelectedItems[0]?.DataContext;
            }
            else if(args.DeselectedItems.Count > 0)
            {
                result = (T?) a.DeselectedItems[0]?.DataContext;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result;
    }
}