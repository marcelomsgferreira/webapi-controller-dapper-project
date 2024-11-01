using App.DTOs.Company;
using App.Entities;

namespace App.Repositories.Abstractions
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreateDTO companyDTO);
        public Task UpdateCompany(int id, CompanyForUpdateDTO companyDTO);
        public Task DeleteCompany(int id);
        public Task<Company> GetCompanyByIdEmployee(int id);
        public Task<Company> GetMultipleResults(int id);
        public Task<List<Company>> MultipleMapping();
    }
}
