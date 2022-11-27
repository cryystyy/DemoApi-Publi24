using DemoUI.Services.Base;

namespace DemoUI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Response<List<CompanyDto>>> GetAll();
        Task<Response<CompanyDto>> GetOne(Guid id);
        Task<Response<CompanyDto>> GetByIsin(string isin);
        Task<Response<CompanyDto>> CreateCompany(CreateCompanyDto model);
        Task<Response<CompanyDto>> UpdateCompany(Guid id, CompanyDto model);
        Task<Response<bool>> DeleteCompany(Guid id);

    }
}
