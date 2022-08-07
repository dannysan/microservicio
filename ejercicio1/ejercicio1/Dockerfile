#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ejercicio1/ejercicio1.csproj", "ejercicio1/"]
RUN dotnet restore "ejercicio1/ejercicio1.csproj"
COPY . .
WORKDIR "/src/ejercicio1"
RUN dotnet build "ejercicio1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ejercicio1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ejercicio1.dll"]