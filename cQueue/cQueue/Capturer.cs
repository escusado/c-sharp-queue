using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace cQueue
{
    class Capturer
    {

        public Queue queue;

        private int capInterval = 10;
        private Timer capTimer = new Timer();

        public int currentFrame = 0;

        public void Run()
        {
            this.capTimer.Interval = capInterval;
            this.capTimer.Elapsed += this.Capture;
            this.capTimer.Start();
        }

        public void Capture(Object source, ElapsedEventArgs e)
        {
            lock (this.queue)
            {
                if (currentFrame == 1001)
                {
                    this.capTimer.Stop();
                    this.capTimer.Dispose();
                    Console.WriteLine("\n FINISHED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! \n");
                    return;
                }

                Console.WriteLine("Captured frame: {0}", this.currentFrame);
                this.queue.Enqueue(new Frame(currentFrame));
                currentFrame++;
            }
            
        }
    }
}
