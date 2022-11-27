using Blazored.LocalStorage;

namespace DemoUI.Services.Base
{
    public class BaseHttpService
    {
        public readonly IClient _client;
        public readonly ILocalStorageService _localStorageService;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<T> ConvertApiExceptions<T>(ApiException apiException)
        {
            if(apiException.StatusCode == 400)
            {
                return new Response<T>()
                {
                    Message = "Validation errors have occured.",
                    ValidationErrors = apiException.Response,
                    Success = false
                };
            }

            if (apiException.StatusCode == 404)
            {
                return new Response<T>()
                {
                    Message = "The requested item was not found.",
                    ValidationErrors = apiException.Response,
                    Success = false
                };
            }

            if (apiException.StatusCode == 402)
            {
                return new Response<T>()
                {
                    Message = "You are not authorized to access this.",
                    ValidationErrors = apiException.Response,
                    Success = false
                };
            }

            return new Response<T>()
            {
                Message = "Something went wrong, please contact support.",
                ValidationErrors = apiException.Response,
                Success = false
            };
        }

        protected async Task GetBearerTokenAsync()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("accessToken");

            if (!string.IsNullOrEmpty(savedToken))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", savedToken);
            }
        }
    }
}
