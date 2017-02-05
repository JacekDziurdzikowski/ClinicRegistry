namespace Clinic_Registry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatedSpecializationsAndGenders : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Specializations (Id, Name) values (1, 'Specialization A')");
            Sql("insert into Specializations (Id, Name) values (2, 'Specialization B')");
            Sql("insert into Specializations (Id, Name) values (3, 'Specialization C')");
            Sql("insert into Specializations (Id, Name) values (4, 'Specialization D')");
            Sql("insert into Specializations (Id, Name) values (5, 'Specialization E')");


            Sql("insert into Genders (Id, Name) values (1, 'Female')");
            Sql("insert into Genders (Id, Name) values (2, 'Male')");
        }
        
        public override void Down()
        {
        }
    }
}
