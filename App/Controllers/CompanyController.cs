using App.DTOs.Company;
using App.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/company/")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();
            return Ok(companies);
        }

        [HttpGet("{id:int}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompany(id);
            if (company == null) 
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyForCreateDTO companyDTO)
        {
            var createdCompany = await _companyRepository.CreateCompany(companyDTO);

            return CreatedAtRoute("CompanyById", new {Id = createdCompany.Id}, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForUpdateDTO companyDTO)
        {
            var dbCompany = await _companyRepository.GetCompany(id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepository.UpdateCompany(id, companyDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var dbCompany = await _companyRepository.GetCompany(id);
            
            if (dbCompany is null)
                return NotFound();

            await _companyRepository.DeleteCompany(id);

            return NoContent();
        }
    }
}
