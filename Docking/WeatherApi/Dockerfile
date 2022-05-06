FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
# RUN dotnet publish -o /app/published-app
dotnet restore "WeatherApi.csproj" --disable_parallel
dotnet publish "WeatherApi.csproj" -c release -o /app --no-restore 

#dotnet restore "./folder/WeatherApi.csproj" --disable_parallel
#dotnet publish "./folder/WeatherApi.csproj" -c release -o /app --no-restore 

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
#COPY --from=build /app/published-app /app
COPY --from=build /app .
#ENV ASPNETCORE_URLS http://*:80
#ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT [ "dotnet", "WeatherApi.dll" ]

# docker build --rm -t my-dev/fancy-api:latest .
# docker image ls | grep fancy-api
# docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=https://+:5000 [image name]