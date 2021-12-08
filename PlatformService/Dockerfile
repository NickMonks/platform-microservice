FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
#pull packages and install them
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

# multi-stage build: we take from our build-env the out folder and 
# paste it into the runtime docker container
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "PlatformService.dll" ]