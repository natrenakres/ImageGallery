FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ImageGallery.Client/ImageGallery.Client.csproj", "ImageGallery.Client/"]
RUN dotnet restore "ImageGallery.Client/ImageGallery.Client.csproj"
COPY . .
WORKDIR "/src/ImageGallery.Client"
RUN dotnet build "ImageGallery.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageGallery.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageGallery.Client.dll"]
