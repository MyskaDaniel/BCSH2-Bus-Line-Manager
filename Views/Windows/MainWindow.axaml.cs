using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BusLineManager.Core.Data;
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
        
        var listBox = this.FindControl<ListBox>("BusOperatorListBox") ?? throw new Exception("Could load Control DopravceList");

        var busOperators = viewModel.BusOperators;

        foreach (var op in busOperators)
        {
            listBox.Items.Add(new BusOperatorPane(op));
        }
        listBox.Items.Add(new BusOperatorPane(new BusOperator(99, "TestFail","TEST"))); //For test of error
    }

    private void CloseApplication(object? sender, RoutedEventArgs e) => Close();
    
    private async Task DoShowDialogAsync(InteractionContext<AlertViewModel, bool> interaction)
    {
        var dialog = new AlertWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<bool>(this);
        interaction.SetOutput(result);
    }
    
}