using System;
using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;

namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new StorageAppDbContext();
            var employeeRepo = new SqlRepository<Employee>(ctx);

            employeeRepo.ItemAdded+=EmployeeRepository_ItemAdded;

            AddEmployees(employeeRepo);
            AddManagers(employeeRepo);
            GetEmployeeById(employeeRepo);
            WriteAllToConsole(employeeRepo);
 
            var organizationRepository = new SqlRepository<Organization>(ctx);

            AddOrganizations(organizationRepository);
            WriteAllToConsole(organizationRepository);      
           
            
        }

        private static void EmployeeRepository_ItemAdded(object? sender, Employee e)
        {
            System.Console.WriteLine($"Employee added => {e.FirstName}");
        }

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            var manager = new Manager { FirstName = "Mike" };
            var managerCopy = manager.Copy();
            
            if(managerCopy is not null)
            {
                managerCopy.FirstName += "_Copy";
                managerRepository.Add(managerCopy);
            }

            var managers = new[]
            {
                manager,
                new Manager{ FirstName = "Paul"},
                new Manager{ FirstName = "Ringo"}

            };
            managerRepository.AddBatch<Manager>(managers);
        }

        private static void AddEmployees(IRepository<Employee> employeeRepo)
        {
            var employees = new[]
            {
                new Employee{FirstName = "John"},
                new Employee{FirstName = "Thomas"},
                new Employee{FirstName = "Julia"},

            };
            employeeRepo.AddBatch<Employee>( employees);
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepo)
        {
            var employee = employeeRepo.GetbyId(1);
            Console.WriteLine($"Employee with Id 1 {employee.FirstName}");
        } 


        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {
                new Organization{Name = "Wired Brain Coffee"},
                new Organization{Name = "Wired Brain CoffeeHouses"}
            };
            // organizationRepository.Add(new Organization{Name = "WiredBrain"});
            // organizationRepository.Add(new Organization{Name = "Coffee"});
            organizationRepository.AddBatch<Organization>(organizations);

            organizationRepository.Save();
        }


        //even specify type as object
        private static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            foreach (var employee in repository.GetAll())
            {
                System.Console.WriteLine(employee);
            }
        }


    }
}
