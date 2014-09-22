using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace cQueue
{
    class WorkerHttp : Worker
    {
        public WorkerHttp(Session session) : base(session) //use Worker Constructor
        {
        }

        public override void _Send()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            byte[] response = client.UploadData("http://192.168.2.1:9393/id0/screenshots/" + this.frame.index, "POST", Encoding.Default.GetBytes("{\"frame\": " + this.frame.index + "}"));
            string s = Encoding.ASCII.GetString(response);
            this._OnWorkerSuccess();
        }

    }
}
