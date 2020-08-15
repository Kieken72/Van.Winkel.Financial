dotnet build ../src/Van.Winkel.Financial.Database/Van.Winkel.Financial.Database.csproj
dotnet ../src/Van.Winkel.Financial.Database/bin/Debug/netcoreapp3.1/Van.Winkel.Financial.Database.dll -c 'Server=localhost;Database=financialservice;User Id=financialservicesuser;Password=financialservicesuser;' -r -s
$SHELL