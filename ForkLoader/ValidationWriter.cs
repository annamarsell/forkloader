using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class ValidationWriter
    {
        private List<ForkKey> m_forkKeys; 
        public ValidationWriter(List<ForkKey> forkKeys)
        {
            m_forkKeys = forkKeys;
        }

        public void Write(string fileNameAndPath)
        {
            using(var streamWriter = new StreamWriter(fileNameAndPath))
            {
                foreach (ForkKey forkKey in m_forkKeys)
                {
                    for (int i=0; i<forkKey.Forks.Count; i++)
                    {                  
                        streamWriter.WriteLine(forkKey.TeamNumber + ", " + (i+1).ToString() + ", " + "1, " + forkKey.Forks[i]);
                        if (forkKey.TeamNumber > 1000 && i == 1)
                        {
                            streamWriter.WriteLine(forkKey.TeamNumber + ", " + (i + 1).ToString() + ", " + "2, " + forkKey.Forks[i]);                        
                        }
                    }
                }
            }
        }
    }
}
