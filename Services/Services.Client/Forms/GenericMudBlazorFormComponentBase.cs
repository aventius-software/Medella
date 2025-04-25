using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.Client.Data;
using Services.Shared.Validation;
using System.Numerics;

namespace Services.Client.Forms;

public abstract class GenericMudBlazorFormComponentBase<TFormModel, TValidator, TKey> : ComponentBase
    where TFormModel : class, new()
    where TValidator : AbstractFluentValidationValidator<TFormModel>, new()
    where TKey : IBinaryInteger<TKey>
{
    [Parameter]
    public TKey Key { get; set; } = default!;

    [Inject]
    protected DataService DataService { get; set; } = default!;

    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    protected TFormModel FormModel = new();
    protected MudForm MudFormRef = default!;
    protected bool SubmitButtonIsDisabled = false;
    protected bool SnackBarIsOpen = false;
    protected string SnackBarMessage = string.Empty;
    protected TValidator Validator = new();

    /// <summary>
    /// When the parameters for this component have been set then...
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        // If a key value has been supplied, go and try and get the record 
        if (Key > TKey.Zero)
        {
            // Disable submit button and start loading spinner
            SubmitButtonIsDisabled = true;

            try
            {
                // Try and find the requested record
                var result = await DataService.FindModelAsync<TFormModel>($"{GetApiRoute()}/{Key}");

                // If record not found throw error, otherwise set the current form model ;-)
                if (result is null) throw new Exception("No record found");
                else FormModel = result;
            }
            catch (Exception ex)
            {
                // Execute any error handler
                await OnAfterGetErrorAsync(ex);
            }
            finally
            {
                // Stop loading spinner and re-enable submit
                SubmitButtonIsDisabled = false;
            }
        }
    }

    /// <summary>
    /// Implement this to return the API route required for create/update/delete actions
    /// </summary>
    /// <returns></returns>
    protected abstract string GetApiRoute();

    /// <summary>
    /// Override this to perform actions after a record has
    /// been successfully added/created
    /// </summary>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterAddAsync(HttpResponseMessage responseMessage)
    {
        // Popup message
        Snackbar.Add("Changes saved successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after an 'add' operation has
    /// been attempted but failed
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterAddErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to add the record, please check network connections and form values", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform actions after a record has
    /// been successfully deleted
    /// </summary>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterDeleteAsync(HttpResponseMessage responseMessage)
    {
        // Popup message
        Snackbar.Add("Record deleted successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after a 'delete' operation has
    /// been attempted but failed
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterDeleteErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to delete the record, please check network connections and form values", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform actions after a record has
    /// been succesfully fetched from the server (api)
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterGetErrorAsync(Exception ex)
    {
        Snackbar.Add("Error fetching record, please check network connectivity", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform actions after a record has
    /// been succesfully updated
    /// </summary>
    /// <param name="responseMessage"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterUpdateAsync(HttpResponseMessage responseMessage)
    {
        // Popup message
        Snackbar.Add("Changes saved successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after an 'update' operation has
    /// been attempted but failed
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterUpdateErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to update the record, please check network connections and form values", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this method to perform actions after submission but
    /// before the record is added
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnBeforeAddAsync() => await Task.CompletedTask;

    /// <summary>
    /// Override this method to perform actions after submission but
    /// before the record is deleted
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnBeforeDeleteAsync() => await Task.CompletedTask;

    /// <summary>
    /// Override this method to perform actions after submission but
    /// before the record is updated
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnBeforeUpdateAsync() => await Task.CompletedTask;

    /// <summary>
    /// When the user clicks delete
    /// </summary>
    /// <returns></returns>
    protected async Task OnDelete()
    {
        // Only delete if we've got a record
        if (Key > TKey.Zero)
        {
            // First disable submit button and start loading spinner
            SubmitButtonIsDisabled = true;

            try
            {
                // Before deleting, execute any OnBeforeDelete actions
                await OnBeforeDeleteAsync();

                // Attempt to delete the record
                var response = await DataService.DeleteAsync(GetApiRoute(), Key);

                // Throw error if the action wasn't successful
                if (!response.IsSuccessStatusCode) throw new Exception("Error deleting record");

                // Execute any OnAfterDelete actions
                await OnAfterDeleteAsync(response);
            }
            catch (Exception ex)
            {
                await OnAfterDeleteErrorAsync(ex);
            }
            finally
            {
                // Stop loading spinner and re-enable submit            
                SubmitButtonIsDisabled = false;
            }
        }
    }

    /// <summary>
    /// Reset the form fields and validation
    /// </summary>
    protected async Task OnReset() => await MudFormRef.ResetAsync();

    /// <summary>
    /// When the user clicks submit with a valid form...
    /// </summary>
    protected async Task OnSubmit()
    {
        // First disable submit button and start loading spinner
        SubmitButtonIsDisabled = true;

        // Validate the form
        await MudFormRef.Validate();

        // Was it valid?
        if (MudFormRef.IsValid)
        {
            try
            {
                // Are updating an existing record?
                if (Key != TKey.Zero)
                {
                    // Execute any OnBeforeUpdate actions
                    await OnBeforeUpdateAsync();

                    // Try and update the record with the new data
                    var response = await DataService.UpdateModelAsync(GetApiRoute(), Key, FormModel);

                    // Throw an error if the action was unsuccessful
                    if (!response.IsSuccessStatusCode) throw new Exception("Error updating record");

                    // Execute any OnAfterUpdate actions
                    await OnAfterUpdateAsync(response);
                }
                else
                {
                    // We're creating a new record ;-)
                    await OnBeforeAddAsync();

                    // Try and create the record
                    var response = await DataService.AddModelAsync(GetApiRoute(), FormModel);

                    // Throw an error if the action was unsuccessful
                    if (!response.IsSuccessStatusCode) throw new Exception("Error adding record");

                    // Execute any OnAfterAdd actions
                    await OnAfterAddAsync(response);
                }

                // Reset the form for potential next record...
                await MudFormRef.ResetAsync();
            }
            catch (Exception ex)
            {
                // Execute any error handlers
                if (Key > TKey.Zero)
                {
                    await OnAfterUpdateErrorAsync(ex);
                }
                else
                {
                    await OnAfterAddErrorAsync(ex);
                }
            }
            finally
            {
                // Stop loading spinner and re-enable submit            
                SubmitButtonIsDisabled = false;
            }
        }

        // Stop loading spinner and re-enable submit            
        SubmitButtonIsDisabled = false;
    }
}
