#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["test-v1.Api/test-v1.Api.csproj", "test-v1.Api/"]
COPY ["test-v1.Bll/test-v1.Bll.csproj", "test-v1.Bll/"]
COPY ["test-v1.Dal/test-v1.Dal.csproj", "test-v1.Dal/"]
RUN dotnet restore "test-v1.Api/test-v1.Api.csproj"
COPY . .
WORKDIR "/src/test-v1.Api"
RUN dotnet build "test-v1.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "test-v1.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test-v1.Api.dll"]