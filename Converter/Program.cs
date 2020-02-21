using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Program
    {
        /// <summary>
        /// Конвертер из ANSI в UTF-8
        /// </summary>
        static async Task Main(string[] args)
        {
            string pathToFile = args[0];
            string pathToNewFile = args[1];
            string pathToExceptionFile = Path.GetDirectoryName(pathToNewFile) + "\\Error_" + Path.GetFileNameWithoutExtension(pathToNewFile) + ".txt";

            try
            {
                StreamReader sr = new StreamReader(pathToFile, Encoding.GetEncoding(1251));
                string text = await sr.ReadToEndAsync();
                sr.Dispose();
                File.Delete(pathToFile);
                

                StreamWriter sw = new StreamWriter(pathToNewFile, false, new UTF8Encoding(false));
                await sw.WriteLineAsync(text);
                sw.Dispose();

                Console.WriteLine("Сохранено: " + pathToNewFile);
            }
            catch (Exception e)
            {
                StreamWriter sw = new StreamWriter(pathToExceptionFile, false, Encoding.Default);
                await sw.WriteLineAsync("Date: \n" + DateTime.Now + "\r\n" +
                                        "\nException: \n" + e.Message + "\r\n" +
                                        "\nStackTrace: \n" + e.StackTrace.ToString() + "\r\n");
                sw.Dispose();
                Console.WriteLine(e.Message);
            }
        }
    }
}
