# Hangfire Demo

This project is meant to demonstrate [Hangfire](https://www.hangfire.io/)'s capabilities with a simple application.
The projet runs fully with Docker, the easiest way to launch it is to use the `start.sh` script at root.

The project runs at least 4 containers :
* The CLI, used to queue jobs.
* The SQL Server instance, used to store jobs.
* The ASP.NET Core app, used to display a handy dashboard to monitor and manage jobs.
* The Server, which will be the one running the jobs. This one is scalable, and can be instanciated as many times as needed.

You can check [`docker-compose.yml`](src/docker-compose.yml) for details on how the containers are defined.

# How to run the demo
The demo is pretty much out of the box, the only thing you need to do is add an `.env` file inside the `src` folder.
The `.env` file contains the environment variables used by Docker to create the containers, and amongst other things it is used to store passwords.

Create a new `.env` file inside `src`, it should be next to `docker-compose.yml` and `.dockerignore`.
Write this inside :
```
SA_PASSWD=MySuperSecurePass1234!
```
Feel free to change the password to whatever you like, though be aware of [SQL Server's password policy requirements](https://docs.microsoft.com/en-us/sql/relational-databases/security/password-policy?view=sql-server-ver15).

Then just launch the `start.sh` script in your favorite shell, the CLI will open automatically after Docker is done composing the containers.
Once the containers are up and running, you can access Hangfire's dashboard at [http://localhost:5000](http://localhost:5000)

# Without Docker
This app uses environment variables defined by docker to fetch the connection string to the instance of SQL Server. This is to avoid storing the password in clear text inside the code. Despite this being just a demo, passwords should never be included in the source control, this is why it needs to be manually added (See previous point).

If for some reason you want to launch the project out of docker, you can do it by replacing the connection string in each project's `Program.cs:Main()` file.
Replace this :
```
var cs = Environment.GetEnvironmentVariable("ConnectionString"); 
```
Use your favorite way to fetch the connection string instead.

# Documentation
You can access Hangfire's documentation [here](https://docs.hangfire.io/en/latest/).