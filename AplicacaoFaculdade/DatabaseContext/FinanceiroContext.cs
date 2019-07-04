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
            DataTable dataTable = new DataTable();
            string query = @"
            SELECT 
                movimentoId, movimentoTipo, movimentoOrigem, 
                movimentoValor, movimentoMulta, movimentoPago, 
                contaId, contaNome, Query1.pessoaNome as Favorecido, 
                p.pessoaNome as Movimentador, movimentoDataEmissao, 
                movimentoDatapagamento, contratoId as contratoNumero 
            FROM 
                (SELECT * FROM Movimentacoes movimento
                INNER JOIN Contas
	                ON movimentoFkConta = contaId
                INNER JOIN Usuarios
	                ON movimentoFkUsuario = usuarioId
                INNER JOIN Pessoas
	                ON movimentoFkPessoa = pessoaId) as Query1
            INNER JOIN Pessoas p
                ON usuarioFkPessoa = p.pessoaId
            LEFT JOIN Contratos
                ON movimentoFkContrato = contratoId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetMovimentos(DateTime dataInicial, DateTime dataFinal, int movimentoOrigem, int movimentoTipo, int movimentoStatus) {
            DataTable dataTable = new DataTable();
            string query = $@"
            SELECT 
                movimentoId, movimentoTipo, movimentoOrigem, 
                movimentoValor, movimentoMulta, movimentoPago, 
                contaId, contaNome, Query1.pessoaNome as Favorecido, 
                p.pessoaNome as Movimentador, movimentoDataEmissao, 
                movimentoDataPagamento, contratoId as contratoNumero 
            FROM 
                (SELECT * FROM Movimentacoes movimento
                INNER JOIN Contas
	                ON movimentoFkConta = contaId
                INNER JOIN Usuarios
	                ON movimentoFkUsuario = usuarioId
                INNER JOIN Pessoas
	                ON movimentoFkPessoa = pessoaId) as Query1
            INNER JOIN Pessoas p
                ON usuarioFkPessoa = p.pessoaId
            LEFT JOIN Contratos
                ON movimentoFkContrato = contratoId
            WHERE 
                (movimentoDataEmissao >= @DataInicial AND movimentoDataEmissao <= @DataFinal)
                AND
                    movimentoOrigem = @MovimentoOrigem
                AND
                    movimentoTipo = @MovimentoTipo
                AND {(movimentoStatus == 1 ? "movimentoDataPagamento IS NOT NULL" : "movimentoDataPagamento IS NULL")}";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-mm-dd"));
            mySqlCommand.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-mm-dd"));
            mySqlCommand.Parameters.AddWithValue("@MovimentoOrigem", movimentoOrigem.ToString());
            mySqlCommand.Parameters.AddWithValue("@MovimentoTipo", movimentoTipo.ToString());
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
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