# DDD-Workshop
DDD Workshop - 05/30/2015 - Iteration zero

This is `iteration zero` of a solution that will be used during the DDD-Workshop. In this solution we are concentrating on the back-end and specifically on the domain thus the solution does not contain a UI. The client to test our application will be Postman (REST client). The solution makes use of 

- SQL Server Express as a data store, 
- NHibernate as the ORM, 
- Fluent NHibernate for the mapping
- StructureMap as the IoC container
- ASP.NET Web API
- Log4Net for logging


##Prerequisites
To successfully run the `Iteration zero` project we assume that your system is prepared and contains the following

- Windows 7 or higher
- Microsoft SQL Server Express 2012 or equivalent
- Visual Studio 2013 Professional
- SQL Server Management Studio
- Git - e.g. msysgit, GitHub client for Windows or Source Tree 
- Google Chrome
- Postman REST client for Chrome (or equivalent)

##How to get started
Clone the repository from GitHub [https://github.com/gnschenker/DDD-Workshop](https://github.com/gnschenker/DDD-Workshop) to your local computer. 

Open a Powershell console and navigate to the folder to which you cloned the repository. To build the solution and run all tests we are using a Powershell build file that uses [Psake](https://github.com/psake/psake). 

Run the command `.\psake\psake.ps1 .\build.ps1 Test`

##DDD - Implementation
In this sample we use the following implementation

![Domain](https://github.com/gnschenker/DDD-Workshop/blob/master/images/Domain.PNG)

Requests (or commands) flow through the `controller` which delegates them to the `application service`. The application service is the host for the `aggregate` and uses a `repository` to (re-) hydrate the aggregate from the data store and save the changes back to the data store. The aggregate contains all the business logic. The `state` of the aggregate is private to the aggregate and represents the entity that is persisted in the DB using the repository and NHibernate. Thus the `State` is the NHibernate entity. 

##Sample code
Iteration zero contains a SamplesController, SamplesApplicationService, SamplesAggregate and Sample (=state) as a template and to show how things are fitting together.

Try to run the application and use Postman to execute the following 2 GET requests

    localhost:4000/api/samples [GET]
    localhost:4000/api/samples/<sampleId> [GET]

Now also try some POST requests. To start a sample use this URL

    localhost:4000/api/samples/start [POST]
define a header

    content/type  application/json
and a (raw) body

    { name: "Some sample 1" }


and then the next step

    localhost:4000/api/samples/<sampleId>/next [POST]

and a (raw) body

    { someDate: "2015-05-21" }
