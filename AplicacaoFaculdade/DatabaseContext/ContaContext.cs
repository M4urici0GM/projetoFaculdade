using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade.Enums;
using System.Data;
using MySql.Data.MySqlClient;

namespace AplicacaoFaculdade.DatabaseContext {
    public class ContaContext {

        public int? AffectedRows { get; private set; }
        public int? LastInsertId { get; private set; }
        public Conta LastCreated { get; private set; }
        public DataTable LastSelection { get; private set; }

        private readonly MySqlConnection databaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public ContaContext() {
            databaseConnection = Database.GetInstance().GetConnection();
            LastCreated = new Conta();
            LastSelection = new DataTable();
        }


        public DataTable GetContas(bool ativos = true, int limit = 10, Order order = Order.ASC) {
            string query = @"
                SELECT * FROM Contas 
                WHERE 
                    contaStatus = @ContaStatus
                ORDER BY 
                    contaNome @Order
                LIMIT @Limit";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaStatus", ativos);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetContas(Conta conta, int limit = 10, Order order = Order.ASC) {
            string query = @"
                SELECT * FROM Contas 
                WHERE 
                    contaId = @ContaId
                ORDER BY 
                    contaNome @Order
                LIMIT @Limit";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaId", conta.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public bool CreateConta(Conta conta) {
            string query = "INSERT INTO Contas () VALUES (null, @ContaNome, true)";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaNome", conta.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInsertId = (int)mySqlCommand.LastInsertedId;
                LastCreated = conta;
            }
            return (AffectedRows > 0);
        }

        public bool UpdateConta(Conta conta) {
            string query = "UPDATE Contas SET contaNome = @ContaNome, contaStatus = @ContaStatus WHERE contaId = @ContaId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContaNome", conta.Nome);
            mySqlCommand.Parameters.AddWithValue("@ContaStatus", conta.Status.Value);
            mySqlCommand.Parameters.AddWithValue("@ContaId", conta.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInsertId = (int)mySqlCommand.LastInsertedId;
            }
            return (AffectedRows > 0);
        }


    }
}