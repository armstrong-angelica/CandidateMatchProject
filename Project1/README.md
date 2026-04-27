📌 Job Matching System
🧠 Overview

This project simulates a simplified recruitment system from the perspective of a recruiter or hiring manager.

In many real-world hiring processes, candidates who are not selected for a role are still retained in a company’s internal system for potential future opportunities. This application models that workflow by:

Allowing recruiters to create and manage job postings
Automatically matching previously rejected/existing candidates to new jobs
Enabling recruiters to review matched candidates and make hiring decisions

For simplicity, the application skips detailed steps like applications and interviews and focuses on the core decision-making process:

Match → Review → Hire or Reject

Once a candidate is hired:

They are no longer available for other roles, status marked as hired
The job posting is marked as closed
___________________________________________________________________________________
👤 Core Entities

-Recruiter
Represents internal employees responsible for hiring.

Attributes:
Id
Name

Responsibilities:
Create job postings
View all jobs they’ve created (open/closed)
Update or delete job postings
Review matched candidates
Select candidates to hire

-Job
Represents a job posting created by a recruiter.

Attributes:
Id
Title
RequiredEducation
Status (Opened / Closed)
RecruiterId

Behavior:
Automatically matches candidates upon creation
Update status to Closed when a candidate is hired

-Candidate
Represents individuals in the hiring pool.

Attributes:
Id
Name
HighestEducation
Status (Active / Hired)

Behavior:
Can be matched to multiple jobs
Once hired, becomes unavailable for other positions, status update to Hired

-JobCandidateMatch (Junction Table)
Represents the relationship between Jobs and Candidates.

Attributes:
JobId
CandidateId
Status (Matched / Hired)

Purpose:
Enables many-to-many relationship between Jobs and Candidates
___________________________________________________________________________________

🔗 Relationships
Recruiter → Job
One-to-Many (A recruiter can create multiple jobs)
Job ↔ Candidate
Many-to-Many (via JobCandidateMatch)
Job → Hired Candidate
One-to-One (A job can have one hired candidate)
___________________________________________________________________________________

🔄 User Flow
1. Login
User enters Recruiter ID
If valid → proceed to dashboard
If invalid → retry or exit

2. Dashboard
Options:
View existing job postings
Create a new job

3. Create Job
Enter job details
System automatically matches candidates

4. View Job Postings
See all jobs (open/closed)
Select a job to view matched candidates


5. Hiring Decision
Choose a candidate to hire via Id
Candidate status → Hired
Job status → Closed
___________________________________________________________________________________
⚙️ Functionality
-Recruiter (Admin/Test Support)
Create
Read

-Job
Create (auto-match candidates)
Read (all jobs, recruiter-specific, id-specific, filter by status)
Delete (ensure stability)
Match candidates 
Hire candidate (Updates job + candidate status)

-Candidate (Admin/Test Support)
Create
Read (all, id-specific, filter by status or sort by education level)
Update (education,status(not needed))

___________________________________________________________________________________
🧩 Notes
Matching is based primarily on education level compatibility

____________________________________________________________________________________
🚫 Out of Scope

To keep the project focused, the following are intentionally excluded:

Application and interview workflows
Authentication (login simplified using Recruiter ID)
Skill Requirement-based matching
Location-based matching
Concurrency handling (e.g., multiple recruiters hiring same candidate)
Automatic archival/deletion of old job postings