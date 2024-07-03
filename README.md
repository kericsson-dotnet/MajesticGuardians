# Lexicon Learning Management System (LMS)

## Description
The task is to develop a Learning Management System (LMS) for Lexicon's advanced courses. The system will centralize and simplify communication between teachers and students by putting schedules, course materials, information, assignments, and submissions all in one place. The project will involve full-stack development using Blazor Web App, Entity Framework Core, and Bootstrap 5. The system will manage users, courses, modules, activities, and documents.

## Video Demonstration (Coming soon)
[Link](https://www.youtube.com/watch?v=lo9FIRZQRGI)

### ER Diagram and Assignment description
[Entityframework Relationship](https://drive.google.com/file/d/1zR73l3ImWsBmax1V0GBj2C04I6gqgPFj/view?usp=sharing)
[Assignment_description](https://github.com/user-attachments/files/15871981/Projektdokument_.Lexicon.LMS.pdf)

## C# (.NET) Installation Guide
We used the programming language C# (.NET)

### Step 1: Download C# (.NET)
First download latest version of [.NET](https://dotnet.microsoft.com/en-us/download) to install.

### Step 2: Verify Installation
Open a new command prompt (or terminal) and type:

```bash
dotnet --version
```
This should display .NET's version, indicating a successful installation.

## Usage
The application API will run on port 7233 by default. If another port would be desired, this can be set in the application.properties file.

## Testing with Postman
Postman is a popular tool for testing APIs. It allows you to send requests to your API and view responses in a user-friendly interface.

### Step 1: Install Postman
Download and install Postman from [Postman's official website](https://www.postman.com/).

### Step 2: Send Requests

With Postman open, you can send requests to the LexiconLMS API. For example, to retrieve all users on the platform, set the request type to "GET" and enter the endpoint URL: `http://localhost:7233/api/users`.

You can also add headers, body data, and other request parameters as needed for different endpoints.

### Step 3: Lagalt Endpoints
Further down you can find all our endpoints for the application that you can use

## Database
- A relational database management system spporting SQL Server Management Studio (SSMS) [More Info](https://learn.microsoft.com/en-us/sql/ssms/sql-server-management-studio-ssms?view=sql-server-ver16)
- We used the tool [MSSQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), which allowed us to view relationships, tables, and values within the tables.


In the application to configure to the database, you need to use: 
```
update-database

```

### API Endpoints
| HTTP Verbs | Endpoints | Action |
| --------- | --------- | --------- |
| **Users** | | |
| POST | /api/users | To create a new user |
| GET | /api/users | To retrieve all users on the platform |
| GET | /api/users/:userId | To retrieve details of a single user |
| PATCH | /api/users/:userId | To edit/update  the details of a single user |
| DELETE | /api/users/:userId | To delete a single user |
| **Documents** | | |
| POST | /api/documents | To create a new document |
| GET | /api/documents | To retrieve all documents on the platform |
| GET | /api/documents/:documentId | To retrieve details of a single document |
| PATCH | /api/documents/:documentId | To edit/update  the details of a single document |
| DELETE | /api/documents/:documentId | To delete a single document |
| **Courses** | | |
| POST | /api/courses | To create a new course |
| POST | /api/courses/:userId/addUserToCourse | Add user to course |
| GET | /api/courses | To retrieve all courses on the platform |
| GET | /api/courses/:courseId | To retrieve details of a single course |
| GET | /api/courses/:courseId/getAllUsersInCourse | Retrieve all users in a specific course |
| GET | /api/courses/:courseId/getAllAvailableUserForCourse | Retrieve all available users for a course |
| GET | /api/courses/getAllUsersInCourse/:courseId | Retrieve all users in a specific course |
| PATCH | /api/courses/:courseId | To edit/update  the details of a single course |
| DELETE | /api/courses/:courseId | To delete a single course |
| DELETE | /api/courses/:userId/removeUserFromCourse | Remove user from course |
| **Activities** | | |
| POST | /api/activities | To create a new activity |
| GET | /api/activities | To retrieve all activities on the platform |
| GET | /api/activities/:activityId | To retrieve details of a single activity |
| PATCH | /api/activities/:activityId | To edit/update  the details of a single activity |
| DELETE | /api/activities/:activityId | To delete a single activity |
| **Modules** | | |
| POST | /api/modules | To create a new module |
| GET | /api/modules | To retrieve all modules on the platform |
| GET | /api/modules/:moduleId | To retrieve details of a single module |
| PATCH | /api/modules/:moduleId | To edit/update  the details of a single module |
| DELETE | /api/modules/:moduleId | To delete a single module |
| **Login** | | |
| POST | /api/login/validate | Receive a token when login succeeds |


## Authors
[Emre Demirel](https://github.com/98emre)

[Karl Ericsson](https://github.com/kericsson-dotnet)

[Felix Edenborgh](https://github.com/FelixEdenborgh)

[Jiangyi Liu](https://github.com/fionaliu66)

[Abed Seyed Fetrat](https://github.com/abedfetrat)




