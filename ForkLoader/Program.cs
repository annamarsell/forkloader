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
            bool verifySourceAndTargetOnly = false;
            int eventClassId;

            Console.WriteLine("Welcome to the fork key loader.");
            if (args.Length == 0)
            {
                Console.WriteLine("This program needs the event class id as command line argument.");
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                return;
            }
            string eventClassIdString = args[0];

            if (string.Equals(eventClassIdString, "-h", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(eventClassIdString, "-help", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(eventClassIdString, "?", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("First argument is event class id (integer)");
                Console.WriteLine("Second argument is options (optional):");
                Console.WriteLine("-s Verify source only, no import.");
                Console.WriteLine("-t Verify target only, no import.");
                Console.WriteLine("-st Verify source and target only, no import.");
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                return;
            }
            try
            {
                eventClassId = Convert.ToInt32(args[0]);
                Console.WriteLine("Working with event class " + eventClassId);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to convert first argument to event class id (integer): " + args[0]);
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                return;
            }
            if (args.Length > 1)
            {
                string option = args[1];
                if (string.Equals(option, "-s", StringComparison.OrdinalIgnoreCase))
                {
                    verifySourceOnly = true;
                    Console.WriteLine("Set to verify the source only.");
                }
                else if (string.Equals(option, "-t", StringComparison.OrdinalIgnoreCase))
                {
                    verifyTargetOnly = true;
                    Console.WriteLine("Set to verify the target only");
                }
                else if (string.Equals(option, "-st", StringComparison.OrdinalIgnoreCase))
                {
                    verifySourceAndTargetOnly = true;
                    Console.WriteLine("Set to verify only (source and target).");
                }
                else
                {
                    Console.WriteLine("Valid options are -s, -t, and -st. Using default option -s");
                    verifySourceOnly = true;
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
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                return;
            }

            var forkKeyReader = new ForkKeyReader(forkKeyFileName, eventClassId);

            string courseDataFileName = string.Empty;
            CourseDataReader courseDataReader;
            try
            {
                courseDataFileName = ConfigurationManager.AppSettings.Get("CourseDataFile");
            }
            catch (Exception)
            {
                Console.WriteLine("Could not find configuration application setting 'CourseDataFile'.");
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                return;
            }
            courseDataReader = new CourseDataReader(courseDataFileName);
       
            Console.WriteLine("Reading fork keys from " + forkKeyFileName);
            List<ForkKey> forkKeys = forkKeyReader.ReadForkKeys();
            Console.WriteLine("Read " + forkKeys.Count + " fork keys.");

            Console.WriteLine("Reading course data from " + courseDataFileName);
            Dictionary<string, Course> courses = courseDataReader.ReadCourses();
            Console.WriteLine("Read " + courses.Count + " courses.");

            OlaWriter writer = null;
            string olaConnectionString = string.Empty;
            if (!verifySourceOnly) // If we want to do anything more than verifying the source we need a connection to the target
            {
                try
                {
                    olaConnectionString = ConfigurationManager.AppSettings.Get("OlaConnection");
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not find configuration application setting 'OlaConnection'.");
                    Console.WriteLine("Press any key to terminate the program.");
                    Console.ReadKey();
                    return;
                }
                writer = new OlaWriter(olaConnectionString, eventClassId);
            }
            if (!verifyTargetOnly)
            {

                var forkKeysValidator = new ForkKeysValidator(forkKeys, courses);
                Console.WriteLine("Validating source.");
                bool forkKeysOK = forkKeysValidator.Validate();

                if (forkKeysOK)
                {
                    Console.WriteLine("Fork keys validated OK.");
                }
                else
                {
                    Console.WriteLine("Fork keys did not validate OK. Fork keys will not be written to the database");
                    Console.WriteLine("Press any key to terminate the program.");
                    Console.ReadKey();
                    return;
                }
                if (!verifySourceOnly && !verifySourceAndTargetOnly)
                {
                    Console.WriteLine("Writing fork keys.");
                    writer.WriteForkKeys(forkKeys);
                    Console.WriteLine("Fork keys written.");
                }
            }
            // Finally, verify the target
            if (!verifySourceOnly)
            {
                Console.WriteLine("Validating target.");
                List<ForkKey> targetForkKeys = writer.ReadForkKeys();
                if (targetForkKeys == null)
                {
                    Console.WriteLine("No fork keys could be read from the database.");
                }
                else
                {
                    bool targetValidationOk = true;
                    foreach (ForkKey targetForkKey in targetForkKeys)
                    {
                        ForkKey forkKey =
                            forkKeys.Single(
                                fk => fk.ClassId == targetForkKey.ClassId && fk.TeamNumber == targetForkKey.TeamNumber);
                        for (int i = 0; i < forkKey.Forks.Count; i++)
                        {
                            if (!string.Equals(forkKey.Forks[i], targetForkKey.Forks[i]))
                            {
                                targetValidationOk = false;
                                Console.WriteLine("Validation of target failed for team " + forkKey.TeamNumber +
                                                  " in class " + forkKey.ClassId + " on leg " + (i + 1).ToString() +
                                                  " fork key was: " + targetForkKey.Forks[i] + ", expected: " + forkKey.Forks[i]);
                            }
                        }
                    }
                    if (targetValidationOk)
                    {
                        Console.WriteLine("Target validation OK.");
                    }
                }
            }
            Console.WriteLine("Press any key to terminate the program.");
            Console.ReadKey();
        }
    }
}
