using FluentMigrator;

namespace test_v1.Migrations;

[Migration(0)]
public class InitialMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("categories")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Name").AsAnsiString().NotNullable()
                .WithColumn("ParentId").AsInt32().NotNullable();

        Create.ForeignKey("FK_Caregories_Parents")
            .FromTable("categories").ForeignColumn("ParentId")
            .ToTable("categories").PrimaryColumn("Id");
    }
}
