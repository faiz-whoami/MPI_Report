# Database and first-run setup

This project is ASP.NET MVC 5, .NET Framework 4.8, Entity Framework 6, C# 7.3, Crystal Reports. Do not use C# 8 features.

I did not run migrations, IIS, or SQL from the development agent. Do the steps below on your machine.

## 1. Target framework

Visual Studio should show target **.NET Framework 4.8**. `Web.config` compilation and httpRuntime are `4.8`. Language version is **7.3**.

Install the .NET Framework 4.8 developer pack if the project will not load.

## 2. NuGet restore

In Visual Studio: **Restore NuGet Packages**. Package folders are expected at `d:\Projects\SkySoft\packages` (HintPath `..\packages\`).

You also need **SAP Crystal Reports for Visual Studio** runtime matching 13.0.4000.

## 3. Database

Connection string in `Web.config`:

```
Server=(localdb)\MSSQLLocalDB;Database=MPI_Report;Trusted_Connection=True;TrustServerCertificate=True;
```

Change the server name if you use a full SQL Server instance.

### Option A — Entity Framework (preferred)

In **Package Manager Console** (default project `MPI_Report`):

```
Update-Database
```

That applies:

1. `Migrations/202609221052224_InitialCreate.cs` (customers + MPI report tables)
2. `Migrations/202609300100000_AddInspectionTrackPlatform.cs` (users, rigs, jobs, inventory, checklists, CAs, meetings)

`Configuration.Seed` then creates:

- Roles: Admin, Inspector, Reviewer, Client
- User **admin** / **Admin@123**
- Sample rig/job if `Sample Drilling Co.` already exists

If `Update-Database` complains about the platform migration snapshot (`Target` is null in the designer), use Option B for the extra tables, then continue.

After Option A, still run `Scripts/SeedMpiCert1.sql` if you want the MPI certificate sample row.

### Option B — SQL scripts

1. Create the database if needed.
2. If tables from InitialCreate are missing, run `Update-Database` once, or generate SQL from that migration.
3. Run `Scripts/AddInspectionTrackPlatform.sql`
4. Run `Scripts/SeedMpiCert1.sql`
5. Run `Scripts/SeedPlatform.sql`

## 4. Default login

- URL: `/Account/Login`
- Username: `admin`
- Password: `Admin@123`

Password hash is SHA-256 of `MPI|` + password (see `Infrastructure/PasswordHasher.cs`). The SQL seed uses the same algorithm via `HASHBYTES('SHA2_256', ...)`.

## 5. Run the site

F5 in Visual Studio (IIS Express, `https://localhost:44318/`).

Flow:

1. Login
2. Dashboard: select Client/Rig/Job → **Fetch data**
3. Inventory, Checklists, Corrective Actions, Daily Meetings
4. MPI Reports → Generate Report → `/Report/MPI/{id}` downloads the Crystal PDF

## 6. Crystal Reports (no stored procedures)

The PDF is a **push** report. C# loads SQL into `MPIReportDataSet` and calls `SetDataSource`. Crystal must **not** connect to SQL Server.

In Crystal Designer:

1. Open `Reports/InspectionReport2.rpt` (this is the file the service loads) or `Reports/InspectionReport.rpt`
2. Database Expert → **ADO.NET (XML)** → `Reports/Data/MPIReportDataSet.xsd`
3. Tables: `ReportHeader`, `Equipment`, `Consumables`, `TestEvaluation`
4. Do not add OLE DB / SQL Server / stored procedures

If export asks for a database login, the `.rpt` still has a live connection. Remove it and bind the XSD again.

## 7. No stored procedures

Not required. EF reads tables; Crystal receives an in-memory DataSet.

## 8. Adding more schema later

Keep C# 7.3. Add a new EF6 migration in Package Manager Console:

```
Add-Migration YourName
Update-Database
```

Do not enable automatic migrations.
