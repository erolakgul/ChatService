using Newtonsoft.Json;

namespace chatService.startup.Configurations
{
    /// <summary>
    /// the class that will read configuration settings from settings.json file
    /// </summary>
    public class ConnectionSettings
    {
        public string? IpAddress { get; set; }
        public string? PortNumber { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// method that will read from json file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ConnectionSettings ReadJsonFile(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                try
                {
                    string jsonData = file.ReadToEnd();
                    ConnectionSettings data = null;
                    RootJsonFile res = JsonConvert.DeserializeObject<RootJsonFile>(jsonData);

                    foreach (var item in res.Settings)
                    {
                        if (item.Value.IsActive)
                        {
                            data = new ConnectionSettings()
                            {
                                IpAddress = item.Value.IpAddress,
                                PortNumber = item.Value.PortNumber,
                                IsActive = item.Value.IsActive
                            };
                        }
                        Console.WriteLine("");
                        Console.WriteLine(" Name     = {0} \n Ipaddress  = {1} \n PortNumber = {2} \n IsActive   = {3}", item.Key, item.Value.IpAddress, item.Value.PortNumber, item.Value.IsActive);
                    }

                    return data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("File not read => " + ex.Message);
                    return null;
                }

            }
        }
    }

    public class RootJsonFile
    {
        public Dictionary<string, ConnectionSettings>? Settings { get; set; }
    }
}
