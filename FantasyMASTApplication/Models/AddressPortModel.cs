using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMASTApplication.Models
{
    public class AddressPortModel
    {

        public AddressPortModel()
        {

        }

        public AddressPortModel(string sendPort, string receivePort, string groupAddress, string discoverPort)
        {
            SendPort = sendPort;
            ReceivePort = receivePort;
            GroupAddress = groupAddress;
            DiscoverPort = discoverPort;
        }

        public string SendPort { get; set; }

        public string ReceivePort { get; set; }
        public string GroupAddress { get; set; }
        public string DiscoverPort { get; set; }
    }
}
