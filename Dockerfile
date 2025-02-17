# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrchidPharmedApi.csproj", "./"]
RUN dotnet restore "OrchidPharmedApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OrchidPharmedApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OrchidPharmedApi.csproj" -c Release -o /app/publish

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrchidPharmedApi.dll"]
