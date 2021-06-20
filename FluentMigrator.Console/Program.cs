using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentMigrator.Console.Migrations;

namespace FluentMigrator.Console
{
    internal class Program
    {
        private static void Main()
        {
            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        private static IServiceProvider CreateServices() =>
            new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    rb => rb
                        .AddSqlServer()
                        .WithGlobalConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=User;Integrated Security=True;")
                        .ScanIn(typeof(CreateUserTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
