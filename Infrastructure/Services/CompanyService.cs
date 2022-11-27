using AutoMapper;
using Infrastructure.Models.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Infrastructure;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAll();
    CompanyDto GetById(Guid id);
    CompanyDto GetByIsin(string Isin);
    Task<CompanyDto> CreateAsync(CreateCompanyDto model);
    Task<CompanyDto> UpdateAsync(Guid Id, CompanyDto model);
    void Delete(Guid id);

}

public class CompanyService : ICompanyService
{
    private DemoContext _context;
    public readonly IMapper _mapper;

    public CompanyService(DemoContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<CompanyDto> GetAll()
    {
        var list = _mapper.Map<IList<Company>, IList<CompanyDto>>(_context.Companies.ToList());
        return list;
    }

    public CompanyDto GetById(Guid id)
    {
        return getCompanyDto(id);
    }

    public CompanyDto GetByIsin(string Isin)
    {
        var company = _context.Companies.Where(c => c.Isin == Isin).FirstOrDefault();
        if (company == null) throw new KeyNotFoundException("Company not found with this ISIN");

        var _mappedCompany = _mapper.Map<CompanyDto>(company);
        return _mappedCompany;
    }

    public async Task<CompanyDto> UpdateAsync(Guid Id, CompanyDto model)
    {
       if(Id != model.Id)
        {
            throw new Exception("Provided Id does not match with model company id");
        }

        var foundEntity = _context.Companies.Find(Id);
        if(foundEntity != null)
        {
            foundEntity.Isin = model.Isin;
            foundEntity.Ticker = model.Ticker;
            foundEntity.Exchange = model.Exchange;
            foundEntity.Name = model.Name;
            foundEntity.Website = model.Website;
            _context.Entry(foundEntity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        else
        {
            throw new Exception("Unable to find company with the provided Id");
        }

    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto model)
    {
        var company = _mapper.Map<Company>(model);
        company.Id = Guid.NewGuid();
     
        _context.Companies.Add(company);
        _context.SaveChanges();

        var companyDto = _mapper.Map<CompanyDto>(_context.Companies.Find(company.Id));
        return companyDto;
    }

    public void Delete(Guid id)
    {
        var company = getCompany(id);
        _context.Remove(company);
        _context.SaveChanges();
    }

    private CompanyDto getCompanyDto(Guid id)
    {
        var company = _context.Companies.Find(id);
        if (company == null) throw new KeyNotFoundException("Company not found");

        var _mappedCompany = _mapper.Map<CompanyDto>(company);
        return _mappedCompany;
    }

    private Company getCompany(Guid id)
    {
        var company = _context.Companies.Find(id);
        if (company == null) throw new KeyNotFoundException("Company not found");
        return company;
    }
}