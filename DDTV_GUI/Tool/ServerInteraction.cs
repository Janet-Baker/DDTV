using DDTV_Core.Tool.ServerMessageClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDTV_GUI.Tool
{
    public class ServerInteraction
    {
        public static void Start()
        {
            Notice.Start();
            //Dokidoki.Start();
            
        }

        public class Notice
        {
            public static event EventHandler<EventArgs> NewNotice;
            private static bool Is = false;
            public static void Start()
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
                                string N = DDTV_Core.Tool.Notice.GetNotice("GUI");
                                MessageBase.pack<MessageClass.NoticeText> pack = JsonConvert.DeserializeObject<MessageBase.pack<MessageClass.NoticeText>>(N);
                                if (NewNotice != null)
                                {
                                    NewNotice.Invoke(pack.data.Text, EventArgs.Empty);
                                }
                            }
                            catch (Exception)
                            {

                            }
                            Thread.Sleep(3600 * 1000);
                        }
                    });
                }
            }
        }
    }
}
