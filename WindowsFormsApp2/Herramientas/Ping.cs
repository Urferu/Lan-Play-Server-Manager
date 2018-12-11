using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace WindowsFormsApp2
{
    class PingData
    {
        public static PingReply getPing(string server)
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(server, 1000);
                return reply;
            }
            catch
            {
                return null;
            }
        }
    }
}
