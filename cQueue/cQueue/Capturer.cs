using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using System.ComponentModel;using System.Threading.Tasks;

namespace cQueue
{
    class Capturer
    {

        public Queue<Frame> queue;

        private int capInterval = 10;
        private Timer capTimer = new Timer();

        private int currentFrame = 0;

        public void Run()
        {
            this.capTimer.Interval = capInterval;
            this.capTimer.Elapsed += this.RunCaptureWorker;
            this.capTimer.Start();
        }

        public void RunCaptureWorker(Object source, ElapsedEventArgs e)
        {
            BackgroundWorker _bw = new BackgroundWorker();
            _bw.DoWork += this.Capture;
            _bw.RunWorkerAsync();
        }

        public void Capture(object sender, DoWorkEventArgs e)
        {
            if (currentFrame == 101)
            {
                this.capTimer.Stop();
                Console.WriteLine("\n FINISHED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! \n");
                return;
            }
            Console.WriteLine("Captured frame: {0} at: {1}", this.currentFrame, DateTime.Now.Ticks);
            queue.Enqueue(new Frame(currentFrame));
            currentFrame++;
        }
    }
}
