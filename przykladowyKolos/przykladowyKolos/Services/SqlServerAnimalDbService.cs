using przykladowyKolos.DTOs.Requests;
using przykladowyKolos.DTOs.Responses;
using przykladowyKolos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace przykladowyKolos.Services
{
    public class SqlServerAnimalDbService : IAnimalDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18849;Integrated Security=True";
        public List<GetAnimalResponse> GetAnimal(string orderBy)
        {
            if (orderBy == null)
            {
                orderBy = "AdmissionDate";
            }
            var list = new List<GetAnimalResponse>();
            using (var con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;



                com.CommandText = "select a.IdAnimal, a.Name, a.Type, a.AdmissionDate, o.LastName from Animal a inner join Owner o on o.IdOwner = a.IdOwner order by " + orderBy;




                using (var dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var an = new GetAnimalResponse();
                        an.Name = dr["Name"].ToString();
                        an.Type = dr["Type"].ToString();
                        an.AdmissionDate = DateTime.Parse(dr["AdmissionDate"].ToString());
                        an.LastNameOfOwner = dr["LastName"].ToString();
                        list.Add(an);
                    }
                }

            }
            return list;
        }

        public void AddAnimal(AddAnimalRequest request)
        {
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select IdOwner from Owner where IdOwner=@owner";
                    com.Parameters.AddWithValue("owner", request.IdOwner);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        throw new ArgumentException();
                    }

                    com.CommandText = "select IdProcedure from Procedures where IdProcedure=@procedure";
                    com.Parameters.AddWithValue("procedure", request.IdProcedure);
                    dr.Close();
                    var dr2 = com.ExecuteReader();
                    if (!dr2.Read())
                    {
                        dr2.Close();
                        tran.Rollback();
                        throw new ArgumentException();
                    }

                }
                catch (SqlException exc)
                {

                }
            }
        }
    }
}

