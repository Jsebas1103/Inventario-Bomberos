using Microsoft.AspNetCore.Components;
using Entities;
using Services;
namespace WasmApp.Pages;

public class ListaCategoriaBase : ComponentBase
{
    [Inject] ICategoriaService categoriaService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<Categoria> categorias = new List<Categoria>();

    protected override async Task OnInitializedAsync()
    {
        categorias= (await categoriaService.GetAllAsync()).ToList();
        
    }

    protected void NavigateToDetails(int id)
    {
        Navigation.NavigateTo($"/infoCategoria/{id}");
    }

    protected void NavigateToCreate()
    {
        Navigation.NavigateTo("/nuevaCategoria");
    }
}
