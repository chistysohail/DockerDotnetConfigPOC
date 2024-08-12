using System;
using System.IO;
using System.Text.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading configuration...");

            // Adjust the path to the configuration file depending on the execution environment
            var basePath = AppContext.BaseDirectory;
            var configFilePath = Path.Combine(basePath, "../../../Common/Configuration/config.json");
            if (!File.Exists(configFilePath))
            {
                // Assuming running from the project root in development
                configFilePath = Path.Combine(basePath, "Common/Configuration/config.json");
            }

            if (File.Exists(configFilePath))
            {
                var json = File.ReadAllText(configFilePath);
                var config = JsonSerializer.Deserialize<Config>(json);
                Console.WriteLine($"Setting1: {config.AppSettings.Setting1}");
                Console.WriteLine($"Setting2: {config.AppSettings.Setting2}");

                // Adjust the data file path based on configuration
                var dataFilePath = config.AppSettings.DataFilePath.Replace("/app/", basePath);
                dataFilePath = dataFilePath.Replace("/", "\\"); // Windows path adjustment if necessary

                if (File.Exists(dataFilePath))
                {
                    var dataContent = File.ReadAllText(dataFilePath);
                    Console.WriteLine($"Data file content: {dataContent}");
                }
                else
                {
                    Console.WriteLine("Data file not found at: " + dataFilePath);
                }
            }
            else
            {
                Console.WriteLine("Configuration file not found at: " + configFilePath);
            }
        }
    }

    public class Config
    {
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
        public string DataFilePath { get; set; }
    }
}
