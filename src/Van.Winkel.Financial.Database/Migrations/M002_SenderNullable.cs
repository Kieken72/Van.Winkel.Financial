using FluentMigrator;

namespace Van.Winkel.Financial.Database.Migrations
{
        [Migration(2)]
        public class M002_SenderNullable : Migration
        {
            private readonly bool _seed;

            public M002_SenderNullable(Options options)
            {
                _seed = options.Seed;
            }
            public override void Up()
            {
                Alter.Table("Transaction").AlterColumn("SenderAccountId").AsGuid().Nullable();
            }

            public override void Down()
            {
                Alter.Table("Transaction").AlterColumn("SenderAccountId").AsGuid().NotNullable();
            }
        }
}
