using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeWebApp.Data.Models.BlobStorage
{
    public class Blob
    {
        public string BucketName { get; set; }
        public string Location { get; set; }
        public string ObjectName { get; set; }
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
        public string Url { get; set; }
    }
}
