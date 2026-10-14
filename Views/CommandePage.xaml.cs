using GestionCommandes.Maui.ViewModels;

namespace GestionCommandes.Maui.Views;

public partial class CommandePage : ContentPage
{
    private readonly CommandeViewModel vm;

    public CommandePage(CommandeViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.ChargerCommand.ExecuteAsync(null);   // remplit la liste des clients
    }
}
