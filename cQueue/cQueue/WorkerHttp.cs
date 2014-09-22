using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cQueue
{
    class WorkerHttp : Worker
    {

        public WorkerHttp(Queue<Frame> queue) : base(queue) //use Worker Constructor
        {
        }

        public override void _Send()
        {
            //Console.WriteLine("Sending frame {0}...", this.frame.index);
            System.Threading.Thread.Sleep(new Random().Next(100, 5000));
            this._OnWorkerSuccess(this.frame.index);
        }
    }
}
