
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080


RUN apt-get update && apt-get install -y libgssapi-krb5-2

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["AkerTeklif.csproj", "."]
RUN dotnet restore "./AkerTeklif.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AkerTeklif.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "AkerTeklif.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AkerTeklif.dll"]