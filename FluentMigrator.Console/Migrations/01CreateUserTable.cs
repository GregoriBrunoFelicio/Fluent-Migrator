namespace FluentMigrator.Console.Migrations
{
    [Migration(01)]
    public class CreateUserTable : Migration
    {
        private const string tableName = "User";

        public override void Up() =>
            Create.Table(tableName)
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("Name").AsAnsiString().NotNullable()
                .WithColumn("Date").AsDateTime().Nullable();

        public override void Down() => Delete.Table(tableName);
    }
}
