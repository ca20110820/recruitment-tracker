using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace TrackerApp
{
    public class RecruitmentSystem
    {
        public List<Contractor> contractors = new List<Contractor>();
        public List<Job> jobs = new List<Job>();

        public void AddContractor(Contractor newContractor)
        {
            // Check if contractor exist in contractors
            bool contractorExists = contractors.Any(contractor => contractor.FirstName == newContractor.FirstName && contractor.LastName == newContractor.LastName);

            if (!contractorExists) // Add the New Contractor if they do not exists in our list of contractors
            {
                contractors.Add(newContractor);
            }
            else // Throw exception if contractor already exist in the list
            {
                throw new Exception("This contractor already exist");
            }
        }

        public void RemoveContractor(string firstName, string lastName)
        {
            /* Remove a Contractor based from First and Last Name */

            Contractor? contractor = GetContractor(firstName, lastName); // Assert that its only one person for this project

            if (contractor != null) // Contractor Exist
            {
                // Remove Association with a Job, if applicable
                foreach(Job job in jobs)
                {
                    Contractor? tempContractor = job.ContractorAssigned;
                    if (tempContractor != null && tempContractor.FirstName==firstName && tempContractor.LastName == lastName)
                    {
                        // Set null to job.ContractorAssigned
                        job.DeassignContractor();
                        break;
                    }
                }

                contractors.Remove(contractor);
            }
        }
        public void RemoveContractor(Contractor contractorToRemove)
        {
            contractors.Remove(contractorToRemove);
        }

        public void AddJob(Job job)
        {
            // Check if job exist in jobs
            //bool jobExists = jobs.Any(x => x.Title == job.Title);
            bool jobExists = jobs.Any(x => x.Equals(job));

            if (!jobExists)
            {
                jobs.Add(job);
            }
            else
            {
                throw new Exception("Job already exist");
            }
        }

        public void AssignJob(string jobTitle, string firstName, string lastName)
        {
            // Assigning a Job based from Title, and Contractor's First & Last Names.

            Job? job = GetJob(jobTitle);
            Contractor? contractor = GetContractor(firstName, lastName);

            if (contractor != null && job != null)
            {
                // Overwrite if the Job has an existing Contractor Association
                job.AssignContractor(contractor);
            }
        }
        public void AssignJob(Job job, Contractor contractor)
        {
            // Check if Contractor is not Available
            // Check if Job is already assigned or completed
            if (job.Completed || job.ContractorAssigned == null || !contractor.IsAvailable)
            {
                throw new InvalidOperationException("Cant assign job to contractor");
            }

            job.AssignContractor(contractor);
        }

        public void CompleteJob(string jobTitle)
        {
            Job? job = GetJob(jobTitle);

            if (job != null) // Check if job exists
            {
                job.JobFinished();
            }
        }
        public void CompleteJob(Job job)
        {
            job.JobFinished();
        }

        public List<Contractor> GetAvailableContractors()
        {
            /* Return all Available Contractors */
            return contractors.FindAll(contractor => contractor.IsAvailable);
        }

        public List<Job> GetUnassignedJobs()
        {
            /* Return all the jobs that have not Contractor assigned and Not Complete */
            return jobs.FindAll(job => job.ContractorAssigned is null && !job.Completed);
        }

        public List<Job> GetJobByCost(double minCost, double maxCost)
        {
            if (minCost > maxCost || minCost < 0 || maxCost < 0)
                throw new ArgumentOutOfRangeException("minCost or maxCost is out-of-range or invalid!");

            return jobs.FindAll(job => minCost <= job.Cost && job.Cost <= maxCost);
        }

        private Contractor? GetContractor(string firstName, string lastName)
        {
            /* Get Contractor based from First and Last Name, return null if Contractor does not exist */
            return contractors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

        private Job? GetJob(string title)
        {
            /* Get Job based from Title, return null if job does not exist */
            return jobs.FirstOrDefault(x => x.Title.Contains(title));
        }

    }
}
