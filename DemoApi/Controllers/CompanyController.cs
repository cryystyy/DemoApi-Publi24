using Infrastructure;
using Infrastructure.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCompanies")]
        public ActionResult<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            try
            {
                var categories = _companyService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable to obtain a list of companies");
            }
        }

        [HttpGet]
        [Route("GetCompany/{id}")]
        public ActionResult<CompanyDto> GetCompany(Guid id)
        {
            try
            {
                var categories = _companyService.GetById(id);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable to obtain the company based on id");
            }
        }


        [HttpGet]
        [Route("GetCompanyByIsin/{isin}")]
        public ActionResult<CompanyDto> GetCompanyByIsin(string isin)
        {
            try
            {
                var categories = _companyService.GetByIsin(isin);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable toobtain the coisin");
            }
        }


        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto model)
        {
            try
            {
                var response = await _companyService.CreateAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable to create the specified company");
            }
        }

        [HttpPut]
        [Route("UpdateCompany/{id}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(Guid id, CompanyDto model)
        {
            try
            {
                var updatedDto = await _companyService.UpdateAsync(id, model);
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable to update the specified company.");
            }
        }

        [HttpDelete]
        [Route("DeleteCompany")]
        public IActionResult DeleteCompany(Guid id)
        {
            try
            {
                _companyService.Delete(id);
                return Ok(new { message = "Category deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("Unable to delete the specified company");
            }
        }

    }
}
