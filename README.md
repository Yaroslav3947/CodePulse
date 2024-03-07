# CodePulse
 Full Stack Web Applications with Angular, ASP.NET Core Web API, Entity Framework Core, C# REST API, JWT Token


# SQL Server Database Restoration

## Overview
Restore the `CodePulseDb` database using the provided backup file.

## Prerequisites
- SQL Server installed.
- Have the backup file (`CodePulseDb.bak`) in the specified path.

## Steps

1. **Open SSMS:**
   - Connect to your SQL Server instance.

2. **Access Query Window:**
   - Open a new query window.

3. **Execute Restore:**
   ```sql
   RESTORE DATABASE CodePulseDb FROM DISK = '{path/to/project}/Db-backups/CodePulseDb.bak' WITH RECOVERY;
