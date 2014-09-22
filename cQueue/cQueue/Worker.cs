using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
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
        protected Queue<Frame> queue;

        abstract public void _Send();

        private int checkInterval = new Random().Next(100,200);
        private Timer monitorTimer = new Timer();

        public Worker(Queue<Frame> queue)
        {
            this.queue = queue;
            this.monitorTimer.Interval = this.checkInterval;
            this.monitorTimer.Elapsed += this._RunWorker;
        }

        public void Run()
        {
            this.monitorTimer.Start();
        }

        public void _RunWorker(Object source, ElapsedEventArgs e)
        {
            BackgroundWorker _bw = new BackgroundWorker();
            _bw.DoWork += this._Run;
            _bw.RunWorkerAsync();
        }

        public void _Run(object sender, DoWorkEventArgs e)
        {

            if (this.frame == null)
            {
                if (this.queue.Count > 0)
                {
                    this.frame = queue.Dequeue();
                }
                else
                {
                    Console.WriteLine("No Frames in queue");
                    return;
                }
            }

            this.monitorTimer.Stop();

            if (this.currentRetries > this.maxRetries)
            {
                this.queue.Enqueue(this.frame); //return frame to queue at the beginning
                this.WorkerError();
            }

            try
            {
                this._Send();
                this.frame = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                this.currentRetries++;
            }

            this.monitorTimer.Start();
        }

        protected void _OnWorkerSuccess(int frameIndex){
            this.WorkerSuccess(frameIndex);
        }
    }
}
