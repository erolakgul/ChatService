using Newtonsoft.Json;
using System.Reflection;

namespace chatService.startup.Configurations
{
    /// <summary>
    /// the class that will read configuration settings from settings.json file
    /// </summary>
    public class ConnectionSettings
    {
        private readonly string _fileName = String.Empty;
        public string? IpAddress { get; set; }
        public int PortNumber { get; set; }
        public bool IsActive { get; set; }
        public int MaxCountQueue { get; set; }

        public ConnectionSettings()
        {
            _fileName = "settings.json";
        }

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
                                IsActive = item.Value.IsActive,
                                MaxCountQueue = item.Value.MaxCountQueue
                            };
                        }
                        Console.WriteLine("");
                        Console.WriteLine(" Name       = {0} \n Ipaddress  = {1} \n PortNumber = {2} \n IsActive   = {3}", item.Key, item.Value.IpAddress, item.Value.PortNumber, item.Value.IsActive);
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

        /// <summary>
        /// get current file path
        /// </summary>
        /// <returns></returns>
        public string GetLibraryPath()
        {
            string codeExePath = Assembly.GetCallingAssembly().CodeBase; // çağıran projeye ait lokasyonu alır

            string projectPath = codeExePath.Substring(0, codeExePath.LastIndexOf("bin"));
            projectPath = projectPath.Remove(projectPath.Length - 1);
            projectPath = projectPath.Substring(0, projectPath.LastIndexOf("/"));
            projectPath = projectPath + "/" + Assembly.GetExecutingAssembly().GetName().Name;

            string filePath = new Uri(projectPath).LocalPath + "\\" + _fileName;

            return filePath;
        }
    }

    public class RootJsonFile
    {
        public Dictionary<string, ConnectionSettings>? Settings { get; set; }
    }
}
