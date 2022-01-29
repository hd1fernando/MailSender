FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
RUN dotnet tool install --global dotnet-ef 
ENV PATH="${PATH}:/root/.dotnet/tools"

EXPOSE 80

WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MailSender.Api.dll"]