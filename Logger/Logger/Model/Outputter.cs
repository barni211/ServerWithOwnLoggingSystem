using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface Outputter
    {
        void WriteLog(Level lvl, string value);
        Outputter GetInstance();
        OutputType GetOutputType();
    }
}
