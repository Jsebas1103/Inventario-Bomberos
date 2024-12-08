using Microsoft.AspNetCore.Components;
using Entities;
using Services;
namespace WasmApp.Pages;

public class ListaElementosBase : ComponentBase
{
    [Inject] IElementoService elementoService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<Elemento> elementos = new List<Elemento>();

    protected override async Task OnInitializedAsync()
    {
        elementos= (await elementoService.GetAllAsync()).ToList();
        
    }

    protected void NavigateToDetails(int id)
    {
        Navigation.NavigateTo($"/infoElemento/{id}");
    }

    protected void NavigateToCreate()
    {
        Navigation.NavigateTo("/nuevoElemento");
    }
}
