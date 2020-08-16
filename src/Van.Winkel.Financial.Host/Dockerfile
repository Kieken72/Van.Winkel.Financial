FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.0 AS base

# Set the working directory
WORKDIR /app

# Copy the app
COPY / /app

EXPOSE 80/tcp

ENV TZ=Europe/Brussels
ENV CONNECTIONSTRING=
#ENV CERTIFICATE_FILENAME=...
#ENV CERTIFICATE_PASSWORD=...

# Start the app
ENTRYPOINT ["dotnet", "Van.Winkel.Financial.Host.dll"]