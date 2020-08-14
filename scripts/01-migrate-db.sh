dotnet build ../src/Van.Winkel.Financial.Database/Van.Winkel.Financial.csproj
dotnet ../src/Van.Winkel.Financial.Database/bin/Debug/netcoreapp3.1/Van.Winkel.Financial.Database.dll -c 'Server=localhost;Database=financialservicesuser;User Id=home;Password=financialservicesuser;' -r -s
$SHELL