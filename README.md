# school-system-api

![image](https://github.com/therodycc/school-system-api/assets/72664020/6fcfae7f-eaa4-4d14-a883-7f64969c4b21)

## Versión

```bash
6.0.416
```

***`Sql Server`***

**`C# Net Core.`**

### SqlServer on Mac

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=MiC0ntr@s3ñ@' -p 1433:1433 --name sqlserver -v RUTA_LOCAL:/var/opt/mssql -d mcr.microsoft.com/mssql/server

# Conéctate al contenedor
docker exec -it sqlserver /bin/bash

# Conéctate a la instancia de SQL Server
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MiC0ntr@s3ñ@

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MiC0ntr@s3ñ@ -Q 'EXEC sp_configure 'remote access', 1; RECONFIGURE;'
```

`***You can use a database administrator tool like dbeaver***`

![Dbeaver](https://repository-images.githubusercontent.com/44662669/f3f5c080-808b-11ea-9713-2bea65875d95)

## Secrets

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=localhost,1433;Initial Catalog=school-system;User ID=SA;Password=MiC0ntr@s3ñ@;TrustServerCertificate=True;"
```

## Instalation

```bash
dotnet ef database update
```

```bash
dotnet run seeddata
```

## Swagger (endpoints)

```markdown
https://localhost:7071/swagger/index.html
```