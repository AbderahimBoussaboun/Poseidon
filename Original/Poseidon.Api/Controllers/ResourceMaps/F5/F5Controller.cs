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

            try
            {
                // Crear un GUID o usar un timestamp para generar un nombre de archivo único
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + archivo.FileName;

                // Especificar la carpeta temporal
                // Para carpeta temporal del sistema: Path.GetTempPath()
                string tempFolderPath = ".\\Controllers\\ResourceMaps\\F5\\fileToProcess";

                // Crea la carpeta si no existe
                if (!Directory.Exists(tempFolderPath))
                {
                    Directory.CreateDirectory(tempFolderPath);
                }

                // Unir directorio y nombre de archivo
                var filePath = Path.Combine(tempFolderPath, uniqueFileName);

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

                // Elimina el archivo después de procesarlo
                System.IO.File.Delete(filePath);

                // Elimina todo el contenido de la carpeta temporal
                var directoryInfo = new DirectoryInfo(tempFolderPath);
                foreach (var file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (var dir in directoryInfo.GetDirectories())
                {
                    dir.Delete(true);
                }

                // Maneja cualquier resultado del programa Python si es necesario

                return Ok("Procesamiento exitoso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        #endregion
    }
}
