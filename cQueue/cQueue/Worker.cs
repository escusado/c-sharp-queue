using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace cQueue
{

    public delegate void SuccessHandler(int frameIndex);
    public delegate void ErrorHandler();

    public abstract class Worker
    {
        public event SuccessHandler WorkerSuccess;
        public event ErrorHandler WorkerError;

        private int maxRetries = 15;
        private int currentRetries = 0;

        protected Frame frame = null;
        public Session session;

        abstract public void _Send();

        private int checkInterval = 100;
        private Timer monitorTimer = new Timer();

        public Worker(Session session)
        {
            this.session = session;
            this.monitorTimer.Interval = this.checkInterval;
            this.monitorTimer.Elapsed += this._Run;
        }

        public void Run()
        {
            this.monitorTimer.Start();
        }

        public void _Run(Object source, ElapsedEventArgs e)
        {
            
            if (this.frame == null)
            {
                this.frame = this.session.getNext();
            }

            if (this.frame == null)
            {
                return;
            }

            this.monitorTimer.Stop();

            if (this.currentRetries > this.maxRetries)
            {
                this.session.Requeue(this.frame);
                this.frame = null;
                this.WorkerError();
                return;
            }

            this._Send();
        }

        protected void _OnWorkerSuccess(){
            this.WorkerSuccess(this.frame.index);
            this.frame = null;
            this.monitorTimer.Start();
        }
    }
}
