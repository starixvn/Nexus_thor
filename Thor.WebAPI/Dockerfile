#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.nexdev.net:5050/docker/dotnet/core/aspnet:5.0-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM registry.nexdev.net:5050/docker/dotnet/core/sdk:5.0.102-ca-patch-buster-slim AS build

WORKDIR /src
COPY ["Thor.WebAPI/Thor.WebAPI.csproj", "Thor.WebAPI/"]
RUN dotnet nuget add source http://nuget.nexdev.net/api/v2
RUN dotnet restore "Thor.WebAPI/Thor.WebAPI.csproj"
COPY . .
WORKDIR "/src/Thor.WebAPI"
RUN dotnet build "Thor.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thor.WebAPI.csproj" -c Release -o /app/publish

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thor.WebAPI.dll"]