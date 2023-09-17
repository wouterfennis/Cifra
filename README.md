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
By filling in a just few parameters, a spreadsheet can be automatically generated and used directly.

Currently, Cifra is a console application with a basic user interface.

## Build
With Docker Compose the build can be easily started

```powershell
docker-compose build --build-arg NUGET_AUTH_USERNAME=token --build-arg NUGET_AUTH_TOKEN=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
```

## Start

```powershell
docker-compose up
```

## Tutorial
Cifra needs some data in order to get started. There is data that has to be filled in only once (Classes and Tests), and data that has to be filled in for each spreadsheet (Naming of files).

### Creating a class of students
Students of a class will be represented on the spreadsheet.
There are two ways of creating classes in Cifra. Creating them manual or importing a Magister export.

#### Manual
1. Go to the manual class form:
```
Open the Class menu --> Create a new class manually
```

2. Fill in the name of the class.

3. Now you will be adding students to the class mentioned in step 2. 
For each student, fill in the first and last name and optionally a infix.
After each student you will be asked if another student needs to be added. Anser with Y(es) or N(o) respectively.

4. If no more students needs to be added choose for N(o) and the class will be saved.

#### Using a magister export
1. In the `./Magister` folder on your computer. Add Magister CSV exports of class(es) that needs to be imported.

2. Go to the magister class form.
```
Open the Class menu --> Create a new class from magister
```

3. The available files will be shown. Choose the file that needs to be imported.

4. If the import is successfull the class will be saved.

### Creating a test
The layout of the test will be represented on the spreadsheet.

1. Go to the create test form:
```
Open the Test menu --> Create a new test
```

2. Fill in the name of the test. This will be used later to search for the test when creating a spreadsheet.

3. Fill in how many versions are the of the test. Sometimes there are multiple versions of the same test when the test is being taken in the same classroom for example.

4. Fill in the minimum grade. This is the grade that will remain if no points where scored.

5. Fill in the standardization factor. This is the factor that is needed to calculate the grade

6. Fill in how many assignments are present on the test. Take note of the following hierarchy:
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

8. Fill in if there a bonus question in this test.
A bonus question is calculated separately from the total number of points that can be achieved.
If a test has achieved the maximum score on the normal questions. The bonus question can be added on top of that score.

### Creating a spreadsheet
If at least one class and test has been defined a spreadsheet can be created.

1. Go to the create spreadsheet form:
```
Open the spreadsheet menu --> Create a spreadsheet
```

2. Choose the predefined class to respresent in the spreadsheet.

3. Choose the predefined test to represent in the spreadsheet.

4. Fill in the name of the spreadsheet. This will also be the file name.

5. The spreadsheet is created in `./Spreadsheets`

## Api

### Setup certificates

```cmd
dotnet dev-certs https --clean
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
dotnet dev-certs https --trust
```

```bash
dotnet dev-certs https --clean
dotnet dev-certs https -ep "$USERPROFILE\.aspnet\https\aspnetapp.pfx" -p password
dotnet dev-certs https --trust
```


## Database

### Generate migration

```powershell
dotnet ef migrations add InitialCreate --project src/Cifra.Database/Cifra.Database.csproj --output-dir Migrations --context Context --startup-project src/Cifra.Api/Cifra.Api.csproj
```