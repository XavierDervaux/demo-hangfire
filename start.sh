cd src

#Down any instance of the project that might be previously running
docker-compose --project-name demo_hangfire down

#Build and up, running the CLI
docker-compose build --no-cache
docker-compose --project-name demo_hangfire run demohangfire.cli