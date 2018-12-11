FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Restonode.Messaging/Restonode.Messaging.csproj Restonode.Messaging/
COPY Restonode.Common/Restonode.Common.csproj Restonode.Common/
COPY Restonode.DAL/Restonode.DAL.csproj Restonode.DAL/
RUN dotnet restore Restonode.Messaging/Restonode.Messaging.csproj
COPY . .
WORKDIR /src/Restonode.Messaging
RUN dotnet build Restonode.Messaging.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Restonode.Messaging.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Restonode.Messaging.dll"]
