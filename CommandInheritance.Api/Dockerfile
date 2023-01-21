FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /app
COPY --from=build /app/out .

# Install Postgres
RUN apt-get update && apt-get install -y postgresql-client

# Create the database and grant privileges to the user
RUN service postgresql start && \
    psql --command "CREATE DATABASE mydatabase;" && \
    psql --command "GRANT ALL PRIVILEGES ON DATABASE mydatabase TO myuser;"

EXPOSE 80
ENTRYPOINT ["dotnet", "webapi.dll"]