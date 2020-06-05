# Module 4: Extending ASP.NET Core HTTP services

1. Wherever a path to a file starts at *[Repository Root]*, replace it with the absolute path to the directory in which the 20487 repository resides. 
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: **[Repository Root]\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. For example, the initials for **John Doe** will be **jd**.
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and then prepare them beforehand to save classroom time.


# Lesson 1: The ASP.NET Core Request Pipeline

### Demonstration: Creating a Middleware for Custom Error Handling

1. Open a **Command Prompt** window.
2. To change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\ErrorHandlingMiddleware\Starter
    ```
3. To restore all dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
4. Open the project in Microsoft Visual Studio Code, and then run the following command:
    ```bash
    code .
    ```
5. Expand the **BlueYonder.Flights.DAL** project, expand the **Repository** folder, and then select the **PassengerRepository** file.
6. To throw the **KeyNotFoundException** exception, locate the **GetPassenger** method, and then add the following code before **return**:
    ```cs
     if (passenger == null)
        throw new KeyNotFoundException();
    ```
7. Right-click **BlueYonder.Flights.Service**, select **New Folder**, and then name it **Middleware**.
8. Right-click the **Middleware** folder, select **New File**, name it **ExceptionHandlingMiddleware.cs**, and then select the new class.
9. Add the following **using** statements to the class:
    ```cs
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    ```
10. To add a namespace, enter the following code:
    ```cs
    namespace BlueYonder.Flights.Service.Middleware
    {

    }
    ```
11. To add a class declaration, enter the following code inside namespace brackets:
    ```cs
    public class ExceptionHandlingMiddleware
    {

    }
    ```
12. To add a constructor, enter the following code inside class brackets:
    ```cs
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    ```
13. To add an **Invoke** method to catch all the exceptions, enter the following code:
    ```cs
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
             await _next(httpContext);
        }catch(Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    ```
14. To handle the exception and add **Status Code**, enter the following code:
    ```cs
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        if (exception is KeyNotFoundException) code = HttpStatusCode.NotFound;
        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
    ```
15. To add **extension method** for **IApplicationBuilder**, enter the following code outside class brackets but inside namespace brackets:
    ```cs
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
    ```
16. In the **BlueYonder.Flights.Service** project, click the **Startup** file:
17. Add the following **using** statement:
    ```cs
    using BlueYonder.Flights.Service.Middleware;
    ```
18. To use **Exception Handling Middleware**, locate the **Configure** method and enter the following code before **app.UseHttpsRedirection();**:
    ```cs
    app.UseExceptionHandlingMiddleware();
    ```
19. Switch to the **Command Prompt** window.
20. To change the directory to **BlueYonder.Flights.Service**, run the following command:
    ```bash
    cd BlueYonder.Flights.Service  
    ```
21. To run the server, run the following command:
    ```bash
    dotnet run
    ```
22. Open Microsoft Edge, and then go to the following URL:
    ```url
    https://localhost:5001/api/passenger/5
    ```
23. To open the **Developer Tools**, press **F12**.
24. In **Developer Tools**, click **Network**.
25. In the **Network** tab, locate the following URL:
    ```url
    https://localhost:5001/api/passenger/5
    ```
26. Click the URL, and then verify that that the result column shows **404**.
27. Close all open windows.

# Lesson 2: Customizing Controllers and Actions

### Demonstration: Creating Asynchronous Actions

1. Open the **Command Prompt** window.
2. To change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter
    ```
3. To restore all the dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
4. To open the project in Visual Studio Code, run the following command:
    ```bash
    code .
    ```
5. Expand the **BlueYonder.Flights.Service** project and verify whether a folder named **wwwroot** exists within the project. If the folder exists, continue to the next step. If the folder does not exist, right-click **BlueYonder.Flights.Service** and then click **New Folder** to create the folder. Name the folder **wwwroot**.
6. In the **BlueYonder.Flights.Service** project, expand the **Controllers** folder, and then click the **PassengerController** file.
7. To add the *asynchronous actions get photo as* parameter, inside the class, enter the following code:
    ```cs
    [HttpPut("UpdatePhoto")]
    public async Task<IActionResult> UpdatePhoto(IFormFile file)
    {
        if (file == null || !file.ContentType.Contains("image"))
            return BadRequest();
            
        if (file.Length > 0)
        {
            using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, file.FileName), FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        return Ok();
    }
    ```
    This method gets the photo as a paramter and saves it in the **Image** folder inside **wwwroot** folder.
8. Switch to the **Command Prompt** window.
9. To change the directory to **BlueYonder.Flights.Service**, run the following command:
   ```bash
   cd BlueYonder.Flights.Service
   ```
10. To run the service, run the following command:
    ```cd
    dotnet run
    ```
11. Open a new **Command Prompt** window.
12. To change the directory to **BlueYonder.Flights.Client**, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter\BlueYonder.Flights.Client
    ```
13. To run the client application, run the following command:
    ```cd
    dotnet run
    ```
14. In the console, verify that the status code is **OK**.
15. Verify that you have placed the image at the following path:
    ```bash
    [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter\BlueYonder.Flights.Service\wwwroot\Image
    ```

### Creating Custom Filters and Formatters

1. Open a **Command Prompt** window.
2. To change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\CustomFiltersAndFormatters\Starter\CustomFiltersAndFormatters
    ```
3. To open the project in Visual Studio Code, run the following command:
    ```bash
    code .
    ```
4. Beside **CustomFiltersAndFormatters**, select **New Folder**, and then name it **Formatter**.
5. Right-click the **Formatter** folder, select **New File**, and then name it **ImageFormatter.cs**.
6. To **ImageFormatter.cs**, add the following code:
    ```cs
    using CustomFiltersAndFormatters.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Threading.Tasks;

    namespace CustomFiltersAndFormatters.Formatter
    {
        public class ImageFormatter : OutputFormatter 
        {
            public ImageFormatter()
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/png"));
            }

            public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
            {
                HttpResponse response = context.HttpContext.Response;
                Value value = context.Object as Value;
                if(value != null)
                    await response.SendFileAsync((value).Thumbnail);
            }
        }
    }
    ```
    In the **WriteResponseBodyAsync** method, get the **Value** class, copy the path of the image, and then send the image to the client.
7. To add the new **ImageFormatter** to **OutputFormatters**, click the **Startup** class, locate **ConfigureServices**, and then replace it with the following code:
    ```cs
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.OutputFormatters.Insert(0, new ImageFormatter());
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }
    ```
  
8. Expand **Controllers**, click **PassengerController**, and then view the class.
    >**Note**: The **GetPhoto** method returns the **Value** class but the client will get the photo through **ImageFormatter**.
9. Switch to the **Command Prompt** window.
10. To run the server, run the following command:
    ```cd
    dotnet run
    ```
11. Open Microsoft Edge, and then go to the following URL:
    ```url
    https://localhost:5001/api/passenger/photo/2
    ```
12. In Microsoft Edge, you should see an image.
13. Close all open windows.

# Lesson 3: Injecting Dependencies into Controllers

### Demonstration: Using Dependency Injection with Controllers

1. Open a **Command Prompt** window.
2. To change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\DependencyInjection\Starter
    ```
3. To restore all dependencies and tools of a project, run the following command:
    ```base
    dotnet restore
    ```
4. Open the project in Visual Studio Code, and then enter the following command:
    ```bash
    code .
    ```
5. Expand the **BlueYonder.Flights.DAL** project, right-click **Repository**, select **New File**, and then name it **IPassengerRepository.cs**.
6. To implement **IPassengerRepository**, enter the following code:
    ```cs
    using BlueYonder.Flights.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace BlueYonder.Flights.DAL.Repository
    {
        public interface IPassengerRepository
        {
            Task<IEnumerable<Passenger>> GetAllPassengers();
            Task<Passenger> GetPassenger(int passengerId);
            Task<Passenger> Add(Passenger newPassenger);
            Task<Passenger> Update(Passenger passengerToUpdate);
            void Delete(int passengerId);
        }
    }
    ```
7. In the **Repository** folder, click the **PassengerRepository** file.
8. To use the **IPassengerRepository** interface, locate the class declaration and replace it with the following code:
    ```cs
    public class PassengerRepository : IPassengerRepository
    ```
9. Expand the **BlueYonder.Flights.Service** folder, and then double-click the **Startup.cs** file.
10. To register the repository, locate the **ConfigureServices** method, and then add the following code:
    ```cs
    services.AddTransient<IPassengerRepository, PassengerRepository>();
    ```
11. In the **BlueYonder.Flights.Service** project, expand the **Controllers** folder, and then double-click the **PassengerController** file.
12. To use **IPassengerRepository**, locate the **_passengerRepository** field, and then replace it with the following code:
    ```cs
    private readonly IPassengerRepository _passengerRepository;
    ```
13. To get the **IHotelBookingRepository** interface as parameter, locate the class constructor and replace it with the following code:
    ```cs
    public PassengerController(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }
    ```
14. Switch to the **Command Prompt** window.
15. To change the directory to **BlueYonder.Flights.Service**, run the following command:
    ```bash
    cd BlueYonder.Flights.Service
    ```
16. To run the service, run the following command:
    ```cd
    dotnet run
    ```
17. Open Microsoft Edge, and then go to the following URL.
    ```url
    https://localhost:5001/api/passenger
    ```
18. Verify that you get all the data from the server.
19. Close all open windows

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
