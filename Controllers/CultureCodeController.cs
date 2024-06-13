using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MitsTest.Data;
using MitsTest.Models;
using System.Xml.Linq;

namespace MitsTest.Controllers;

[ApiController]
[Route("/CultureCode")]
public class CultureCodeController : ControllerBase
{
    private DataContext _dataContext;
    public CultureCodeController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    [HttpGet("GetCultureCodes")]
    public async Task<ActionResult<List<CultureCodeModel>>> GetCultureCodes()
    {
        try
        {
            return Ok(await _dataContext.CultureCodes.ToListAsync());
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpGet("GetCultureCodeById/{Id}", Name = "GetCultureCodeById")]
    public async Task<ActionResult<CultureCodeModel>> GetCultureCodeById(int Id)
    {
        try
        {
            var cultureCode = await _dataContext.CultureCodes.FindAsync(Id);
            if (cultureCode == null)
            {
                return NotFound();
            }
            return Ok(cultureCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpGet("GetCultureCodeByName")]
    public async Task<ActionResult<CultureCodeModel>> GetCultureCodeByName(string name)
    {
        try
        {
            var cultureCodes = await _dataContext.CultureCodes
                                    .Where(c => c.Name.Contains(name))
                                    .ToListAsync();
            if (cultureCodes.Count == 0)
            {
                return NotFound();
            }
            return Ok(cultureCodes);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("AddCultureCode")]
    public async Task<ActionResult<CultureCodeModel>> AddCultureCode(CultureCodeModel cultureCode)
    {
        try
        {
            var existingCultureCode = await _dataContext.CultureCodes
                                    .FirstOrDefaultAsync(cc => cc.Name == cultureCode.Name && cc.CultureCode == cultureCode.CultureCode);
            if (existingCultureCode != null)
            {
                // Return Status Cide 409 Conflict data exists in DB
                return Conflict(new { message = "A Culture Code with the same Name and Culture Code already exists." });
            }

            _dataContext.Add(cultureCode);
            await _dataContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetCultureCodeById", new { cultureCode.Id } , cultureCode);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}
