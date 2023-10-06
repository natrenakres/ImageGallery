FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ImageGallery.API/ImageGallery.API.csproj", "ImageGallery.API/"]
RUN dotnet restore "ImageGallery.API/ImageGallery.API.csproj"
COPY . .
WORKDIR "/src/ImageGallery.API"
RUN dotnet build "ImageGallery.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageGallery.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageGallery.API.dll"]
