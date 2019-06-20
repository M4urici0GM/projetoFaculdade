using System;
using System.Data;
using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;

namespace AplicacaoFaculdade.DatabaseContext {
    public class TelefoneContext {

        public int? AffectedRows { get; private set; }
        public int? LastInsertedId { get; private set; }
        private readonly MySqlConnection database;
        private MySqlCommand mySqlCommand;
        private MySqlDataReader mySqlDataReader;
        private MySqlDataAdapter mySqlDataAdapter;
        private DataTable telefoneDataTable;


        public TelefoneContext() { 
            database = Database.GetInstance().GetConnection();
            telefoneDataTable = new DataTable();
        }

        public DataTable GetTelefones(bool telefoneAtivos = true) {
            string query = "SELECT * FROM Telefones INNER JOIN TiposTelefone ON telefoneFkTipo = tipoTelefoneId WHERE telefoneStatus = @TelefoneStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TelefoneStatus", telefoneAtivos);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(telefoneDataTable);
                }
            }
            return telefoneDataTable;
        }

        public DataTable GetTelefones(Pessoa pessoa, bool telefoneAtivos = true) {
            string query = "SELECT * FROM Telefones INNER JOIN TiposTelefone ON telefoneFkTipo = tipoTelefoneId INNER JOIN PessoasTelefone ON telefoneId = fkTelefone INNER JOIN Pessoas ON fkPessoa = pessoaId WHERE PessoaId = @PessoaId AND telefoneStatus = @TelefoneStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id);
            mySqlCommand.Parameters.AddWithValue("@TelefoneStatus", telefoneAtivos);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(telefoneDataTable);
                }
            }
            return telefoneDataTable;
        }

        public DataTable GetTelefoneTipos(bool ativos = true) {
            string query = "SELECT * FROM TiposTelefone WHERE tipoTelefone = @TipoTelefoneStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneStatus", ativos);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(telefoneDataTable);
                }
            }
            return telefoneDataTable;
        }

        public DataTable GetTelefoneTipos(TipoTelefone tipoTelefone, bool ativos = true) {
            string query = "SELECT * FROM TiposTelefone WHERE tipoTelefoneId = @TipoTelefoneId AND tipoTelefoneStatus = @TipoTelefoneStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneStatus", ativos);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneId", tipoTelefone.Id.Value);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(telefoneDataTable);
                }
            }
            return telefoneDataTable;
        }

        public bool CreateTelefone(Telefone telefone, TipoTelefone tiposTelefone, Pessoa pessoa) {
            string query = "INSERT INTO Telefones () VALUES (null, @TelefoneDDD, @TelefoneNumero, @TelefonePadrao, @TelefoneFKTipo, true)";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TelefoneDDD", telefone.Id);
            mySqlCommand.Parameters.AddWithValue("@TelefoneNumero", telefone.Numero);
            mySqlCommand.Parameters.AddWithValue("@TelefonePadrao", telefone.Padrao.GetValueOrDefault(false));
            mySqlCommand.Parameters.AddWithValue("@TelefoneFKTipo", tiposTelefone.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInsertedId = (int)mySqlCommand.LastInsertedId;
                if (AffectedRows > 0) {
                    query = "INSERT INTO PessoasTelefone () VALUES (@PessoaId, @TelefoneId)";
                    mySqlCommand = new MySqlCommand(query, database);
                    mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
                    mySqlCommand.Parameters.AddWithValue("@TelefoneId", LastInsertedId);
                    using (mySqlCommand) {
                        AffectedRows = mySqlCommand.ExecuteNonQuery();
                        LastInsertedId = (int)mySqlCommand.LastInsertedId;
                        return (AffectedRows > 0);
                    }
                }
            }
            return false;
        }

        public bool UpdateTelefone(Telefone telefone) {
            string query = "UPDATE Telefones SET telefoneDdd = @TelefoneDDD, telefoneNumero = @TelefoneNumero, telefonePadrao = @TelefonePadrao, telefoneFkTipo = @TelefoneFKTipo, telefoneStatus = @TelefoneStatus WHERE telefoneId = @TelefoneId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TelefoneDDD", telefone.Ddd);
            mySqlCommand.Parameters.AddWithValue("@TelefoneNumero", telefone.Numero);
            mySqlCommand.Parameters.AddWithValue("@TelefonePadrao", telefone.Padrao.GetValueOrDefault(false));
            mySqlCommand.Parameters.AddWithValue("@TelefoneFKTipo", telefone.FkTipo);
            mySqlCommand.Parameters.AddWithValue("@TelefoneStatus", telefone.Status.GetValueOrDefault(false));
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return AffectedRows.Value > 0;
        }

        public bool UpdateTipoTelefone(TipoTelefone tipoTelefone) {
            string query = "UPDATE TiposTelefone SET tipoTelefoneNome = @TipoTelefoneNome, tipoTelefoneStatus = @TipoTelefoneStatus WHERE tipoTelefoneId = @TipoTelefoneId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneNome", tipoTelefone.Nome);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneStatus", tipoTelefone.Status.GetValueOrDefault(false));
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows.Value > 0);
        }


        public bool CreateTipoTelefone(TipoTelefone tipoTelefone) {
            string query = "INSERT INTO TiposTelefone () VALUES (null, @TipoTelefoneNome, @TipoTelefoneStatus)";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneNome", tipoTelefone.Nome);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneStatus", tipoTelefone.Status.GetValueOrDefault(false));
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows.Value > 0);
        }

        public bool DeleteTelefone(Telefone telefone) {
            string query = "UPDATE Telefones SET telefoneStatus = false WHERE telefoneId = @TelefoneId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TelefoneId", telefone.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows.Value > 0);
        }

        public bool DeleteTipoTelefone(TipoTelefone tipoTelefone) {
            string query = "UPDATE TiposTelefone SET tipoTelefoneStatus = false WHERE tipoTelefoneStatus = @TipoTelefoneStatus";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TipoTelefoneStatus", tipoTelefone.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows.Value > 0);
        }
    }
}
