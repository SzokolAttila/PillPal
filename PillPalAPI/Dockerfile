# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER 1000
WORKDIR /app
EXPOSE 5236

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

#copy lib project
COPY ["./PillPalLib/PillPalLib.csproj", "PillPalLib/"]
RUN dotnet restore "./PillPalLib/PillPalLib.csproj"
COPY . .

#build api project
COPY ["./PillPalAPI/PillPalAPI.csproj", "PillPalAPI/"]
RUN dotnet restore "./PillPalAPI/PillPalAPI.csproj"
COPY . .
WORKDIR "/app/PillPalAPI"
RUN dotnet build "./PillPalAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PillPalAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PillPalAPI.dll"]