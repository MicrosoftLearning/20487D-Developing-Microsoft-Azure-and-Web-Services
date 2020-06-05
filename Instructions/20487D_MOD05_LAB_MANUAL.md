# Module 5: Hosting Services On-Premises and in Azure

# Lab: Host an ASP.NET Core Service in a Windows Service

#### Scenario

In this lab, you will learn how to create a new **ASP .NET Core** project and use **Kestrel - Run As Service** to
host this project in a new Windows service.
In this lab, you will explore several frameworks and platforms, such as Entity Framework Core, ASP.NET Core Web API, and Microsoft Azure that are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create a new ASP.NET Core Web API service.
- Register a new Windows service.
- Host a web application in a Windows service.

### Exercise 1: Creating a new ASP.NET Core Application

#### Scenario

In this exercise, you will create a new Web API via ASP.NET Core by using the command prompt.

#### Task 1: Create a new ASP.NET Core Application project

1. Create a new **ASP.NET Core Web API** project by using the command prompt with the **dotnet** tool.
2. Open the project in VSCode and create a new **Models** folder.
3. Open File Explorer navigate to **[Repository Root]\Allfiles\Mod05\LabFiles\Exercise1\Assets**.
4. From **Assets** folder copy **Flight.cs** file and paste in the **Models** folder.
5. From **Assets** folder copy **FlightsController.cs** file and paste in the **Controllers** folder.
6. In **BlueYonder.Flights.Service.csproj** locate the last **\<PropertyGroup\>** section and under **\<TargetFramework\>** tag add:
   ```xml
    <RuntimeIdentifier>win7-x64</RuntimeIdentifier>
   ```
    > **Results**: After completing this exercise, you should have a basic Web API ASP.NET Core project.

#### Task 2: Install the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package

1. Install the **Microsoft.AspNetCore.Hosting.WindowsServices** NuGet package by using the command prompt with the **dotnet** tool.
    > **Results**: After completing this exercise, you should have **Microsoft.AspNetCore.Hosting.WindowsServices** NuGet package
   on your ASP .NET Core project that you have created in Task 1 which allows you to run the ASP.NET Core project as a Windows service.

#### Task 3: Modify the Main method to use Kestrel RunAsService hosting

1. Use Microsoft Visual Studio Code to add **using Microsoft.AspNetCore.Hosting.WindowsServices;** to the **program.cs** class that allows you to use the **Microsoft.AspNetCore.Hosting.WindowsServices** NuGet package that you installed in Task 2.
2. Change the **Main** method implementation by using **Kestrel’s RunAsService hosting** to host your **ASP .NET Core** project in a Windows service.

    ```cs
    CreateWebHostBuilder(args).Build().RunAsService();
    ```

### Exercise 2: Registering the Windows Service

#### Scenario

In this exercise, you will create a new Windows service, start and stop it by using the command prompt with the **sc** tool.

#### Task 1: Register the Windows Service

1. To create a new Windows service, publish the **ASP .NET Core** project that you created earlier into a folder.<br/>
   You do this by using a command line with the **dotnet** tool.
2. Create a new Windows service by using the **sc** command tool, which will take the **.exe** file from the publish in the last point and will host your application in a new Windows service.<br/>
   To do this, at the command prompt, run the following command:<br/>
    ```bash
       sc create {The Service Name} binPath= "{Your publish folder path.exe}"
    ```

#### Task 2: Start the Windows Service and test it

1. Use the sc command line to start your new Windows service.
2. Navigate to your API by using port **5000**, and make sure you are getting the expected response.
3. Use the sc command line to stop the service.
4. Navigate to the same address to see that you are not getting the expected response.
5. Close all open windows.
    > **Results**: After completing this exercise, you will have a new **ASP .NET Core** project that is hosted by a **Windows service**.

# Lab: Host an ASP.NET Core Web API in an Azure Web App

### Exercise 1: Creating a Web App in the Azure Portal

#### Task 1: Run a setup script to upload a database to Azure

1. Change the directory in powershell by running the following command, and then press Enter:
   ```bash
    cd [Repository Root]\Allfiles\Mod05\Labfiles\Exercise2\HostInAzure\Setup
   ```
2. Run the command **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1** in PowerShell as an administrator.
   >**Note**: If prompted for trust this repository type **A** and then press **Enter**.
3. Run the following command, and follow the steps to sign in your Azure subscription:
    ```bash
     .\createAzureSQL.ps1
    ```
    >**Note**: If your getting an error saying that PowersShell files is not digitally signed, Run the following command: **Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass**.
   
    >**Note**: If prompted for Do you want to change the execution policy? type **Y** and then press **Enter**.

#### Task 2: Create a free website

1. Add a new App service in **http://portal.azure.com** with a unique App name:
    ```
       blueyonder-flights-{Your Initials}
    ```
2. In **App Service plan**, type **MyFlightAppService** and in **Pricing tier**, choose **F1**.

#### Task 3: Configure an environment variable and the database connection string

1. To the new web app **blueyonder-flights-{Your Initials}**, add a new setting with the key named **BLUEYONDER_TENANT** and with the the value **Testing** .
2. Add a new setting with the key named **dbConnectionString** and the following value:
    ```
   Server=tcp:blueyonder05-{Your Initials}.database.windows.net,1433;Initial Catalog=BlueYonder.Flights.Lab05;Persist Security Info=False;User ID=BlueYonderAdmin;Password=Pa$$w0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
    ```

#### Task 4: Configure IIS logs

1. Inside **Deployment center**: 
   - Select **FTP** and then click **Dashboard**.
   - In the **FTP** pane, click **User Credentials** in the **username** box, enter a globally unique name.
   - Set the **Password**.
   
   >**Note**: You will need the credentials for the next steps. Copy them to any code editor.
2. Inside **MONITORING** menu:
   - Set the **Application Logging (Filesystem)** option to **On**.
   - Set the level to **Verbose**.
   - Set **Failed request tracing** to **On**.
   - Select **File System** with **Retention Period (Days)** of **21** days.
   - Copy the **FTP host name** value to any code editor.
  
    >**Note**: You will need it in the next exercise.

### Exercise 2: Deploying an ASP.NET Core Web API to the Web App

#### Task 1: Deploy an ASP.NET Core project to the web app

1. Open File Explorer and browse to the **[RepositoryRoot]\Allfiles\Mod05\LabFiles\Exercise2\HostInAzure\BlueYonder.Flights.Service\Properties\PublishProfiles** folder.
2. In the **Azure.pubxml** file:
   - Replace **PublishSiteName**.
   - Replace the **UserName** and **Password** values.
3. Publish **BlueYonder.Flights.Service** to host your web app in the newly created App service.

#### Task 2: Test and verify if the web app uses the database and environment variable

1. Make a **HTTP Request** to your Web App via PowerShell.
    ```ps
    Invoke-WebRequest https://{Your App Name}.azurewebsites.net/api/flights
    ```
    >**Note**: Replace **{Your App Name}** with your actual app name that you have copied earlier.

    >**Note**: If there any error regarding invoke web request run the following command then try to run the command again: **[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12**.
2. Check that you are getting the **"X-Tenant-ID"** header with the value as **Testing**.
3. Check that you are getting a good JSON response of flights.
    >**Note**: If there any error regarding invoke web request run the following command then try to run the command again:
    **[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12**

    >**Note**: If you are not seeing all the response content, type the following command:
    ```ps
      (Invoke-WebRequest https://{Your App Name}.azurewebsites.net/api/flights).Content
    ```

#### Task 3: Use FTP deployment server to view the web app and its log files

1. Make a bad HTTP request to your Web App via PowerShell.
2. Open a browser and navigate to your FTP hostname address, and sign in.
3. Under the **LogFiles\\** folder, locate **W3SVC#########** folder.
4. Review the new **xml** file:
   - **Request Summary** section
   - **Errors & Warnings** section
5. Sign in to your Azure account via PowerShell.
6. Start streaming logs into the PowerShell.
7. Run a command to see a log in the PowerShell instance.
8. Run a command to see another log in the other PowerShell instance.
9. You can see the status code at the end of the log.
10. Close all open windows.

# Lab: Host an ASP.NET Core Service in Azure Container Instances

### Exercise 1: Publish the service to a Docker container

#### Task 1: Create a Dockerfile for the service

1. Open File Explorer and in **[Repository Root]\AllFiles\Mod05\Labfiles\Exercise3\Starter\BlueYonder.Hotels.Service**, navigate to the **BlueYonder.Hotels.Service** folder.
2. In the **BlueYonder.Hotels.Service** folder, add a new file called **DockerFile** (without extension) and make sure you download a base docker image for **ASP.NET CORE** and define docker settings for your **BlueYonder.Hotels.Service** project.
3. Build your project by using **DockerFile** that you have created earlier using the command line.
4. Run the docker container which is listening on a default port.
5. Open a browser and navigate to **localhost:8080/api/hotels**, and check that you are getting the expected response in the JSON format.

#### Task 2: Publish the application into a local container using Visual Studio

1. In Microsoft Visual Studio 2017, open **[Repository Root]\AllFiles\Mod05\Labfiles\Exercise3\Starter\BlueYonder.Hotels.Service\BlueYonder.Hotels.Service.sln** .
2. Add **Docker Support** and in the **Docker File Options** window, select **Linux**.
   >**Note**: If Microsoft Visual studio dialog box appears click **No**.
3. Build the solution and start debugging.
4. Check that you are getting the expected response in the JSON format.

#### Task 3: Push the container to a public container registry

1. On your computer, run **Docker Client** by using your **Docker Hub** credentials.
2. Using the command prompt, sign in to Docker Hub.
3. In Docker Hub, create a tag name for your docker image.
4. Push your container image into Docker Hub.
5. Open a browser and navigate to **https://hub.docker.com**, and then sign in with your credentials.
6. On the **Repositories** page, verify that you are seeing the last push as *{Your Docker Hub UserName}***/hotels_service**.

### Exercise 2: Hosting the Service in Azure Container Instances

#### Task 1: Create a Resource Group for Azure Container Instances

1. Open the Azure command prompt (Azure CLI) to sign in to your Azure account.
2. Write the code that you have got from the command line in the **https://microsoft.com/devicelogin** URL.
3. Create a new **Resource Group** called **blueyonder-hotels**.
   

#### Task 2: Create an Azure Container Instance from the container image

1. At the command prompt, create a new **container** instance by using the previously-created Docker container for the hotel's service.
2. To get details of your container instance, run **az container list**.
3. Copy the **"ip"** property inside the **"ipAddress"** object.
4. Add **/api/hotels** and **IP address** in your browser's address bar.
5. Check that you are getting the expected response in the JSON format.
   
# Lab: Implementing an Azure Function 

### Exercise 1: Develop the service locally

#### Task 1: Create a new Function App project in Visual Studio

1. At the command prompt, change the directory by pasting the following command, and then press Enter:
    ```bash
    cd [RepositoryRoot]\AllFiles\Mod05\Labfiles\Exercise4
    ```
2.  Create a **local Functions Project** named **BlueYonder.Flights.GroupProxy**.
3.  Open the project in Visual Studio Code and add a new **Traveler.cs** file with the following code:  
    ```bash
    using System;

    namespace BlueYonder.Flights.GroupProxy
    {
       public class Traveler
       {
    	  public int TravelerId { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string MobilePhone { get; set; }
          public string Passport { get; set; }
          public string Email { get; set; }
       }
    }
    ```
4.  Create a new Azure function for your new Functions project with the **HttpTrigger** template.

#### Task 2: Implement an HTTP trigger that invokes the flights booking Azure Web App

1. In your new **BookFlightFunc** class, locate the **using** assemblies, and then press Enter after the last **using**, which is **Newtonsoft.Json**.
2. Add the following **using** in the new line after **using Newtonsoft.Json**:
    ```
    using System.Net.Http;
    using System.Text;
    using System.Collections.Generic;
    ```
3. Replace all the **Run** method implementations with the following code that makes a request to the **Flights booking Azure** Web App that you created in Lab 2:
    ```cs
    log.LogInformation("C# HTTP trigger function processed a request to the flights booking service.");

    var flightId = req.Query["flightId"];

    var flightServiceUrl = $"http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights/bookFlight?flightId={flightId}";

    log.LogInformation($"Flights service url:{flightServiceUrl}");

    var travelers = new List<Traveler>
     {
        new Traveler { Email = "204837Dazure@gmail.com" , FirstName = "Jonathan", LastName = "James", MobilePhone = "+61 0658748", Passport = "204837DCBA" },
        new Traveler { Email = "204837Dfunction@gmail.com", FirstName = "James", LastName = "Barkal", MobilePhone = "+61 0658355", Passport = "204837DCBABC" }
     };

    var travelersAsJson = JsonConvert.SerializeObject(travelers);

    using (var client = new HttpClient())
    {
        client.PostAsync(flightServiceUrl,
                         new StringContent(travelersAsJson,
                                           Encoding.UTF8,
                                           "application/json")).Wait();
    }

    return (ActionResult)new OkObjectResult($"Request to book flight was sent successfully");
    ```
    >**Note**: Replace *{Your Initials}* in the **flightServiceUrl** with your actual initials.

#### Task 3: Test the Function App locally in a browser

1. Test the new Azure function locally.
2. Open a browser and navigate to:
   ```url
    http://localhost:7071/api/BookFlightFunc?flightId=1
   ```
3. Check that you are getting the expected response.
4. To check whether the travelers were actually booked to flight number 1, open a browser and go to the following address:
   ```url
   http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights
   ```
    >**Note**: Replace **{Your Initials}** with your actual initials.

5. Check that you are getting the expected response in the JSON format.

### Exercise 2: Deploy the service to Azure Functions

#### Task 1: Deploy the service to Azure Functions from Visual Studio

1. Create **Function App** in Azure Portal.
2. Test the new Azure function locally, { func host start --build } by using the command prompt.
3.  Sign in to Azure with your credentials.
4.  Publish your new Azure function to Azure and press Enter:
    ```bash
        func azure functionapp publish {Your App name}
    ```
    >**Note**: Replace *{Your App name}* with your actual app name in Azure that you wrote in the previous point.
    
#### Task 2: Test the Function App on Azure in a browser 

1. Open a browser and navigate to:
   ```url
    https://{Your App Name}.azurewebsites.net/api/BookFlightFunc?flightId=1
   ```
    >**Note**: Replace **{Your App Name}** with your actual App name.
2. Check whether you are getting the expected response.
3. To check whether the travelers were actually booked to flight number 1, open a browser and navigate to:
   ```url
    http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights.
   ```
    >**Note**: Replace **{Your Initials}** with your actual initials.

4. Check whether you are getting the expected response.
5. Close all open windows.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
