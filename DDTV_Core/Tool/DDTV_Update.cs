using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDTV_Core.Tool
{
    public class DDTV_Update
    {
        public static bool ComparisonVersion(string Type, string LocalVersion)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>()
            {
                {"Type",Type },
                {"LocalVersion",LocalVersion },
                {"CAID",InitDDTV_Core.ClientAID }
            };
            try
            {
                string Ver = SystemAssembly.NetworkRequestModule.Post.Post.HttpPost("http://api.ddtv.pro/api/Ver", Parameters);
                ServerMessageClass.MessageBase.pack<ServerMessageClass.MessageClass.VerClass> pack = JsonConvert.DeserializeObject<ServerMessageClass.MessageBase.pack<ServerMessageClass.MessageClass.VerClass>>(Ver);
                Version v1 = new Version(pack.data.Ver);
                Version v2 = new Version(LocalVersion);
                if (v1 > v2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class FileInfoClass
        {
            public string Ver { get; set; }
            public string Description { get; set; }
            public List<Files> files { set; get; } = new List<Files>();
            public string Bucket { get; set; }
            public string Type { get; set; }
            public class Files
            {
                public string FileName { get; set; }
                public long Size { get; set; }
                public string FileMd5 { get; set; }
                public string FilePath { get; set; }
            }
        }
    }
}
