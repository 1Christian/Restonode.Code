FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Restonode.Tests/Restonode.Tests.csproj Restonode.Tests/
COPY RestonodeAPI/Restonode.API.csproj RestonodeAPI/
COPY Restonode.Business/Restonode.Business.csproj Restonode.Business/
COPY Restonode.DAL/Restonode.DAL.csproj Restonode.DAL/
COPY Restonode.Common/Restonode.Common.csproj Restonode.Common/
COPY Restonode.Messaging/Restonode.Messaging.csproj Restonode.Messaging/
RUN dotnet restore Restonode.Tests/Restonode.Tests.csproj
COPY . .
WORKDIR /src/Restonode.Tests
RUN dotnet build Restonode.Tests.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Restonode.Tests.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Restonode.Tests.dll"]
