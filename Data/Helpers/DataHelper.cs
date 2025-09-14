using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ComercioInteriorV1.Data.Helpers;
using ComercioInteriorV1.Domain;

namespace ComercioInteriorV1.Data.Helper
{   
        public class DataHelper
        {
        private readonly string _connection;

        public DataHelper(string connectionString)
        {
            _connection = connectionString;
        }

        
        public DataTable ExecuteSpQuery(string sp, List<SpParameter>? param = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using var connection = new SqlConnection(_connection);
                using var cmd = new SqlCommand(sp, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                connection.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error en ExecuteSpQuery: {ex.Message}");
                dt = new DataTable();
            }

            return dt;
        }

        public bool ExecuteSpDml(string sp, List<SpParameter>? param = null)
        {
            try
            {
                using var connection = new SqlConnection(_connection);
                using var cmd = new SqlCommand(sp, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (var p in param)
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                }

                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool executeTransaction(Facturas factura)
        {
            using var connection = new SqlConnection(_connection);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var cmd = new SqlCommand("sp_Guardar_Factura", connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@pago", factura.Pago);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);

                var idfactura = new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(idfactura);

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows <= 0) { transaction.Rollback(); return false; }

                int idparafactura = (int)idfactura.Value;

                foreach (var i in factura.detalleFacturas)
                {
                    if (i.NroArticulo == null) { transaction.Rollback(); return false; }

                    var cmdDetalle = new SqlCommand("sp_Guardar_Detalle", connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nroFactura", idparafactura);
                    cmdDetalle.Parameters.AddWithValue("@nroArticulo", i.NroArticulo.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", i.Cantidad);

                    if (cmdDetalle.ExecuteNonQuery() <= 0) { transaction.Rollback(); return false; }
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }


    }
}
