using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.Services
{
    public class DirectDB : IDirectDB
    {
        private string _mysqlString;
        public DirectDB(IConfiguration config)
        {
            _mysqlString = config["ConnectionStrings:MySqlConnection"];
        }
        public async Task<Dictionary<string, object>> GetData(string query)
        {
            Dictionary<string, object> dato = new Dictionary<string, object>();
            using (var conn = new MySqlConnection(_mysqlString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var TipoDato = Type.GetTypeCode(reader.GetFieldType(i));
                                    switch (TipoDato)
                                    {
                                        case TypeCode.String:
                                            dato[reader.GetName(i)] = reader.GetString(i);
                                            break;
                                        case TypeCode.Decimal:
                                            dato[reader.GetName(i)] = reader.GetDecimal(i);
                                            break;
                                        case TypeCode.DateTime:
                                            dato[reader.GetName(i)] = reader.GetDateTime(i);
                                            break;
                                        case TypeCode.Int32:
                                            dato[reader.GetName(i)] = reader.GetInt32(i);
                                            break;
                                        case TypeCode.Int64:
                                            dato[reader.GetName(i)] = reader.GetInt64(i);
                                            break;
                                        case TypeCode.Double:
                                            dato[reader.GetName(i)] = reader.GetDouble(i);
                                            break;
                                    }
                                }
                            }

                            conn.Close();
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            return dato;

        }

        public async Task<List<Dictionary<string, object>>> GetListData(string query)
        {
            List<Dictionary<string,object>> list = new List<Dictionary<string, object>>();

            using (var conn = new MySqlConnection(_mysqlString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Dictionary<string, object> dato = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var TipoDato = Type.GetTypeCode(reader.GetFieldType(i));
                                    switch (TipoDato)
                                    {
                                        case TypeCode.String:
                                            dato[reader.GetName(i)] = reader.GetString(i);
                                            break;
                                        case TypeCode.Decimal:
                                            dato[reader.GetName(i)] = reader.GetDecimal(i);
                                            break;
                                        case TypeCode.DateTime:
                                            dato[reader.GetName(i)] = reader.GetDateTime(i);
                                            break;
                                        case TypeCode.Int32:
                                            dato[reader.GetName(i)] = reader.GetInt32(i);
                                            break;
                                        case TypeCode.Int64:
                                            dato[reader.GetName(i)] = reader.GetInt64(i);
                                            break;
                                        case TypeCode.Double:
                                            dato[reader.GetName(i)] = reader.GetDouble(i);
                                            break;
                                    }
                                }
                                list.Add(dato);
                            }

                            conn.Close();
                        }
                    }
                }
                catch (Exception e)
                {

                }

                return list;
            }
        }
    }
}
