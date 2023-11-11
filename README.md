# Vakıfbank Full-Stack Bootcamp - Final Project

This project is a Vakıfbank Full-Stack Bootcamp Final Project within the framework of Asp.Net and Angular with Restful APIs, Database Designs, necessary Services and frontend design.Clean Architecture Design and CQRS design pattern were used to make the project.

## Requirements
- There are 2 roles in the project, 'Admin and 'Bayi'.
- Admin represents the company. CRUD operations for Products, Users are done only by admin.
- Admin cannot have a basket. He cannot add products, shop, create orders.
- The user can add products to the cart from the page where the product prices are listed according to their own profit value, go to the cart and see the products, the total price, and make the payment transaction with the payment method of their choice. There are 4 payment methods as "Card", "Money Order", "Open Account", "EFT".
If the payment is realized, the order status is "Successful". otherwise it is processed as "Failed".
- If the user creates a successful order, transactions are written to the 'Account Transaction', 'Card Transaction', 'Open Account Transaction' tables according to the payment method.
- After this process, the product is added to the company (admin) orders and the company must approve the order.
- If the company approves the order, the order status is updated as 'Confirm', if not, it is updated as 'Cancelled'.
- If the company does not approve, the money is transferred to the paying user and written to the transactions according to this payment method.


### What I have used so far:
- Asp.Net Core Web API `.Net7.0` framework. and front end "Angular"
- EntityFramworkCore as an ORM and Tools packages.
- MSSQL as an Database and packages.
- MediatR library for CQRS design pattern.
- FluentValidation for validating user inputs.
- JWT Bearer Authentication library to generate tokens.
- SeriLog for logging the exceptions.
- AutoMapper for mapping results.
- Postman and Swagger used for tests.

## Installation and Usage

- To get the Backend project :
```
    git clone https://github.com/mhtaldmr/shoppingListProject.git](https://github.com/zorbeyycelikk/FinalProject_PatikaDev.git)
```
- To create the database first, in the `Vk.Data > Migrations` folder :
```c
    cd sln -> dotnet ef database update --project  "./Vk.Data" --startup-project "./VkApi"   -- apply migrations files to database
```
- To start the project,
```c
    dotnet run
```
- "***dotnet restore***" command can be used to restore dependencies and tools inside the project.
-  The project can be directly started in VisualStudio by pressing '***F5***'.
-  The port will be listenin on : https://localhost:5070

-  - To get the Frontend project :
```
      1- Open the project
      2- Open new terminal
      3- run 'npm install'
      4- run 'ng serve -o'
```

## Folder Structure
- Clean Architecutre design principles used in the project structure. 
- ***VkApi*** => => Base project for Backend. It contains *Vk.Api*, *Vk.Base*, *Vk.Data*, *Vk.Operation*, *Vk.Schema*.- ***Vk.Base*** => will be communicating with databases and interface implementations.
- ***Vk.Api*** => => will be responsible for presenting the project to the outside world through APIs and *Middlewares* will be here
- ***Vk.Data*** => It contains the entities needed for the database tables, their creation, the *Generic Repository* needed for the database and the queries in the *UOW* layer.
- ***Vk.Operation*** => This is the layer where the services and queries required for the project's api's take place.
- ***Vk.Schema*** => Contains the *requests* and *responses* entities required for the CQRS implemented in the project.

<img src="https://github.com/zorbeyycelikk/FinalProject_PatikaDev/blob/main/Img/folderStructure.png"/>

## Project Structure

- APIs sending a request. Then the requests are coming to MediatR.
- MediatR routing the requests related command or queries. 
- Command and Queries calling the related service to do the actual job.
- Services calling the repositories which has only access to database.


## Database Design

- To create a list, a category and user must be exist and selected.
- To add items, a list must be exist. Items can have different units.
- For doing all above, a user must have credentials and roles to be able to make actions.
- Relationships can be seen from the picture below.

<img src="https://github.com/zorbeyycelikk/FinalProject_PatikaDev/blob/main/Img/databaseSchema.png" />
