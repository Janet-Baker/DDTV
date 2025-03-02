using System.Text.Json.Nodes;

namespace Core.LiveChat
{
    public class CutOffEventArg : MessageEventArgs
    {
        public string msg { get; set; }
        public int roomID { set; get; }

        internal CutOffEventArg(JsonObject obj) : base(obj)
        {
            msg = (string)obj["msg"];
            roomID = (int)obj["roomID"];
        }
    }
}
