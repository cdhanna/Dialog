using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace DialogTool
{
    class StateService : WebSocketBehavior
    {

        protected override void OnOpen()
        {
            
            Console.WriteLine("Got Connection From Origin " + Context.Origin);

            for (int i = 0; i < 50000; i++)
            {
                Send(JsonConvert.SerializeObject(new
                {
                    topic = "facts",
                    facts = new object[]
                    {
                        new
                        {
                            key = "k." + i%15,
                            value = "a" + i

                        }
                    },
                }));
                Thread.Sleep(10);
            }

            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var json = e.Data;
            //Console.WriteLine("Got message " + json);
            var msg = JsonConvert.DeserializeObject<Message>(e.Data);

            base.OnMessage(e);
        }


        class Message
        {
            [JsonProperty("topic")]
            public string Topic { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }
    }
}
