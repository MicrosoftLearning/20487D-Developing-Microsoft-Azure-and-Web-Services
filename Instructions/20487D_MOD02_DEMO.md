# Module 2: Querying and Manipulating Data Using Entity Framework

1. Wherever a path to a file starts with *[Repository Root]*, replace it with the absolute path to the folder in which the 20487 repository resides.
 For example, if you cloned or extracted the 20487 repository to **C:\Users\John Doe\Downloads\20487**, change the path: *[Repository Root]***\AllFiles\20487D\Mod01** to **C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01**.
2. Wherever *[YourInitials]* appears, replace it with your actual initials. (For example, the initials for John Doe will be jd.)
3. Before performing the demonstration, you should allow some time for the provisioning of the different Microsoft Azure resources required for the demonstration. You should review the demonstrations before the actual class, identify the resources, and prepare them beforehand to save classroom time.


# Lesson 2: Creating an Entity Data Model

### Demonstration: Creating an Entity Type, DbContext, and DbInitializer

1. Open the Command Prompt window.
2. Create a new **ASP.NET Core Web API** project. At the command prompt, paste the following command, and then press Enter:
   ```bash
    dotnet new console --name MyFirstEF --output [Repository Root]\Allfiles\Mod02\DemoFiles\MyFirstEF\Starter
   ```
3. After the project is created, to change the directory, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod02\DemoFiles\MyFirstEF\Starter
   ```
4. To use **Entity Framework Core**, install the following package from the command prompt:
   ```base
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version=2.1.1
    dotnet restore
   ```
5. To Open the project in Microsoft Visual Studio Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
6. To add a new folder named **Models**, right-click in the Explorer pane, and then select **New Folder**.
7. Right-click the **Models** folder, select **New File**, in the box on the top, type **Product.cs**, and then press Enter.
8. In **Product.cs**, paste the following code:
   ```cs
    namespace MyFirstEF.Models
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
   ```
9. Right-click the **Models** folder, select **New File**, in the box on the top, type **Store.cs**, and then press Enter.
10. In **Store.cs**, paste the following code:
   ```cs
    using System.Collections.Generic;

    namespace MyFirstEF.Models
    {
        public class Store
        {
             public int Id { get; set; }
             public string Name { get; set; }
             public IEnumerable<Product> Products { get; set; }
        }
    }
   ```
11. To add a new folder named **Database**, right-click in the Explorer pane, and then select **New Folder**.
12. Right-click the **Database** folder, select **New File**, in the box on the top, type **MyDbContext.cs**, and then press Enter.
13. In **MyDbContext.cs**, add the following using statements:
   ```cs
    using Microsoft.EntityFrameworkCore;
    using MyFirstEF.Models;
   ```
14. In **MyDbContext.cs**, add the following code:
   ```cs
    namespace MyFirstEF.Database
    {
        public class MyDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }
            public DbSet<Store> Stores { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MyFirstEF;Trusted_Connection=True;");
            }
        }
    }
   ```
15. Right-click the **Database** folder, select **New File**, in the box on the top, type **DbInitializer.cs**, and then press Enter.
16. In **DbInitializer.cs**, add the following code:
   ```cs
    namespace MyFirstEF.Database
    {
        public static class  DbInitializer
        {
            public static void Initialize(MyDbContext context)
            {
                context.Database.EnsureCreated();
                // Code to create initial data
            }
        }
    }
   ```
17. Navigate to **Program.cs** and add the following using statement:
   ```cs
    using MyFirstEF.Database;
   ```
18. In **Program.cs**, replace the **main** method with the following code:
   ```cs
    static void Main(string[] args)
    {
        using(var context = new MyDbContext())
        {
            DbInitializer.Initialize(context);
        }
        Console.WriteLine("Database created");
    }
   ```
19. To run the application, in command prompt, run the following command:
   ```bash
   dotnet run
   ```
20. Open **Azure Data Studio**.
21. Click **New Connection**. The **Connection** window will appear.
22. In the **Server** box, type **.\SQLEXPRESS**, and then click **Connect**.
23. On the **Server** blade, expand **.\sqlexpress**, and then expand **Database**.
24. Ensure the **MyFirstEF** database appears.
25. In **Server**, expand the **MyFirstEF** node, and then expand the **Tables** node.
26. Notice that both classes defined in the **MyFirstEF** project appear as tables, **dbo.Stores** and **dbo.Products**.
    >**Note**: Database tables are usually named in the plural form, which is why Entity Framework changed the names of the generated tables from Store and Product to Stores and Products. The dbo prefix is the name of the schema in which the tables were created.

27. Expand the **dbo.Products** and **dbo.Stores** tables, and then expand the **Columns** node in each of them to see that both the tables have **Id** and **Name** columns, similar to their corresponding class properties.
28. Close **Azure Data Studio**.
29. Close all open windows.

# Lesson 3: Querying Data

### Demonstration: Using Language-Integrated Query (LINQ) to Entities

1. Open the Command Prompt window.
2. To change the directory to the starter project, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\Allfiles\Mod02\DemoFiles\UsingLINQtoEntities\Starter
   ```
3. To restore all the dependencies and tools of a project, at the command prompt, run the following command:
   ```base
    dotnet restore
   ```
4. To open the project in VS Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
5. On the **Explorer** blade, under **STARTER**, double-click **Program.cs**.
6. To create a new **SchoolContext** object, append the following code to the **Main** method:
   ```cs
    using (var context = new SchoolContext())
    {
    }
   ```
    To control the release of unmanaged resources used by the context, such as a database connection, use the **using** statement.
7. To create the database, add the following code in the **using** block:
   ```cs
    DbInitializer.Initialize(context);
   ```
8. To select all the courses from the database, in the **using** block under **DBInitializer**, add the following LINQ to the **Entities** code:
   ```cs
    var courses = from c in context.Courses
                  select c;
   ```
9. To print the courses and students list to the console window, in the **using** block after the LINQ to the **Entities** code, add the following code:
   ```cs
    foreach (var course in courses)
    {
        Console.WriteLine($"Course: {course.Name}");
        foreach (var student in course.Students)
        {
            Console.WriteLine($"\t Student name: {student.Name}");
        }
    }
    Console.ReadLine();
   ```
10. To run the application, in command prompt, run the following command:
   ```bash
   dotnet run
   ```
11. In the console window, review the course and student lists printed to the console window.
12. To stop the application, in the command prompt press Ctrl+C.
13. Close all open windows.

### Demonstration 2: Running Stored Procedures with Entity Framework

#### Demonstration Steps

1. Open the Command Prompt window.
2. To change the directory to the starter project, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\StoredProcedure
   ```
3. To restore all dependencies and tools of a project, at the command prompt, run the following command:
   ```base
    dotnet restore
   ```
4. To open the project in VS Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
5. On the **Explorer** blade, under **STARTER**, double-click **Program.cs**.
6. Navigate to the **Main** method, and notice that a **SchoolContext** instance is created to establish a connection to the database.
7. Review the query that is being assigned to the *averageGradeInCourse* variable. Notice that the average grade of the **ASP.NET Core** course is calculated, and then printed to the console.
8. The **ExecuteSqlCommand** statement calls the **spUpdateGrades** stored procedure with two parameters, **CourseName** and **GradeChange**.
9. To run the application, in command prompt, run the following command:
   ```bash
   dotnet run
   ```
   >**Note**: Notice that the updated average grade is printed to the console before and after the change.
10. To stop the application, in the command prompt press Ctrl+C.
11. Close all open windows.

# Lesson 4: Manipulating Data

### Demonstration: CRUD Operations in Entity Framework

#### Demonstration Steps

1. Open the Command Prompt window.
2. To change the directory to the starter project, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\CRUD\Starter
   ```
3. To restore all the dependencies and tools of a project, at the command prompt, run the following command:
   ```base
    dotnet restore
   ```
4. To open the project in VS Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
5. On the **Explorer** blade, under **STARTER**, double-click **Program.cs**.
6. To retrieve the **ASP** course from the **Courses** table, inside the **using** block under **DBInitializer**, append the following code:
   ```cs
    Course ASPCourse = (from course in context.Courses
    where course.Name == "ASP.NET Core" select course).Single();
   ```
7. To create two new student records named **Thomas Anderson** and **Terry Adams**, append the following code to the **using** block:
   ```cs
    Student firstStudent = new Student() { Name = "Thomas Anderson" };
    Student secondStudent = new Student() { Name = "Terry Adams" };
   ```
8. To add the two newly created student records to the **ASP** course, append the following code to the **using** block:

   ```cs
    ASPCourse.Students.Add(firstStudent);
    ASPCourse.Students.Add(secondStudent);
   ```
9. To give the teacher of the **ASP** course a $1000 salary raise, append the following code to the **using** block:
   ```cs
    ASPCourse.CourseTeacher.Salary += 1000;
   ```
10. To select **Student_1** from the **ASP** course, append the following code to the **using** block:

   ```cs
    Student studentToRemove = ASPCourse.Students.FirstOrDefault((student) => student.Name == "Student_1");
   ```
11. To remove the student record from the **ASP** course, append the following code to the **using** block:
   ```cs
    ASPCourse.Students.Remove(studentToRemove);
   ```
12. Save the changes, print the result, and then append the following code to the **using** block:
   ```cs
    context.SaveChanges();
    Console.WriteLine(ASPCourse);
    Console.ReadLine();
   ```
13. To run the application, in command prompt, run the following command:
   ```bash
   dotnet run
   ```
14. After a few seconds, the list of students appears in the console window. Notice that there are two new student records at the bottom of the list, and Student 1 is missing from the list. Also notice that the salary of the teacher is now **$101,000**.
15. Notice the Structured Query Language (SQL) update, and delete and insert statements that correspond to the salary update, student record deletion, and the addition of the two new student records.
16. To stop the application, in the command prompt press Ctrl+C.
17. Close all open windows.

### Demonstration: Using Entity Framework with In-Memory Database

#### Demonstration Steps

1. Open the Command Prompt window.
2. To change the directory to the starter project, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\InMemory\Starter\InMemory.Dal.Test
   ```
3. To use **Entity Framework Core InMemory**, install the following package from the command prompt:
   ```base
    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version=2.1.1
   ```
4. To restore all the dependencies and tools of a project, at the command prompt, run the following command:
   ```base
    dotnet restore
   ```
5. Move to the solution folder, paste the following command, and then press Enter:
   ```bash
   cd ..
   ```
6. To open the solution in VS Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
7. Expand **InMemory.Dal**, expand **Database**, and then click **SchoolContext**.
8. To add an empty constructor to the class, paste the following code:
   ```cs
    public SchoolContext()
    {

    }
   ```
9. To add a constructor with the **DbContextOptions\<SchoolContext\>** parameter to the class, paste the following code:
   ```cs
   public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
   {
   }
   ```
10. Locate the **OnConfiguring** method and add the following code:
   ```cs
   if(!optionsBuilder.IsConfigured)
   {
       optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
   }
   ```
   >**Note**: In your tests, you are going to externally configure the context to use the **InMemory** provider. If you are configuring a database provider by overriding **OnConfiguring** in your context, then you need to add some conditional code to ensure that you configure the database provider only if it has not already been configured.    

11. Expand **InMemory.Dal.Test**, and then click **DBInMemoryTest**.
12. Add the following property to the class:
   ```cs
    private DbContextOptions<SchoolContext> _options =
                new DbContextOptionsBuilder<SchoolContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;
   ```
13. Add the following **Test Method** to the class:
   ```cs
    [TestMethod]
    public void UpdateTeacherSalaryTest()
    {
        Teacher teacher = new Teacher { Name = "Terry Adams", Salary = 10000 };
        teacher = _teacherRepository.Add(teacher);
        teacher.Salary += 10000;
        teacher = _teacherRepository.Update(teacher);

        using (var context = new SchoolContext(_options))
        {
            var result = context.Teachers.FirstOrDefault((s) => s.PersonId == teacher.PersonId);
            Assert.AreEqual(result.Salary, 20000);
        }
    }
   ```
14. Switch to the Command Prompt window. To change directory to the **InMemory.Dal.Test** project, at the command prompt, run the following command:
    ```bash
    cd InMemory.Dal.Test
    ```
15. To run the all the test methods, paste the following command, and then press Enter:
   ```bash
    dotnet test
   ```
16. Close all open windows.

### Demonstration: Using Entity Framework with SQLite

#### Demonstration Steps

1. Open the Command Prompt window.
2. To change the directory to the starter project, at the command prompt, run the following command:
   ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\Sqlite\Starter\Sqlite.Dal.Test
   ```
3. To use **Entity Framework Core SQLite**, install the following package from the command prompt:
   ```base
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version=2.1.1
   ```
4. To restore all the dependencies and tools of a project, at the command prompt, run the following command:
   ```base
    dotnet restore
   ```
5. Move to the solution folder, paste the following command, and then press Enter:
   ```bash
    cd ..
   ```
6. Open the solution in VS Code, paste the following command, and then press Enter:
   ```bash
    code .
   ```
7. Expand **Sqlite.Dal.Test**, and then click **DBSqliteTest**.
8. Add the following property to the class:
   ```cs
     private DbContextOptions<SchoolContext> _options =
                new DbContextOptionsBuilder<SchoolContext>()
                    .UseSqlite(@"Data Source = [Repository Root]\AllFiles\Mod02\DemoFiles\SQLite\Database\SqliteSchool.db")
                    .Options;
   ```
9. Switch to the Command Prompt window. To change directory to the **Sqlite.Dal.Test** project, at the command prompt, run the following command:
    ```bash
    cd Sqlite.Dal.Test
    ```
10. To run the all the test methods, paste the following command, and then press Enter:
   ```bash
    dotnet test
   ```
11. Open **DB Browser for SQLite**.
12. On the menu bar, click **File**, and then click **Open Database**.
13. In the **Choose a database file** window, browse to **[Repository Root]\AllFiles\Mod02\DemoFiles\SQLite\Database**, and then double-click **SqliteSchool.db**.
14. In the **Database Structure** tab, expand **Tables**.
15. Right-click the **Persons** table, and then select **Browse Table**.
16. In the **Browse Data** tab, verify that the student record **Kari Hensien** is in the database.
17. Close all open windows.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
