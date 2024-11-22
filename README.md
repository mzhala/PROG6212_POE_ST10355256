# Contract Monthly Claim System (CMCS)

The Contract Monthly Claim System (CMCS) enables lecturers to submit their work claims easily, including details like program, module, rate per hour, hours worked, and supporting documents. The system automatically calculates the total claim amount based on the rate per hour and hours worked entered by the lecturer. Managers can then verify, approve, or reject the claims after review, streamlining the claims process. 

Additionally, new features have been added such as an Auto Update Claim button that applies predefined rules to update claims and a database view, Program_Module_Lecture, to assist in determining whether claims should be approved or rejected. HR personnel can also filter and generate reports of approved claims, and edit lecturer details via the HR View, which allows for data management directly from the lecturer_details table.

## Features
1. Submit Claims: Lecturers can submit claims with details such as the program, module, rate per hour, hours worked, and upload supporting documents like registers.
2. Supporting Documents: Lecturers can upload files to support their claims.
3. Claim Status Tracking: Each claim status (Submitted, Approved, Rejected) is clearly displayed for easy tracking.
4. Claim Verification: Management can review claims, view supporting documents, and approve or reject them.
5. Auto Calculation of Total Amount: The total claim amount is automatically calculated based on the rate per hour and the hours worked entered by the lecturer.
6. Manage Claims – Auto Update Claim: A new Auto Update Claim button has been added, which uses predefined rules stored in the database table to automatically update claims.
7. Claim Approval Logic – Program_Module_Lecture View: A database view, Program_Module_Lecture, has been created to determine whether to approve or reject claims submitted by lecturers based on predefined conditions.
8. HR View for Claim Management: A new view, HR View, has two sections:
  - The first part allows HR personnel to filter data and generate a report of approved claims based on specific criteria.
  - The second part displays lecturer data, where HR personnel can edit lecturer details directly from the lecturer_details table.

## Usage
1. Submit Claims (Lecturers):
Select program and module.
Enter rate per hour and hours worked.
The total claim amount will be automatically calculated based on these inputs.
Upload supporting documents and submit claims.
2. Review Claims (Management):
Managers can view, approve, or reject submitted claims.
Review attached supporting documents during the verification process.
Use the Auto Update Claim feature to apply predefined rules for claims updates.
3. Track Claim Status:
View the current status of each claim (Submitted, Approved, Rejected).
4. HR Features:
HR personnel can filter and generate reports of approved claims.
HR personnel can also edit lecturer details from the lecturer_details table through the HR View.

## GitHub Link
https://github.com/mzhala/PROG6212_POE_ST10355256.git

## License
This project is licensed under the MIT License.

## Installation
Clone this repository: https://github.com/mzhala/PROG6212_POE_ST10355256.git
Run the preload (1, 2, and 3) SQL query on your MS SQL Server.
Update the connection string in the Visual Studio project to match your database.
Build and run the project in Visual Studio.

## Credits
This project was created by Halalisile Mzobe ST10355256.
