# Module 8: Monitoring and Diagnostics

1. Wherever a path to a file starts with *[Repository Root]*, replace it with the absolute path to the folder in which the 20487 repository resides.
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *[YourInitials]* appears, replace it with your actual initials. (For example, the initials for **John Doe** will be **jd**.)
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and prepare them beforehand to save classroom time.

# Lesson 1: Logging in ASP.NET Core

### Demonstration: Recording logs to the Console and EventSource providers

#### Demonstration Steps

1. Open the command prompt.
2. To change directory to the **DemoFiles** project, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles\Mod8Demo1Logger
   ```
3. To create a new **WebApi** project, run the following command:
   ```bash
    dotnet new webapi -n Mod8Demo1LoggerStarter    
   ```
4. To change directory to the **Mod8Demo1LoggerStarter** project, run the following command:
   ```bash
    cd Mod8Demo1LoggerStarter
   ```
5. To open the project in Microsoft Visual Studio Code, run the following command:
   ```bash
    code .
   ```
6. Click **Program.cs**, and then after **WebHost.CreateDefaultBuilder(args)**, paste the following code in the **CreateWebHostBuilder** method:
   ```cs
    .ConfigureLogging((hostingContext, logging) => 
    {
        logging.AddConsole();
        logging.AddEventSourceLogger();
    })
   ```
7. Expand the **Controllers** folder, and then select the **ValuesController.cs** file.
8. Add the following **using** statements:
   ```cs
    using Microsoft.Extensions.Logging;
   ```
9. Add the following **logger** field:
    ```cs
    private readonly ILogger _logger;
    ```
10. Add a new **constructor** that will inject the **logger**:
    ```cs
    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }
    ```
11. To the first **Get** method, before the **return**, add the following log information:
    ```cs
    _logger.LogInformation("Getting all values");
    ```
12. To the second **Get** method, before the **return**, add the following log information:
    ```cs
    _logger.LogInformation($"Getting value {id}");
    ```
13. Switch to the command prompt.
14. To run the project, run the following command:
    ```bash
    dotnet run    
    ```
15. Open a browser and go to the following URL:
    ```url
    https://localhost:5001/api/values
    ```
16. In the Command Prompt window, verify that the **Getting all the values** log is shown.
17. Go to the following URL:
    ```url
    https://localhost:5001/api/values/1
    ```
18. In the the Command Prompt window, verify that the **Getting value 1** log is shown.
19. Close all the windows.

### Demonstration: Using Serilog

#### Demonstration Steps

1. Open the command prompt.
2. To change directory to the **DemoFiles** project, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles\Mod8Demo2Serilog
   ```
3. To create a new **WebApi** project, run the following command:
   ```bash
    dotnet new webapi -n Mod8Demo2SerilogStarter    
   ```
4. To change directory to the **Mod8Demo2SerilogStarter** project, run the following command:
   ```bash
    cd Mod8Demo2SerilogStarter
   ```
5. To install the **Serilog** package, run the following cammand:
   ```bash
    dotnet add package Serilog.AspNetCore --version 2.1.1
   ```
6. To install the **Serilog Configuration** package, run the following command:
   ```bash
    dotnet add package Serilog.Settings.Configuration --version 2.6.1
   ```
7. To install the **Serilog Colored Console** package, run the following command:
   ```bash
    dotnet add package Serilog.Sinks.ColoredConsole --version 3.0.1
   ```
8. To restore all the packages, run the following command:
   ```bash
    dotnet restore
   ```
9. To open the project in Visual Studio Code, run the following command:
   ```bash
    code .
   ```
10. In Visual Studio Code, **Explorer** pane click **Program.cs**, to use **Serilog**, add the following **using** statement:
    ```cs
    using Serilog;
    ```
11. To use **Serilog** and write the logs to the console, click **Program.cs**, and then after **UseStartup\<Startup\>()**, paste the following code in the **CreateWebHostBuilder** method:
    ```cs
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
	    .ReadFrom.Configuration(hostingContext.Configuration)
	    .Enrich.FromLogContext()
	    .WriteTo.Console());
    ```
12. To run the project, switch to the command prompt, and then run the following command:
    ```bash
    dotnet run
    ```
14. Open a browser and go to the following URL:
    ```url
    https://localhost:5001/api/values
    ```
15. In the Command Prompt window, explore the logs from **Serilog**.
16. Close all the windows.

# Lesson 2: Diagnostic Tools

### Demonstration: Collecting ASP.NET Core LTTng events on Linux

#### Demonstration Steps

1. Open the command prompt.
2. To change directory to the **DemoFiles** project, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles
   ```
3. To create a new **WebApi** project, run the following command:
   ```bash
    dotnet new webapi -n MonitorLTTng    
   ```
4. To change directory to the **MonitorLTTng** project, run the following command:
   ```bash
    cd MonitorLTTng
   ```
5. To open the project in Visual Studio Code, run the following command:
   ```bash
    code .
   ```
6. In Visual Studio Code, add a new Dockerfile (without file extension) to the root directory.
7. To the Dockerfile, add the following commands:
    ```sh
    FROM microsoft/dotnet:sdk AS build-env
	WORKDIR /app

	# Copy csproj and restore as distinct layers
	COPY *.csproj ./
	RUN dotnet restore

	# Copy everything else and build
	COPY . ./
	RUN dotnet publish -c Release -o out

	# Build runtime image
	FROM microsoft/dotnet:aspnetcore-runtime
	WORKDIR /app
	COPY --from=build-env /app/out .
    # Enviroment variable
	ENV COMPlus_EnableEventLog 1
	ENTRYPOINT ["dotnet", "MonitorLTTng.dll"]
    ```
8. Switch to the command prompt.
9. To build the docker image, run the following command:
    ```bash
    docker build -t monitor .
    ```
10. To run a new container with the **monitor** image, run the following command:
    ```
    docker run -d -p 8080:80 --name myapp monitor
    ```
11. To get a shell in the container, run the following command:
    ```
    docker exec -it myapp bash
    ```
12. To update packages in the container, run the following command:
    ```bash
    apt update
    ```
13. To install **software-properties-common**, run the following command:
    ```bash
    apt-get install software-properties-common
    ```
14. To update the list of packages, run the following command:
    ```bash
    apt-get update
    ```
15. To install the main **LTTng** packages, run the following command:
    ```bash
    apt-get install lttng-tools lttng-modules-dkms liblttng-ust-dev
    ```
16. To create a new **LTTng** session, run the following command:
    ```bash
    lttng create exceptions-trace
    ```
17. To add context data (process id, thread id, process name) to each event, run the following command:
    ```bash
    lttng add-context --userspace --type vpid
    lttng add-context --userspace --type vtid
    lttng add-context --userspace --type procname
    ```
18. To create an event rule to record all the events starting with **DotNETRuntime**, run the following command:
    ```bash
    lttng enable-event --userspace --tracepoint DotNETRuntime:*
    ```
19. To start recording events, run the following command:
    ```bash
    lttng start
    ```
20. Open a browser and go to the following URL:
    ```url
    http://localhost:8080/api/values
    ```
21. Refresh the page a few times.
22. Switch to the command prompt.
23. To stop the recording, run the following command:
    ```bash
    lttng stop
    ```
24. To **destroy** the session, run the following command:
    ```bash
    lttng destroy
    ```
25. To see all the recorded events, paste the following command, press Tab, and then press Enter:
    ```bash
    babeltrace /root/lttng-traces/exceptions-trace
    ```
26. Explore all the events.
27. To exit from bash, run the following command:
    ```bash
    exit
    ```
28. To kill the running container, run the following command:
    ```bash
    docker kill myapp
    ```
29. To remove the container, run the following command:
    ```bash
    docker rm myapp
    ```
30. Close all the windows.

# Lesson 3: Application Insights

### Demonstration: Integrating and viewing Application Insights

### Preparation Steps

1. Open Windows PowerShell as Administrator.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
   >**Note**: If prompted for trust this repository, Type **A** and then press **Enter**.
4. Browse to *[repository root]***\AllFiles\Mod08\DemoFiles\ApplicationInsights\Setup**.
5. Run the following command:
    ```batch
     .\createAzureServices.ps1
    ```
    >**Note**: If prompted for Run only scripts that you trusted, type **R** and then press **Enter**.

#### Demonstration Steps

1. Open the Microsoft Azure portal.
2. Click **All resources**, and  then click **blueyondermod08demo5**{YourInitials}.
3. In the left pane, in the **SETTINGS** section, click **Application Insights** .
4. Click **Turn on site Extension**, and then add the following information:
    - Select **Create new resource**.
    - Click **Apply**.
    - In the **Apply monitoring settings** dialog box, click **Yes**.
5. In the **Application Insights** blade, click **View Application Insights data**.
6. On the top bar, copy **Instrumentation Key**.
7. Open the command prompt.
8. To change the directory to the **Starter** folder, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod08\DemoFiles\ApplicationInsights\Starter
    ```
9. To install the **ApplicationInsights** package, run the following command:
    ```base
    dotnet add package Microsoft.ApplicationInsights.AspNetCore --version=2.4.1
    ```
10. To open the project in Visual Studio Code, run the following command: 
    ```bash
    code .
    ```
11. Click **appsettings.json**, and then add the following code:
    ```json
    "ApplicationInsights": {
        "InstrumentationKey": "{InstrumentationKey}"
    }
    ```
12. Replace the **InstrumentationKey** key with the value copied in step 6.
13. In the **Progam** class, locate the **CreateWebHostBuilder** lamda, and then replace it with the following code:
    ```cs
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseApplicationInsights();
    ```
14. Switch to the command prompt.
15. To publish the service, paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
16. Switch to the Azure portal.
17. To monitor the service, under **INVESTIGATE**, click **Live Metrics Stream**.
18. Open a new browser tab and navigate to the following URL a number of times:
    ```url
    https://blueyondermod08demo5{YourInitials}.azurewebsites.net/api/values
    ```
19. To diagnose the service, switch to the Azure portal.
20. Close the **Live Metrics Stream** blade.
21. Under **INVESTIGATE**, click **Performance**.
22. To view all the requests to the service, under **Select operation**, click **Load more**.
    >**Note**: If we didn't see any values under select operation wait couple of minutes and then refresh the above URL.
23. Click **GET Values/Get** then click **Samples** to view all the requests to the **Get** method of the **ValuesController** class.
24. Select one of the samples and view all the details of the request. 

### Demonstration: Viewing application dependencies and request timelines

#### Demonstration Steps

1. Open the command prompt as an administrator.
   >**Note:** In User Account Control dialogue box click Yes.
2. To create a new ASP.NET Core Web API project, paste the following command at the command prompt, and then press Enter:
    ```bash
    dotnet new webapi --name BlueYonder.Flights.Service --output [Repository Root]\Allfiles\Mod08\DemoFiles\Mod8Demo6\Starter\Code\BlueYonder.Flights.Service 
    ```
3. After the project is created, at the command prompt, change directory by running the following command and then press Enter:
    ```bash
    cd [Repository Root]\Allfiles\Mod08\DemoFiles\Mod8Demo6\Starter\Code\BlueYonder.Flights.Service
    ```
4. Add the following package:
   ```bash
    dotnet add package Microsoft.ApplicationInsights.AspNetCore --version 2.4.1
   ```
5.  To restore all dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
5. To open the project in Visual Studio Code, paste the following command, and then press **Enter**: 
    ```bash
    code .
    ```
6. Expand the **Controllers** folder and double-click the **ValuesController.cs** file.
7. Paste the following code to add **using** to the **class**:
    ```cs
    using System.Net.Http;
    ```
8.  Inside the **ValuesController.cs** class brackets, add the following method:
    ```cs
    // GET api/values
    [HttpGet("sendRequest")]
    public async Task<string> SendRequest()
    {
        var client = new HttpClient();
        await client.GetAsync("http://httpbin.org/get");
        return "Success";
    }
    ```
9.  In the **Explorer** blade, under the **BlueYonder.Flights.Service** pane, double-click the **Program.cs** class.
10. Locate the **CreateWebHostBuilder** method, and replace it with the following code:
    ```cs
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
       WebHost.CreateDefaultBuilder(args)
              .UseApplicationInsights()
              .UseStartup<Startup>();
    ```
11. Right-click the **Properties** folder, select **New Folder**, and then name the folder **PublishProfiles**.
12. In **PublishProfiles**, add the file **Azure.pubxml** and double-click it.
13. In **Azure.pubxml** paste the following code:
    ```cs
    <Project>
        <PropertyGroup>
          <PublishProtocol>Kudu</PublishProtocol>
          <PublishSiteName>[App Service Url]</PublishSiteName>
          <UserName>[FTP/Deployment UserName]</UserName>
          <Password>[Password]</Password>
        </PropertyGroup>
    </Project>
    ```
14. In **PublishProfiles** Add a new file named **Azureprofile.xml** and paste the following code:
    ```cs
    <?xml version="1.0" encoding="utf-8"?>
    <publishData>
      <publishProfile profileName="" publishMethod="MSDeploy" publishUrl="" msdeploySite="" userName="" userPWD="" destinationAppUrl=""     SQLServerDBConnectionString="" mySQLDBConnectionString="" hostingProviderForumLink="" controlPanelLink="http://windows.azure.com"   webSystem="WebSites">
        <databases />
      </publishProfile>
      <publishProfile profileName="" publishMethod="FTP" publishUrl="" ftpPassiveMode="True" userName="" userPWD="" destinationAppUrl=""    SQLServerDBConnectionString="" mySQLDBConnectionString="" hostingProviderForumLink="" controlPanelLink="http://windows.azure.com"  webSystem="WebSites">
        <databases />
      </publishProfile>
    </publishData>
    ```
15. Click **Start**, search for **PowerShell** and right-click and select **Run as Administrator**.
16. In the **User Account Control** modal, click **Yes**.
17. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
    >**Note:** If prompted for trust this repository type A and then press Enter.
18. In PowerShell, to change directory, type the following command, and then press Enter:
    ```bash
      cd [Repository Root]\AllFiles\Mod08\DemoFiles\Mod8Demo6\Starter\Setup
    ```
19. Run the following command:
    ```bash
      .\createAzureServices.ps1
    ```
20. You will be asked to supply a subscription ID, which you can get by performing the following steps:
    1. Open a browser and navigate to **http://portal.azure.com**. If a page appears, asking for your email address, enter your email address, and then click **Continue**. Wait for the **Sign-in** page to appear, enter your email address and password, and then click **Sign In**.
    2. In the top bar, in the search box, type **Cost**, and then in the results, click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window opens.
    3. Under **Individual billing**, click **Subscriptions**.
    4. Under **My subscriptions**, you should have at least one subscription. Click the subscription that you want to use.
    5. Copy the value from **Subscription ID**, and then paste it at the PowerShell prompt. 
    6. In the **Sign in** window that appears, enter your details, and then sign in.
    7. In the **Administrator: Windows PowerShell** window, enter your initials when prompted.    

21. Open Microsoft Edge.
22. Navigate to **https://portal.azure.com** and login with your credentials.
23. In the left menu panel, click **App Services**.
24. Click **blueyonderMod8Demo6**{yourinitials}.
25. In the left menu panel, under **Settings**, click **Application Insights**.
26. Click **Turn on site extension**.
    - Select **Create new resource**.
    - Select **blueyonderMod8Demo6**{yourinitials}.
27. Under **Instrument your application**, select **.NET Core**.
28. Click **Apply**, and then click **Yes**.
29. Wait until all changes are saved.
30. Scroll to the top on the page and click **View Application Insights data**.
31. In the right panel, copy the **Instrumentation Key**.
32. Switch to Visual Studio Code.
33. In **BlueYonder.Flights.Service**, locate **appsettings.json** and add the following code:
    ```cs 
    "ApplicationInsights": {
            "InstrumentationKey": "{Your Instrumentation Key}"
        }
    ```
34. In *{Your Instrumentation Key}*, paste the **Instrumentation Key** value you copied in step 32.
35. Switch to the command prompt.
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
36. Open Microsoft Edge.
37. Navigate to the following URL:
    ```url
    https://blueyondermod8demo6{YourIntials}.azurewebsites.net/api/values/sendrequest
    ```
38. To refresh the page, press F5 a couple of times.
39. Switch to Azure portal.
40. On the left panel click **All resources**.
41. Click the **blueyonderMod8Demo6**{YourInitial} Application Insights.
42. Under **Investigate**, click **Performance**.
43. Click **Dependencies** and then locate the **HTTP: GET httpbin.org/get** request.
44. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
