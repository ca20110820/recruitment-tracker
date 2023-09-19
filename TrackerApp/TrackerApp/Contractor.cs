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

        public Contractor(string firstName, string lastName, DateOnly? startDate, double hourlyWage, bool isAvailable=false)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate == null ? null : startDate;
            HourlyWage = hourlyWage;
            IsAvailable = isAvailable; // Default to False
        }
        public Contractor(string firstName, string lastName, string? startDate, double hourlyWage, bool isAvailable=false)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate == null ? null : DateOnly.Parse(startDate); // Parse if startDate given as a string
            HourlyWage = hourlyWage;
            IsAvailable = isAvailable; // Default to False
        }

    }
}
