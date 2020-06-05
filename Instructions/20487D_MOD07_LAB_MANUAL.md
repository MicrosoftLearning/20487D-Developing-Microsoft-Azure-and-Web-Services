# Module 7: Implementing Data Storage in Azure

#### Scenario

In this exercise, you will store files in Microsoft Azure Blob Storage, query graph data with Azure Cosmos DB, and cache out of process with Azure Redis Cache.

#### Objectives

After completing this lab, you will be able to:

- Store publicly accessible files in Blob Storage.
- Generate and store private files in Blob Storage.
- Create the Azure Cosmos DB graph database.
- Query the Azure Cosmos DB database.
- Create the Azure Redis Cache service.
- Access the cache service from code.

# Lab: Storing Files in Azure Storage

### Exercise 1: Store publicly accessible files in Microsoft Azure Blob Storage

#### Task 1: Store publicly accessible files in Blob Storage

1. Open the Azure portal.
2. Create a storage account, and name it **blueyonder**{YourInitials}.
3. Create a new blob container, name it **aircraft-images**, and then set its public access level as **Container (anonymous read access for containers and blobs)**.

#### Task 2: Upload a file to the Blob Storage container from the Azure CLI
1. To upload the **france.jpg** file to the **aircraft-images** blob container, at the Azure CLI, enter the following path:
   ```url
   [Repository Root]\AllFiles\Mod07\Labfiles\Lab1\Assets\france.jpg
   ```

#### Task 3: Run a script to upload multiple aircraft images

1. Open PowerShell, and then, in the **Assets** folder, run the **uploadPhotos** script.
2. To run the script, a key is required from the storage account.
   >**Note**: The script is to upload the images from the **Assets** folder to the **aircraft-images** blob container.

#### Task 4: Modify the flight reservations service to return aircraft images from the container

1. Open the **Starter** project.
2. In **AircraftController.cs**, locate the **_baseUrl** box, and then replace *YourInitials* with your actual initials.

#### Task 5: Test the service from a browser

1. At the command prompt, run the service.
2. Open Microsoft Edge and go to the following URL:
   ```url
   https://localhost:5001/api/aircraft/image/france
   ```
3. Copy the URL from the response.
4. Open a new tab in Microsoft Edge, and then in the address bar, paste the copied URL.
5. Verify that the image is shown.
6. Stop the service.

### Exercise 2:  Generate and Store Private Files in Blob Storage

#### Task 1: Create a Blob Storage container

1. Create a new container, name it **manifests**, and assign it **Private (no anonymous access)** accessibility.

#### Task 2: Generate a passenger manifest and store it in the container

1. Switch to the **Starter** project.
2. In **FlightsController.cs**, locate the **FinalizeFlight** method.
3. Refactor the method to upload the **manifests.txt** file to the **manifests** container by using the **GeneratedManifests** method. This method creates new manifest files.
   >**Note**: Use the **WindowsAzure.Storage** package.

#### Task 3: Generate a shared access signature (SAS) to the passenger manifest and return it from the service

1. Create an SAS token.
2. Add the **GetPassengerManifest** action that returns the file URL with the SAS token.
3. Switch to the command prompt and run the service.
4. Open Microsoft Edge and go to the following URL:
   ```url
   https://localhost:5001/api/flights/finalizeflight
   ```
5. Verify that the **manifests.txt** file was upload to the container.
6. Go to the following URL:
   ```url
   https://localhost:5001/api/flights/passengermanifest
   ```
7. Copy the URL from the response. In Microsoft Edge, open a new tab, and then on the address bar, paste the copied URL.
8. Verify that the Passenger manifest list is shown.
9. To access the storage account with the expired SAS token, wait for one minute, and then refresh the page.
10. Verify that the response is an error.

# Lab: Querying Graph Data with Azure Cosmos DB

### Exercise 1: Create the Cosmos DB Graph Database

#### Task 1: Create a new Cosmos DB graph database in the Azure portal

1. Open the Azure portal.
2. To create an Azure Cosmos DB graph database, in **Account Name** box type **blueyonder-destinations{YourInitials}**, and then, in **API**, select **Gremlin (graph)**. 
3. Click **blueyonder-destinations**{YourInitials}, and then type **Azure Cosmos DB account**.
4. To add a graph, in **Database id**, select **Create new**, type **blueyonder**, and then, in **Graph Id**, type **traveler**.

#### Task 2: Run a script to import itinerary data (flights and destinations) to the database

1. To upload data to the graph, from the **Assets** folder, upload the **GraphData.json** file.
2. In the **traveler** graph collection, explore all vertices and edges.

#### Task 3: Explore the generated database

1. In the **Paris** vertex data, view all the attractions.
2. Write a Gremlin query to view the **Moscow** vertex data.
3. Write a Gremlin query to get all the flights from **Moscow**.

### Exercise 2: Query the Cosmos DB Database

#### Task 1: Add a query to return related attractions from the itinerary service

1. In **Settings** section , click **Keys** and then copy the **PRIMARY KEY** value.
1. Open the **Starter** project.
2. Install the **Gremlin.Net** package.
3. In the **appsettings.json** file, update **HostName** and **Authkey**.
4. Open the **DestinationController** file.
5. Refactor the **GetAttractions** method to return all the attractions by giving a *destination* parameter, and then filter by using the *distanceKm* parameter.

#### Task 2: Add a query to return possible interesting stop-overs to a destination

1. Query all the flights from the source to the destination filtered by *maxDurationHours* in the **GetStopOvers** method:
   ```cs
   string gremlinQuery = $"g.V('{source}').repeat(outE().inV().simplePath()).until(hasId('{destination}')).path().by('id').by('duration')";
   ```
2. Run the query, which gets the flights that are shorter than the maximum duration, and then modify the data to sum the duration of each flight’s path.

#### Task 3: Test the new service operations from a browser

1. At the command prompt, run the service.
2. Open Microsoft Edge.
3. Go to the following URL and verify that the JSON response with attractions does not include Eiffel Tower:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/4
   ```
4. Go to the following URL and verify that the JSON response with attractions includes Eiffel Tower:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/6
   ```
5. Go to the following URL and verify that the JSON response includes only the flights from Paris:
   ```url
   https://localhost:5001/api/Destination/StopOvers/Moscow/New York/14
   ```
6. Go to the following URL and verify that the JSON response includes only the flights from Paris and Rome:
   ```url
   https://localhost:5001/api/Destination/StopOvers/Moscow/New York/20
   ```

# Lab: Caching Out-of-Process with Azure Redis cache

### Preparation Steps

1. Open PowerShell as Administrator and run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
2. Browse to [repository root]**\Mod07\Labfiles\Lab3\Setup**.
3. Run the following command:
   ```batch
    .\createAzureServices.ps1
   ```
4. Enter your details, sign in, and then follow the on-screen instructions. Wait for the deployment to complete successfully.
5. Write down the name of the Azure App Service that is created.

### Exercise 1: Create the Azure Redis Cache Service

#### Task 1: Create the Azure Redis Cache service

1. Go to the Azure portal.
2. To create an Azure Redis Cache, in **DNS name**, type **blueyonder-cache**{YourInitials}.

#### Task 2: Locate the service key

1. From the new Azure Redis Cache, copy the primary connection string.

### Exercise 2: Access the Cache Service from Code

#### Task 1: Install the StackExchange.Redis NuGet package

1. In the **Starter** folder, to the **BlueYonder.Flights.Service** project, add the **StackExchange.Redis** package.

#### Task 2: Configure the cache credentials

1. Open the project in Microsoft Visual Studio Code.
2. To the **appsettings.json** file, add the primary connection string copied from the Azure Redis Cache.

#### Task 3: Add code to use the cache service

1. In the **Startup.cs** file, register the **IConnectionMultiplexer** interface as a singleton.
2. In the **FlightsController.cs** constructor class, inject **IConnectionMultiplexer**.
3. To verify whether the data is cached, refactor the **Get** action.
    - If the data is cached, return the cache object and add a header that says the data is cached.
    - If the data is not cached, use **FlightsRepository** to get the data, and then add the new data to the Azure Redis Cache.
    - The key to the Azure Redis Cache is built by using the source, the destination, and the current date. 


#### Task 4: Configure the web application to disable instance affinity

1. Switch to the Azure portal.
2. In **App Services**, click **blueyondermod07lab03**{YourInitials}.
3. In the **SETTING** section, click **Scale up (App Service plan)**.
4. In the **Dev/Test** tab, select **B1**.
5. In the **SETTING** section, click **Scale out (App Service plan)**.
6. In the **Configure** tab, change the **Instance count** to **2**.
7. In the **SETTING** section, click **Application settings**.
8. Use the toggle to turn off ARR affinity.

#### Task 5: Deploy the application to a scaled web app

1. Publish the service.
   ```cs
   dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
   ```

### Exercise 3: Test the Application

#### Task 1: Run the application multiple times

1. Open Microsoft Edge and go to the following URL:
   ```url
   https://blueyondermod07lab3{YourInitials}.azurewebsites.net/api/flights
   ```
2. Check the **X-BlueYonder-Server** header and view the server name.

#### Task 2: Verify you are accessing multiple instances

1. Refresh the page a couple of times and verify that the **X-BlueYonder-Server** header has changed. 

#### Task 3: Verify you are getting the cached data

1. Go to the following URL:
   ```url
   https://blueyondermod07lab3{YourInitials}.azurewebsites.net/api/flights/New York/Paris/MM-DD-YYYY
   ```
2. Replace the end of the URL with the current date.
3. In the **Network** tab, locate the URL, and then, in the **Response Headers** section, locate the **X-Cache** header. If the **X-Cache** header is missing, it means that the result is not cached.
3. Refresh the page and get the **X-Cache** header.
4. Refresh the page twice and verify that the **X-BlueYonder-Server** header has changed, and the results are cached.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
