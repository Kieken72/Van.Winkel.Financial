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

                if (_seed)
                {
                    this.SeedDevData();
                }
            }

            public override void Down()
            {
            }
        }
}
