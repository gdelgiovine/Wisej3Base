using Dapper.Contrib.Extensions;
using Passero.Framework;
using System;

namespace Wisej3Base
{
    [Table("Titles")]
    public class PasseroModel_Titles: Passero.Framework .ModelBase  
    {
        [ExplicitKey]
        public string Title_Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Pub_Id { get; set; }
        public decimal Price { get; set; }
        public decimal Advance { get; set; }
        public int Royalty { get; set; }

        //[ColumnMapping("ytd_sales")]
        public int Ytd_Sales { get; set; }
        public string Notes { get; set; }
        public DateTime PubDate { get; set; }

        static PasseroModel_Titles()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(Model_Titles), new Dapper.ColumnMapper.ColumnTypeMapper(typeof(Model_Titles)));
            //oppure forma breve --- NOTA: se si passa un ColumTypeMapper nullo la mappatura viere riportata al default.
            //C'è anche una forma abbreviata
            //ColumnTypeMapper.RegisterForTypes(typeof(Model_Titles));
        }
        public static void RegisterDapperTypeMap()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(Model_Titles), new Dapper.ColumnMapper.ColumnTypeMapper(typeof(Model_Titles)));
        }
    }
}
