using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger
    {
        private Level outputLevel;
        private Dictionary<OutputType, Outputter> instanceMap;

        public Logger()
        {
            instanceMap = new Dictionary<OutputType, Outputter>();
        }
        public void WriteLog(Level lvl, string value)
        {
            if((int)outputLevel >= (int)lvl)
            {
                foreach(Outputter output in instanceMap.Values)
                {
                    output.WriteLog(lvl, value);
                }
            }
        }

        public void SetLoggerOutput(Outputter output)
        {
            if (CheckOutput(output)==false)
            {
                instanceMap.Add(output.GetOutputType(), output.GetInstance());
            }
        }

        public void SetLoggerLevel(Level lvl)
        {
            this.outputLevel = lvl;
        }

        private bool CheckOutput(Outputter check)
        {
            foreach (Outputter output in instanceMap.Values)
            {
                if(check.GetOutputType() == output.GetOutputType())
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteLoggerOutput(Outputter output)
        {
            if(CheckOutput(output)==true)
            {
                instanceMap.Remove(output.GetOutputType());
            }
        }

    }
}
