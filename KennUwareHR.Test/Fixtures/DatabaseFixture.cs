using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using System.Linq;
using Xunit;
using KennUwareHR.Models;
using System;

public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {

        var options = new DbContextOptionsBuilder<HRContext>()
                .UseInMemoryDatabase(databaseName: "testing_db")
                .Options;

        Context = new HRContext(options);

        // ... initialize data in the test database ...

        Region region1 = new Region
        {
            Id = 1,
            Name = "New York",
            Country = "USA",
            BonusRevenueThreshold = 1000000
        };
        Region region2 = new Region
        {
            Id = 2,
            Name = "Texas",
            Country = "USA",
            BonusRevenueThreshold = 2000000
        };

        Role role1 = new Role
        {
            Id = 1,
            Title = "Role 1"
        };

        Role role2 = new Role
        {
            Id = 2,
            Title = "Role 2"
        };

        Department department1 = new Department
        {
            Id = 1,
            Name = "Test Department 1"
        };
        Department department2 = new Department
        {
            Id = 2,
            Name = "HR"
        };

        Position position1 = new Position
        {
            Id = 1,
            Title = "Sales Rep"
        };

        Position position2 = new Position
        {
            Id = 2,
            Title = "Test position 2"
        };

        Employee employee1 = new Employee
        {
            Id = 1,
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = new DateTime(),
            Address = "1 Testing Street",
            PhoneNumber = "5851231234",
            EmploymentEndDate = new DateTime(),
            EmploymentStartDate = new DateTime(),
            RegionId = region1.Id,
            Region = region1,
            RoleId = role2.Id,
            Role = role2,
            Commission = 10000,
            DepartmentId = department2.Id,
            Department = department2,
            PositionId = position2.Id,
            Position = position2
        };

        Employee employee2 = new Employee
        {
            Id = 2,
            FirstName = "SpongeBob",
            LastName = "Square Pants",
            DateOfBirth = new DateTime(),
            Address = "124 Conch Street",
            PhoneNumber = "29998559671349",
            EmploymentEndDate = new DateTime(),
            EmploymentStartDate = new DateTime(),
            RegionId = region2.Id,
            Region = region2,
            RoleId = role2.Id,
            Role = role2,
            Commission = 10000,
            DepartmentId = department2.Id,
            Department = department2,
            PositionId = position1.Id,
            Position = position1
        };

        Context.Roles.Add(role1);
        Context.Roles.Add(role2);
        Context.Regions.Add(region1);
        Context.Regions.Add(region2);
        Context.Departments.Add(department1);
        Context.Departments.Add(department2);
        Context.Positions.Add(position1);
        Context.Positions.Add(position2);
        Context.Employees.Add(employee1);
        Context.Employees.Add(employee2);
        Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }

    public HRContext Context { get; private set; }
}

public class MyDatabaseTests : IClassFixture<DatabaseFixture>
{
    readonly DatabaseFixture fixture;

    public MyDatabaseTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    // ... write tests, using fixture.Context to get access to the SQL Server ...
}