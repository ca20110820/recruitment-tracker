using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp
{
    public class RecruitmentSystem
    {
        private List<Contractor> contractors = new List<Contractor>();
        private List<Job> jobs = new List<Job>();

        public List<Contractor> Contractors { get {  return contractors; } }
        public List<Job> Jobs { get {  return jobs; } }

        public void AddContractor(Contractor newContractor)
        {
            // Check if contractor exist in contractors
            bool contractorExists = contractors.Any(contractor => contractor.FirstName == newContractor.FirstName && contractor.LastName == newContractor.LastName);

            // Add the New Contractor if they do not exists in our list of contractors
            if (!contractorExists)
            {
                contractors.Add(newContractor);
            }
        }

    }
}
