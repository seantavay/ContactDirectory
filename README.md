# ContactDirectory

To Start the app locally
------------------------
1. In VS ensure the ContactDirectory.API as the startup project by right clicking it > set as startup project
2. Build the entire solution (this should force EF to create the local database)
3. In the search menu open the package manager console
	- run the command update-database to generate the DB if not done already and seed one row of data from Configuration.cs
4. Start-up the BE in VS 
	- Open a command prompt as admin
5. Run the API's
  - Contact controller pulls normalized data from three tables: Contact, ContactInfo, and Address
  - Controller uses Table Controller implementation which allows for Odata usage and offline use
  - Endpoint is /tables/Contacts (note: it is not api/Contacts)
  - Address and ContactInfo controllers are also generated but not needed for the current purpose of project

