# ASP.NET MVC Core Boilerplate

This project shows a complete architecture for a highly scalable and flexible enterprise ready solution. It's made completely on Linux using Microsoft's ASP.NET Core and EntityFramework Core.

I used MS SQL Server 2017 for Linux to have a database backend ready to go.

> **Tested with .NET Core Version 2.1.4 as available in Feb 2017**

## Features

It's a multitier architecture with a Service Layer. The goal is to have a division between a Web Front End (WFE) server, which can scal out to match the requests and a backend or Application Server (APP) that scales for computing power and database requests. The APP is also able to provide additional features as micro service, easily scalable through Docker containers.

The Service Layer has an OpenAPI (formerly known as Swagger) interface. It exposes the services meta data. The WFE makes use of AutoREST to create a proxy for the service. 

## Setup

To setup you need:

* .NET Core 2.0 (or newer)
* MS SQL Server 2017 for Linux
* NodeJS 8 LTS and NPM 5.5 (or newer)

> I recommend using Visual Studio Code, which runs very well on Ubuntu 16.

Because the server infrastructure is divided into two parts, you run two setups:

1. ServiceLayer
2. WebFrontEnd

The service layer is pure .NET:

~~~
$ dotnet restore
$ dotnet build
~~~

The setup the database in project **UI-Tools/DatabaseSetup**. It's a console that creates the database and adds some demo data.

Once done start the service layer. It's needed to expose the OpenAPI definition as JSON.

~~~
$ cd ServiceLayer
$ dotnet run
~~~

Go to the folder of the WebFrontEnd project.

~~~
$ npm install
$ npm run build
~~~

It's required to use NodeJS here because of the AutoREST lib. The npm task calls the required .NET cli and invokes the C#-compiler.