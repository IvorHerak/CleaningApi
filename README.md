# NOUS Coding challenge
*As you continue reading the requirements of this assessment, we like to point out that we expect you to treat it like a real world task. You shall design your API, code, commits, tests, comments and database like you'd be doing in your job.*

## Introduction
Our SaaS-Solution aims to automate the way cleaning processes are organized. 
We build service oriented REST APIs, which allow customers to create cleaning plans.

## Summary
In summary you shall design and write a REST API, which has at least following endpoints:
* An Endpoint for creating a cleaning plan for a certain customer
* An Endpoint that lists all cleaning plans of a certain customer
* An Endpoint that provides a single cleaning plan by its cleaning plan id.
* An Endpoint that deletes a cleaning plan using its id
---
## Requirements & Project Setup
You should implement your solution using:
* asp.net core 5.0, 
* entityframework core 5.0 (with an in-memory database)
This repository already provides a project that is preconfigured with setup that can be used. 


### REST Endpoints, asp.net core controller
You have to figure out which http methods to use, how to define their routes and how to retrieve/store the data using ef core. 
There's an empty controller in the project you can use to add your endpoints. You can find it in `CleaningManagementApi\CleaningManagement.Api\Controller\CleaningManagementController.cs`

**An endpoint must always validate the provided data and respond with adequate http status codes in case of success or error.**

### Database, DbContext, ef core
You can reuse the DbContext in `CleaningManagement.DAL\CleaningManagementDbContext.cs` that uses an in memory database, but you're allowed to use a different setup if prefered.

---
## Cleaning Plan Model
A cleaning plan consists of following attributes and constraints:
* ID (=Guid): __non-nullable__ and __auto-generated__
* Title: __non-nullable__ and a __max. length of 256 characters__
* Customer Identification Number (=`CustomerId`):  __non-nullable__ integer
* Creation Date: __non-nullable__ and __auto-generated__, which identifies the datetime of the created plan.
* Description: __optional__  with a max. length of 512 characters.

## Endpoint: Create Cleaning Plan
Provide an endpoint that allows a client to create a cleaning plan. Don't forget to take all model constraints into account, e.g. title length. Please validate any request accordingly.

We expect you to store the cleaning plan in the provided in-memory database using ef core.

Here's an example for **request** body:
```
{ 
   "title": "Hotel Room Cleaning, double bed",
   "customerId": 123223,
   "description": "This plan is meant to be used for double bed rooms."
}
```

Example response body:
```
{
   "id": "8c442581-c67a-41e5-8d2d-b1176de31087"
   "title": "Hotel Room Cleaning, double bed",
   "customerId": 123223,
   "description": "This plan is meant to be used for double bed rooms.",
   "createdAt" : "2022-01-01T23:00:00Z"
}
```

### Endpoint: List Cleaning Plans
This endpoint should provide a list of all cleaningplans using a provided `customerId`. 

An empty response body should be provided, if a `customerId` is being used, that does not exist.

Example response body:
```
[
  {
    "id": "8c442581-c67a-41e5-8d2d-b1176de31087"
    "title": "Hotel Room Cleaning, double bed",
    "customerId": 123223,
    "description": "This plan is meant to be used for double bed rooms.",
    "createdAt" : "2022-01-01T23:00:00Z"
  },
  {
    "id": "8c442581-c67a-41e5-8d2d-b1176de31087"
    "title": "Mall Cleaning, inner city",
    "customerId": 123223,
    "description": "Suitable only for malls smaller than 23000 m².",
    "createdAt" : "2022-01-02T23:12:22Z"
  }, 
  ...
]
```

### Endpoint: Update Cleaning Plan information using its id
This endpoint should allow a client to update a cleaning plan using its id. A client should be able to update the cleaning plan fields. 

Example **request** body:
```
{ 
   "title": "Hotel Room Cleaning, double bed",
   "customerId": 123223,
   "description": "This plan is meant to be used for double bed rooms."
}
```

### Endpoint: Delete Cleaning Plan by id
This endpoint should allow a client to delete a cleaning plan using its id.

Example response body:
```
[
  {
    "id": "8c442581-c67a-41e5-8d2d-b1176de31087"
    "title": "Hotel Room Cleaning, double bed",
    "customerId": 123223,
    "description": "This plan is meant to be used for double bed rooms.",
    "createdAt" : "2022-01-01T23:00:00Z"
  },
  {
    "id": "8c442581-c67a-41e5-8d2d-b1176de31087"
    "title": "Mall Cleaning, inner city",
    "customerId": 123223,
    "description": "Suitable only for malls smaller than 23000 m².",
    "createdAt" : "2022-01-02T23:12:22Z"
  }, 
  ...
]
```