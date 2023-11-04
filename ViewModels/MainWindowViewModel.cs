using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BusLineManager.Core.Data;
using BusLineManager.Core.Database;
using BusLineManager.Views;
using ReactiveUI;

namespace BusLineManager.ViewModels;

public class MainWindowViewModel : ViewModelBase, IReactiveObject
{
    private readonly Database _database = new();
    
    public BusOperator[] GetAllBusOperators() => _database.GetAllBusOperators().ToArray();
}