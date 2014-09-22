using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cQueue
{
    public class Session
    {
        /*private string appId = "dummy-app-id";
        private string callback = "dummy-callback";
        private string encode = "dummy-encode";
        private int capturedFrames = 0;
        private int fps = 4;
        private string quality = "l";
        private int sentFrames = 0;
        private string sessionId = "dummy-session-id";*/

        private Capturer dummyCapturer = new Capturer();
        private Queue queue;

        private int _numberOfWorkers = 42;

        private int framesSentCounter = 0;
        private object successLock = new Object();

        public Session()
        {
            Queue queue = new Queue();
            this.queue = Queue.Synchronized(queue);
        }

        public void Run()
        {

            //dummy capture sequence
            dummyCapturer.queue = this.queue;
            dummyCapturer.Run();

            Worker tmpWorker;
            for (var i = 0; i < this._numberOfWorkers; i+=1)
            {
                tmpWorker = new WorkerHttp(this);
                tmpWorker.WorkerSuccess += this.FrameSent;
                tmpWorker.WorkerError += this.UploadFailed;
                tmpWorker.Run();
            }
           
        }

        public void FinalizeSession()
        {
            //follow same finalize strategy
        }

        public void FrameSent(int frameIndex)
        {
            lock (successLock)
            {
                Console.WriteLine("Frame Sent: {0}, Total: {1}", frameIndex, this.framesSentCounter);
                this.framesSentCounter++;
                //notify progress to UI
            }
            
        }

        public void UploadFailed()
        {
            //notify error
        }

        public void Dispose()
        {
            //cleanly dispose
            //queue
            //workers
            //each Worker
        }

        public Frame getNext()
        {
            lock (this.queue)
            {
                if (this.queue.Count > 0)
                {
                    return (Frame)this.queue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        public void Requeue(object frame)
        {
            lock (this.queue)
            {
                this.queue.Enqueue(frame);
            }
        }
    }
}
