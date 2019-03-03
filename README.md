# .Net-Mentoring-D1

Deployed projects: CoreWebUI and CoreConsoleUI

# How to access CoreWebUI
Simply go to http://40.71.169.105:8080/

# How to access CoreConsoleUI
* Login to 40.71.169.105:22 under user credentials
* go to /home/AntonPlotnikau/publish directory
* run next command:
```
dotnet CoreConsoleUI.dll
```
# How to deploy CoreWebUI on your vm
* Install Docker and .Net Core on your vm
https://docs.docker.com/install/
https://dotnet.microsoft.com/download
* run next command to access container 
```
docker login corewebui20190301071103.azurecr.io
```
* run next command to pull repository
```
docker pull corewebui20190301071103.azurecr.io/corewebui
```
* build and run docker image
```
docker build -t CoreWebUI .
docker run -d -p 8080:80 --name CoreWebUI
```
Then you can access app on 8080 port of your machine
