using Blazored.LocalStorage;
using DemoUI.Services.Base;
using DemoUI.Services.Interfaces;

namespace DemoUI.Services.Implementation
{
    public class CompanyService : BaseHttpService, ICompanyService
    {
        private readonly IClient _client;

        public CompanyService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
        {
            _client = client;
        }

        public async Task<Response<CompanyDto>> CreateCompany(CreateCompanyDto model)
        {
            Response<CompanyDto> response = new Response<CompanyDto> { Success = true };
            try
            {
                await GetBearerTokenAsync();
                var data = await _client.CompanyAsync(model);
                response = new Response<CompanyDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CompanyDto>(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteCompany(Guid id)
        {
            Response<bool> response = new Response<bool> { Success = true };
            try
            {
                await GetBearerTokenAsync();
                await _client.DeleteCompanyAsync(id);
                response = new Response<bool>()
                {
                    Data = true,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }

            return response;
        }

        public async Task<Response<List<CompanyDto>>> GetAll()
        {
            Response<List<CompanyDto>> response;

            try
            {
                await GetBearerTokenAsync();
                var data = await _client.GetAllCompaniesAsync();

                response = new Response<List<CompanyDto>>()
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<CompanyDto>>(ex);
            }

            return response;
        }

        public async Task<Response<CompanyDto>> GetByIsin(string isin)
        {
            Response<CompanyDto> response;

            try
            {
                await GetBearerTokenAsync();
                var data = await _client.GetCompanyByIsinAsync(isin);

                response = new Response<CompanyDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CompanyDto>(ex);
            }

            return response;
        }

        public async Task<Response<CompanyDto>> GetOne(Guid id)
        {
            Response<CompanyDto> response;

            try
            {
                await GetBearerTokenAsync();
                var data = await _client.GetCompanyAsync(id);

                response = new Response<CompanyDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CompanyDto>(ex);
            }

            return response;
        }

        public async Task<Response<CompanyDto>> UpdateCompany(Guid id, CompanyDto model)
        {
            Response<CompanyDto> response;

            try
            {
                await GetBearerTokenAsync();
                var data = await _client.UpdateCompanyAsync(id, model);

                response = new Response<CompanyDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CompanyDto>(ex);
            }

            return response;
        }
    }
}
