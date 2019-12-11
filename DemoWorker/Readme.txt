cd "E:\Codes sources\commonfeatures-webapi-aspnetcore\DemoWorker"
dotnet publish -o C:\Temp\publish\services
sc.exe create DemoWorker binpath= C:\Temp\publish\services\DemoWorker.exe
sc.exe start DemoWorker
sc.exe delete DemoWorker