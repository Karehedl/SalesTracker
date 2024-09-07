# SalesTracker
<img src="https://github.com/user-attachments/assets/3453c12c-0bbc-49a8-b46c-4fd93aa8268b" width="900px">

Created by Katelyn Hedlund, Charlie Lipperd, Nelson Fant, and Adam Lair using ASP.NET Core 7 API with MS SQL Server database.

## About the App
Sales Tracker is a web app for users to view and manage their business' transactions. This includes allowing a user to...
 - Create, read, update, and delete (CRUD) customers
 - Perform CRUD with orders from a specific customer
 - Perform CRUD on the transactions from the orders of customers
 - List product types
 - List items

## Running the App
The following assumes you are on Windows:

First, clone the repository. Enter "dotnet run --project .\SalesTracker.WebAPI\" and run it from the terminal while in the root directory. Because this is currently the completed backend only, click the link to the "localhost" shown after running the command, add "/swagger" to the url, and press enter. You should now be able to test the backend functionality and see all endpoints working.

## Known Issues
 - No front end
 - Amounts of products sold in some transactions are not being accurately represented

## Technologies and Features
 - C#
 - ASP.Net Core 7
 - MS SQL
