using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.DatabaseContext {
    public class PessoaContext {

        private MySqlConnection database;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public PessoaContext() {
            database = Database.GetInstance().GetConnection();
        }

        public DataTable GetPessoas(bool pessoaStatus = true) {
            DataTable pessoasDataTable = new DataTable();
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaStatus = @PessoaStatus", database);
            mySqlCommand.Parameters.AddWithValue("PessoaStatus", pessoaStatus);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(pessoasDataTable);
                return pessoasDataTable;
            }
        }

        public Pessoa GetPessoa(int pessoaId) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoaId);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                        return new Pessoa() {
                            pessoaId = mySqlDataReader.GetInt32(0),
                            pessoaNome = mySqlDataReader.GetString(1),
                            pessoaSobreNome = mySqlDataReader.GetString(2),
                            pessoaJuridica  = mySqlDataReader.GetBoolean(3),
                            pessoaStatus    = mySqlDataReader.GetBoolean(4),
                            pessoaSexo      = mySqlDataReader.GetBoolean(5),
                            pessoaNascimento = mySqlDataReader.GetDateTime(6)
                        };
                    } else {
                        throw new Exception("Pessoa não encontrada");
                    }
                }
            }
        }

        public bool DeletePessoa(Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("UPDATE Pessoas SET pessoaStatus = 0 WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.pessoaId);
            using (mySqlCommand) {
                int affectedRows = mySqlCommand.ExecuteNonQuery();
                return (affectedRows > 0);
            }
        }

    }
}