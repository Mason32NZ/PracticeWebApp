using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Minio;
using Minio.Exceptions;
using PracticeWebApp.Data.Models.BlobStorage;

namespace PracticeWebApp.Data
{
    public class BlobStorage
    {
        public BlobStorage(Credentials credentials, bool useSsl = false)
        {
            minio = new MinioClient(credentials.Endpoint, credentials.AccessKey, credentials.SecretKey);

            if (useSsl)
            {
                minio = minio.WithSSL();
            }
        }

        private static MinioClient minio;

        public static async Task<Blob> UploadFile(Blob file, bool randomizeFileName = true)
        {
            try
            {
                // Make a bucket on the server, if not already present.
                var found = await minio.BucketExistsAsync(file.BucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(file.BucketName, file.Location);
                }

                // Randomize name to avoid duplicates.
                if (randomizeFileName)
                {
                    var ext = Path.GetExtension(file.ObjectName);
                    file.ObjectName = Regex.Replace(Guid.NewGuid().ToString(), "-", string.Empty) + ext;
                }

                // Upload a file to bucket.
                await minio.PutObjectAsync(file.BucketName, file.ObjectName, file.FileStream, file.FileStream.Length, file.ContentType, file.MetaData);
                return file; // Return file with updated name.
            }
            catch (MinioException e)
            {
                throw new Exception($"Upload File Error: {e.Message}");
            }
        }

        public static async Task<Blob> GetFileUrl(Blob file)
        {
            try
            {
                var url = await minio.PresignedGetObjectAsync(file.BucketName, file.ObjectName, 60 * 60); // Return URL that will expire in 1 hour.
                file.Url = url;
                return file; // Return file with updated URL.
            }
            catch (MinioException e)
            {
                throw new Exception($"Get File Error: {e.Message}");
            }
        }
    }
}
