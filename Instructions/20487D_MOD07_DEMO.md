# Module 7: Implementing data storage in Azure

1. Wherever a path to a file starts with *[Repository Root]*, replace it with the absolute path to the folder in which the 20487 repository resides. 
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. (For example, the initials for **John Doe** will be **jd**.)
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and prepare them beforehand to save classroom time.

# Lesson 2: Accessing Data in Azure Storage

### Demonstration: Accessing Microsoft Azure Blob Storage from a Microsoft ASP.NET Core Application

#### Demonstration Steps

1. Open the Microsoft Azure portal.
2. Go to https://portal.azure.com and sign in with your credentials.
   >**Note**: If the Stay signed in? dialog box appears, click **Yes**.
3. In the left pane, click **Storage accounts**.
4. In the **Storage accounts** blade, click **Add**.
5. In the **Storage Account Name** box, type **blueyonder**{YourInitials}.
6. In the **Account kind** list, select **StorageV2 (general purpose v2)**.
7. In the **Replication** list, select **Locally-redundant storage (LRS)**.
8. In the **Resource group** section, select **Create new**, type **Mod7Demo1** and then click **OK**.
9.  Click **Review + Create** and then click **Create**.
10. Click **blueyonder**{YourInitials}.
11. In the left pane, in **BLOB SERVICE**, click **Blobs**.
12. To create a new container, on the top bar, click **Container**.
13. In the **Name** box, type **vouchers**.
14. In the **Public access level** list, select **Private (no anonymous access)**.
15. Click **OK**.
16. In the left pane, in the **SETTINGS** section, click **Access keys**.
17. Copy the **Key1** Connection string value.
18. Open the command prompt.
19. To change the directory, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod07\Demofiles\Mod7Demo1Blob
   ```
20. To open the project in Microsoft Visual Studio Code, run the following command:
   ```bash
    code .
   ```
21. Click the **appsettings.json** file, and then replace the **connection string**.
22. Expand **Controllers**, click **ReservationController.cs**, and then explore the following method:
    - Explore the constructor connected to **blob container**.
    - Explore the **CreateVoucher** method that creates and uploads the **voucher** file to the storage.
    - Explore the **GetVoucher** method that gets the file by id from the storage.
   >**Note**: Connection to storage is through the **WindowsAzure.Storage** NuGet.
23. To run the application, switch to the command prompt, and then run the following command:
   ```bash
    dotnet run
   ```
24. Open PowerShell.
25. To invoke the **CreateVoucher** action, run the following command:
   ```bash
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12;
    Invoke-WebRequest -Uri https://localhost:5001/api/reservation/createvoucher -ContentType "application/json" -Method POST -Body "'{Your Name}'"
   ```
   >**Note**: Replace **{Your Name}** with Your name.
26. From content, copy the **Guid** key.
27. Switch to the Azure portal, locate the **vouchers** container, and then verify that a new file was added.
28. To invoke the **GetVoucher** action and receive the new file from the blob container, switch to PowerShell, and then run the following command:
   ```bash
    $request = Invoke-WebRequest -Uri https://localhost:5001/api/reservation/Voucher/{Guid voucher}  -Method Get
    $request.Content
   ```
   >**Note**: Replace **{Guid voucher}** with Guid value you have noted in point 26.
29. From the blob container, view the content in the **vouchers** file. 


# Lesson 3: Working with Structured Data in Azure

### Demonstration: Uploading an Azure SQL Database to Azure and Accessing it Locally

#### Demonstration Steps

1. Open Microsoft Edge.
2. Go to **https://portal.azure.com** and sign in with your credentials.
3. In the left pane, click **Storage accounts**.
4. On the top bar, click **Add**.
5. In the **Storage account name** box, type **mod7demo2**{YourInitials}.
6. In **Resource group** section, select **Create new**, in the **Name** box type **Mod7demo2ResourceGroup** and then click **OK**.
7. Click **Review + Create** and then click **Create**.
8. After the new storage account is created, on the top bar, click **Refresh**.
9. Click the **mod7demo2**{YourInitials} storage account.
10. In the left pane, under the **BLOB SERVICE** section, click **Blobs**.
11. Click **+ Container**, type **bacpaccontainer**, and then click **OK**.
12. Click **bacpaccontainer**.
13. Click **Upload**.
14. Click folder, browse to *[Repository Root]***\Allfiles\Mod07\Demofiles\Mod7Demo2bacpac**, select the **Mod7Demo2DB.bacpac** file and then click **Open**.
15. Click **Upload**.
16. In the left pane, click **Create a resource**.
17. In the **search** box, type **SQL server**. 
18. Select **SQL server (logical server)**.
19. Click **Create**.
20. In the **Server name** box, type **mod7demo2sqlserver**{YourInitials}.
21. In the **Server admin login** box, type **MyAdmin**.
22. In the **Password** and **Confirm Password** boxes, type **Password123!**.
23. In **Resource group**, select **Use existing**, and then select the **Mod7demo2ResourceGroup** resource group.
24. Click **Create**.
25. Click **All resources**, and then click **mod7demo2sqlserver**{YourInitials}.
26. In the **mod7demo2sqlserver**{YourInitials} pane, on the top bar, click **Import database**.
27. In the **Import database** pane, click **Storage(Premium not supported)**, and then click **mod7demo2**{YourInitials}.
28. In the **Containers** pane, click **bacpaccontainer**, click the **Mod7Demo2DB.bacpac** file, and then click **Select**.
29. In the **Password** box, type **Password123!**.
30. Click **OK**.
31. After the database is created, in the left pane, click **SQL databases**.
32. Click **Mod7Demo2DB**.
33. Click **Set server firewall**.
34. Click **Add client IP**, and then click **Save**.
35. In success window click **OK**.
36. Open SQL Operations Studio.
37. In **Connection type**, select **Microsoft SQL Server**.
38. In the **Server** box, type **mod7demo2sqlserver**{YourInitials}**.database.windows.net**.
39. From the **Authentication type** drop down list select **SQL Login**.
40. In the **User name** box, type **MyAdmin**.
41. In the **Password** box, type **Password123!**.
42. Click **Connect**.
43. Now you can query the database locally.

### Demonstration: Using Microsoft Azure Cosmos DB with the MongoDB API

#### Demonstration Steps

1. Open Microsoft Edge.
2. Go to **https://portal.azure.com** and sign in with your credentials.
3. In the left pane, click **Azure Cosmos DB**.
4. On the top bar, click **Add**.
5. In the **Create Azure Cosmos DB Account** pane, in the **Account name** box, type **mod7demo3**{YourInitials}.
6. In **API**, select **MongoDB**.
7. In **Resource group** section, select **Create new**, in the **Name** box type **Mod7Demo3ResourceGroup** and then click **OK**.
8. Click **Review + Create** and then click **Create**.
9. Wait for the database to be created.
10. In the left pane, click **Azure Cosmos DB**, and then click **mod7demo3**{YourInitials}.
11. In **mod7demo3**{YourInitials}, in the left pane, click **Data Explorer**.
12. On the top bar, click **New Collection**.
13. In **Database id**, select **Create new**, and then type **mydb**.
14. In **Collection id**, type **customers**.
15. Click **Add unique key**, and then type **customerId**.
16. Click **Ok**.
17. On the top bar, click **New Collection**.
18. In **Database id**, select **Use existing**, and then select **mydb**.
19. In **Collection id**, type **orders**.
20. Click **Ok**.
21. Right-click **customers**, and then select **New Shell**.
22. In *[Repository Root]***\Allfiles\Mod07\DemoFiles\Mod7Demo3Assets**, from the **CustomersCollectionData.json** file, copy all the content.
23. In the **shell 1** console, paste the customer data copied from **CustomersCollectionData.json**, and then press **Enter**.
24. In *[Repository Root]***\Allfiles\Mod07\DemoFiles\Mod7Demo3Assets**, from the **OrdersCollectionData.json** file, copy all the content.
25. In **shell 1** console, paste the orders data copied from **OrdersCollectionData.json**, and then press **Enter**.
26. Right-click **orders**, and then select **New Query**.
27. To get all the orders, paste the following query, and then click **Execute Query**.
   ```json
    {}
   ```
28. To get all the orders at a price greater than $20, paste the following query, and then click **Execute Query**.
   ```query
    { price: {$gt: 20} }
   ```
29. To get all the orders of customer 1, paste the following query, and then click **Execute Query**.
   ```query
    { customerId: "1" }
   ```
30. Close all windows.

### Demonstration: Using Cosmos DB with a Graph Database API

#### Demonstration Steps

1. Open Microsoft Edge.
2. Go to **https://portal.azure.com** and sign in with your credentials.
3. In the left pane, click **Azure Cosmos DB**.
4. On the top bar, click **Add**.
5. In **Create Azure Cosmos DB Account**, in the **Account name** box, type **mod7demo4**{YourInitials}.
6. In **API**, select **Gremlin (graph)**.
7. In **Resource group** section, select **Create new**, in the **Name** box type **Mod7Demo4ResourceGroup**, and then click **OK**.
8. Click **Review + Create** and then click **Create**.
9. Wait for the database to be created.
10. In the left pane, click **Azure Cosmos DB**, and then click **mod7demo4**{YourInitials}.
11. On the top bar, click **+ Add Graph**.
12. In **Database id**, select **Create new**, and then type **mygraphgdb**.
13. In **Graph Id**, type **taskManager**, and then click **OK**.
14. Expand **mygraphdb** and click **taskManager**. 
15. To upload all the data with the JSON file, on the top bar, click **Upload**.
16. click **folder**, in *[Repository Root]***\Allfiles\Mod07\DemoFiles\Mod7Demo4Assets**, select the **GraphData.json** file, and then click **Open**.
17. Click **Upload**.
18. Expand **taskManager**, and then click **Graph**.
19. To get all the vertices in the graph, click **Execute Gremlin Query**.
20. Explore all the graph connections.
21. To view the result of **gremlin query** in JSON format, click the **JSON** tab.
22. To get all the edges from the **Lab 1** vertex, type **g.V('Lab 1').outE('assigned-to')**, and then click **Execute Gremlin Query**.
23. View the JSON result that shows all the users who are assigned to the **Lab 1** task. 
24. To get all the edges from the **Sean Stewart** vertex, type **g.V('Sean Stewart').inE('managed-by')**, and then click **Execute Gremlin Query**.
25. View the JSON result that shows all the users managed by **Sean Stewart**.
26. Close all windows. 

# Lesson 4: Geographically Distributing Data with Azure CDN

### Demonstration: Configuring a CDN Endpoint for a Static Website

#### Demonstration Steps

1. Open Microsoft Edge.
2. Go to **https://portal.azure.com** and sign in with your credentials.
   >**Note**: If the Stay signed in? dialog box appears, click **Yes**.
3. In the left pane, click **Storage accounts**.
4. On the top bar, click **Add**.
5. In **Create storage account** page, in the **Storage account name** box, type **mod7demo5**{YourInitials}.
6. In **Account kind**, select **StorageV2(general purpose v2)**.
7. In **Resource group** section, select **Create new**, in the **Nane** box type **Mod7Demo5ResourceGroup** and then click **OK**.
8. Click **Review + Create** and then click **Create**.
9.  Wait for the storage account to be created.
10. In the left pane, click **Storage accounts**, and then click **mod7demo5**{YourInitials}.
11. In **mod7demo5**{YourInitials} pane, under **SETTINGS**, click **Static website(Preview)**.
12. Click **Enabled**.
13. On the top bar, click **Save**.
14. Click the **$web** container that was created by enabling the static website.
15. On the top bar, click **Upload**.
16. Click **Folder**, browse to *[Repository Root]***\Allfiles\Mod07\DemoFiles\Mod7Demo5Assets**, and then select all the files in this repository.
17. Click **Upload**.
18. Return to **mod7demo5**{YourInitials} **- Static website (preview)**.
19. In **Index document name**, type **index.html**.
20. On the top bar, click **Save**.
21. Copy the primary endpoint value for later use.
   >**Note**: You can now browse to the primary endpoint to view **index.hml** or **airplane1.jpg**.
22. In the left menu, click **+ Create a resource**.
23. In the **search** box, type **CDN**, and then click **Create**.
24. In **CDN profile**, in **Name**, type **MyCDNProfile**{YourInitials}.
25. In **Resource group**, select **Use existing**, and then select **Mod7Demo5ResourceGroup**.
26. In **Pricing tier**, select **Premium Verizon**.
27. Select **Create a new CDN endpoint now**.
28. In **CDN endpoint name**, type **mod7demo5endpoint**{YourInitials}.
29. In **Origin type**, select **Custom origin**.
30. In **Origin hostname**, paste the primary endpoint value that was copied earlier. It will have the following format: *account-name.zone-name.web.core.windows.net*.
31. Click **Create**.
32. Wait until the CDN profile and the endpoint are created.
   >**Note**: It can take up to one hour to synchronize between the storage static website and the endpoint.
33. Click **All resources**, then click **mod7demo5endpoint**{YourInitials}.
34. Click **Endpoint hostname**.
    >**Note**: At this point, the **My Static Website** message should appear. If it does not, wait a little more.
35. Open Microsoft Edge, press F12, and then click the **Network** tab.
36. In **URL**, type **/airplane1.jpg**, and then press Enter.
37. In the **Network** tab, click **airplane1.jpg** and in **Response Headers**, verify that **x-cache: HIT** is not present.
38. Refresh the page five times.
39. In **Response Headers**, verify that **x-cache: HIT** is present.
40. Close all windows.

# Lesson 5: Scaling with Out-of-Process Cache

### Demonstration: Using Microsoft Azure Redis Cache for Caching Data

#### Demonstration Steps

1. Open the Azure portal.
2. In the left pane, click **+ Create a resource**.
3. In the **search** box, type **Redis Cache**, and then click **Create**.
4. In **DNS name**, type **mod7demo6redis**{YourInitials}.
5. In **Resource Group**, select **Create new**, in the **Name** box type **Mod07Demo6ResourceGroup** ad then click **OK**.
6. Click **Create**, and then wait until the service is created.
7. To display all resources, in the left pane, click **All resources**.
8. Click **mod7demo6redis**{YourInitials}.
9. Under the **SETTINGS** section, click **Access keys**.
10. Copy **StackExchange.Redis**, which is the primary connection string, for later use.
11. Open the command prompt.
12. To change the directory to **BlueYonder.Hotels.Service**, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo6Redis\BlueYonder.Hotels.Service
   ```
13.  To open the project in Visual Studio Code, run the following command:
   ```bash
    code .
   ```
14. Click the **appsettings.json** file, and paste the primary connection string you copied earlier.
15. From the **Controllers** folder, open the **HotelsController.cs** file, and then explore the following: 
    - Constructor injection of the Azure Redis Cache connection.
    - The **Get** method, which gets data from the cache as long as it's available. If the data is not available in the cache, the method gets the data from the repository and caches it for one minute.
    - The **Post** method, which adds new hotels to the repository.  
16. To run the service, switch to the command prompt, and then run the following command:
   ```bash
    dotnet run
   ```
17. Open Microsoft Edge and browse to the following URL:
    ```url
    https://localhost:5001/api/hotels
    ```
18. Verify that there is a response with an array of hotels.
19. Press **F12**, and then click the **Network** tab.
20. Refresh the page, and then verify that the response header has **X-cache: true**.
21. Open PowerShell.
22. To add a new hotel to the repository, run the following command:
   ```bash
   [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
   $hotelName = Read-Host -Prompt 'Enter hotel name' 
   Invoke-WebRequest -Uri https://localhost:5001/api/hotels -ContentType "application/json" -Method POST -Body "'$hotelName'"
   ```
23. Refresh the browser page again and check if the new hotel was added to the list.
   >**Note**: The cache expires after one minute.
24. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
