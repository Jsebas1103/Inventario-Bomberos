
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Entities;
using Services;

namespace WasmApp.Pages;

public class InfoElementoBase : ComponentBase
{
    #region Inject y Variables
    [Inject] IElementoService elementoService { get; set; }
    [Inject] IToastService ToastService { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected bool showConfirmation;
    protected int idElemento;
    protected string nameElemento;
    protected bool isEditMode = false;

    public Elemento elemento = new Elemento();
    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            elemento = await elementoService.GetByIdAsync(Id);
            idElemento = elemento.id;
            nameElemento = elemento.nombre;
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
            await elementoService.UpdateAsync(idElemento, elemento);
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
                await elementoService.DeleteAsync(idElemento);
                ToastService.ShowSuccess($"Se han eliminado los datos de {nameElemento} correctamente");
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Se ha presentado un error al tratar de eliminar: {ex.Message}");
        }
        finally
        {
            Navigation.NavigateTo("/elementos");
            showConfirmation = false;
        }
    }
}
