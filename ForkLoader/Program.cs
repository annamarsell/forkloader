using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            bool verifySourceOnly = false;
            bool verifyTargetOnly = false;

            if (args.Length > 0)
            {
                string option = args[0];
                if (string.Equals(option, "-h", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(option, "-help", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(option, "?", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("-s Verify source only, no import.");
                    Console.WriteLine("-t Verify target only, no import.");
                }
                if (string.Equals(option, "-s", StringComparison.OrdinalIgnoreCase))
                {
                    verifySourceOnly = true;
                }
                if (string.Equals(option, "-t", StringComparison.OrdinalIgnoreCase))
                {
                    verifyTargetOnly = true;
                }
            }

            string forkKeyFileName = string.Empty;
            try
            {
                forkKeyFileName = ConfigurationManager.AppSettings.Get("ForkKeyFile");
            }
            catch (Exception)
            {
                Console.WriteLine("Could not find configuration application setting 'ForkKeyFile'.");
                throw;
            }
            var forkKeyReader = new ForkKeyReader(forkKeyFileName);

            string courseDataFileName = string.Empty;
            CourseDataReader courseDataReader;
            try
            {
                courseDataFileName = ConfigurationManager.AppSettings.Get("CourseDataFile");
            }
            catch (Exception)
            {
                Console.WriteLine("Could not find configuration application setting 'CourseDataFile'.");
                throw;
            }
            courseDataReader = new CourseDataReader(courseDataFileName);

            List<ForkKey> forkKeys = forkKeyReader.ReadForkKeys();
            Dictionary<string, Course> corses = courseDataReader.ReadCourses();


            Console.ReadKey();
        }
    }
}
