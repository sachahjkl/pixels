FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

VOLUME ["/data"]
ENV CanvasConfig__Width=1000
ENV CanvasConfig__Height=1000

EXPOSE 8080
ENTRYPOINT ["dotnet", "pixels_site.Api.dll"]
