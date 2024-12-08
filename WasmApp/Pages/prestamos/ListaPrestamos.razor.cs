using Microsoft.AspNetCore.Components;
using Entities;
using Services;
namespace WasmApp.Pages;

public class ListaPrestamoBase : ComponentBase
{
    [Inject] IPrestamoService prestamoService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<Prestamo> prestamos = new List<Prestamo>();

    protected override async Task OnInitializedAsync()
    {
        prestamos= (await prestamoService.GetAllAsync()).ToList();
        
    }

    protected void NavigateToDetails(int id)
    {
        Navigation.NavigateTo($"/infoPrestamo/{id}");
    }

    protected void NavigateToCreate()
    {
        Navigation.NavigateTo("/nuevoPrestamo");
    }
}
