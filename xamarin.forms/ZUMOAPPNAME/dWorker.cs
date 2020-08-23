using System;
using System.Collections.Generic;
using System.Text;


    public class dWorker
    {
        public string tag, job, name;
        public int cnt, time;

        public dWorker()
        {

        }

        public dWorker(string name, string tag, string job, int cnt, int time)
        {
            this.name = name;
            this.tag = tag;
            this.job = job;
            this.cnt = cnt;
            this.time = time;
        }
    }
public class WholeWorker
{
    public string WhGate { get; set; }
    public string WhIn { get; set; }
    public string WhOut { get; set; }
    public string Whleft { get; set; }
}
public class Worker
{
    public string WGate { get; set; }
    public string WIn { get; set; }
    public string WOut { get; set; }
    public string Wleft { get; set; }
    
}

