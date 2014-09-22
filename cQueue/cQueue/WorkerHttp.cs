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

        Timer tmr;

        public WorkerHttp(Session session) : base(session) //use Worker Constructor
        {
        }

        public override void _Send()
        {
            tmr = new Timer();
            tmr.Interval = new Random().Next(500, 2000);
            tmr.Elapsed += this._SendCallback;
            tmr.Start();
        }

        public void _SendCallback(Object source, ElapsedEventArgs e)
        {
            tmr.Stop();
            this._OnWorkerSuccess();
        }
    }
}
