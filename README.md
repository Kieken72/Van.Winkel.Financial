# Van.Winkel.Financial

## Remarks

Option for in memory database is also a possibility just comment the line in Startup.cs that says 'UseSqlServer' and uncomment 'UseInMemory'

## Important

In the following files: `./scripts/sql-scripts/create-user.sql` & `.scripts/01-migrate-db.sh` we use placeholder values for a user and a database. Do not use this in an actual production environment.

## How to run

### 0. Build solution

Navigate to `.src` and run `dotnet build` (Skip if running in visual studio)

### 1. Create database & user 

Go to `.scripts/` and execute the script using cmd `sh 00-create-db.sh` you can also create a user and database manualy for example on azure and provide the connectionstring in the next steps.

### 2. Migrate database

Go to `.scripts/` and execute the script using cmd `sh 01-migrate-db.sh` edit the file if you didn't follow step 1 and created a database manually. 
Alternatively you can navigate to `.\src\Van.Winkel.Financial.Database\bin\Debug\netcoreapp3.1` and execute `dotnet Van.Winkel.Financial.Database.dll -c "$(ConnectionString)"`


### 3. Run project 

#### 3.1 Frontend

Go to `.\src\Van.Winkel.Financial.Frontend` and execute `npm install` & `npm run build`. This will build the frontend and place it in the wwwroot of the application.

#### 3.2 Backend

Open project `.\src\Van.Winkel.Financial.sln` in visual studio and press run with `Van.Winkel.Financial.Host` as startup project.

OR 

Go to `.\src\Van.Winkel.Financial.Host\` and execute `dotnet publish`  then naviagte to `.\src\Van.Winkel.Financial.Host\bin\Debug\netcoreapp3.1\publish` and run `dotnet Van.Winkel.Financial.Host.dll` this opens will start the application and start the app at `https://localhost:5001`. Do not forget your connectionstring in `.\src\Van.Winkel.Financial.Host\appsettings.json` if you created your own database.

### 4. Run tests

Open project `.\src\Van.Winkel.Financial.sln` in visual studio and in Test Menu select `Run all tests`

OR

Go to `.\src\` and execute `dotnet test` 

