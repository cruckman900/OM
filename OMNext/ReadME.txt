Operation Montserrat - think Ron will need to set up a new server again in IIS.  I remember this being flakey in IIS vs localmachine,
If I figure it out or someone else does.. using this similar set... please keep a record of documents that were used when building your version.
Otherwise, in a virtual server, IIS has to be put on and the app that your creating will have to be the topmost node.  I couldn't figure out how
to get it to work as a subdomain.

I used EF Core for MVC in this application.. Razor is also available.  Razor is better for front end I believe...
but MVC is better to understand as a back-end. It will fit in later on if you guys are still running these down the road or 
want to try something different.  As for Razor... it was replaced also.. so I would probably stick with the Html.
https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1

This is going to be the tricky one... read through the manual before making this from scratch... and I don't know how many files and folders are involved
but its a mess.
Bare bones, you need a Hub that acts as the server, and client-side JavaScript.  I don't know a very easy way to navigate this project... worst case scenario,
if you are going to be working on this and get stuck.. see if you can get my email address from Challenger and send me a video meeting request.. I will see if I can try
to help.
https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/tutorial-getting-started-with-signalr

DataBase and Variable Names that may be tricky to keep straight:

DataDrive and DataDriveData
DataDrive refers to values that are being pulled in from XML and passed along from the Flight Director to the teams.
DataDriveData refers to values being sent back to the Flight Director as a response to the data received earlier.

MissionID and Group both refer to the MissionID.  They are used mostly in chat.js and ChatHub.cs

Where to find things
wwwroot\App_data:		 contains all of the files for the DataDrive and static html text for Archived Data, Post Brief, etc on the team interfaces
wwwroot\assets\images:	 favicons, and the images used for the maps for Evac team
wwwroot\assets\css:		 the application's css file
wwwroot\images:			 not sure why these are in a different folder anymore, but logo, the background image and Diagram for the MedComm team
Controllers:			 contains most of the logic for the interfaces
Data:					 DBContext and a small seed file for testing
Helpers:				 Extensions and other Helpers
Hubs:					 The hub is the SignalR implementation that allows 2 way data request/responses between server and client
Migrations:				 Entity Framework database files
Models:					 classes that contain the data structures for the different database tables needed
ViewComponents:			 invoke components used in the UI
Views:					 UI files