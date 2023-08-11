using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using hr_system_backend.Entities;

namespace hr_system_backend.Services.FakeData
{
    public static class FakeData
    {
        public static List<Superior> Superiors { get; set; } = new();
        public static List<Employee> Employees { get; set; } = new();
        public static void GenerateFakeSuperiors()
        {
            Randomizer.Seed = new Random(100);
            var superiorsBogus = new Faker<Superior>()
              .RuleFor(s => s.Name, f => f.Name.FirstName())
              .RuleFor(s => s.Surname, f => f.Name.LastName())
              .RuleFor(s => s.Position, f => f.Name.JobTitle())
              .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.Name, s.Surname))
              .RuleFor(s => s.Daysoff, f => f.Random.Even(0, 26))
              .RuleFor(s => s.Area, f => f.Name.JobArea())
              .RuleFor(s => s.Id, f => Guid.NewGuid())
              .RuleFor(s => s.Role, f => Role.Manager)
              .RuleFor(s => s.Status, f => Status.Employed)
              .Generate(100);

            Superiors.AddRange(superiorsBogus);
        }

        public static void GenerateFakeEmployees()
        {
            Randomizer.Seed = new Random(1000);

            var employeesBogus = new Faker<Employee>()
                .RuleFor(e => e.Name, f => f.Name.FirstName())
                .RuleFor(e => e.Surname, f => f.Name.LastName())
                .RuleFor(e => e.Daysoff, f => f.Random.Even(0, 26))
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name, e.Surname))
                .RuleFor(e => e.Id, f => Guid.NewGuid())
                .RuleFor(e => e.Role, f => Role.Employee)
                .RuleFor(e => e.Status, f => Status.Employed)
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.SuperiorId, f => f.Random.CollectionItem(Superiors).Id)
                .Generate(1000);

            FakeData.Employees.AddRange(employeesBogus);
        }
    }
}
