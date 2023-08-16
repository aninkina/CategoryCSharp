using FluentMigrator;

namespace test_v1.Migrations;

[Migration(1)]
public class InsertMigration : AutoReversingMigration
{
    public override void Up()
    {
        InsertCategory(0, "ROOT");
        InsertCategory(1, "Centro-Oeste");
        InsertCategory(2, "Nordeste");
        InsertCategory(3, "Norte", 1);
        InsertCategory(4, "Sudeste", 3);
        InsertCategory(5, "Sul", 2);
    }

    private void InsertCategory(int id, string name, int parentId = 0)
    {
        Insert.IntoTable("categories").Row(new
        {
            Id = id,
            ParentId = parentId,
            Name = name
        });
    }
}
