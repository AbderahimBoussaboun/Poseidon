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
        public async Task<IActionResult> ProcesarArchivo([FromForm] IFormFile archivo, [FromForm] string balancerName, [FromForm] string commitMessage)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                {
                    return BadRequest("Archivo no recibido o vacío");
                }

                // Crear un GUID o usar un timestamp para generar un nombre de archivo único
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + archivo.FileName;

                // Especificar la carpeta temporal
                // Para carpeta temporal del sistema: Path.GetTempPath()
                string tempFolderPath = ".\\Controllers\\ResourceMaps\\F5\\fileToProcess";

                // Crear la carpeta si no existe
                if (!Directory.Exists(tempFolderPath))
                {
                    Directory.CreateDirectory(tempFolderPath);
                }

                // Unir directorio y nombre de archivo
                var filePath = Path.Combine(tempFolderPath, uniqueFileName);

                // Agregar líneas al archivo antes de copiarlo
                string[] linesToAdd = new string[]
                {
            "ltm monitor /Common/https {}",
            "ltm monitor /Common/http {}",
            "ltm monitor /Common/https_443 {}",
            "ltm monitor /Common/tcp {}"
                };

                // Escribir líneas al archivo
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (string line in linesToAdd)
                    {
                        writer.WriteLine(line);
                    }
                }

                using (var stream = new FileStream(filePath, FileMode.Append))
                {
                    await archivo.CopyToAsync(stream);
                }

                string logFilePath = ".\\log.txt";

                // Escribir balancerName y commitMessage en el archivo de registro
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine("Balancer Name: " + balancerName);
                    writer.WriteLine("Commit Message: " + commitMessage);
                }

                // Llamar al programa Python con el archivo
                string scriptPath = ".\\Controllers\\ResourceMaps\\F5\\script\\parserator.py";
                string argumentos = $"\"{filePath}\" \"{balancerName}\" \"{commitMessage}\""; // Agregar balancerName y commitMessage como argumentos

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\\Program Files\\Python312\\python.exe", // Ejecutar python
                    Arguments = $"-u {scriptPath} {argumentos}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using (Process proceso = new Process { StartInfo = startInfo })
                {
                    proceso.StartInfo.RedirectStandardOutput = true;
                    proceso.StartInfo.RedirectStandardError = true;
                    proceso.Start();

                    // Leer la salida estándar y la salida de error y escribir en el archivo de registro
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine(proceso.StandardOutput.ReadToEnd());
                        writer.WriteLine(proceso.StandardError.ReadToEnd());
                    }

                    proceso.WaitForExit();
                }

                // Eliminar el archivo después de procesarlo
                System.IO.File.Delete(filePath);

                // Eliminar todo el contenido de la carpeta temporal
                var directoryInfo = new DirectoryInfo(tempFolderPath);
                foreach (var file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (var dir in directoryInfo.GetDirectories())
                {
                    dir.Delete(true);
                }

                // Manejar cualquier resultado del programa Python si es necesario
                return Ok("Procesamiento exitoso");
            }
            catch (Exception ex)
            {
                // Loguear el error en un archivo
                string logFilePath = ".\\error_log.txt";
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine(); // Agregar una línea en blanco para separar los errores
                }

                return StatusCode(500, "Se ha producido un error. Consulte el archivo de registro para más detalles.");
            }
        }
        #endregion
    }
}
