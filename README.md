## HH.Meeting

This is an education platform that connects users who wants to learn similar things together. 

Link to project live - [Homeruns](https://www.homeruns.io)

## Technology

The application is built as an monolitic app but with multiple solutions, that makes it practical incase we want to decouple different domains into multiple microservices. 

The targeted application goes in hand with a front-end application that is written with React/Redux. The link is 

## Project Structure

HH.Meeting = Contains Api Controllers

HH.Meeting.Internal = Contains core business logic, 

HH.Meeting.Public = Used as a public library, including api request/response objects, client libraries. Will come in hand in case we split into microservices. 

HH.Meeting.Tests.Integration = Contains tests that involves a whole user interaction flow

HH.Meeting.Tests.Unit = Contains isolated unit tests

## Architecture 

## Dependencies used
- .NET Framework 4.5
- AspNet.WebApi
- AspNet.WebApi.Client
- AspNet.WebApi.Core
- IdentityServer 
- OWIN 
- EntityFramework
- SimpleInjector (DI)
- Serilog
- Azure WebJobs
- Azure ServiceBus
- NewtonsoftJson
- NUnit
- Moq

## Links
- [Homeruns](https://www.homeruns.io)
- [Azure WebJobs - Service Bus](https://docs.microsoft.com/en-us/azure/app-service-web/websites-dotnet-webjobs-sdk-service-bus)
- [Axios](https://github.com/mzabriskie/axios)
- [SuperAgent](https://github.com/visionmedia/superagent)
- [Material-UI](http://www.material-ui.com/)