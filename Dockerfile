FROM microsoft/aspnetcore AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build AS build
WORKDIR /src
COPY erp-2175-erp-humanresources.sln ./
COPY KennUwareHR/*.csproj ./KennUwareHR/

# add more projects here
# COPY WebAPIProject/*.csproj ./WebAPIProject/

RUN dotnet restore
COPY . .
WORKDIR /src/KennUwareHR
RUN dotnet build -c Release -o /app

# Each new project must be built with this
# WORKDIR /src/WebAPIProject
# RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KennUwareHR.dll"]