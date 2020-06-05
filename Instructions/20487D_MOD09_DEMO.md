# Module 9: Securing services on-premises and in Azure

1. Wherever a path to a file starts with *[Repository Root]*, replace it with the absolute path to the folder in which the 20487 repository resides.
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *[YourInitials]* appears, replace it with your actual initials. (For example, the initials for **John Doe** will be **jd**.)
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and prepare them beforehand to save classroom time.


### Demonstration: Using ASP.NET Identity


### Demonstration: Creating an Azure Active Directory and Users

#### Preparation Steps

  You need two available emails, one for the Azure portal and the other for creating a new user.

#### Demonstration Steps

1. Open Microsoft Azure portal.
2. On the left menu, click **Azure Active Directory**.
3. In the **Azure Active Directory** blade, click **Custom domain names**, and then copy the value in the **NAME** field.
4. Go back to the **Azure Active Directory** blade, in the **Manage** section, click **Users**.
5. In the top menu, click **New user**.
    - In the **Name** box, enter your full name.
    - In the **User name** box, enter your name + **@***[Paste Domain name]*. (Domain name which you have copied in step 3)
    - Click **Create**.
6. In the top menu, click **New guest user**.
7. Enter the email address in the box, and then click **Invite**.
8. Open the email and accept the invitation.
9. In the portal, go back to **Azure Active Directory**.
10. In the **Manage** section, click **Groups**.
11. In the top menu, click **New group**.
    - In the **Group type** list, select **security**.
    - In the **Group name** box, enter **Mod9Group**.
    - In the **Membership type** list, select **Assigned**.
    - Click **Members**, select the guest user, and then click **Select**.
    - Click **Create**.
12. Open **Groups**, see that the group is created.
13. Close all open windows.


### Demonstration: Securing an ASP.NET Core application using OpenID Connect and AAD

#### Preparation Steps

1. Open Azure Portal.
2. On the left-hand side menu, click **Azure Active Directory**.
3. In the **Azure Active Directory** blade, click **App registrations**, and then click **New application registration**.
   - In the **Name** box, enter **Mod09OpenIdConnect**.
   - In the **Sign-on URL** box, enter **https://localhost:5001/signin-oidc**.
   - Click **Create**.
4. Copy the **Application ID**. This is the Client ID.
5. Close the registered app window.
6. Click **Endpoints**, copy one of the links, and then extract the GUID. This is the TenentID.

#### Demonstration Steps

1. Open the command prompt.
2. To change the current directory to **DemoFiles**, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod09\Demofiles\OpenIDConnectAAD\OpenIDConnectAAD
   ```
3. To open the project in Microsoft Visual Studio Code, run the following command:
   ```bash
    code .
   ```
4. Open the **appsettings.json** file, and then fill **ClientId** and **Authority** with the data from the preparation steps.
5. Switch to the command prompt.
6. To run the project, run the following command :
    ```bash
    dotnet run    
    ```
7. Open a browser and navigate to the following URL:
    ```url
    https://localhost:5001/api/values
    ```
8. You will see your username as part of the result.
9. Close all open windows.

### Demonstration: Using AAD B2C with ASP.NET Core

#### Preparation Steps

1. Open Azure Portal.
2. In the left pane, click **+ Create a resource**.
3. In the search box, type **Azure Active Directory B2C**, from the search results select **Azure Active Directory B2C**, and then click **Create**.
4. Select **Create a new Azure AD B2C Tenant**.
5. Enter the **Organization name** and **Initial domain name**, and then click **Create**.
6. After the tenant is created, click **Directory and subscription filter**, and then select your tenant.
7. In the top-left corner of the Azure portal, choose **All services**, and then search for and select **Azure AD B2C**.
8. From the blade, select **Applications**, and then click **Add**.
9. Fill the name of the application.
10. Under **Web App / Web API**, select **Yes**.
11. Under **Reply URL**, add **https://jwt.ms**, and then click **Create**.
12. From the blade, select **User flows (policies)**, click **New user flow**, and then select **Sign-up and sign in**.
13. Enter a policy name for your application to reference.
14. Under the identity providers select **Email signup**. Optionally, you can also select social identity providers, if already configured. Click **OK**.
15. Under the **User attributes and claims**, choose return claim check box for **Given name**.

#### Demonstration Steps

1. Open the command prompt.
2. To change the current directory to **DemoFiles**, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod09\Demofiles\AADB2C
   ```
3. To open the project in Visual Studio Code, run the following command:
   ```bash
    code .
   ```
4. Open the **appsettings.json** file, and then fill **ClientId** and **TenantName** with the data from the preparation steps.
5. Switch to the command prompt.
6. To run the project, run the following command:
    ```bash
    dotnet run    
    ```
7. Open a browser and navigate to the following URL:
    ```url
    https://[TenantName].b2clogin.com/[TenantName].onmicrosoft.com/oauth2/v2.0/authorize?p=[Policy name]&client_id=[ClientId]&nonce=defaultNonce&redirect_uri=https%3A%2F%2Fjwt.ms&scope=openid&response_type=id_token&prompt=login
    ```
8. Copy the token from the box
9. Open the new  Command prompt.
10. To change the current directory to DemoFiles, run the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod09\Demofiles\AADB2C
    ```
11. To run the project, run the following command:
    ```bash
    curl https://localhost:5001/api/values -i --header "Authorization: Bearer [Token]"    
    ```
12. You will see your username as part of the result.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
