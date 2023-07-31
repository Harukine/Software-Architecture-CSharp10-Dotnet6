# Interacting with Data in C# - Entity Framework Core
- Object-Relational Mapping (ORM) and its importance in building data layers for enterprise applications. We discuss the basics of ORM and its role in mapping data between the storage subsystem and objects. We then dive into Entity Framework Core, a popular ORM tool included in .NET 6. We cover the configuration of Entity Framework Core and discuss how to perform database migrations to manage schema changes. Additionally, we explore compiled models and how they can improve performance.
- We also delves into querying and updating data using Entity Framework Core, providing examples and best practices. We discuss deploying the data layer and highlight advanced features of Entity Framework Core, such as global filters. You will have a solid understanding of ORM, Entity Framework Core, and how to effectively work with data in C# applications.

## Understanding ORM basics
- We explore Object-Relational Mapping (ORM) and its significance in data layer implementation. We learn how ORMs map database tables to in-memory objects and vice versa, with various data types being mapped accordingly. We discuss the use of data annotations, name conventions, and the fluent configuration interface in Entity Framework Core for configuring the ORM.
- We also examine the importance of ORM providers or connectors, which are specific adapters for different database types, and Entity Framework Core's support for various database engines. We explore how relationships between tables are represented using object pointers and how the DbContext class acts as an in-memory cache class containing the mapping configuration.
- Querying and updating data using Entity Framework Core is covered, with LINQ queries and lazy evaluation discussed. We see how updates, deletions, and additions to the database are performed through modifications to the in-memory collections representing the tables, followed by explicit synchronization with the database using methods like SaveChangesAsync().
- Transactions play a crucial role in synchronization operations, with changes being executed within a single transaction. We conclude the chapter with a deeper understanding of how Entity Framework Core handles data interactions and synchronization with the database.

## Configuring Entity Framework Core
- We can configure Entity Framework Core by creating a separate class library project. Choose a .NET 6 library project and add the necessary dependencies, such as the Microsoft.EntityFrameworkCore.SqlServer package for SQL Server integration.
- Rename the default Class1 class to MainDbContext and define it as a subclass of DbContext. The MainDbContext class requires the DbContextOptions in its constructor, which contains the necessary options for the database connection.
- Inside the MainDbContext class, we will add properties for each collection that represents a mapped database table. The mapping configuration will be defined within the OnModelCreating method using the ModelBuilder object.
- To continue the configuration, we create entity classes for each database table, which are referred to as entities. These entity classes are placed in the Models folder of the project.

### Defining DB entities
- Create an entity class called Destination to represent the rows of the location database table. The Destination class includes properties such as Id, Name, Country, and Description, which map to the corresponding DB fields. We apply attributes like MaxLength and Required to specify length limits and mandatory fields.
- Since the Destination entity is connected to the Package entity through a one-to-many relationship, it includes a collection property called Packages to refer to the related packages. The Destination class is placed in the Models folder and is decorated with appropriate attributes.
- Define the Package entity class, which represents the travel packages table. It includes properties such as Id, Name, Description, Price, DurationInDays, StartValidityDate, EndValidityDate, MyDestination, and DestinationId. We apply attributes to specify length limits and define the foreign key relationship with the Destination entity.
- The DestinationId property acts as the external key for the one-to-many relationship between packages and destinations. By explicitly representing the foreign key, we can simplify update operations and queries.

### Defining the mapped collections
- Define the in-memory collections that represent the database tables in our MainDbContext class. We add DbSet<T> collection properties for each entity, T, in our case, Package and Destination. These properties are named in plural form, following the convention of pluralizing the entity name.
- By adding these DbSet properties, we provide Entity Framework Core with the necessary information to interact with the corresponding database tables.

### Completing the mapping configuration
- We complete the mapping configuration by adding additional configuration code using a fluent interface. We use the `OnModelCreating` method in our `DbContext` class to define the configuration information for each entity.
- The configuration information includes defining relationships between entities, specifying foreign keys, setting up indexes, and more. We can configure one-to-many relationships, specifying the navigation properties and foreign keys. We can also define indexes using attributes or with the `HasIndex` method.
- Additionally, we can create separate configuration classes for each entity to group entity-specific configuration options. These classes implement the `IEntityTypeConfiguration<>` interface and define the configuration in the `Configure` method. We can apply the configuration classes using attributes or by calling the `Configure` method within the `OnModelCreating` method.
- It is important to note that the configuration can be customized based on individual preferences and project requirements. The example provided shows how to configure a one-to-many relationship and use configuration classes for specific entities.
- Lastly, Entity Framework Core also supports many-to-many relationships, allowing for the creation of join entities and join tables.
- Once the mapping configuration is complete, we can proceed to create the database and handle database structure updates as the application evolves.

## Entity Framework Core migrations
- We explore Entity Framework Core migrations, which are used to generate the physical database and create a database structure snapshot that allows Entity Framework Core to interact with the database.
- We have two options to use the Entity Framework Core design tools for migrations: the tools that work in any operating system console using the `dotnet ef` format, and the tools specific to the Visual Studio Package Manager Console.
- The process of working with migrations involves several steps:
1. Modify the `DbContext` and entity definitions as needed.
2. Use the Entity Framework Core design tools to detect and process the changes made in the `DbContext` and entities.
3. The design tools update the database structure snapshot and generate a new migration file containing instructions to modify the physical database to reflect the changes.
4. Another tool is used to apply the migration and update the database with the new changes.
5. Test the newly configured database layer and, if necessary, make further changes and create new migrations.
6. Deploy the data layer to staging or production, where all migrations are applied to the actual database.
- If we are working with an existing database, we need to configure the `DbContext` and models to reflect the existing database structure. We can use the design tools with the `IgnoreChanges` option to generate an empty migration and synchronize the database structure version with the database snapshot.
- To enable the design tools to interact with the database, we need to define the `DbContextOptions` options that include the connection string of the test/design database. This is done by implementing the `IDesignTimeDbContextFactory<>` interface.
- To create a migration, we use the `Add-Migration` command in the Package Manager Console, specifying the migration name. If we encounter errors, we can use the `Remove-Migration` command to undo the migration and make necessary changes to the code.
- The migration files contain the `Up` and `Down` methods, where `Up` represents the migration, and `Down` undoes its changes.
- Finally, migrations can be applied to the database using the `Update-Database` command in the Package Manager Console or the `dotnet ef database update` command in the console.

### Understanding stored procedures and direct SQL commands
- To incorporate database structures like stored procedures and direct SQL commands, Entity Framework Core commands and declarations are not sufficient. Instead, we can manually include stored procedures or generic SQL strings in the `Up` and `Down` methods of a migration using the `migrationBuilder.Sql("<sql command>")` method.
- To do this safely, we can create an empty migration that doesn't perform any configuration changes. Then, we add the necessary SQL commands to the `Up` method and their inverse commands in the `Down` method. Storing SQL strings in resource files (.resx files) is a recommended practice.
- Before interacting with the database, an optional step can be performed: model optimizations.

## Compiled models
- In Entity Framework Core version 6, compiled models can be created to significantly improve performance, especially for models with numerous entities. This is achieved by generating code that, once compiled together with the data layer project, creates optimized data structures used by the context classes.
- The Optimize-DbContext or `dotnet ef dbcontext optimize` command, provided by the Microsoft.EntityFrameworkCore.Tools NuGet package, is used to generate the optimization code. It requires specifying the folder name and namespace to store the generated classes. The optimization code depends on the ORM configuration and should be repeated each time a new migration is created.
- To enable optimizations, the root of the optimization model must be passed as an option when creating an instance of the context class. This can be achieved by using the UseModel method and providing the `Optimization.MainDbContextModel.Instance` as an argument.
- With these optimizations in place, the data layer is ready to interact with the database through Entity Framework Core with improved performance.

# Querying and updating data with Entity Framework Core
- To test the DB layer, create a console project named WWTravelClubDBTest and add the data layer as a dependency. Use the LibraryDesignTimeDbContextFactory class to create an instance of the DbContext subclass for database operations.
- To add data, create instances of the entities and add them to the mapped collections. Call SaveChangesAsync() to persist changes to the database. Primary keys are auto-generated.
- To modify data, query the entity to be modified using Where() and FirstOrDefaultAsync(). Load related entities with Include() to make changes. Use SaveChangesAsync() to update the database.
- To improve performance in Entity Framework Core 6, use compiled models with Optimize-DbContext command. It generates code that optimizes performance by creating precompiled data structures.
- Queries can be optimized using AsSplitQuery() to split complex LINQ queries into multiple SQL queries and improve performance. The ToQueryString() method can be used to inspect the SQL generated by LINQ quer

## Returning data to the presentation layer
- To keep the layers separated and adapt queries to the data needed by each use case, Data Transfer Objects (DTOs) are used to project data from database entities into smaller classes containing only the necessary information.
- To create a DTO, define a separate class with properties that hold the required data. Use the LINQ Select clause to directly project data from the database into the DTO, minimizing data exchange.
- As an example, create a PackagesListDTO class containing summary information about packages. Use a LINQ query with Select to populate the DTOs, retrieving packages available around a specific date.
- The Main method in the console project can be used to test and display the results of the queries. Use Console.ReadKey() to pause the program and observe the output.

## Issuing direct SQL commands
- Some database operations cannot be efficiently executed using LINQ queries and in-memory entities. In such cases, direct SQL commands or stored procedures may be necessary.
- To issue direct SQL commands, you can use the `DbContext.Database.ExecuteSqlRawAsync()` method for SQL statements that don't return entities, and `FromSqlRaw()` method of the mapped collection for SQL statements that return collections of entities.
- When using `ExecuteSqlRawAsync()`, you can pass parameters to the SQL command using placeholders in the SQL string and providing the corresponding parameter values as arguments.
- For SQL commands that return collections of entities, use the FromSqlRaw() method, specifying the SQL string and any parameters.
- To maintain separation of concerns and avoid dependencies on a specific database, it is recommended to encapsulate SQL commands within public methods in your DbContext subclasses and store SQL strings in resource files.

## Handling transactions
- To handle transactions in Entity Framework Core, you can use the BeginTransaction() method to start a transaction for a DbContext instance. All changes made to the DbContext within the transaction are passed together when SaveChangesAsync() is called.
- To explicitly handle transactions, you can use a using block with a transaction object obtained from BeginTransaction(). Inside the using block, you can perform queries and updates, and if everything is successful, you can commit the transaction using the Commit() method. If there's an error or exception, you can roll back the transaction using the Rollback() method to undo any changes made within the transaction.
- This approach ensures that all operations within the using block are included in the same transaction, providing better control over data consistency and integrity.