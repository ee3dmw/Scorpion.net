using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Scorpion.Database
{
    public class MigrationScriptBuilder : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(HistoryOperation insertHistoryOperation)
        {
            Statement("GO");
            base.Generate(insertHistoryOperation);
            Statement("GO");
        }

        protected override void Generate(CreateProcedureOperation createProcedureOperation)
        {
            Statement("GO");
            base.Generate(createProcedureOperation);
        }

        protected override void Generate(AlterProcedureOperation alterProcedureOperation)
        {
            Statement("GO");
            base.Generate(alterProcedureOperation);
        }

        protected override void Generate(SqlOperation sqlOperation)
        {
            Statement("GO");
            base.Generate(sqlOperation);
        }
    }
}
