using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Diagnostics;

namespace Poseidon.Api.Controllers.ResourceMaps.F5
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class F5Controller : ControllerBase
    {

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcesarArchivo([FromForm] IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("Archivo no recibido o vacío");
            }

            try { 
            
                // Aquí puedes guardar el archivo en el servidor si es necesario
                var filePath = Path.Combine(".\\Controllers\\ResourceMaps\\F5\\fileToProcess", archivo.FileName);

            Console.WriteLine("El primero funciona");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }
                string logFilePath = ".\\log.txt";

                // Llama al programa Python con el archivo
                string scriptPath = ".\\Controllers\\ResourceMaps\\F5\\script\\parserator.py";
                string argumentos = $"\"{filePath}\" > \"{logFilePath}\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python", //Ejecutar python
                    Arguments = $"{scriptPath} {argumentos}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using (Process proceso = new Process { StartInfo = startInfo })
                {
                    proceso.StartInfo.RedirectStandardOutput = true;
                    proceso.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);

                    Console.WriteLine("Ejecutando script....");
                    proceso.Start();
                    proceso.BeginOutputReadLine(); // Comienza a redirigir la salida estándar

                    proceso.WaitForExit();

                    Console.WriteLine("Hasta aquí todo bien");

                }        

                // Maneja cualquier resultado del programa Python si es necesario

                return Ok("Procesamiento exitoso");
            }catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
    }
        #endregion
    }
}
