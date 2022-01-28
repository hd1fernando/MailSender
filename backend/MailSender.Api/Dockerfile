FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet ef database update
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MailSender.Api.dll"]