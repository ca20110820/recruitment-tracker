using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp
{
    public class Job
    {

        private Contractor? contractor = null; // Default to null

        public string Title { get; private set; }
        public DateOnly Date { get; set; }
        public double Cost { get; set; }
        public bool Completed { get; set; }

        public Contractor? ContractorAssigned { get {  return contractor; } }

        public Job(string title, string date, double cost, Contractor contractor)
        {
            this.Title = title;
            this.Date = DateOnly.Parse(date);
            this.Cost = cost;
            this.contractor = contractor;
        }
        public Job(string title, string date, double cost)
        {
            // Assume: No Contractor given, null by default from backing field.
            this.Title = title;
            this.Date = DateOnly.Parse(date);
            this.Cost = cost;
        }

        public void AssignContractor(Contractor contractor)
        {
            /* Assign a Contractor to the Job */
            this.contractor = contractor;
        }
    }
}
