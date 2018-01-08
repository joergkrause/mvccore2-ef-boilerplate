-- Create a new database called 'CoreDemo'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'CoreDemo'
)
CREATE DATABASE CoreDemo
GO
-- Create a new table called 'CoreDemoUser' in schema 'SchemaName'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.CoreDemoUser', 'U') IS NOT NULL
DROP TABLE dbo.CoreDemoUser
GO
-- Create the table in the specified schema
CREATE TABLE dbo.CoreDemoUser
(
    Id INT NOT NULL PRIMARY KEY, -- primary key column
    FirstName [NVARCHAR](50) NOT NULL,
    LastName [NVARCHAR](50) NOT NULL,
    City [NVARCHAR](100) NULL
);
GO