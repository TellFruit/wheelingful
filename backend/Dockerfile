FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /api

COPY *.sln .
COPY Wheelingful.API/*.csproj ./Wheelingful.API/
COPY Wheelingful.Core/*.csproj ./Wheelingful.Core/
COPY Wheelingful.Data/*.csproj ./Wheelingful.Data/
COPY Wheelingful.Services/*.csproj ./Wheelingful.Services/

RUN dotnet restore

COPY Wheelingful.API/. ./Wheelingful.API/
COPY Wheelingful.Data/. ./Wheelingful.Data/

WORKDIR /api/Wheelingful.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /api

COPY --from=build /api/Wheelingful.API/out ./
ENTRYPOINT ["dotnet", "Wheelingful.API.dll"]