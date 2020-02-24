Application built using Visual Studio Code and Visual Studio 2019

Required Node.js, Angular 8, .NET Core 3.1 using WebAPI and EntityFramework, localdb

Solution is structured to have a Domain for the entities, Persistance for access to the database using EntityFramework and Phonebook.API as a WebAPI with a controller that receives get/post/put/delete requests. A database solution is included for the structure of the database.

The Angular Client is in angularClient. This has a home component which shows phonebooks, a phonebookdetails component which shows which entries are in the phonebook, a phonebookentrydetails component which shows and allows editing of the specific entry. 
There are two services, phonebookservice and phonebookentryservice used to call the API.

Solution also includes unit tests:
-PhoenbookAPIFixture - Tests which call the API
-PersistanceFixture - Tests which call the Database (will not work unless database schema published to local db)

To run:

1. Inside Database\localdb attach the .mdf file to your localdb server.
2. Inside the Release folder modify the appsettings.json to use the connection string to the database on your machine, make sure \ characters are escaped e.g. Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Source\\AbsaPhoneBook\\Database\\localdb\\MSSQLLocalDBAbsaPhonebook_Primary.mdf
3. Run StartServer.bat
4. Run StartAngularClient.bat
5. Browse to http://localhost:4224/