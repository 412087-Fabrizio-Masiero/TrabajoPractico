using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ComercioInterior.Data.Helpers;
using ComercioInterior.Domain;

namespace ComercioInterior.Data.Helper
{   
        public class DataHelper
        {
            private static DataHelper _instance;
            private SqlConnection _connection;

            private DataHelper()
            {
                _connection = new SqlConnection(Properties.Resources.Conexion);
            }
            public static DataHelper GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new DataHelper();
                }
                return _instance;
            }

            public DataTable ExecuteSpQuery(string sp, List<SpParameter>? param = null)
            {
                DataTable dt = new DataTable();
                try
                {
                    // Abrimos la conexión
                    _connection.Open();
                    var cmd = new SqlCommand(sp, _connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos parámetros si los hay
                    if (param != null)
                    {
                        foreach (SpParameter p in param)
                        {
                            cmd.Parameters.AddWithValue(p.Name, p.Valor);
                        }
                    }

                    dt.Load(cmd.ExecuteReader());
                }
                catch (SqlException ex)
                {
                    dt = null;
                }
                finally
                {
                    _connection.Close();
                }

                return dt;
            }


            public bool ExecuteSpDml(string sp, List<SpParameter>? param = null)
            {
                bool result;
                try
                {

                    _connection.Open();
                    var cmd = new SqlCommand(sp, _connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    if (param != null)
                    {
                        foreach (SpParameter p in param)
                        {
                            cmd.Parameters.AddWithValue(p.Name, p.Valor);
                        }
                    }

                    int affectedRows = cmd.ExecuteNonQuery();

                    result = affectedRows > 0;
                }
                catch (SqlException ex)
                {

                    result = false;
                }
                finally
                {
                    
                    _connection.Close();
                }

                return result;
            }

        public bool executeTransaction(Facturas factura)
        {

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            var cmd = new SqlCommand("", _connection, transaction);

            cmd.CommandText = "sp_Guardar_Factura";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", factura.Codigo);
            cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@pago", factura.Pago);
            SqlParameter idfactura = new SqlParameter("id", SqlDbType.Int);
            idfactura.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idfactura);
            int affectedRows = cmd.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                transaction.Rollback();
                return false;
            }
            else
            {
                int idparafactura = (int)idfactura.Value;
                
                foreach (DetalleFactura i in factura.detalleFacturas)
                {
                    SqlCommand cmdDetalle = new SqlCommand("", _connection, transaction);
                    cmdDetalle.CommandText = "sp_Guardar_Detalle";
                    cmdDetalle.CommandType = CommandType.StoredProcedure;



                    cmdDetalle.Parameters.AddWithValue("@nroFactura", idparafactura);
                    cmdDetalle.Parameters.AddWithValue("@nroArticulo", i.NroArticulo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", i.Cantidad);

                    int affectedRowsDetalle = cmdDetalle.ExecuteNonQuery();

                    if (affectedRowsDetalle <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }


                }
                transaction.Commit();
                return true;

            }

            
        }
    }
}
