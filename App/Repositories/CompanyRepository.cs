using App.Context;
using App.DTOs.Company;
using App.Entities;
using App.Repositories.Abstractions;
using Dapper;
using System.Data;

namespace App.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        
        public CompanyRepository(DapperContext context) => _context = context;


        public async Task<Company> CreateCompany(CompanyForCreateDTO companyDTO)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", companyDTO.Name, DbType.String);
            parameters.Add("Address", companyDTO.Address, DbType.String);
            parameters.Add("Country", companyDTO.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Company
                {
                    Id = id,
                    Name = companyDTO.Name,
                    Address = companyDTO.Address,
                    Country = companyDTO.Country
                };

                return createdCompany;
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE DROM Companies WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });    
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {

            var query = "SELECT * FROM Companies";

            using(var connection = _context.CreateConnection()) 
            {
                var companies = await connection.QueryAsync<Company>(query);

                return companies.ToList();
            }

        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                return company;
            }
        }

        public Task<Company> GetCompanyByIdEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetMultipleResults(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Company>> MultipleMapping()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDTO companyDTO)
        {
            var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country, WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", companyDTO.Name, DbType.String);
            parameters.Add("Address", companyDTO.Address, DbType.String);
            parameters.Add("Country", companyDTO.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
