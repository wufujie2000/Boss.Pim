using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;

namespace Boss.Pim.AdoNet
{
    /// <summary>
    /// 数据访问基础类
    /// 作者：付亮
    /// </summary>
    public class SQLUtil : IDisposable
    {
        /// <summary>
        /// 读库IP
        /// </summary>
        public static String DevIPDBR
        {
            get
            {
                var ipdb = System.Configuration.ConfigurationManager.ConnectionStrings["DevIPDBR"];
                if (ipdb != null)
                {
                    return ipdb.ConnectionString;
                }
                return null;
            }
        }
        /// <summary>
        /// 写库IP
        /// </summary>
        public static String DevIPDBW
        {
            get
            {
                var ipdb = System.Configuration.ConfigurationManager.ConnectionStrings["DevIPDBW"];
                if (ipdb != null)
                {
                    return ipdb.ConnectionString;
                }
                return null;
            }
        }
        public static String DefaultConnStr
        {
            get
            {
                var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
                if (connStr != null)
                {
                    return connStr.ConnectionString;
                }
                return null;
            }
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string connStr = null;
        /// <summary>
        /// 数据库操作命令
        /// </summary>
        SqlCommand cmd = null;

        private SqlConnection GetConnection(string cmdText)
        {
            //如果sql 语句中 连接字符串地址是读库的地址            
            if (!string.IsNullOrWhiteSpace(DevIPDBW) && !string.IsNullOrWhiteSpace(DevIPDBR) && connStr.IndexOf(DevIPDBW) >= 0)
            {
                // 同时 sql语句中，包含 insert into  或者包含update  set  或者包含delete， 则替换为写库地址，防止程序误操作，导致数据库写操作，连接到了读库  ，从而是数据库订阅数据失败。
                if ((cmdText.ToLower().IndexOf("insert") >= 0 && cmdText.ToLower().IndexOf("into") >= 0)
                    || (cmdText.ToLower().IndexOf("update") >= 0 && cmdText.ToLower().IndexOf("set") >= 0)
                    || (cmdText.ToLower().IndexOf("delete") >= 0))
                {
                    connStr = connStr.Replace(DevIPDBR, DevIPDBW);
                }
            }
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
        #region 构造函数
        public SQLUtil() : this(DefaultConnStr)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">数据库连接字符串，使用时尽量调用APIConfig中的相应连接字符串</param>
        public SQLUtil(string connStr)
        {
            cmd = new SqlCommand();
            cmd.CommandTimeout = 36000;
            this.connStr = connStr ?? throw new ArgumentNullException(connStr);
        }
        #endregion

        #region 设置参数相关
        public void AddParameter<TVal>(StringBuilder builderSql, string column, string paramName, bool condition, TVal paramValue)
        {
            if (condition)
            {
                builderSql.Append(column).Append("=").Append(paramName);
                AddParameter(paramName, paramValue);
            }
        }
        public void AddParameter(string paramName, object val)
        {
            if (val == null)
            {
                val = DBNull.Value;
            }
            SqlParameter param = new SqlParameter(paramName, val);
            param.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(param);
        }

        public void AddParameter(string paramName, object val, SqlDbType sqlDbType)
        {
            if (val == null)
            {
                val = DBNull.Value;
            }
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = sqlDbType;
            param.Value = val;

            cmd.Parameters.Add(param);
        }

        public void AddParameter(string paramName, object val, SqlDbType sqlDbType, int size)
        {
            if (val == null)
            {
                val = DBNull.Value;
            }
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            param.SqlDbType = sqlDbType;
            param.Size = size;
            param.Value = val;

            cmd.Parameters.Add(param);
        }

        public void AddOutputParameter(string paramName)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 100;

            cmd.Parameters.Add(param);
        }

        public void AddOutputParameter(string paramName, SqlDbType sqlDbType, int size)
        {
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.SqlDbType = sqlDbType;
            size = size > 0 ? size : 100;

            cmd.Parameters.Add(param);
        }
        #endregion

        #region ExecNonQuery
        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令，常用于对数据库执行增、删、改操作
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecNonQuery(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdtype;
                    conn.Open();
                    int resual = cmd.ExecuteNonQuery();
                    conn.Close();
                    return resual;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Parameters.Count > 0)
                    {
                        cmd.Parameters.Clear();
                    }
                    cmd.Dispose();
                }
            }
        }
        #endregion

        #region ExecNonQueryAsync
        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令，常用于对数据库执行增、删、改操作
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public async Task<int> ExecNonQueryAsync(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdtype;
                    conn.Open();
                    int resual = await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    return resual;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Parameters.Count > 0)
                    {
                        cmd.Parameters.Clear();
                    }
                    cmd.Dispose();
                }
            }
        }
        #endregion

        #region ExecScalar
        /// <summary>
        /// 执行一个查询，返回第一行第一列数据，常用于取最大值，查看表中有多少数据等操作
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回第一行第一列数据（object）</returns>
        public object ExecScalar(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdtype;
                    conn.Open();
                    object resual = cmd.ExecuteScalar();
                    conn.Close();
                    return resual;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Parameters.Count > 0)
                    {
                        cmd.Parameters.Clear();
                    }
                    cmd.Dispose();
                }
            }
        }
        #endregion

        #region ExecScalarAsync
        /// <summary>
        /// 执行一个查询，返回第一行第一列数据，常用于取最大值，查看表中有多少数据等操作
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回第一行第一列数据（object）</returns>
        public async Task<object> ExecScalarAsync(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdtype;
                    conn.Open();
                    object resual = await cmd.ExecuteScalarAsync();
                    conn.Close();
                    return resual;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmd.Parameters.Count > 0)
                    {
                        cmd.Parameters.Clear();
                    }
                    cmd.Dispose();
                }
            }
        }
        #endregion

        #region ExecReader
        /// <summary>
        /// 连接式只读查询，返回一个（SqlDataReader类型的）结果集，由于是连接数据库查询只读查询，因此速度快，节省内存，对要查询的数据量大有优势
        /// </summary>       
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回（SqlDataReader类型的）结果集</returns>
        public SqlDataReader ExecReader(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            SqlConnection conn = GetConnection(cmdText);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//关闭关联的Connection 

                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                return rdr;
            }
            catch (SqlException ex)
            {
                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                throw ex;
            }
        }
        #endregion

        #region ExecReaderAsync
        /// <summary>
        /// 连接式只读查询，返回一个（SqlDataReader类型的）结果集，由于是连接数据库查询只读查询，因此速度快，节省内存，对要查询的数据量大有优势
        /// </summary>       
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="cmdtype">SqlCommand命令类型 (存储过程， T-SQL语句等)</param>
        /// <returns>返回（SqlDataReader类型的）结果集</returns>
        public async Task<SqlDataReader> ExecReaderAsync(string cmdText, CommandType cmdtype = CommandType.Text)
        {
            SqlConnection conn = GetConnection(cmdText);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);//关闭关联的Connection 

                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                return rdr;
            }
            catch (SqlException ex)
            {
                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                throw ex;
            }
        }
        #endregion

        #region ExecDataTable
        /// <summary>
        /// 执行查询，返回DataTable结果集，由于DataTable存储于内存中，效率高，但是比较占用内存，因此常用于查询量较少的数据。
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <returns>返回（DataTable类型的）结果集</returns>
        public DataTable ExecDataTable(string cmdText)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                        return dt;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmd.Parameters.Count > 0)
                        {
                            cmd.Parameters.Clear();
                        }
                        cmd.Dispose();
                    }
                }
            }
        }  /// <summary>
           /// 执行查询，返回DataTable结果集，由于DataTable存储于内存中，效率高，但是比较占用内存，因此常用于查询量较少的数据。
           /// </summary>
           /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
           /// <param name="tabName">表别名</param>
           /// <returns>返回（DataTable类型的）结果集</returns>
        public DataTable ExecDataTable(string cmdText, string tabName)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                        dt.TableName = tabName;
                        return dt;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmd.Parameters.Count > 0)
                        {
                            cmd.Parameters.Clear();
                        }
                        cmd.Dispose();
                    }
                }
            }
        }
        #endregion

        #region ExecDataSet
        /// <summary>
        /// 执行查询，返回ExecDataSet结果集，由于DataSet存储于内存中，效率高，但是比较占用内存，因此常用于查询量较少的数据。
        /// </summary>
        /// <param name="cmdText">T-SQL 语句</param>
        /// <param name="tabNames">表别名</param>
        /// <returns>返回（DataSet类型的）结果集</returns>
        public DataSet ExecDataSet(string cmdText, params string[] tabNames)
        {
            using (SqlConnection conn = GetConnection(cmdText))
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        conn.Open();

                        if (tabNames != null && tabNames.Length > 0)
                        {
                            var i = 0;
                            foreach (var tabName in tabNames)
                            {
                                if (i == 0)
                                {
                                    da.TableMappings.Add("Table", tabName);
                                }
                                else
                                {
                                    da.TableMappings.Add("Table" + i.ToString(), tabName);
                                }
                                i++;
                            }
                        }
                        da.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmd.Parameters.Count > 0)
                        {
                            cmd.Parameters.Clear();
                        }
                        cmd.Dispose();
                    }
                }
            }
        }
        #endregion

        #region ExecGetList
        /// <summary>  
        /// 执行sql语句，得到实体对象集，无数据，返回的List的Count为0（内部使用ADO.NET的DbDataReader，可以查询任意列，但使用反射，效率略低）
        /// </summary>  
        /// <param name="sql">sql语句或存储过程</param>  
        /// <returns></returns>  
        public List<T> ExecGetList<T>(string sql) where T : class, new()
        {
            List<T> list = new List<T>();
            using (var dr = ExecReader(sql))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T model = new T();
                        list.Add(ToModel(dr, model));
                    }
                }
            }
            return list;
        }
        #endregion

        #region ExecGetListAsync
        /// <summary>  
        /// 执行sql语句，得到实体对象集，无数据，返回的List的Count为0（内部使用ADO.NET的DbDataReader，可以查询任意列，但使用反射，效率略低）
        /// </summary>  
        /// <param name="sql">sql语句或存储过程</param>  
        /// <returns></returns>  
        public async Task<List<T>> ExecGetListAsync<T>(string sql) where T : class, new()
        {
            List<T> list = new List<T>();
            using (var dr = await ExecReaderAsync(sql))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T model = new T();
                        list.Add(ToModel(dr, model));
                    }
                }
            }
            return list;
        }
        #endregion

        #region ExecGet
        /// <summary>  
        /// 执行sql语句，得到第一行实体对象，无数据，返回默认模型（内部使用ADO.NET的DbDataReader，与ExecGet差异在于：ExecGet的sql语句查询必须是所有列，而该方法无此要求，可以查询任意列，但使用反射，效率略低）
        /// </summary>
        /// <param name="sql">sql语句或存储过程</param>  
        /// <returns></returns>  
        public T ExecGet<T>(string sql) where T : class, new()
        {
            T model = new T();
            using (var dr = ExecReader(sql))
            {
                if (dr.HasRows)
                {
                    dr.Read();
                    model = ToModel(dr, model);
                }
            }
            return model;
        }
        #endregion

        #region ExecGetAsync
        /// <summary>  
        /// 执行sql语句，得到第一行实体对象，无数据，返回默认模型（内部使用ADO.NET的DbDataReader，与ExecGet差异在于：ExecGet的sql语句查询必须是所有列，而该方法无此要求，可以查询任意列，但使用反射，效率略低）
        /// </summary>
        /// <param name="sql">sql语句或存储过程</param>  
        /// <returns></returns>  
        public async Task<T> ExecGetAsync<T>(string sql) where T : class, new()
        {
            T model = new T();
            using (var dr = await ExecReaderAsync(sql))
            {
                if (dr.HasRows)
                {
                    dr.Read();
                    model = ToModel(dr, model);
                }
            }
            return model;
        }
        #endregion

        #region ToModel
        /// <summary>
        /// DataReader 类型  转换为 T模板，内部使用反射
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public T ToModel<T>(DbDataReader dr, T model = null) where T : class, new()
        {
            if (model == null)
            {
                model = new T();
            }
            string modelName = "";
            // 获得此模型的公共属性
            PropertyInfo[] pis = model.GetType().GetProperties();
            if (pis.Length > 0)
            {
                foreach (PropertyInfo pi in pis)
                {
                    modelName = pi.Name;
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        //判断是否含有tempName字段
                        var dbCol = dr.GetName(i);
                        if (dbCol.Equals(modelName) || (modelName == "Id" && dbCol.ToLower() == modelName.ToLower()))
                        //if (dr.GetSchemaTable().Columns.Contains(tempName))
                        {
                            object value = dr[dbCol];
                            if (value != null)
                            {
                                // 判断此属性是否有Setter
                                if (!pi.CanWrite) continue;
                                if (value != DBNull.Value)
                                {
                                    //if (dr[pi.Name].GetType().Name.ToString().ToLower() == "datetime")
                                    //    pi.SetValue(model, Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"), null);
                                    //else
                                    pi.SetValue(model, value, null);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            return model;
        }
        #endregion

        /// <summary>
        /// 得到实体对象集，按照传入的分页大小分页查询，返回的List数据Count=0，用法GetList("where ... ")
        /// </summary>
        /// <param name="strWhere">如需要条件，字符串格式where ..</param>
        /// <param name="columns">要查询的列使用,分割的字符串，默认为*</param>
        /// <returns></returns>
        public List<List<T>> GetList<T>(string connStr, int pageSize = 1000, string strWhere = "", string columns = "*") where T : class, new()
        {
            string tabName = typeof(T).Name;
            if (!string.IsNullOrWhiteSpace(tabName))
            {
                List<List<T>> result = new List<List<T>>();

                SQLUtil db = new SQLUtil(connStr);
                string sql = string.Format(@"SELECT CONVERT(INT, CEILING(CAST(Count(*) AS FLOAT)/{1})) AS count FROM {2} {0}", strWhere, pageSize, tabName);
                var pagecount = ChangeInt(db.ExecScalar(sql));
                if (pagecount > 0)
                {
                    var orderBy = " ID ";
                    for (int pageIndex = 1; pageIndex <= pagecount; pageIndex++)
                    {
                        string pagersql = string.Format("select top {0} * from (select ROW_NUMBER() over(order by {1}) [rowNum],{4} from {5} {2}) [t] where [t].[rowNum]>{0}*({3}-1);", pageSize.ToString(), orderBy, strWhere, pageIndex.ToString(), columns, tabName);
                        var lst = db.ExecGetList<T>(pagersql);
                        if (lst != null && lst.Count > 0)
                        {
                            result.Add(lst);
                        }
                    }
                }

                return result;
            }
            return null;
        }

        public List<DataTable> GetListTable<T>(string tabName, string connStr, int pageSize = 1000, string strWhere = "", string columns = "*") where T : class, new()
        {
            if (!string.IsNullOrWhiteSpace(tabName))
            {
                List<DataTable> result = new List<DataTable>();
                SQLUtil db = new SQLUtil(connStr);
                string sql = string.Format(@"SELECT CONVERT(INT, CEILING(CAST(Count(*) AS FLOAT)/{1})) AS count FROM {2} {0}", strWhere, pageSize, tabName);
                var pagecount = ChangeInt(db.ExecScalar(sql));
                if (pagecount > 0)
                {
                    var orderBy = " ID ";
                    for (int pageIndex = 1; pageIndex <= pagecount; pageIndex++)
                    {
                        string pagersql = string.Format("select top {0} * from (select ROW_NUMBER() over(order by {1}) [rowNum],{4} from {5} {2}) [t] where [t].[rowNum]>{0}*({3}-1);", pageSize.ToString(), orderBy, strWhere, pageIndex.ToString(), columns, tabName);
                        var dt = db.ExecDataTable(pagersql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result.Add(dt);
                        }
                    }
                }

                return result;
            }
            return null;
        }

        public void Dispose()
        {
            if (cmd != null)
            {
                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                cmd.Dispose();
            }
        }
        private int ChangeInt(object val, int result = 0)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()))
            {
                return result;
            }
            else
            {
                int dt;
                if (int.TryParse(val.ToString(), out dt))
                {
                    result = dt;
                }
                return result;
            }
        }
    }
}
