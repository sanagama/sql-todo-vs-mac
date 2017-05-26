## What's here? ##
A simple *Todo list* Web API app for [SQL Server](https://www.microsoft.com/en-us/sql) developed using [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/).
The app uses [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/) with [EF Core](https://docs.microsoft.com/en-us/ef/core/) *code-first* to interact with SQL Server.

## 1. Pre-requisites
* Install [.NET Core 1.1](https://www.microsoft.com/net/core#macos) for macOS
* Install [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/)
* Install [SQL Server 2017 in Docker](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup-docker) for macOS

## 2. Get the source code
Copy/paste the commands below into a Terminal window to clone the Git repo:
```
cd ~
git clone https://github.com/sanagama/sql-todo-vs-mac
```

## 3. Build the project
* Launch Visual Studio for Mac
* Open solution **~/sql-todo-vs-mac/sql-todo-vs-mac.sln**
* In *Solution Explorer*, expand the *Dependencies* node, right-click on *Nuget* and click *Restore*
* Update the connection string in the file **appsettings.json** to point to SQL Server 2017 running in Docker
* Right-click the project in the solution window and click **Rebuild**

## 4. Run the app
* From the main menu, click *Run* -> *Start Without Debugging*
* At this point, the Kestrel web server should be running in a new terminal window
* Launch your browser and navigate to the test URLs below to use the Web APIs:
    - Browse to: <http://localhost:50000/api/Persons>
    - Browse to: <http://localhost:50000/api/Persons/1>
    - Browse to: <http://localhost:50000/api/Tasks>
    - Browse to: <http://localhost:50000/api/Tasks/3>
