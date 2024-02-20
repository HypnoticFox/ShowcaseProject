#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Products/ShowcaseProject.Products.API/ShowcaseProject.Products.API.csproj", "src/Products/ShowcaseProject.Products.API/"]
RUN dotnet restore "src/Products/ShowcaseProject.Products.API/ShowcaseProject.Products.API.csproj"
COPY . .
WORKDIR "/src/src/Products/ShowcaseProject.Products.API"
RUN dotnet build "ShowcaseProject.Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShowcaseProject.Products.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseProject.Products.API.dll"]