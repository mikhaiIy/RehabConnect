# Use the .NET 8 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /AspnetCoreMvcStarter

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

# Copy the entire project and build
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /AspnetCoreMvcStarter
COPY --from=build-env /AspnetCoreMvcStarter/out .

# Expose port 5050 for the application
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AspnetCoreMvcStarter.dll"]

# Display a message in the terminal during build
RUN echo  -----------------------------------------------
RUN echo  Application is running at http://localhost:5050
RUN echo  -----------------------------------------------
