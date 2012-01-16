using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    Database db = null;
    public DataAccess(string connectionString) {
        db = DatabaseFactory.CreateDatabase(connectionString);
    }

    protected DataSet ExecuteDataset(string spName, params object[] parameterValues) {
        DataSet dst = new DataSet();

        try {
            dst = db.ExecuteDataSet(spName, parameterValues);
        } catch (SqlException ex) {
            ex.Data.Add("Store Proc", spName);
            ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            //throw;
        } catch (Exception ex) {
            ex.Data.Add("Store Proc", spName);
            ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            //throw;
        }

        return dst;

    }
    protected DataTable ExecuteQuery(string spName, params object[] parameterValues) {
        try {
            IDataReader dataReader = db.ExecuteReader(spName, parameterValues);
            DataTable dt = LoadDataTable(dataReader);

            if (dataReader != null || !dataReader.IsClosed)
                dataReader.Close();

            return dt;
        } catch (SqlException ex) {
            //ex.Data.Add("Store Proc", spName);
            //ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            throw ex;
        } catch (Exception ex) {
            //ex.Data.Add("Store Proc", spName);
            //ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            throw ex;
        }

    }
    protected int ExecuteNonQuery(string spName, params object[] parameterValues) {
        try {
            return db.ExecuteNonQuery(spName, parameterValues);
        } catch (SqlException ex) {
            ex.Data.Add("Store Proc", spName);
            ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            throw ex;
        } catch (Exception ex) {
            ex.Data.Add("Store Proc", spName);
            ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            throw ex;
        }
    }
    protected int ExecuteNonQuery(string spName, System.Data.Common.DbTransaction sqltransaction, params object[] parametervalues) {
        try {
            return db.ExecuteNonQuery(sqltransaction, spName, parametervalues);
        } catch (Exception ex) {
            ex.Data.Add("Store Proc", spName);
            ExceptionPolicy.HandleException(ex, "DataAccessPolicy");
            throw ex;
        }
    }
    protected DataTable LoadDataTable(IDataReader dataReader) {
        DataTable dataTable = new DataTable();

        // Populate the columns (name, type)
        for (int i = 0; i < dataReader.FieldCount; i++) {
            dataTable.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
        }

        while (dataReader.Read()) {
            // Populate the values 
            object[] values = new object[dataReader.FieldCount];
            dataReader.GetValues(values);
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

}
