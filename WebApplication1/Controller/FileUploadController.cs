//using Microsoft.AspNetCore.Mvc;

//namespace WebApplication1.Controller
//{
//    public class FileUploadController : ControllerBase
//    {
//        [HttpPost("upload-csv")]
//        public async Task<IActionResult> UploadCsvFile(IFormFile file)
//        {

//            if (file == null || file.Length == 0)
//                return BadRequest("File is not selected");

//            if (Path.GetExtension(file.FileName) != ".csv")
//                return BadRequest("File format is not supported");

//            using (var stream = new MemoryStream())
//            {
//                await file.CopyToAsync(stream);
//                // Далее можно произвести с данными из файла нужные операции

//                //while (!stream.)
//                //{
//                //    var line = reader.ReadLine();
//                //    var values = line.Split(';');

//                //    string dateTimeString = values[0];
//                //    int timeInSeconds = int.Parse(values[1]);
//                //    double floatValue = double.Parse(values[2]);

//                //    DateTime dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd_HH-mm-ss", null);

//                //    // Далее, вы можете обрабатывать полученные данные по вашему усмотрению
//                //}

//                return Ok("File uploaded successfully");
//            }
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models; // Предположим, что здесь находится ваша модель Result
using Microsoft.EntityFrameworkCore; // Подключаем Entity Framework

namespace WebApplication1.Controller
{
    public class FileUploadController : ControllerBase
    {
        private readonly DBContext _context; // Подставьте ваш контекст базы данных

        public FileUploadController(DBContext context) // Инициализация через конструктор
        {
            _context = context;
        }

        [HttpPost("upload-custom-csv")]
        public async Task<IActionResult> UploadCustomCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            if (Path.GetExtension(file.FileName) != ".csv")
                return BadRequest("Формат файла не поддерживается");

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(';');
                    if (values.Length == 1)
                    {
                        var subValues = values[0].Split(';');
                        if (subValues.Length == 3)
                        {
                            if (DateTime.TryParseExact(subValues[0], "yyyy-MM-dd_HH-mm-ss", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime)
                                && int.TryParse(subValues[1], out int timeInSeconds)
                                && double.TryParse(subValues[2].Replace(',', '.'), out double indicatorValue))
                            {
                                var result = new ResultModel
                                {
                                    StartTime = dateTime,
                                    TotalTime = TimeSpan.FromSeconds(timeInSeconds),
                                    AverageIndicatorValue = indicatorValue
                                };

                                await _context.Results.AddAsync(result);
                            }
                            else
                            {
                                return BadRequest("Неверный формат данных в файле");
                            }
                        }
                        else
                        {
                            return BadRequest("Неверный формат строки данных в файле");
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return Ok("Файл успешно загружен и данные записаны в базу");
            }
        }


    }
}
