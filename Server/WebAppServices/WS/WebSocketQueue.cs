using Core.RuntimeObject;
using Newtonsoft.Json;

namespace Server.WebAppServices.WS
{
    public class WebSocketQueue
    {
        private static bool _start = false;
        public static void Start()
        {
            Task.Run(() =>
            {
                if (!_start)
                {
                    _start = true;
                    Core.LogModule.OperationQueue.AddOperationRecord += OperationQueue_AddOperationRecord;
                }
            });
        }

        private static void OperationQueue_AddOperationRecord(object? sender, EventArgs e)
        {
            Core.LogModule.OperationQueue.pack<RoomCardClass> pack = (Core.LogModule.OperationQueue.pack<RoomCardClass>)sender;
            string MessagePack = JsonConvert.SerializeObject(pack);
            MessageBase.WS_Send(MessagePack);
        }
    }
}
