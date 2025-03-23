IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'PillPal')
BEGIN
    CREATE DATABASE PillPal;
END