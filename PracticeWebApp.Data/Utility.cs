using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PracticeWebApp.Data.Models.BlobStorage;

namespace PracticeWebApp.Data
{
    public static class Utility
    {
        public static AppDbContext GetDbContext(string db = "Default")
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(GetConnectionString(db));
            return new AppDbContext(optionsBuilder.Options);
        }

        public static string GetConnectionString(string db)
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            return config["ConnectionStrings:" + db];
        }

        public static BlobStorage GetBlobStorage(bool useSsl = false)
        {
            return new BlobStorage(GetBlobStorageCredentials(), useSsl);
        }

        public static Credentials GetBlobStorageCredentials()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();

            return new Credentials
            {
                Endpoint = config["BlobStorageCredentials:Endpoint"],
                AccessKey = config["BlobStorageCredentials:AccessKey"],
                SecretKey = config["BlobStorageCredentials:SecretKey"]
            };
        }
    }
}
