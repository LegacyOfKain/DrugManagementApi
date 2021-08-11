using DrugManagementApi.Attributes;
using DrugManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugManagementApi.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("api/Drugs")]
    public class DrugController : ControllerBase
    {
        private readonly ApiContext apiContext;

        public DrugController(ApiContext apiContext)
        {
            this.apiContext = apiContext;

            if (!apiContext.Drugs.Any())
            {
                
                apiContext.Drugs.Add(new Drug
                { Code = "Drg1", Label = "Drg1Lab", Description = "Drg1Desc", Price = 12.30 });
                apiContext.Drugs.Add(new Drug
                { Code = "Drg2", Label = "Drg2Lab", Description = "Drg2Desc", Price = 123.30 });
                apiContext.Drugs.Add(new Drug
                { Code = "Drg3", Label = "Drg3Lab", Description = "Drg3Desc", Price = 122.30 });
                apiContext.Drugs.Add(new Drug
                { Code = "Drg4", Label = "Drg4Lab", Description = "Drg4Desc", Price = 12312.30 });
                apiContext.Drugs.Add(new Drug
                { Code = "Drg5", Label = "Drg5Lab", Description = "Drg5Desc", Price = 1263.30 });

                apiContext.SaveChanges();
            }
        }

        // GET all drugs
        //  GET: api/Drugs
        [HttpGet]
        public async Task<IEnumerable<Drug>> GetAllDrugs()
        {
            return await apiContext.Drugs.ToListAsync();
        }

        // GET all drugs by Code
        // GET: api/Drugs/DrugCode/code
        [HttpGet("DrugCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            List<Drug> drugs = await Task.Run(
                () => (apiContext.Drugs.Where(x=>x.Code.Equals(code)).ToList<Drug> ())
                );

            return Ok(drugs);
        }

        // GET all drugs by Label
        // GET: api/Drugs/DrugLabel/label
        [HttpGet("DrugLabel/{label}")]
        public async Task<IActionResult> GetByLabel(string label)
        {
            List<Drug> drugs = await Task.Run(
                () => (apiContext.Drugs.Where(x => x.Label.Equals(label)).ToList<Drug>())
                );

            return Ok(drugs);
        }

        // create a new drug
        // POST: api/Drugs/AddDrug
        [HttpPost("AddDrug")]
        public async Task<IActionResult> AddWorkshop(Drug dg)
        {
            if (dg == null)
                return BadRequest();

            apiContext.Drugs.Add(dg);
            await apiContext.SaveChangesAsync();

            return Ok(dg);
        }

        // Update all drugs by Code
        // PUT: api/Drugs/DrugCode/code
        [HttpPut("DrugCode/{code}")]
        public async Task<IActionResult> DeleteByCode(string code, [FromBody] Drug dg)
        {
            List<Drug> drugs = await Task.Run(
                () => (apiContext.Drugs.Where(x => x.Code.Equals(code)).ToList<Drug>())
                );


            if (drugs?.Any() != true)
            {
                return NotFound();
            }
            else
            {
                foreach (Drug drug in drugs)
                {
                    drug.Label = dg.Label;
                    drug.Description = dg.Description;
                    drug.Price = dg.Price;
                    apiContext.Drugs.Update(drug);
                    apiContext.SaveChanges();
                     
                }
            }


            return NoContent();
        }

        // Delete all drugs by Code
        // DELETE: api/Drugs/DrugCode/code
        [HttpDelete("DrugCode/{code}")]
        public async Task<IActionResult> DeleteByCode(string code)
        {
            List<Drug> drugs = await Task.Run(
                () => (apiContext.Drugs.Where(x => x.Code.Equals(code)).ToList<Drug>())
                );
            

            if (drugs?.Any() != true)
            {
                return NotFound();
            }
            else
            {
                foreach(Drug drug in drugs)
                {
                    apiContext.Drugs.Remove(drug);
                    apiContext.SaveChanges();
                     
                }
            }


            return NoContent();
        }
    }
}
