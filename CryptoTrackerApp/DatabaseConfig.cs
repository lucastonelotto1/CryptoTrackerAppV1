using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp
{
    public class DatabaseConfig
    {
        public string Url { get; set; }
        public string Key { get; set; }

        public static DatabaseConfig Load(string filePath)
        {
            var config = new DatabaseConfig();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    if (key == "Url")
                        config.Url = value;
                    else if (key == "Key")
                        config.Key = value;
                }
            }
            return config;
        }
    }
}
