using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Services;
using Entities;

namespace WasmApp.Pages;

public class NuevoElementoBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IElementoService elementoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public Elemento elemento = new Elemento();
    #endregion

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            var newElemento = await elementoService.SaveAsync(elemento);
            ToastService.ShowSuccess("Se ha creado el elemento correctamente");
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al crear el elemento: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/elementos");
        }
    }
}
