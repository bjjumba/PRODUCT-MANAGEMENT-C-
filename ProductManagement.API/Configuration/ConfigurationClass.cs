using System.Reflection.PortableExecutable;

namespace ProductManagement.API.Configuration
{
    public static class ConfigurationClass
    {
        //static readonly HttpClient client = new HttpClient();

        public static IConfiguration AppSetting { get; }
        static ConfigurationClass()
        {
            AppSetting= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

    }

}
