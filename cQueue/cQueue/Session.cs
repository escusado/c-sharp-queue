using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cQueue
{
    class Session
    {
        private string appId = "dummy-app-id";
        private string callback = "dummy-callback";
        private string encode = "dummy-encode";
        private int capturedFrames = 0;
        private int fps = 4;
        private string quality = "l";
        private int sentFrames = 0;
        private string sessionId = "dummy-session-id";

        private Capturer capturer = new Capturer();
        
        //private int[] queue = new int[];
        //private Worker[] workers = new Worker[];

        public void Dispose()
        {
            //cleanly dispose
            //queue
            //workers
                //each Worker
        }

        public void Finalize()
        {
            //follow same finalize strategy
        }

        public void FrameSent(int frameIndex)
        {
            //notify UI eventually
            //save sent frame for testing
        }

        public void UploadFailed()
        {
            //notify error
        }

        public void Run()
        {
            //instanciate and initialize workers
        }

    }
}
