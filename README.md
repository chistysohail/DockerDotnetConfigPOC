DockerDotnetConfigPOC
This project demonstrates how to build and run a .NET 6 console application using Docker. It includes configuration files and data that are copied into the Docker container for use by the application.

![image](https://github.com/user-attachments/assets/36317d21-9f92-431b-a296-ba578b6b8788)
      
Dockerfile: Contains instructions to build the Docker image.
Common: A directory containing configuration and data files used by the application.
DockerDotnetConfigPOC: The main project folder with source code and project files.
How to Use
Building the Docker Image
To build the Docker image, you need to run the docker build command in your terminal. Here are the steps:

Open Terminal: Open a terminal or command prompt on your computer.

Navigate to Project Directory: Change to the directory where the Dockerfile is located. You can do this by running:


cd "C:\Developer\DockerDotnetConfigPOC"
Build the Docker Image: Run the following command to build the Docker image:
docker build -t dotnet-console-app -f Dockerfile .
-t dotnet-console-app: This option tags the image with the name dotnet-console-app.
-f Dockerfile: Specifies the Dockerfile to use for building the image.
.: Tells Docker to use the current directory as the context for the build.
Running the Docker Container
Once the image is built, you can run the container using the following command:
docker run --rm dotnet-console-app
--rm: This option automatically removes the container when it stops running.
dotnet-console-app: The name of the Docker image you want to run.
Checking Folders Inside the Docker Container
To check what folders are inside the Docker container or if you want to explore the container's file system:

Run the Container in Interactive Mode:
You can start the container and get an interactive shell by running:
docker run --rm -it dotnet-console-app sh
-it: Runs the container in interactive mode and attaches a terminal session.
sh: Starts a shell session inside the container.
List Directories:

Once inside the container, use the following command to list the directories:
ls /app
This will show you the contents of the /app directory, where your application and the Common directory are located.
![image](https://github.com/user-attachments/assets/4a5ca6d2-1f4f-43cb-ae8a-f51fb1d93152)

Explanation of the Dockerfile
Here's a simplified breakdown of what the Dockerfile does:

# Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app

# Copy the project file and restore dependencies
COPY DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj ./DockerDotnetConfigPOC/
RUN dotnet restore DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj

# Copy all source files and build the app
COPY DockerDotnetConfigPOC/ ./DockerDotnetConfigPOC/
RUN dotnet publish DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj -c Release -o out

# Use the .NET runtime to run the app
FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine
WORKDIR /app

# Copy the published app and the Common folder into the runtime image
COPY --from=build-env /app/out .
COPY Common /app/Common

# Set the command to run the application
ENTRYPOINT ["dotnet", "DockerDotnetConfigPOC.dll"]
Build Environment: Uses the .NET SDK to build the application from the source files.
Runtime Environment: Uses a smaller .NET runtime image to run the application, making it lightweight.
COPY Commands: Copies the necessary project files and directories into the Docker image.
Additional Notes
Docker Hub: The base images are pulled from Docker Hub for .NET.
Security: Using Alpine images helps keep the image size small and potentially more secure.
By following these instructions, you can easily build and run your .NET application using Docker. 
