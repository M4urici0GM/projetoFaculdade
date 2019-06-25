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


        public DataTable GetMovimentos(bool ativos = true, int limit = 10, Order order = Order.ASC) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Movimentacoes INNER JOIN Usuarios ON movimentoFkUsuario = usuarioId WHERE movimentoStatus = @MovimentoStatus ORDER BY @Order LIMIT @Limit", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@MovimentoStatus", ativos);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetMovimentos(Usuario usuario, int limit = 10, Order order = Order.ASC) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Movimentacoes INNER JOIN Usuarios ON movimentoFkUsuario = usuarioId WHERE usuarioId = @UsuarioId ORDER BY @Order LIMIT @Limit", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuario.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetMovimentos(Usuario usuario, Pessoa pessoa, int limit = 10, Order order = Order.ASC) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Movimentacoes INNER JOIN Usuarios ON movimentoFkUsuario = usuarioId INNER JOIN Pessoas ON movimentoFkPessoa = pessoaId  WHERE usuarioId = @UsuarioId AND pessoaId = @PessoaId ORDER BY @Order LIMIT @Limit", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuario.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetMovimentos(DateTime inicio, DateTime final, int limit = 10, Order order = Order.ASC) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Movimentacoes INNER JOIN Usuarios ON movimentoFkUsuario = usuarioId INNER JOIN Pessoas ON movimentoFkPessoa = pessoaId  WHERE movimentoDataEmissao BETWEEN @DataInicial AND @DataFinal ORDER BY movimentoDataEmissao @Order", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@DataInicial", inicio);
            mySqlCommand.Parameters.AddWithValue("@DataFinal", final);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetMovimentos(Usuario usuario, Pessoa pessoa, DateTime inicio, DateTime fim, int limit = 10, Order order = Order.ASC) {

            string query = @"SELECT * FROM Movimentacoes 
                             INNER JOIN 
                                Usuarios ON movimentoFkUsuario = usuarioId 
                             INNER JOIN 
                                Pessoas ON movimentoFkPessoa = pessoaId 
                             WHERE 
                                pessoaId = @PessoaId AND usuarioId = @UsuarioId 
                             AND 
                                movimentoDataEmissao 
                                    BETWEEN 
                                        @DataInicio 
                                    AND 
                                        @DataFinal
                             ORDER BY @Order
                             LIMIT @Limit";

            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuario.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
            mySqlCommand.Parameters.AddWithValue("@DataInicio", inicio);
            mySqlCommand.Parameters.AddWithValue("@DataFinal", fim);
            mySqlCommand.Parameters.AddWithValue("@Order", order);
            mySqlCommand.Parameters.AddWithValue("@Limit", limit);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public Movimento GetMovimentos(Movimento movimento) {
            string query = @"SELECT * FROM Movimentacoes 
                            INNER JOIN 
                                Usuarios ON movimentoFkUsuario = usuarioId
                            INNER JOIN
                                Pessoas ON movimentoFkPessoa = pessoaId
                            WHERE
                                movimentoId = @MovimentoId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@MovimentoId", movimento.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new Movimento() {
                        Id = mySqlDataReader.GetInt32(0),
                        DataEmissao = mySqlDataReader.GetDateTime(7),
                        DataPagamento = mySqlDataReader.GetDateTime(8),
                        FkConta = mySqlDataReader.GetInt32(4),
                        FkPessoa = mySqlDataReader.GetInt32(6),
                        FkUsuario = mySqlDataReader.GetInt32(5),
                        Origem = mySqlDataReader.GetString(2),
                        Status = mySqlDataReader.GetBoolean(9),
                        Tipo = mySqlDataReader.GetString(1),
                        Valor = mySqlDataReader.GetFloat(3)
                    };
                }
            }
            return new Movimento();
        }
    }
}