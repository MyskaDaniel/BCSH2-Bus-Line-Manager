﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
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

    public List<BusOperator> BusOperators => _busOperators;
    public ObservableCollection<LinePane> LinePanesItems { get; } = new();
    
    public MainWindowViewModel()
    {
        LinePanesItems.Add(new LinePane());
        
        ShowDialog = new Interaction<AlertViewModel, bool>();
        _busOperators = _database.GetAllBusOperators();
        Selection = new SelectionModel<BusOperatorPane>();
        Selection.SelectionChanged += SelectionChanged;
    }
    
    public SelectionModel<BusOperatorPane> Selection { get; }
    
    public Interaction<AlertViewModel, bool> ShowDialog { get; }
    
    private async void SelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs<BusOperatorPane> args)
    {
        var busOperatorName = "string.Empty"; //args.SelectedItems[0]?.BusOperatorName.Text ?? string.Empty;
        
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
            
            Console.WriteLine(line.ToString());
        }
    }
}
