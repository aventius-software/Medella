//#region Namespaces

//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.WebUtilities;
//using Services.Client.Extensions;
//using Services.Shared.Pagination;
//using System.Net.Http.Json;

//#endregion

//namespace Services.Client.Data;

//public class DataService
//{
//    #region Private Fields

//    private readonly HttpClient _httpClient;

//    #endregion

//    #region Constructors

//    public DataService(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//    }

//    #endregion       

//    #region Public Methods

//    /// <summary>
//    /// Add the specified record
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route">The route to make POST request to</param>
//    /// <param name="model">The model to be added</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> AddModelAsync<TModel>(string route, TModel model, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.PostAsJsonAsync(route, model, cancellationToken);
//    }

//    /// <summary>
//    /// Delete the specified record
//    /// </summary>
//    /// <param name="route">The route to make DELETE request to</param>
//    /// <param name="key">Key for the specified model to delete</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> DeleteAsync<TKey>(string route, TKey key, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.DeleteAsync($"{route}/{key}", cancellationToken);
//    }

//    /// <summary>
//    /// Delete the specified record, but passing URL query string parameters as specified instead of a primary key
//    /// </summary>
//    /// <param name="route">The route to make DELETE request to</param>
//    /// <param name="parameters">Dictionary of query string parameters to pass</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> DeleteAsync(string route, IDictionary<string, string?> parameters, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.DeleteAsync(QueryHelpers.AddQueryString($"{route}?hash={DateTime.Now}", parameters), cancellationToken);
//    }

//    /// <summary>
//    /// Find model
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route">The route to make GET request to</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<TModel?> FindModelAsync<TModel>(string route, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetFromJsonAsync<TModel>($"{route}?hash={DateTime.Now}", cancellationToken);
//    }

//    /// <summary>
//    /// Find model by key
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route">The route to make GET request to</param>
//    /// <param name="key">Key for the specified model to retrieve</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<TModel?> FindModelAsync<TModel, TKey>(string route, TKey key, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetFromJsonAsync<TModel>($"{route}/{key}?hash={DateTime.Now}", cancellationToken);
//    }

//    /// <summary>
//    /// Find model by query string parameters
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route">The route to make GET request to</param>
//    /// <param name="parameters">Dictionary of query string parameters to pass</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<TModel?> FindModelAsync<TModel>(string route, IDictionary<string, string?> parameters, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetFromJsonAsync<TModel>(QueryHelpers.AddQueryString($"{route}?hash={DateTime.Now}", parameters), cancellationToken);
//    }

//    /// <summary>
//    /// Request a paged list of models
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route"></param>
//    /// <param name="pageIndex"></param>
//    /// <param name="pageSize"></param>
//    /// <param name="searchText"></param>
//    /// <param name="sortBy"></param>
//    /// <param name="sortAscending"></param>
//    /// <param name="customParameters"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns>Response in form of PaginationResponseModel</returns>
//    public async Task<PagedResponseModel<TModel>?> GetPagedListAsync<TModel>(string route, int pageIndex, int pageSize, string searchText, string sortBy, bool sortAscending, IEnumerable<CustomQueryStringParameter>? customParameters = null, CancellationToken cancellationToken = default)
//    {
//        // Set the standard parameters
//        var parameters = new Dictionary<string, string?>
//        {
//            { "hash", $"{DateTime.Now}" },
//            { nameof(PagedRequestModel.PageIndex), pageIndex.ToString() },
//            { nameof(PagedRequestModel.PageSize), pageSize.ToString() },
//            { nameof(PagedRequestModel.SearchText), searchText },
//            { nameof(PagedRequestModel.SortBy), sortBy },
//            { nameof(PagedRequestModel.SortAscending), sortAscending.ToString() }
//        };

//        // Any custom request parameters?
//        if (customParameters is not null)
//        {
//            // Yes...
//            for (var i = 0; i < customParameters.Count(); i++)
//            {
//                // Add this parameters name
//                parameters.Add(
//                    $"{nameof(PagedRequestModel.CustomParameters)}[{i}].{nameof(CustomQueryStringParameter.ParameterName)}",
//                    customParameters.ElementAt(i).ParameterName
//                );

//                // Add this parameters value
//                parameters.Add(
//                    $"{nameof(PagedRequestModel.CustomParameters)}[{i}].{nameof(CustomQueryStringParameter.ParameterValue)}",
//                    customParameters.ElementAt(i).ParameterValue
//                );
//            }
//        }

//        // Set parameters
//        var url = QueryHelpers.AddQueryString(route, parameters);

//        return await _httpClient.GetFromJsonAsync<PagedResponseModel<TModel>>(url, cancellationToken);
//    }

//    /// <summary>
//    /// Request a list of models from a starting index
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route"></param>
//    /// <param name="startIndex"></param>
//    /// <param name="count"></param>
//    /// <param name="searchText"></param>
//    /// <param name="sortBy"></param>
//    /// <param name="sortAscending"></param>
//    /// <param name="customParameters"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public async Task<PagedResponseModel<TModel>?> GetListAsync<TModel>(string route, int startIndex, int count, string searchText, string sortBy, bool sortAscending, IEnumerable<CustomQueryStringParameter>? customParameters = null, CancellationToken cancellationToken = default)
//    {
//        // Set the standard parameters
//        var parameters = new Dictionary<string, string?>
//        {
//            { "hash", $"{DateTime.Now}" },
//            { nameof(UnpagedListRequestModel.StartIndex), startIndex.ToString() },
//            { nameof(UnpagedListRequestModel.Count), count.ToString() },
//            { nameof(UnpagedListRequestModel.SearchText), searchText },
//            { nameof(UnpagedListRequestModel.SortBy), sortBy },
//            { nameof(UnpagedListRequestModel.SortAscending), sortAscending.ToString() }
//        };

//        // Any custom parameters?
//        if (customParameters is not null)
//        {
//            for (var i = 0; i < customParameters.Count(); i++)
//            {
//                // Add this parameters name
//                parameters.Add(
//                    $"{nameof(UnpagedListRequestModel.CustomParameters)}[{i}].{nameof(CustomQueryStringParameter.ParameterName)}",
//                    customParameters.ElementAt(i).ParameterName
//                );

//                // Add this parameters value
//                parameters.Add(
//                    $"{nameof(UnpagedListRequestModel.CustomParameters)}[{i}].{nameof(CustomQueryStringParameter.ParameterValue)}",
//                    customParameters.ElementAt(i).ParameterValue
//                );
//            }
//        }

//        // Set parameters
//        var url = QueryHelpers.AddQueryString(route, parameters);

//        // Make request...
//        return await _httpClient.GetFromJsonAsync<PagedResponseModel<TModel>>(url, cancellationToken);
//    }

//    /// <summary>
//    /// Perform a simple HTTP GET request
//    /// </summary>
//    /// <param name="url">Route to make the GET request</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns>A string value</returns>
//    public async Task<string> HttpGetAsync(string url, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetStringAsync(url, cancellationToken);
//    }

//    /// <summary>
//    /// Perform a HTTP GET request
//    /// </summary>
//    /// <param name="url">Route to make the GET request</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns>A HttpResponseMessage object</returns>
//    public async Task<HttpResponseMessage> HttpGetResponseAsync(string url, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetAsync(url, cancellationToken);
//    }

//    /// <summary>
//    /// Perform a HTTP GET request and map JSON result to type specified
//    /// </summary>
//    /// <typeparam name="T">Type to map the JSON response to</typeparam>
//    /// <param name="url">Route to make the GET request</param>
//    /// <param name="cancellationToken">Optional cancellation token</param>
//    /// <returns></returns>
//    public async Task<T?> HttpGetAsJsonAsync<T>(string url, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.GetFromJsonAsync<T>(url, cancellationToken);
//    }

//    /// <summary>
//    /// Patch (update) a model
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route"></param>
//    /// <param name="key"></param>
//    /// <param name="model"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> PatchModelAsync<TModel, TKey>(string route, TKey key, JsonPatchDocument<TModel> model, CancellationToken cancellationToken = default) where TModel : class
//    {
//        return await _httpClient.PatchAsync($"{route}/{key}", model, cancellationToken);
//    }

//    /// <summary>
//    /// Updates the specified model, using specified key
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route"></param>
//    /// <param name="key"></param>
//    /// <param name="model"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> UpdateModelAsync<TModel, TKey>(string route, TKey key, TModel model, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.PutAsJsonAsync($"{route}/{key}", model, cancellationToken);
//    }

//    /// <summary>
//    /// Updates the specified model, using query string parameters
//    /// </summary>
//    /// <typeparam name="TModel"></typeparam>
//    /// <param name="route">API route</param>
//    /// <param name="parameters">Dictionary of query string key/value parameters</param>
//    /// <param name="model"></param>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    public async Task<HttpResponseMessage> UpdateModelAsync<TModel>(string route, IDictionary<string, string?> parameters, TModel model, CancellationToken cancellationToken = default)
//    {
//        return await _httpClient.PutAsJsonAsync(QueryHelpers.AddQueryString(route, parameters), model, cancellationToken);
//    }

//    #endregion
//}