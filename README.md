# DemoApi

How to run:
Open DemoAPI.sln and run DemoApi and DemoUI projects.

In DemoApi edit appsettings.json and update database string
I have uploaded in the repository the demo database demo-app.bacpac, but it can also be created with test data through the migrations files i have added in the Repository project
Note: in order to run the migration scrips you need to update the connection string in Repository-> DemoContext also
The Backend url needs to be updated in appsettings.json and appsettings.Development.json under DemoUI in order for the frontend to reach the backend

I have created 2 dummy accounts, one is admin and can access the Companies page one is user and can only access main menu
I have also added the nswagprofile.config file that I used to generate the client data in the frontend based on the api endpoints
