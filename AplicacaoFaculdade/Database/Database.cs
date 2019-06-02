using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace AplicacaoFaculdade.Database
{
    public class Database {
        private static Database databaseInstance = new Database();

        private string _connectionString;
        private MySqlConnection _mySqlConn;
        private MySqlCommand _mySqlCommand;
        private MySqlDataAdapter _mySqlReader;
        private DataSet _dataSet;

        public Database(){
            _connectionString = ConfigurationManager.ConnectionStrings["mainConnectionString"].ConnectionString;
            _mySqlCommand = new MySqlCommand();
            _mySqlConn = new MySqlConnection(_connectionString);
            _mySqlReader = new MySqlDataAdapter();
        }

        public DataSet ExecuteSelect(string query) {
            if (query.Equals(""))
                throw new ArgumentException("You need first to pass a query to execute this method");
            _mySqlCommand.CommandText = query;
            using (_mySqlCommand) {
                _mySqlReader.SelectCommand = _mySqlCommand;
                _dataSet = new DataSet();
                _mySqlReader.Fill(_dataSet);
                return _dataSet;
            }
        }

        public bool ExecuteQuery(string query){
            if (query.Equals(""))
                throw new ArgumentException("You need first to pass a query to execute this method");
            _mySqlCommand.CommandText = query;
            using (_mySqlCommand) {
                return (_mySqlCommand.ExecuteNonQuery() > 0);
            }
        }

        public static Database GetInstance() => databaseInstance;

    }
}