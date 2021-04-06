using Newtonsoft.Json;
using PracticeWebApp.Models;

namespace PracticeWebApp.Web.Models.Extended
{
    public class PostExtended : Post
    {
        public bool IsUpdated => UpdatedTimeStampUtc != null;
    }
}