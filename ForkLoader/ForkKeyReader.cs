using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ForkLoader
{
    public class ForkKeyReader
    {
        private readonly string m_filename;
        private static readonly ILog m_loggger = LogManager.GetLogger(typeof (ForkKeyReader));

        public ForkKeyReader(string filename)
        {
            m_filename = filename;
        }

        public List<ForkKey> ReadForkKeys()
        {
            var forkKeys = new List<ForkKey>();
            using(var sr = new StreamReader(m_filename))
            {
                string forkKeyString;
                while (!string.IsNullOrEmpty(forkKeyString = sr.ReadLine()))
                {
                    ForkKey forkKey = new ForkKey();
                    string[] parts = forkKeyString.Split(new char[] {';'});
                    if (!string.IsNullOrEmpty(parts[0]))
                    {
                        try
                        {
                            forkKey.TeamNumber = Convert.ToInt32(parts[0]);
                        }
                        catch (Exception)
                        {
                            if (m_loggger.IsErrorEnabled)
                                m_loggger.ErrorFormat("{0} does not begin with a valid team number.", forkKeyString);
                            throw;
                        }
                        forkKey.Forks = new List<string>();
                        for (int i = 1; i < parts.Length; i++)
                        {
                            forkKey.Forks.Add(parts[i]);
                        }
                        forkKeys.Add(forkKey);
                    }
                }
            }
            return forkKeys;
        }
    }
}
