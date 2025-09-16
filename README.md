# Persiapan Setup new project

## pastikan dotnet CLI sudah terinstal

- Cek versi dotnet
- untuk di project ini, versi dotnet adalah versi 8.x
- Di project ini, text editornya menggunakan VSCode

```bash
dotnet --version
```

- Buat project baru

```bash
dotnet new webapi -n project-name -f net8.0
```

- Runing project

```bash
dotnet run
```

- Runing project hot reload

```bash
dotnet watch run
```

- Pastikan tidak ada proses .NET yang masih jalan

```bash
tasklist | findstr dotnet
```

- kill semua proses dotnet.exe

```bash
taskkill /F /IM dotnet.exe
```

- Tambah dependency (NuGet packages)

```bash
dotnet add package Dapper
dotnet add package MySqlConnector
dotnet add package StackExchange.Redis
dotnet add package NLog.Web.AspNetCore
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

## Keterangan paket:

- Dapper → micro-ORM.
- MySqlConnector → driver MySQL modern.
- StackExchange.Redis → Redis client.
- NLog.Web.AspNetCore → logging NLog integrasi ASP.NET.
- Swashbuckle.AspNetCore → Swagger UI/OpenAPI.
- NewtonsoftJson → JSON handler (jika butuh fitur spesifik Newtonsoft).
