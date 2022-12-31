# login

## This Project is a Login with Bearer and Token JWT example API using C# and SQL-SERVER 

## Description

the features are register new users, login  and refresh the token
when a user make a register the password are hashing and add salt,
this data is save in database, a token and refresh token are generated,
the refresh token was stored in database. when user login the hash off his 
password is compared with the hash in the database, after that a token 
and refresh token are generated. The Refresh receive the expired token 
and refresh token and generate a new token and a new refresh token

the password are hashing with SHA512

## Getting Started

### Dependencies
you are gonna need SQL-SERVER 

### Installing
* download 
* execute the SQL files, create the tables and the storage procedures 
* inside the Application.Api -> appsettings.json -> appsettings.Development.json, change MainConnection to your DataBase Connection string
* run 
