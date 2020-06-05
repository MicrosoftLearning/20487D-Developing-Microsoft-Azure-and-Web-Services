# Module 6: Deploying and Managing Services

1. Wherever a path to a file starts at *[Repository Root]*, replace it with the absolute path to the directory in which the 20487 repository resides. For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *{YourInitials}* appears, replace it with your actual initials. For example, the initials for **John Doe** will be **jd**.
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and then prepare them beforehand to save classroom time.


# Lesson 1: Web Deployment with Microsoft Visual Studio

### Demonstration: Deploying a Web Application with Visual Studio

#### Demonstration Steps

1.  Click **Start**, type **Visual Studio**. From the search results, right-click **Visual Studio 2017**, and then select **Run as administrator**. In the **User Control** dialog box,  click **Yes**.
2.  On the **File** menu, point to **New**, and then click **Project**.
3.  In the **New Project** dialog box, on the navigation pane, expand the  **Installed** node, expand the **Visual C\#** node, click the **Web** node, and then in the list of templates, click **ASP.NET Core Web Application**.
4.  In the **Name** box, type **MyWebSite**.
5.  In the **Location** box, enter *[repository root]***\Allfiles\20487C\Mod06\DemoFiles\DeployWebApp**.
6.  Clear the **Create directory for solution** check box, and then click **OK**.
7.  In the **New ASP.NET Core Web Application - MyWebSite** dialog box, select **API**, and then click **OK**.
8.	Open Microsoft Edge and go to **https://portal.azure.com**.
9.	If a page appears asking for your email address, enter your email address, and then click **Next**. Wait for the **Sign In** page to appear, enter your email address and password, and then click **Sign In**.

	>**Note**: If during sign in, a page appears asking you to choose from a list of previously used accounts, select the account you previously used, and then enter your credentials.

10.	In the left menu of the portal, click **App Services**.
11. In the action bar, click **Add**, select **Web App**, and then at the bottom of the third screen, click **Create**.
12.	In the **App name** box, enter the name **mod6demo1***YourInitials* (Replace *YourInitials* with your initials). This is a unique value, which when combined with the **.azurewebsites.net** suffix is used as the URL to your web app.
13. In the **Resource Group** section, select **Create new**, and then change the name of the resource group to **BlueYonder.Demo.06**.
14.	Click **App Service Plan/Location**, and then click **Create new**.
15. In the **App Service Plan** box, type **BlueYonderDemo06**.
16.	In the **Location** list, select the region closest to your location.
17. In the **Pricing tier** section, select **D1 Shared**, and then click **OK**.
18. Click **Create**. The site is added to the **Web Apps** table and its status is set to **Creating**. 
19.	After the status changes to **Running**, in **MyWebSite - Microsoft Visual Studio(Administrator)**, on the **View** menu, click **Server Explorer**.
20.	In **Server Explorer**, right-click the **Azure** pane and select **Connect To Microsoft Azure Subscription**.
21.	On the **Sign In** page, if a page appears asking you to choose from a list of previously used accounts, select the account you previously used, enter your credentials, and then click **Sign in**.

    >**Note**:	Wait until the sign-in process completes.
        You only need to perform this step once to import your Microsoft Azure account settings to Visual Studio.
        Now Visual Studio 2017 can display the list of Web Apps and Azure Cloud Services to which you can deploy the applications.

22.	In **MyWebSite - Microsoft Visual Studio(Administrator)**, in **Solution Explorer**, right-click **MyWebSite** project, and then select **Publish**.
23.	In the **Pick a publish target** dialog box, select **App Service**, then in the **Azure App Service** view, select the **Select Existing** option, and then click **Create Profile**.
24.	In the **App Service** dialog box, expand the **BlueYonder.Demo.06** folder, select **mod6demo1[YourInitials]**, and then click **OK**.
25. Click **Publish**.
    >**Note**: Visual Studio publishes the application according to the settings that are provided in the profile file. After deployment completes, Visual Studio opens Internet Explorer and displays the web app. The deployment process is quick because the process only copies the content of the application to an existing virtual machine and does not need to wait for a new virtual machine to be created.
26. In the browser, navigate to the following URL:
    ```url
    http://mod6demo1{YourInitials}.azurewebsites.net/api/values
    ```
27. Verify that the response is in the format: **["***value1***","***value2***"]**.
28. Close all open windows.

# Lesson 2: Web Deployment on Linux

### Demonstration: Deploying an ASP.NET Core Web Service with Nginx

#### Demonstration Steps

1. Open Microsoft Edge.
2. Navigate to **https://portal.azure.com**.
3. On the left menu panel, click **Virtual machines**.
4. On **Virtual Machines** blade, click **Create virtual machine**. On next page make sure it selects the latest version of **Ubuntu Server** in image column.
5. To create a new virtual machine, in the **Basics** view, provide the following details:
   - In the **Virtual Machine Name** box, type **myvm**.
   - Click **Disks** and from the **OS disk type list** select **Standard HDD**.
   - In the **Username** box, type **myadmin**.
   - In the **Authentication type** list, select **Password**, and then  in the **Password** and **Confirm Password** boxes, type **Password123!**.
   - In the **Resource group** section click **Create new** and type **Mod6Demo2ResourceGroup** and then click OK.
   - Click **OK**.
6. In the **Size** section click **change size** and provide the following details:
   - Select **D2s_v3**.
   - Click **Select**.
7. In the **Inbound Port Rules** section , select **Allow selected ports** and provide the following details:
    - In **Select public inbound ports**, select the following: **HTTP(80)**, **HTTPS(443)**, and **SSH(22)**.
    - Click **OK**.
8.  In the **Summary** view, click **Review + Create** and then click **Create**.
9.  Open the command prompt.
10. At the command prompt, to change the directory to **Demo2Project**, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod06\Demofiles\Demo2Project
    ```
11. To open the project in Microsoft Visual Studio Code, run the following command:
    ```bash
    code .
    ```
12. In Demo2Project - Visual Studio Code, on the left menu, double-click **Demo2Project.csproj**.
13. In the **Demo2Project.csproj** file, locate the **\<PropertyGroup\>** element, and inside it, paste the following code:
    ```xml
    <RuntimeIdentifiers>win10-x64;osx.10.11-x64;ubuntu.16.10-x64</RuntimeIdentifiers>
    ```
14. Switch to the command prompt.
15. At the command prompt, to publish the app as **Self-contained**, run the following command:
    ```bash
    dotnet publish -c release -r ubuntu.16.10-x64
    ```
16. Switch to Azure Portal.
17. Click **Virtual machines**, and then select **myvm**.
18. In the top panel, click **Connect** and copy the **Login using VM local account** value.
19. Switch to the command prompt.
20. Paste the **Login using VM local account** value and press Enter.
21. At the command prompt, enter **yes**.
22. Type the password **Password123!**.
23. To switch the directory to **var** folder, paste the following command:
    ```bash
    cd /var
    ```
24. To create a new folder named **demo**, paste the following command:
    ```bash
    sudo mkdir demo
    ```
25. To change ownership of the directory, paste the following command:
    ```bash
    sudo chown myadmin demo
    ```
26. Open a new command prompt instance.
27. To change the directory to the published folder, paste the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod06\Demofiles\Demo2Project\bin\release\netcoreapp2.1
    ```
28. To publish the package to the virtual machine, paste the following command:
    ```bash
    scp -r .\ubuntu.16.10-x64\ myadmin@<server_IP_address>:/var/demo
    ```
    >**Note**: Change *\<server_IP_address\>* with the IP address of your virtual machine.
29. Type the following password **Password123!** and press Enter.
30. Switch to the command prompt that connected to the virtual machine.
31. To switch the directory to the **Root** folder, run the following command:
    ```bash
    cd /
    ```
32. To install **Nginx**, run the following commands:
    ```bash
    sudo -s
    nginx=stable
    add-apt-repository ppa:nginx/$nginx
    apt-get update
    apt-get install nginx
    ```
33. When finished, press Enter again.
34. When the question: **Do you want to continue?** appears, type **y**, and then press Enter.
35. To start the **Nginx** service, run the following command:
    ```bash
    sudo service nginx start
    ```
36. Verify that the browser displays the default landing page for **Nginx**. The landing page is:
    ```url
    http://<server_IP_address>/index.nginx-debian.html
    ```
37. To change the directory to the **Nginx** configuration folder, run the following command:
    ```bash
    cd /etc/nginx/sites-available/
    ```
38. To open the configuration file in the **Vim** text editor, run the following command:
    ```bash
    vi default
    ```
39. To change to edit mode, press ESC + I.
40. Replace the **Location** content with the following code:
    ```bash
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    ```
41. To save the file and exit, press **ESC** + **:** + **x** + **Enter**.
42. To verify that the syntax of the configuration file is correct, run the following command:
    ```bash
    sudo nginx -t
    ```
43. To force Nginx to pick up the the changes, run the following command:
    ```bash
    sudo nginx -s reload
    ```
44. To switch the directory to the publish folder, run the following command:
    ```bash
    cd /var/demo/ubuntu.16.10-x64/publish/
    ```
45. To execute the premissions to binary, run the following command:
    ```bash
    chmod a+x ./Demo2Project
    ```
46. To run the app, run the following command:
    ```bash
    ./Demo2Project
    ```
47. Open Microsoft Edge.
48. Navigate to the following URL:
    ```url
    <server_IP_address>/api/values
    ```
49. Verify the response is:
    ```json
    ["value1","value2"]
    ```

# Lesson 3: Continuous Delivery with Microsoft Visual Studio Team Services

### Demonstration: Continuous Delivery to Websites with Git and Visual Studio Team Services

#### Preparation Steps
  
To present this demonstration, you must have a Microsoft account. If you have not created a Microsoft account before, create one before you start the demonstration.

> **Important**! Make sure you are connected to your Microsoft account in Visual Studio 2017 before starting this demonstration.

#### Demonstration Steps

1. Open Microsoft Edge.
2. Navigate to **https://portal.azure.com**.
3. If a page appears asking for your email address, enter your email address, and then click **Next**, and enter your password, and then click **Sign In**.
4. If the **Stay signed in?** dialog box appears, click **Yes**.
   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.
5. To display all the Microsoft Azure App Services, on the left menu panel, click **App Services**.
    - Select the app service template, in the **App Services** blade, click **Add**. 
    - In the **Web** blade, click **Web App**. You see an overview of the template. Click **Create**.
6. To create  a new web app, enter details in the following fields:
    - In the **App Name** box, type the following web app name **mod6demo3** **{YourInitials}**.
        >**Note**: The new web app name will be part of the URL.
    - In the **Resource Group** list, select **Create new**, and then type **mod6demo3**.
    - Click **App Service plan/Location**, and click **Create new**, and then open the **New App Service Plan** blade to enter the following information:
        - In the **App Service plan** box, enter **Mod6Demo3ServicePlan**.
        - Click **OK**.
    - Click **Create** and wait until the app service is created.
7. Open a new tab in the browser and navigate to **https://www.visualstudio.com/team-services/**, and then click **Get Azure DevOps free**.
8. On the **Sign in** page, enter your **Microsoft account** email address, and then click **Next**.
    >**Note**: If instead of a **Sign in** page, you see a **Pick an account** page, pick your account and click **Next**.
    - On the **Enter password** page, enter your password, and then click **Sign in**.
    >**Note**: If this is the first time you logged in with your account to Visual Studio Team Services, follow the next steps, else skip to step 11.
9. If you already have projects in your account, click **New Project**, else skip to the next step.
10. In the **Create new project** page, enter the following details:
    - Project name: **MyApp**
    - Visibility: **Private**
    - Advance:
        - Version control: **Git**
    - Click **Create**. 
    > **Note**: Wait for the project to be created.
11. In the **MyApp** page, click **Repos** on the left blade then click **Generate Git credentials**, and then enter the following details:
    - Alias: **mod6demo3{YourInitials}**
    - Password: **Password123**
    - Confirm Password: **Password123**
    - Click **Save Git Credentials**.
12. In the left blade, click **Pipelines**.
13. Click **New pipeline**.
14. In **Where is your code**, choose **Use the visual designer**.
15. In **Select a source**, choose **Azure Repos Git**.
16. In **Repository** select **MyApp**.
17. Then click **continue**.
18. In **select a tamplate**, choose **Azure Web App for ASP.NET** and click **Apply**.
19. In **Azure subscription**, enter the following details:
    - Select your Azure subscription.
    - Click **Authorize**.
    - In the pop-up window, sign in with your Microsoft Azure credentials.
20. In **App service name**, select **mod6demo3**{YourInitials}.
21. Click the **Triggers** tab, and then under **Branch filters**, click **Add**.
    >**Note:** if **Branch filters** already exist skip this step.
22. Click the **Save & queue** tab, select **Save**, and then click **Save** again in the pop-up window. 
23. Open the command prompt.
24. At the command prompt, to switch the directory, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod06\Demofiles
    ```
25. To clone the repository to a local repository, run the following command:
    ```bash
    git clone  https://dev.azure.com/{_youraccount_}/MyApp/_git/MyApp
    ```
    > **Note**: Replace *_youraccount_* with your user name.
26. Then enter your user name and password.
    - UserName: **{_youraccount_}**
    - Password: **Password123**
27. To switch the directory to **MyApp**, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod06\Demofiles\MyApp
    ```
28. To create a new WebApi project, run the following command:
    ```bash
    dotnet new webapi -n MyProject
    ```
29. to create a new solution, run the following command:
    ```bash
    dotnet new sln -n Mod6Demo3
    ```
30. To add the **MyApp** project to the **Mod6Demo3** solution, run the following command:
    ```bash
    dotnet sln Mod6Demo3.sln add MyProject\MyProject.csproj
    ```
31. To add the new files for the next commit, run the following command:
    ```bash
    git add .
    ```
32. To commit the changes, run the following command:
    ```bash
    git commit -m "my first commit"
    ```
33. To push the changes to our repository, run the following command:
    ```bash
    git push
    ```
34. Return to **Azure DevOps**, click on **Pipelines** and then click on **Builds**.
35. Locate **my first commit**, click on it and verify that all **succeded**.
    > **Note**: Wait until the build is complete. 
36. Navigate to the web app URL that was created in Azure:
    ```url
    https://Mod6Demo3{YourInitials}.azurewebsites.net/api/values
    ```
37. The response should be a JSON with the following values:
    ```json
    ["value1", "value2"]
    ```
38. Close all windows.


# Lesson 4: Deploying to Staging and Production Environments 

### Demonstration: Using Deployment Slots with Web Apps

#### Demonstration Steps

1. Open Microsoft Edge.
2. Navigate to **https://portal.azure.com**.
3. If a page appears asking for your email address, enter your email address, and then click **Next** and enter your password, and then click **Sign In**.
4. If the **Stay signed in?** dialog box appears, click **Yes**.
   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.
5. To display all the app services, on the left menu panel, click **App Services**.
    - To select the app service template, in the **App Services** blade, click **Add**.
    - In the **Web** blade, click **Web App**. An overview of the template will be shown. Click **Create**.
6. To create a new web app, provide the following details:
    - In the **App Name** box, type the following web app name **mod6demo4**{YourInitials}.
        >**Note**: The new web app name will be part of the URL.
    - In **Resource Group**,  select **Create new** and type **mod6demo4**.
    - Click **App Service plan/Location**,  click **Create new**, and then open the **New App Service Plan** blade to provide the following information:
        - In the **App Service plan** box, type **Mod6Demo4ServicePlan**.
        - Click **Pricing tier**.
            - Go to the **Production** tab.
            - In **Recommended pricing tiers**, select **S1**.
            - Click **Apply**. 
        - Click **OK**.
    - Click **Create** and wait until the app service is created.
7. To display all the app services, on the left menu panel, click **App Services**.
8. Click **mod6demo4**{YourInitials} app service. 
9. To add credentials to the app service, under the **DEPLOYMENT** section, click **Deployment center**, in the open page select **FTP**, click **Dashboard** and then enter the following information:
    - In **FTP** Pane click **User Credentials**, in the **username** box type **FTPMod6Demo4**{YourInitials}.
    - In the **Password** and **Confirm password** boxes, type **Password99**.
    - Click **Save Credentials**.
10. On the left blade menu, under the **DEPLOYMENT** section, click **Deployment slots**.
    - Click **Add Slot** and provide the following information:
        - In **Name**, type **Staging**.
        - In **Configuration Source**, select **mod6demo4**{YourInitials}.
        - Click **OK**.
11. Open the command prompt.
12. At the command prompt, to change the directory to the starter project, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod06\Demofiles\SimpleServiceForDeploymentSlots
    ```
13. Open the project in Visual Studio Code and paste the following command and press Enter:
    ```bash
    code .
    ```
14. Expand the **Properties** folder, expand the **PublishProfiles** folder,  and then double-click **Azure.pubxml**.
15. Replace *{YourInitials}* with your initials from the **Azure web App**.
16. In **PublishProfiles**, double-click **Staging.pubxml**.
17. Replace *{YourInitials}* with your initials from the **Azure web App**.
18. Switch to the command prompt, and paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
    > **Note**: If there was an error in the publishing process, restart the **mod6demo4**{YourInitials} app services.
19. Open Microsoft Edge. 
20. Navigate to the following URL:
    ```url
    https://Mod6Demo4{YourInitials}.azurewebsites.net/api/values
    ```
21. The response should be a JSON with the following values:
    ```json
    ["value1", "value2"]
    ```
22. Switch to Visual Studio Code.
23. Expand **Controllers** and double-click **ValuesController.cs**.
24. Locate the **Get** method and add to the array **value3**.
25. Switch to the command prompt.
26. To publish in the staging slot, at the command prompt, paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Staging /p:Configuration=Release
    ```
    > **Note**: If there was an error in the publishing process, restart the **Mod6Demo4{YourInitials}-staging** app services.
27. Open a new Microsoft Edge instance. 
28. Navigate to the following URL:
    ```url
    https://mod6demo4-{YourInitials}-staging.azurewebsites.net/api/values
    ```
29. The response should be a JSON with the following values:
    ```json
    ["value1", "value2", "value3"]
    ```
30. Switch to the Azure Portal.
31. To display all the app services, on the left menu panel, click **App Services**.
32. Click the **mod6demo4**{YourInitials} app service.
33. In the **Overview** blade, on the menu at the top, click **Swap**.
34. In the **Swap** blade, do the following:
    - In **Swap type**, select **Swap**.
    - In **Source**, select **production**.
    - In **Destination**, select **Staging**.
    - Click **OK**. 
35. Switch to Microsoft Edge with the production URL.
36. To refresh the page, press F5.
37. The response should be a JSON with the following values:
    ```json
    ["value1", "value2", "value3"]
    ```
38. Close all open windows.

# Lesson 5: Defining Service Interfaces with Azure API Management 

### Demonstration: Importing and Testing an OpenAPI Specification

#### Demonstration Steps

1. Open Microsoft Edge.
2. Navigate to **https://portal.azure.com**.
3. If a page appears asking for your email address, enter your email address, click **Next**, enter your password, and then click **Sign In**.
4. If the **Stay signed in?** dialog box appears, click **Yes**.
   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.
5. On the menu in the panel on the left side, click **Create a resource**.
6. In the **Search** box, type **API management**, click **Enter**, and then click **Create**.
7. In the **API Management service** page, enter the following details:
   - Name: **Mod6demo5**{YourInitials}
   - Resource Group:
        - Select **Create new**.
        - Type **Mod6Demo5ResourceGroup**.
   - Organization name: Enter the name of your organization
   - Click **Create**
8. Wait until the API Management instance is created.
9. Click **All resources**, and then click on the new **API Management** section that was created.
10. Under the **API MANAGEMENT** section, click **APIs**.
11. Click **OpenAPI Specification**, and then enter the following details:
    - In **OpenAPI Specification** box, paste the following URL: **http://conferenceapi.azurewebsites.net/?format=json**
    > **Note**: The following URL is a Swagger JSON API that was provided by Microsoft and hosted on Microsoft Azure.
    - In **Products**, select **Unlimited**.
    - Click **Create**.
12. In the **All APIs** section, click **Demo Conference API**.
13. Click the **Test** tab, then click **GetSessions**.
14. Click **Send**, and check that the response is **200** with a collection of sessions.
15. Click **GetSession**, then inside the **value** box, type **100**;
16. Click **Send**, and check that the response is **200** with session data.
    >**Note**: The next demo will continue from this point.

# Lesson 5: Defining Service Interfaces with Azure API Management 

### Demonstration: Limiting Call Rates Using API Management

#### Preparation Steps

This demo continues from the previous demo.

#### Demonstration Steps

1. Click **All resources** and then click **API Management service** from the previous demo.
2. In the **API MANAGEMENT** section, click **APIs**.
3. In the **All APIs** section, click **Demo Conference API**.
4. Select **All operations**.
5. In the **Inbound processing** window, click the triangle (next to the pencil) and select **Code editor**.
6. Position the cursor inside the **\<inbound\>** element.
7. In the window on the right side, under **Access restriction policies**, click **+ Limit call rate per key**.
8. Modify your **rate-limit-by-key** code (in the **\<inbound\>** element) to the following code:
   ```xml
    <rate-limit-by-key calls="2" renewal-period="60" counter-key="@(context.Subscription.Id)" />
   ```
9. Click **Save**.
10. Select **Demo Conference API**.
11. Click the **GetSessions** operation.
12. Select the **Test** tab.
13. Click **Send** two times in a row.
14. After sending the request two times, you get a **429 Too many requests** response.
15. Wait for 60 seconds and then click **Send** again. This time you should get a **200 OK** response.
16. Close all windows.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
