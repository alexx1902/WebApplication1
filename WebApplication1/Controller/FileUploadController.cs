using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controller
{
    public class FileUploadController : ControllerBase
    {
        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            var test = Path.GetFileName(file.FileName);
            if (file == null || file.Length == 0)
                return BadRequest("File is not selected");

            if (Path.GetExtension(file.FileName) != ".csv")
                return BadRequest("File format is not supported");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                // Далее можно произвести с данными из файла нужные операции

                while (!stream.)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    string dateTimeString = values[0];
                    int timeInSeconds = int.Parse(values[1]);
                    double floatValue = double.Parse(values[2]);

                    DateTime dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd_HH-mm-ss", null);

                    // Далее, вы можете обрабатывать полученные данные по вашему усмотрению
                }
                
                return Ok("File uploaded successfully");
            }
        }
    }
}