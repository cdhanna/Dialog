using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogTool
{
    class ProgramOptions
    {
        public int WebsocketPort { get; set; } = 5555;
        public int HttpPort { get; set; } = 5500;
        public string ServerPath { get; set; } = "..\\..\\web";
    }
}
