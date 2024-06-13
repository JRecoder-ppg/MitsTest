# To generate tables
1. Open Package Manager Console and write **add-migration Any_Migration_Name**
   This will generate a folder Migrations and the cs file with the migration info
2. In Package Manager Console, write **update-database**
   This command will create the table(s) on Postgres DB (inside test schema)


  **Now you can run the project and some Endpoint are available to Add Culture Code, and some GET functions**

# Code First Commands

|Package Manager Console  | Description                                                 |
|-------------------------|-------------------------------------------------------------|
|add-migration [name]     |Create a new migration with the specific migration name.     |
|remove-migration         |Remove the latest migration.                                 |
|update-database          |Update the database to the latest migration.                 |
|update-database [name]   |Update the database to a specific migration name point.      |
|get-migrations           |Lists all available migrations.                              |
|script-migration         |Generates an SQL script for all migrations.                  |
|drop-database            |Drop the database.                                           |


**Change in SecretManagerService.cs line 25 "your-profile-aws-name" with your profile name (sample : DevEnv-PPG_Developer_ReadWrite-123456789012)**


