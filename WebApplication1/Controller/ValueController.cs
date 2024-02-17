using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebApplication1.Interfaces;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class ValueController : ControllerBase
{
    private readonly IValueRepository _valueRepository;

    public ValueController(IValueRepository valueRepository)
    {
        _valueRepository = valueRepository;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ValueModel>), 200)]
    public IActionResult GetValues()
    {
        var values = _valueRepository.GetValues();
        if(values.Count ==0)
        {
            return BadRequest(ModelState);        
        }
        return Ok(values);
    }


    [HttpPost("upload-csv")]
    public async Task<IActionResult> UploadCsvFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is not selected");

        if (Path.GetExtension(file.FileName) != ".csv")
            return BadRequest("File format is not supported");

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            // Далее можно произвести с данными из файла нужные операции

            return Ok("File uploaded successfully");
        }
    }

    [HttpGet("method2")]
    public IActionResult Method2()
    {
        // Логика второго метода
        return Ok("Method 2 called");
    }

    [HttpGet("method3")]
    public IActionResult Method3()
    {
        // Логика третьего метода
        return Ok("Method 3 called");
    }
}






