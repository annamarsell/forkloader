using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class ForkKeysValidator
    {
        private List<ForkKey> m_keys;
        private List<Course> m_courses;
        public ForkKeysValidator(List<ForkKey> keys, List<Course> courses)
        {
            m_keys = keys;
            m_courses = courses;
        }

        public bool Validate()
        {
            if (m_keys == null || m_keys.Count == 0 || m_courses == null || m_courses.Count == 0)
            {
                Console.WriteLine("No data to validate.");
                return false;
            }
            List<Leg> legsToComplete = GetLegs(m_keys.First());
            foreach (ForkKey forkKey in m_keys)
            {
                
            }
            return false;
        }

        private List<Leg> GetLegs(ForkKey key)
        {
            var legs = new List<Leg>();
            foreach (string fork in key.Forks)
            {
                int courseId = GetCourseIdFromFork(fork);
                Course course = m_courses[courseId];
                legs.Add(new Leg(course.StartPointId, course.Controls[0].ToString(), true, false));
                for (int i = 1; i < course.Controls.Count; i++)
                {
                    legs.Add(new Leg(course.Controls[i-1].ToString(), course.Controls[i].ToString()));
                }
                legs.Add(new Leg(course.Controls.Last().ToString(), course.FinishId, false, true));
            }
            return legs;
        }

        private bool ContainSameLegs(List<Leg> legs1, List<Leg> legs2)
        {
            List<Leg> tempLegs = new List<Leg>(legs2);
            foreach (Leg l1 in legs1)
            {
                if (tempLegs.Any(l2 => l2.IsEqual(l1)))
                {
                    tempLegs.Remove(legs2.First(l2 => l2.IsEqual(l1)));
                }
                else
                {
                    Console.WriteLine("Could not find leg {0}", l1);
                    return false;
                }
            }
            if (tempLegs.Any())
            {
                foreach (Leg leg in tempLegs)
                {
                    Console.WriteLine("Could not find leg {0}", leg);                    
                }
                return false;
            }
            return true;
        }

        private int GetCourseIdFromFork(string fork)
        {
            throw new NotImplementedException();
        }
    }
}
