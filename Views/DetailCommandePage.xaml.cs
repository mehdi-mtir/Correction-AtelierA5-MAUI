using GestionCommandes.Maui.ViewModels;

namespace GestionCommandes.Maui.Views;

public partial class DetailCommandePage : ContentPage
{
    private readonly DetailCommandeViewModel vm;

    public DetailCommandePage(DetailCommandeViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.ChargerCommand.ExecuteAsync(null);
    }
}
