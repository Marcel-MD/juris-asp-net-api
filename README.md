# Juris ASP.NET API

## Description

Simple RESTful API developed with [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) for an internship project.

## Setup

You'll need [Docker](https://www.docker.com/) to run [SQL Server](https://hub.docker.com/_/microsoft-mssql-server) and [Azurite](https://hub.docker.com/_/microsoft-azure-storage-azurite). In the root folder run

```bash
$ docker-compose up
```

Create a new database named `juris` and a new blob container named `juris`. You can use [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15) and [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/#features) for this.

For email notifications feature to work, you'll have to provide the settings in `appsettings.json`.

