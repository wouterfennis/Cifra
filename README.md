# Cifra
An application to create a template for filling in exam points and calculating the test score.

## Tutorial







## Deployment on Linux

Publish your application as a self contained application:
```
dotnet publish -c release -r ubuntu.16.04-x64 --self-contained
```
Copy the publish folder to the Ubuntu machine

Open the Ubuntu machine terminal (CLI) and go to the project directory

Provide execute permissions:
```
chmod 777 ./appname
```
Execute the application
```
./appname
```
