FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./src/app/ungerfall.currency.presentation.webapi/Ungerfall.Currency.Presentation.WebApi.csproj ./src/app/
RUN dotnet restore ./src/app/Ungerfall.Currency.Presentation.WebApi.csproj

COPY ./src/app/ .
RUN dotnet publish "ungerfall.currency.presentation.webapi/Ungerfall.Currency.Presentation.WebApi.csproj" -c Release -o out

# Build the React application
FROM node:14 AS build-node
WORKDIR /web-ui
COPY ./src/web-ui/package.json ./
COPY ./src/web-ui/package-lock.json ./
RUN npm ci --silent
COPY ./src/web-ui/ .
RUN npm run build

# Generate a development certificate for the ASP.NET Core app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dev-certs
WORKDIR /root/.aspnet/https
RUN dotnet dev-certs https -ep aspnetapp.pfx -p da34d149-7f82-4f49-8804-e130e2823f52
RUN dotnet dev-certs https --trust

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 44406
ENV ASPNETCORE_URLS=https://+:44406
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTPS_PORT=44406
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build-env /app/out .
COPY --from=build-node /web-ui/build ./wwwroot
COPY --from=dev-certs /root/.aspnet/https/aspnetapp.pfx .

ENV ASPNETCORE_Kestrel__Certificates__Default__Password="da34d149-7f82-4f49-8804-e130e2823f52"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/app/aspnetapp.pfx"

# Configure the entry point
ENTRYPOINT ["dotnet", "Ungerfall.Currency.Presentation.WebApi.dll"]
