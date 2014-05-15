Care Center
========
A functional client management web application built on top of the SiteBase foundation.

Prerequisites for Building
========
1. .NET 4.5 Runtime
2. Git

To deal with the build warnings, Visual Studio 2012/2013 or the .NET 4.5 SDK (http://msdn.microsoft.com/en-us/windows/desktop/hh852363.aspx) should be installed which will include the reference assemblies for the .NET 4.5 framework.

Build Instructions
========
1. Clone repository. [git clone https://github.com/digitalbeacon/carecenter.git CareCenter]
2. Initialize SiteBase submodule. [git submodule update --init]
3. Create symbolic links. [setup-base.cmd] (This script must be run with administrative privileges.)
4. Build [build.cmd]
5. Create deployment files [publish.cmd]

Prerequisites for Running
========
1. .NET 4.5 Runtime
2. IIS 7
3. SQL Server 2008 or later

Configuration Instructions
========
1. Create database. [Database\init-db.cmd]
2. Initialize database. [Database\reset-db.cmd]
3. Extract or copy deployment files to a new folder on web server.
4. Add new IIS web application with virtual folder "carecenterdemo" pointing to the web site folder.