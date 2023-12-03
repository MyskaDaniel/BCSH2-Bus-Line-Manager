using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BusLineManager.Models;
using BusLineManager.ViewModels;
using ReactiveUI;
using BusOperatorPane = BusLineManager.Views.Controls.BusOperatorPane;

namespace BusLineManager.Views.Windows;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();
        
        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowEditLineDialog.RegisterHandler(ShowLineEditDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowEditBusOperatorDialog.RegisterHandler(ShowBusOperatorEditDialogAsync)));
        
        var listBox = this.FindControl<ListBox>("BusOperatorListBox") ?? throw new Exception("Could load Control DopravceList");

        var busOperators = viewModel.BusOperatorsItems;

        foreach (var op in busOperators)
        {
            listBox.Items.Add(new BusOperatorPane(op));
        }
        listBox.Items.Add(new BusOperatorPane(new BusOperator(99, "TestFail","TEST"))); //For test of error
    }

    private void CloseApplication(object? sender, RoutedEventArgs e) => Close();
    
    private async Task DoShowDialogAsync(InteractionContext<AlertViewModel, bool> interaction)
    {
        var dialog = new AlertWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<bool>(this);
        interaction.SetOutput(result);
    }
    
    private async Task ShowLineEditDialogAsync(InteractionContext<EditLineViewModel, bool> interaction)
    {
        var dialog = new EditLineWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<bool>(this);
        interaction.SetOutput(result);
    }
    
    private async Task ShowBusOperatorEditDialogAsync(InteractionContext<EditBusOperatorViewModel, bool> interaction)
    {
        var dialog = new EditBusOperatorWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<bool>(this);
        interaction.SetOutput(result);
    }
}