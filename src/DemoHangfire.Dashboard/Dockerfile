#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DemoHangfire.Dashboard/DemoHangfire.Dashboard.csproj", "DemoHangfire.Dashboard/"]
COPY ["DemoHangfire.Tasks/DemoHangfire.Tasks.csproj", "DemoHangfire.Tasks/"]
COPY ["DemoHangfire.Helpers/DemoHangfire.Helpers.csproj", "DemoHangfire.Helpers/"]
RUN dotnet restore "DemoHangfire.Dashboard/DemoHangfire.Dashboard.csproj"
COPY . .
WORKDIR "/src/DemoHangfire.Dashboard"
RUN dotnet build "DemoHangfire.Dashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoHangfire.Dashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoHangfire.Dashboard.dll"]