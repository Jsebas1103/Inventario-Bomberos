using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Services;
using Entities;

namespace WasmApp.Pages;

public class NuevaCategoriaBase : ComponentBase
{
    #region Inject y Variables
    [Inject] ICategoriaService categoriaService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public Categoria categoria = new Categoria();
    #endregion

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            var newCategoria = await categoriaService.SaveAsync(categoria);
            ToastService.ShowSuccess("Se ha creado la nueva categoria correctamente");
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al crear la categoria: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/Categorias");
        }
    }
}
