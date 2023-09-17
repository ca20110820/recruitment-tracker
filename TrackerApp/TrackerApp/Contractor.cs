using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp
{
    public class Contractor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly StartDate { get; set; }
        public double HourlyWage { get; set; }
        public bool IsAvailable { get; set; }

        public Contractor(string FirstName, string LastName, string StartDate, double HourlyWage, bool IsAvailable)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StartDate = DateOnly.Parse(StartDate); // Assume StartDate argument is string
            this.HourlyWage = HourlyWage;
            this.IsAvailable = IsAvailable; // No Assumption
        }
        public Contractor(string FirstName, string LastName, double HourlyWage, bool IsAvailable)
        {
            // Assume: User dont include StartDate, use date now.
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StartDate = new DateOnly(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day); // Date Now
            this.HourlyWage = HourlyWage;
            this.IsAvailable = IsAvailable; // No Assumption
        }
        public Contractor(string FirstName, string LastName, string StartDate, double HourlyWage)
        {
            // Assume: Not Available by default
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StartDate = DateOnly.Parse(StartDate); // Assume StartDate argument is string
            this.HourlyWage = HourlyWage;
            this.IsAvailable = false;
        }
        public Contractor(string FirstName, string LastName, double HourlyWage)
        {
            // Assume: User dont include StartDate, use date now; and Not Available by default
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StartDate = new DateOnly(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day); // Date Now
            this.HourlyWage = HourlyWage;
            this.IsAvailable = false;
        }

    }
}
