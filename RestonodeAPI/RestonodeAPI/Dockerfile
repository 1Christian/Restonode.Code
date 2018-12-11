FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49201
EXPOSE 44352

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RestonodeAPI/Restonode.API.csproj RestonodeAPI/
COPY Restonode.Business/Restonode.Business.csproj Restonode.Business/
COPY Restonode.DAL/Restonode.DAL.csproj Restonode.DAL/
COPY Restonode.Common/Restonode.Common.csproj Restonode.Common/
COPY Restonode.Messaging/Restonode.Messaging.csproj Restonode.Messaging/
RUN dotnet restore RestonodeAPI/Restonode.API.csproj
COPY . .
WORKDIR /src/RestonodeAPI
RUN dotnet build Restonode.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Restonode.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Restonode.API.dll"]
