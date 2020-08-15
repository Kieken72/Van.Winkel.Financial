using FluentMigrator;
using Van.Winkel.Financial.Database.Seed;

namespace Van.Winkel.Financial.Database.Migrations
{
        [Migration(1)]
        public class M001_Initial : Migration
        {
            private readonly bool _seed;

            public M001_Initial(Options options)
            {
                _seed = options.Seed;
            }
            public override void Up()
            {

                Create.Table("Customer")
                    .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumn("Name").AsString(250).NotNullable()
                    .WithColumn("Surname").AsString(250).NotNullable()
                    .WithColumn("CreatedOn").AsDateTime().NotNullable();


                Create.Table("Account")
                    .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumn("CustomerId").AsGuid().ForeignKey("Customer", "Id")
                    .WithColumn("Balance").AsDecimal(18, 2).NotNullable()
                    .WithColumn("OpenedOn").AsDateTime().NotNullable();

                Create.Table("Transaction")
                    .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumn("RecipientAccountId").AsGuid().ForeignKey("Account", "Id")
                    .WithColumn("SenderAccountId").AsGuid().ForeignKey("Account", "Id")
                    .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
                    .WithColumn("Note").AsString(250).Nullable()
                    .WithColumn("Date").AsDateTime().NotNullable();

            if (_seed)
                {
                    this.SeedDevData();
                }
            }

            public override void Down()
            {
                Delete.Table("Transaction");
                Delete.Table("Account");
                Delete.Table("Customer");
        }
        }
}
