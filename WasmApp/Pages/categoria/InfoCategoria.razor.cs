
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Entities;
using Services;

namespace WasmApp.Pages;

public class InfoCategoriaBase : ComponentBase
{
    #region Inject y Variables
    [Inject] ICategoriaService categoriaService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected bool showConfirmation;
    protected int idCategoria;
    protected string typeCategoria;
    protected bool isEditMode = false;

    public Categoria categoria = new Categoria();
    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            categoria = await categoriaService.GetByIdAsync(Id);
            idCategoria = categoria.id;
            typeCategoria = categoria.tipo;
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Error: {ex.Message}");
        }
    }

    protected async Task HandleOnValidSubmit()
    {
        try
        {
            await categoriaService.UpdateAsync(idCategoria, categoria);
            ToastService.ShowSuccess("Se han actualizado los datos correctamente");
            isEditMode = false; // Volver al modo de visualización después de guardar
        }
        catch (System.Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al actualizar los datos: {ex.Message}");
        }
    }

    protected void ToggleEditMode()
    {
        isEditMode = !isEditMode;
    }

    protected async Task ShowConfirmationModal()
    {
        showConfirmation = true;
    }

    protected async Task OnConfirmDelete(bool confirmed)
    {
        try
        {
            if (confirmed)
            {
                await categoriaService.DeleteAsync(idCategoria);
                ToastService.ShowSuccess($"Se han eliminado los datos de {typeCategoria} correctamente");
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al tratar de eliminar: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/Categorias");
            showConfirmation = false;
        }
    }
}
