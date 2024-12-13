using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public class ErrorDetail
    {
        public ErrorCode Code { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public ErrorDetail AddMetadata(string key, object data)
        {
            this.Metadata[key] = data;
            return this; 
        }
    }
}
