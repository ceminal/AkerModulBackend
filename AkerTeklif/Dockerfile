# 1. Aþama: Çalýþtýrma Ortamý (Base Image)
# Eðer .NET 7 kullanýyorsan 8.0 yerine 7.0 yazabilirsin
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# --- KRÝTÝK DÜZELTME BURADA ---
# PostgreSQL baðlantýsý için gerekli kütüphaneyi buraya ekliyoruz.
# Bu satýr olmazsa "libgssapi_krb5.so.2" hatasý alýrsýn.
RUN apt-get update && apt-get install -y libgssapi-krb5-2
# ------------------------------

# 2. Aþama: Derleme (Build Image)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AkerTeklif.csproj", "."]
RUN dotnet restore "./AkerTeklif.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AkerTeklif.csproj" -c Release -o /app/build

# 3. Aþama: Yayýnlama (Publish)
FROM build AS publish
RUN dotnet publish "AkerTeklif.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Aþama: Final (Çalýþtýrma)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AkerTeklif.dll"]