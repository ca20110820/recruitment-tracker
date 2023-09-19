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
        public DateOnly? StartDate { get; set; }
        public double HourlyWage { get; set; }
        public bool IsAvailable { get; set; }

        public string FullName 
        { 
            get { return  $"{FirstName} {LastName}"; }
        }

        public Contractor(string firstName, string lastName, double hourlyWage, DateOnly? startDate=null, bool isAvailable=false)
        {
            FirstName = firstName;
            LastName = lastName;
            HourlyWage = hourlyWage;
            StartDate = startDate == null ? null : startDate; // Default to null
            IsAvailable = isAvailable; // Default to False
        }
        public Contractor(string firstName, string lastName, double hourlyWage, string? startDate=null, bool isAvailable=false)
        {
            FirstName = firstName;
            LastName = lastName;
            HourlyWage = hourlyWage;
            StartDate = startDate == null ? null : DateOnly.Parse(startDate); // // Default to null; Parse if startDate given as a string
            IsAvailable = isAvailable; // Default to False
        }

    }
}
