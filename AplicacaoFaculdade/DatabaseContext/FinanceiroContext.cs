using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AplicacaoFaculdade.Models;
using System.Data;
using MySql.Data.MySqlClient;
using AplicacaoFaculdade.Enums;

namespace AplicacaoFaculdade.DatabaseContext {
    public class FinanceiroContext {

        public int? AffectedRows { get; private set; }
        public int? LastInsertId { get; private set; }
        public Movimento LastCreated { get; private set; }
        public DataTable LastSelection { get; private set; }

        private MySqlConnection databaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public FinanceiroContext() {
            LastCreated = new Movimento();
            LastSelection = new DataTable();
            databaseConnection = Database.GetInstance().GetConnection();
        }


        public DataTable GetMovimentos() {

            return new DataTable();
        }


        public DataTable GetContas(bool ativos = true) {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT * FROM Contas
                WHERE contaStatus = @ContaStatus";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaStatus", ativos);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool CreateConta(Conta conta) {
            string query = @"
                INSERT INTO Contas() VALUES(null, @ContaNome, @ContaSaldo, true);
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaNome", conta.Nome);
            mySqlCommand.Parameters.AddWithValue("@ContaSaldo", conta.Saldo);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public bool UpdateConta(Conta conta) {
            string query = @"
                UPDATE Contas 
                SET
                    contaNome = @ContaNome, contaSaldo = @ContaSaldo, contaStatus = @ContaStatus
                WHERE
                    contaId = @ContaId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaNome", conta.Nome);
            mySqlCommand.Parameters.AddWithValue("@ContaSaldo", conta.Saldo);
            mySqlCommand.Parameters.AddWithValue("@ContaStatus", conta.Status);
            mySqlCommand.Parameters.AddWithValue("@ContaId", conta.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public Conta GetContas(Conta conta) {
            string query = @"
                SELECT * FROM Contas WHERE contaId = @ContaId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaId", conta.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                Conta _conta = new Conta() {
                    Id = mySqlDataReader.GetInt32(0),
                    Nome = mySqlDataReader.GetString(1),
                    Saldo = mySqlDataReader.GetFloat(2),
                    Status = mySqlDataReader.GetBoolean(3)
                };
                mySqlDataReader.Close();
                return _conta;
            }
            return new Conta();
        }
    }
}