using Microsoft.AspNetCore.Components;
using Entities;
using Services;
namespace WasmApp.Pages;

public class ListaBomberosBase : ComponentBase
{
    [Inject] IBomberoService BomberoService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<Bombero> bomberosList = new List<Bombero>();

    protected override async Task OnInitializedAsync()
    {
        bomberosList= (await BomberoService.GetAllAsync()).ToList();
        
    }

    protected void NavigateToDetails(int bomberoId)
    {
        Navigation.NavigateTo($"/bomberoInfo/{bomberoId}");
    }

    protected void NavigateToCreate()
    {
        Navigation.NavigateTo("/nuevoBombero");
    }
}
