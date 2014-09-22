using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cQueue
{
    class Session
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
        private Queue<Frame> queue = new Queue<Frame>();
        private List<Worker> workers = new List<Worker>();

        private int _numberOfWorkers = 10;

        private List<int> _sentFrames = new List<int>();

        public void Run()
        {
            Worker tmpWorker;

            for (var i = 0; i < this._numberOfWorkers; i+=1)
            {
                tmpWorker = new WorkerHttp(this.queue);
                tmpWorker.WorkerSuccess += this.FrameSent;
                tmpWorker.WorkerError += this.UploadFailed;
                tmpWorker.Run();
                workers.Add(tmpWorker);
            }
            
            //dummy capture sequence
            dummyCapturer.queue = this.queue;
            dummyCapturer.Run();
        }

        public void FinalizeSession()
        {
            //follow same finalize strategy
        }

        public void FrameSent(int frameIndex)
        {
            //save sent frame for testing
            this._sentFrames.Add(frameIndex);

            //notify UI eventually
            Console.WriteLine("Frame Sent: {0}, Total: {1}", frameIndex, this._sentFrames.Count);
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

    }
}
