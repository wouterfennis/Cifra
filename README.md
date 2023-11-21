         _______         ________   
        /  _____\   ___ /\   ____\ _______  
       /\  \    /  /\__\\ \  \___ /\   ___\  ______ 
      /  \  \__/__ \/\  \\ \   __\\ \  \__/ /  __  \
      \   \________\\ \  \\ \  \_/ \ \  \  /\  \L\  \
       \  /        / \ \__\\ \__\   \ \ _\ \ \__/.\__\
        \/________/   \/__/ \/__/    \/__/  \/__/\/__/ 
------------------------------------------------------------

# Cifra
An application to create a template for filling in exam points and calculating the test score. Cifra can provide a timesaving solution for repetitively creating spreadsheets for filling in test scores.
By filling in a just few parameters, a spreadsheet can be automatically generated and used directly in your favorite spreadsheet editor.

Cifra is currently a Web API with a Blazor front-end. The front-end is still in development and is not yet fully functional.

## Build
With Docker Compose the build can be easily started

```powershell
docker-compose build --build-arg NUGET_AUTH_USERNAME=token --build-arg NUGET_AUTH_TOKEN=TOKEN
```

## Start

```powershell
docker-compose up
```

## Tutorial
Cifra needs some data in order to get started. There is data that has to be filled in only once (Classes and Tests), and data that has to be filled in for each spreadsheet.

### Creating a class of students
Students of a class will be represented on the spreadsheet.
There are two ways of creating classes in Cifra. Creating them manual or importing a CSV export.

#### Manual
1. Go to the class overview page
2. Fill in a new row and fill in the first and last name, optionally a infix can also be provided of the student
3. Add the new row
3. Save the class 

#### Using a CSV export
1. Create a CSV files of the students in the following format:
```csv
FirstName; LastName
John; Doe
```

2. Go to the class overview page
3. Select the file from step 1 and click on import.
4. Save the class 

### Creating a test
The layout of the test will be represented on the spreadsheet.

1. Go to the create test form
2. Fill in the name of the test. This will be used later to search for the test when creating a spreadsheet.
3. Fill in how many versions are the of the test. Sometimes there are multiple versions of the same test when the test is being taken in the same classroom for example.
4. Fill in the minimum grade. This is the grade that will remain if no points where scored.
5. Fill in the standardization factor. This is the factor that is needed to calculate the grade
6. Save the test
7. Fill in how many assignments are present on the test. Take note of the following hierarchy:
```
- Assignment 1
	- Question 1
- Assignment 2
	- Question 2
	- Question 3
etc...
```
Assignments contain one or multiple questions.
First fill in how many assignments are there according to this hierarchy.

7. Fill in the number of questions per assignment.


### Creating a spreadsheet
If at least one class and test has been defined a spreadsheet can be created.

1. Go to the create spreadsheet form
2. Choose the predefined class to respresent in the spreadsheet.
3. Choose the predefined test to represent in the spreadsheet.
4. Fill in the name of the spreadsheet. This will also be the file name.
5. Create the spreadsheet
5. The spreadsheet will be downloaded through your browser

## Database

### Generate migration

```powershell
dotnet ef migrations add "InitialCreate" --project src/Cifra.Database/Cifra.Database.csproj --output-dir Migrations --context Context --startup-project src/Cifra.Api/Cifra.Api.csproj
```