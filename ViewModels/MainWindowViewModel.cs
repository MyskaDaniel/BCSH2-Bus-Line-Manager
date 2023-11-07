using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Threading;
using BusLineManager.Core.Data;
using BusLineManager.Core.Database;
using BusLineManager.Views.Controls;
using ReactiveUI;
using BusOperatorPane = BusLineManager.Views.Controls.BusOperatorPane;

namespace BusLineManager.ViewModels;

public class MainWindowViewModel : ViewModelBase, IReactiveObject
{
    private readonly Database _database = new();
    private readonly List<BusOperator> _busOperators;
    private readonly ObservableCollection<LinePane> _linePanes = new();
    private readonly ObservableCollection<BusPane> _busPanes = new();

    public List<BusOperator> BusOperators => _busOperators;
    public ObservableCollection<LinePane> LinePanesItems => _linePanes;
    public ObservableCollection<BusPane> BusPanesItems => _busPanes;
    
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<AlertViewModel, bool>();
        _busOperators = _database.GetAllBusOperators();
        BusOperatorSelection = new SelectionModel<BusOperatorPane>();
        BusOperatorSelection.SelectionChanged += BusOperatorSelectionChanged;
        LinesSelection = new SelectionModel<LinePane>();
        LinesSelection.SelectionChanged += LinesSelectionChanged;
    }
    
    public SelectionModel<BusOperatorPane> BusOperatorSelection { get; }
    public SelectionModel<LinePane> LinesSelection { get; }
    
    public Interaction<AlertViewModel, bool> ShowDialog { get; }
    
    private async void BusOperatorSelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs<BusOperatorPane> args)
    {
        _linePanes.Clear();
        var busOperatorName = args.SelectedItems[0]?.BusOperatorName.Text ?? string.Empty;
        
        var busOperator = _busOperators.FirstOrDefault(it => it.Name == busOperatorName) ?? 
                          _database.GetAllBusOperators().FirstOrDefault(it => it.Name == busOperatorName);
        
        if (busOperator is null)
        {
            var alert = new AlertViewModel($"Bus operator with name {busOperatorName} was not found");
            await ShowDialog.Handle(alert);
            return;
        }
        
        var lines = await _database.GetLinesForOperatorAsync(busOperator);
        foreach (var line in lines)
        {
            _linePanes.Add(new LinePane(line));
        }
    }
    
    private async void LinesSelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs<LinePane> args)
    {
       _busPanes.Clear();
       var busOperatorName = args.SelectedItems[0]?.BusOperatorName.Text ?? string.Empty;
        
       var busOperator = _busOperators.FirstOrDefault(it => it.Name == busOperatorName) ?? 
                         _database.GetAllBusOperators().FirstOrDefault(it => it.Name == busOperatorName);
       
       if (busOperator is null)
       {
           var alert = new AlertViewModel($"Bus operator with name {busOperatorName} was not found");
           await ShowDialog.Handle(alert);
           return;
       }
       var buses = await _database.GetBusesForOperatorAsync(busOperator);
       foreach (var bus in buses)
       {
           _busPanes.Add(new BusPane());
       }
       
    }
}
