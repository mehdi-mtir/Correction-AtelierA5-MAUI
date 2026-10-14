using GestionCommandes.Maui.ViewModels;

namespace GestionCommandes.Maui.Views;

public partial class CommandesPage : ContentPage
{
    private readonly CommandesViewModel vm;

    public CommandesPage(CommandesViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.ChargerCommand.ExecuteAsync(null);   // recharge à chaque retour
    }
}
