
# Module 3: Creating and Consuming ASP.NET Core Web APIs

1. Wherever a path to a file starts at *[Repository Root]*, replace it with the absolute path to the directory in which the 20487 repository resides. 
 For example, you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: **[Repository Root]\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. For example, the initials for John Doe will be jd.
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and then prepare them beforehand to save classroom time.


# Lesson 2: Creating an ASP.NET Core Web API

### Demonstration: Creating Your First ASP.NET Core Web API

1. Open the Command Prompt window.
2. Create a new **ASP.NET Core Web API** project. Then, run the following command:
    ```bash
    dotnet new webapi --name MyFirstEF --output [Repository Root]\Allfiles\Mod03\DemoFiles\FirstWebApi\Starter
    ```
3. After you create the project, run the following command to change the directory:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\FirstWebApi\Starter
    ```
4. To restore all dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
5. To Open the project in **VSCode**, and then run the following command:
    ```bash
    code .
    ```
6. On the **Explorer** blade, under **STARTER**, expand **Controllers**, and then double-click **ValuesController.cs**.
7. Locate the **Get** method and review the return content of the method.
8. To run the application, run the following command:
    ```bash
    dotnet run
    ```
9. Open a browser and navigate to the following URL:
    ```url
    https://localhost:5001/api/values
    ```
    >**Note**: If there is an error in the **Console** after running the application, run the following command **dotnet dev-certs https --trust**, and then repeat steps 8 and 9.

10. You should see the following result:
    ```json
    ["value1","value2"]
    ```
11. Switch to the Command Prompt window, and then press **Ctrl+C** to stop the process.
12. Close all open windows.


# Lesson 3: Consuming ASP.NET Core Web APIs 

### Demonstration: Consuming Services by Using JavaScript
     
#### Demonstration Steps

1. Open Command Prompt.
2. To change the directory to the **Starter** project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\JavaScriptClient\Starter
    ```
3. To restore all dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
4. To Open the project in **VSCode**, and then run the following command:
    ```bash
    code .
    ```
5. Double-click **Startup.cs**, and then locate the **Configure** method.
6. To use static files, add the following code at the end of the method:
   ```cs
    app.UseStaticFiles();
   ```
   >**Note:** Static files, such as HTML, CSS, images, and JavaScript, are assets of an ASP.NET Core app that serves directly to the clients.
7. Expand the **wwwroot** folder, and then click **index.html**.
8. To add a button element under the **h1** tag, enter the following code:
   ```html
   <button onclick="getData()">Get All Destination</button>
   ```
9. In the **wwwroot** folder, open the **client.js** file.
10. To add a new function to recieve data from the API, enter the following code:
    ```js
    function getData(){
     //fetch api default method is GET  
     fetch(uri)
     .then(response => response.json())
     .then(function(data){
         data.forEach(value => {
             document.getElementById('destinations').innerHTML += '<li id="' + value.id + '">'  + value.id + ': ' + value.cityName + ' - ' + value.airport + '</li>';
         });
     })
    }
    ```
11. To run the application, at the command prompt, run the following command:
    ```bash
    dotnet run
    ```
12. Open a browser and navigate to the following URL:
    ```url
    https://localhost:5001/index.html
    ```
13. To view the list of all destinations from the server, click **Get All Destination**.
14. Switch to the Command Prompt window, and then press **Ctrl+C** to stop the process.
15. Close all open windows.


### Demonstration: Consuming Services by Using HttpClient
     
#### Demonstration Steps

1. Open a Command Prompt window.
2. To change the directory to the **Client** starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\HttpClientApplication\Starter\HttpClientApplication.Client
    ```
3. To install **Microsoft AspNet WebApi Client**, run the following command:
    ```base
    dotnet add package Microsoft.AspNet.WebApi.Client --version=5.2.6
    dotnet restore
    ```
4. To open the project in **VSCode**, run the following command:
    ```bash
    code .
    ```
5. Double-click **Program.cs**.
6. To create a new **HttpClient** instance, in the **Main** method, enter the following code:
    ```cs
    using (var client = new HttpClient())
    {

    }
    ```
7. To create a **GET** request, in the **HttpClient** bracket, enter the following code:
    ```cs
    HttpResponseMessage message = await client.GetAsync("http://localhost:5000/api/destinations");
    ```
8. To read the respone as a string, under the **HttpResponseMessage**, enter the following code:
    ```cs
    Console.WriteLine("Respone data as string");
    string resultAsString = await message.Content.ReadAsStringAsync();
    Console.WriteLine(resultAsString);
    ```
9. To read the respone and deserialize the **JSON** to a **List\<Destination\>** instance, enter the following code:
    ```cs
    List<Destination> destinationsResult = await message.Content.ReadAsAsync<List<Destination>>();
    Console.WriteLine("\nAll Destination");
    foreach (Destination destination in destinationsResult)
    {
        Console.WriteLine($"{destination.CityName} - {destination.Airport}");
    }

    // ReadKey used that the console will not close when the code end to run.
    Console.ReadKey();
    ```
10. To change the directory to the **Host** folder, switch to the Command Prompt window, and then run the following command:
    ```bash
    cd ..\HttpClientApplication.Host
    ```
11. To run the **Host** application, run the following command:
    ```bash
    dotnet run
    ```
12. Open a new Command Prompt window and to change the directory to the **Client** folder, run the following command:
    ```bash
    cd  [Repository Root]\Allfiles\Mod03\DemoFiles\HttpClientApplication\Starter\HttpClientApplication.Client
    ```
13. To run the **Client** application, run the following command:
    ```bash
    dotnet run
    ```
    >**Note** If there is an error in the **Console** after running the application, run the following command: **dotnet dev-certs https --trust**, and then repeat steps 11 to 13.
    
14. In the **Console**, check that all the destinations came from the server.
   
15. Close all open windows.

# Lesson 4: Handling HTTP Requests and Responses

### Demonstration: Throwing Exceptions
     
#### Demonstration Steps

1. Open a Command Prompt window.
2. To change the directory to the startup project, run the following code:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\ThrowHttpResponseException\Starter
    ```
3. To open the project in **VSCode**, run the following command:
    ```bash
    code .
    ```
4. On the **Explorer** blade, under **STARTER**, expand **Controllers**, and then double-click **DestinationsController.cs**.
5. To return **destination** by **id**, enter the following code:
    ```cs
     // GET api/destinations/id
    [HttpGet("{id}")]
    public ActionResult<Destination> Get(int id)
    {
        Destination result = _destinations.FirstOrDefault(x => x.Id == id);
        if (result == null)
            return NotFound();
        return result;
    }
    ```
6. Switch to the Command Prompt window.
7. To run the application, run the following command:
    ```bash
    dotnet run
    ```
8. Open a browser, and navigate to the following URL:
    ```url
    https://localhost:5001/api/destinations/1
    ```
9.  Check if the destination, **Seattle**, came from the server.
10. Induce an error in the URL by adding 0 at the end: 
    ```url
    https://localhost:5001/api/destinations/10
    ```
11. Check if the **HTTP ERROR 404 (Not Found)** error code appears.
12. Close all open windows.

# Lesson 5: Automatically Generating HTTP Requests and Responses

### Demonstration: Testing HTTP requests with Swagger
     
#### Demonstration Steps

1. Open a Command Prompt window.
2. To change the directory to the startup project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\TestingHttpWithSwagger\Starter
    ```
3. To install the **Swagger** package, run the following command:
    ```base
    dotnet add package Swashbuckle.AspNetCore --version=3.0.0
    dotnet restore
    ```
4. To open the project in **VSCode**, run the following command:
    ```bash
    code .
    ```
5. On the **Explorer** blade, under **STARTER**, double-click **Startup.cs**.
6. Enter the following **using** statement:
    ```cs
    using Swashbuckle.AspNetCore.Swagger;
    ```
7. To add the **Swagger** generator to the services collection, in the **ConfigureServices** method, enter the following code:
    ```cs
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    });
    ```
8. To add the **Swagger** middleware, in the **Configure** method, enter the following code:
    ```cs
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
    ```
9. Switch to the Command Prompt window.
10. To run the application, run the following command:
    ```bash
    dotnet run
    ```
11. Open a browser, and navigate to the following URL:
    ```url
    https://localhost:5001
    ```
12. In the browser, verify that all the **Api** are shown.
13. Click **GET /api/Destinations**, in the top-right corner, click **Try it out**, and then click **Execute**. 
14. Locate **Response body**, and then verify that all the **Destinations** came from the server.
15. Close all open windows.

### Demonstration: Generating C# HTTP Clients by Using AutoRest
     
#### Demonstration Steps

1. Open a Command Prompt window.
2. To change the directory to the **AutoRest.Host** project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\AutoRest\Starter\AutoRest.Host
    ```
3. To install the **AutoRest** package in **npm**, run the following command:
    ```bash
    npm install -g autorest@2.0.4280
    ```
4. To run the application, run the following command:
    ```bash
    dotnet run
    ```
5. Open a Command Prompt window and change the directory to the **AutoRest.Sdk** project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\AutoRest\Starter\AutoRest.Sdk
    ```
6. To **Generate** the **swagger** code, run the following command:
    ```bash
    autorest -Input http://localhost:5000/swagger/v1/swagger.json -CodeGenerator CSharp -Namespace AutoRest.Sdk
    ```
7. To change the directory to the **AutoRest.Client** project, run the following command:
    ```bash
    cd ..\AutoRest.Client
    ```
8. To open the project in **VSCode**, run the following command:
    ```bash
    code .
    ```
9. On the **Explorer** blade double-click **Program.cs**.
10. Enter the following **using** statements:
    ```cs
    using AutoRest.Sdk;
    using AutoRest.Sdk.Models;
    ```
11. To get **destinations** from the server, in the **Main** method, enter the following code:
    ```cs
    MyAPI client = new MyAPI(new Uri("http://localhost:5000"));
    IList<Destination> destinationList = client.ApiDestinationsGet();
    ```
12. To show all the the destinations in the **Console**, enter the following code:
    ```cs
    Console.WriteLine("All Destination");
    foreach (Destination destination in destinationList)
    {
        Console.WriteLine($"{destination.CityName} - {destination.Airport}");
    }
    // ReadKey used that the console will not close when the code end to run.
    Console.ReadKey();
    ```
13. Switch to the Command Prompt window.
14. To run the **AutoRest.Client** application, run the following command:
    ```bash
    dotnet run
    ```
15. In the **Console**, check that all the destinations came from the server.
16. Close all open windows.

### Demonstration: Hosting web API in IIS and IIS Express
     
#### Demonstration Steps

1. Open Microsoft Visual Studio 2017 as an administrator.
2. In Visual Studio, on the **File** menu, point to **Open**, and then click **Project/Solution**.
3. In the **Open Project** dialog box, browse to **[Repository Root]\Allfiles\Mod03\DemoFiles\HostingISSAndISSExpress**, click **HostingISSAndISSExpress.sln**, and then click **Open**.
   > **Note**: If a Security Warning for HostingISSAndISSExpress.Host dilaog box appears, then clear the selection for Ask me for every project in this solution and then Click **OK**.
4. To run the application with **IIS Express**, click **Debug**, and then click **Start Without Debugging**, or press Ctrl + F5.
   > **Note**: If a warning regarding SSL certificate appears, Click **Yes**.
5. A new browser window will open. Verify that you are getting the following response:
    json
    ["value1", "value2"]
    
6. You have learned how to host the website on **ISSExpress**. Next, you will learn how to host the website on **IIS**. 
7. From the **Start** menu, open **Internet Information Services(IIS) Manager**.
8. Right-click the **Sites** folder, and then select **Add Website..**.
9. In the **Add Website** dialog box, in the **Site name** box, enter **HotelsSite**.
10. In the **Content Directory** section, in the **Physical path** box, enter **[Repository Root]\Allfiles\Mod03\DemoFiles\HostingISSAndISSExpress**.
11. In the **Binding** section, in the **Port** box, enter **8080**.
12. To create your new website, click **OK**.
13. Click **Application Pools**, and then double-click the website that you just created.
14. In the **.NET CLR version** list box, select **No Managed Code**, and then click **OK**.
15. Close **Internet Information Services(IIS) Manager**.
16. Swich back to Visual Studio.
17. Right-click **HostingISSAndISSExpress**, and then select **Publish**.
18. On the **Publish** blade, select **IIS, FTP, etc**, and then click **Create Profile**.
19. In the **Publish** window, in the **Server** box, enter **localhost**.
20. In the **Site name** box, enter **HotelsSite**, and then click **Next**.
21. In the **Settings** pane, in the **Deployment Mode** list box, select **Self-Contained**.
22. In the **Target Runtime** list box, select **win-x64**, and then click **Save**.
23. On the **Publish** blade, click **Publish**.
24. Wait for the publishing process to complete.
25. Open the browser and browse to **localhost:8080/api/Values**. Verify that you are getting a response similar to the following:
    json
    ["value1", "value2"]
    
©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
