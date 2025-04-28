using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.Shared.Data;
using Services.Shared.Validation;
using System.Numerics;

namespace Services.Shared.Forms;

public abstract class GenericMudBlazorFormComponentBase<TFormModel, TValidator, TKey> : ComponentBase
    where TFormModel : class, new()
    where TValidator : AbstractFluentValidationValidator<TFormModel>, new()
    where TKey : IBinaryInteger<TKey>
{
    [Parameter]
    public TKey Key { get; set; } = default!;

    [Inject]
    protected IDataService<TFormModel, TKey> DataService { get; set; } = default!;

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
        // If a non-zero key value parameter has been supplied, then we're in 'edit'
        // mode. In this case, we should try and get the requested record and populate
        // the form with the values returned
        if (Key != TKey.Zero)
        {
            // Disable submit button so user can't try and submit the form whilst
            // we perform any actions to retrieve the requested record
            SubmitButtonIsDisabled = true;

            try
            {
                // Try and find the requested record
                var result = await DataService.FindModelAsync(Key);

                // If the record was not found we throw an error, otherwise we've
                // managed to fetch the record, so we set the current form model
                // to populate all the form fields
                if (result is null) throw new Exception("No record found");
                else FormModel = result;

                // Perform any actions after a record if fetched successfully
                await OnAfterGetAsync();
            }
            catch (Exception ex)
            {
                // There was an error whilst trying to fetch the record, so
                // lets execute any error handler to perform any required actions
                await OnAfterGetErrorAsync(ex);
            }
            finally
            {
                // All done, so re-enable submit
                SubmitButtonIsDisabled = false;
            }
        }
    }

    /// <summary>
    /// Override this to perform any actions after a record has been successfully added/created
    /// </summary>    
    /// <returns></returns>
    protected virtual async Task OnAfterAddAsync()
    {
        // Popup message
        Snackbar.Add("Changes saved successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after a record has failed to be added/created
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterAddErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to add the record... please check form values, system and network for issues", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform actions after a record has been successfully deleted
    /// </summary>    
    /// <returns></returns>
    protected virtual async Task OnAfterDeleteAsync()
    {
        // Popup message
        Snackbar.Add("Record deleted successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after a 'delete' operation has been attempted but failed
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterDeleteErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to delete the record... please check form values, system and network for issues", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform any actions after successfully fetching a record
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnAfterGetAsync() => await Task.CompletedTask;

    /// <summary>
    /// Override this to perform actions after failure to fetch a record
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterGetErrorAsync(Exception ex)
    {
        Snackbar.Add("Error fetching record, please check system and network connectivity", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to perform actions after a record has been succesfully updated
    /// </summary>    
    /// <returns></returns>
    protected virtual async Task OnAfterUpdateAsync()
    {
        // Popup message
        Snackbar.Add("Changes saved successfully", Severity.Success, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this to handle errors after an 'update' operation has been attempted but failed
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    protected virtual async Task OnAfterUpdateErrorAsync(Exception ex)
    {
        // Oh dear...
        Snackbar.Add("Sorry, there was a problem whilst trying to update the record... please check form values, system and network for issues", Severity.Error, options =>
        {
            options.VisibleStateDuration = 10000;
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// Override this method to perform actions after submission, but before the record is added
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnBeforeAddAsync() => await Task.CompletedTask;

    /// <summary>
    /// Override this method to perform actions after submission but before the record is deleted
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnBeforeDeleteAsync() => await Task.CompletedTask;

    /// <summary>
    /// Override this method to perform actions after submission but before the record is updated
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
        if (Key != TKey.Zero)
        {
            // First disable submit button
            SubmitButtonIsDisabled = true;

            try
            {
                // Before deleting, execute any OnBeforeDelete actions
                await OnBeforeDeleteAsync();

                // Attempt to delete the record
                var success = await DataService.DeleteModelAsync(Key);

                // Throw error if the action wasn't successful
                if (!success) throw new Exception("Error deleting record");

                // Execute any OnAfterDelete actions
                await OnAfterDeleteAsync();
            }
            catch (Exception ex)
            {
                // If there was an error, call this method to perform any actions required
                await OnAfterDeleteErrorAsync(ex);
            }
            finally
            {
                // Re-enable submit            
                SubmitButtonIsDisabled = false;
            }
        }
    }

    /// <summary>
    /// Reset the form fields and validation
    /// </summary>
    protected async Task OnReset() => await MudFormRef.ResetAsync();

    /// <summary>
    /// When the user clicks submit button, with a valid form...
    /// </summary>
    protected async Task OnSubmit()
    {
        // First we disable submit button so user can't double click the button
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
                    // Yes, we're performing an update, first execute any OnBeforeUpdate actions
                    await OnBeforeUpdateAsync();

                    // Now, try and update the record with the new data
                    var success = await DataService.UpdateModelAsync(FormModel);

                    // Throw an error if the action was unsuccessful
                    if (!success) throw new Exception("Error updating record");

                    // If we're here, the update went fine, so now execute any OnAfterUpdate actions
                    await OnAfterUpdateAsync();
                }
                else
                {
                    // Adding a new record, first run this method to perform any required
                    // actions that need to run before we try to add the record...
                    await OnBeforeAddAsync();

                    // Now, try and create the record
                    var success = await DataService.AddModelAsync(FormModel);

                    // Throw an error if the action was unsuccessful
                    if (!success) throw new Exception("Error adding record");

                    // Execute any OnAfterAdd actions
                    await OnAfterAddAsync();
                }

                // Reset the form for potential next record...
                await MudFormRef.ResetAsync();
            }
            catch (Exception ex)
            {
                // Execute any error handlers
                if (Key > TKey.Zero)
                {
                    // There was an error whilst trying to update a record
                    await OnAfterUpdateErrorAsync(ex);
                }
                else
                {
                    // There was an error whilst trying to add a record
                    await OnAfterAddErrorAsync(ex);
                }
            }
            finally
            {
                // Re-enable submit            
                SubmitButtonIsDisabled = false;
            }
        }

        // Re-enable submit after everything is done
        SubmitButtonIsDisabled = false;
    }
}
