using Dapper;
using Passero.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wisej3Base
{
    public class vmTitles : Passero.Framework.ViewModel<PasseroModel_Titles>    
    {


        public ExecutionResult<IList<PasseroModel_Titles>> GetItemsByTitle(string TitleFilter)
        {
            ExecutionResult<IList<PasseroModel_Titles>> ER = new ExecutionResult<IList<PasseroModel_Titles>>();
            try
            {
                //string sql = "SELECT * FROM Titles WHERE Title LIKE @TitleFilter ORDER BY Title";
                //ER = this.GetItems(sql, new { TitleFilter = "%" + TitleFilter + "%" });

                string sql = "SELECT title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate " +
                    "FROM titles " +
                    "WHERE title like(@title) " +
                    "ORDER BY title_id ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@title", TitleFilter);
                ER = this.GetItems(sql,parameters );
                

            }
            catch (Exception ex)
            {
               ER.ErrorCode = -1;   
               ER.ResultMessage  = ex.Message;    
               ER.Exception = ex;   

            }

            return ER;
        }   

    }
}
