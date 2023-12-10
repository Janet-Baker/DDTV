using DDTV_Core.Tool.ServerMessageClass;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDTV_Core.Tool
{
    public class ServerInteraction
    {
        public class UpdateNotice
        {
            private static bool Is = false;
            private static int i = 0;
            public static void Start(string Type)
            {
                if (!Is)
                {
                    Is = true;
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                string N = DDTV_Core.Tool.Notice.GetNotice(Type);
                                MessageBase.pack<MessageClass.NoticeText> pack = JsonConvert.DeserializeObject<MessageBase.pack<MessageClass.NoticeText>>(N);
                                if (!string.IsNullOrEmpty(pack.data.Text))
                                {
                                    DDTV_Core.InitDDTV_Core.UpdateNotice = pack.data.Text;
                                }
                                Thread.Sleep(3600 * 1000);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    });
                }
            }
        }

        public class Dokidoki
        {
            private static bool Is = false;
            private static int i = 0;
            public static void Start(string Type)
            {
                if (!Is)
                {
                    Is = true;
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                DDTV_Core.Tool.Dokidoki.SendDokidoki(Type, i.ToString());
                                i++;
                                Thread.Sleep(3600 * 1000);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    });
                }
            }
        }
    }
}
