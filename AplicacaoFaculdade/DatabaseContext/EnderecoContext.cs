using System.Data;
using MySql.Data.MySqlClient;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade;

namespace AplicacaoFaculdade.DatabaseContext {
    public class EnderecoContext {

        public int? LastInsertId { get; set; }
        public int? AffectedRows { get; set; }
        public Endereco LastInserted { get; set; }
        public DataTable LastSelection { get; set; }

        private MySqlConnection databaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public EnderecoContext() {
            databaseConnection = Database.GetInstance().GetConnection();
            AffectedRows = 0;
        }

        public DataTable GetEnderecos(bool _ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Enderecos WHERE enderecoStatus = @Status", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@Status", _ativos);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(LastSelection);
                }
            }
            return LastSelection;
        }

        public Endereco GetEnderecos(Endereco _endeco) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Enderecos WHERE enderecoId = @EnderecoId", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@EnderecoId", _endeco.Id.Value);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows) {
                    if (mySqlDataReader.Read()) {
                        return new Endereco() {
                            Id = mySqlDataReader.GetInt32(0),
                            Logradouro = mySqlDataReader.GetString(1),
                            Desc = mySqlDataReader.GetString(2),
                            Numero = mySqlDataReader.GetInt32(3),
                            Bairro = mySqlDataReader.GetString(4),
                            Cidade = mySqlDataReader.GetString(5),
                            Cep = mySqlDataReader.GetInt32(6),
                            Status = mySqlDataReader.GetBoolean(7)
                        };
                    }
                }
            }
            return new Endereco();
        }

        public DataTable GetEnderecos(Pessoa _pessoa) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Enderecos INNER JOIN PessoasEnderecos ON enderecoId = fkEndereco INNER JOIN Pessoas on fkPessoa = pessoaId WHERE pessoaId = @PessoaId", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", _pessoa.Id.Value);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(LastSelection);
                }
            }
            return LastSelection;
        }

        public bool CreateEndereco(Endereco endereco, Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("INSER INTO Enderecos () VALUES (null, @Logradouro, @Desc, @Numero, @Bairro, @Cidade, @Cep, true)");
            mySqlCommand.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
            mySqlCommand.Parameters.AddWithValue("@Desc", endereco.Desc);
            mySqlCommand.Parameters.AddWithValue("@Numero", endereco.Numero);
            mySqlCommand.Parameters.AddWithValue("@Bairro", endereco.Bairro);
            mySqlCommand.Parameters.AddWithValue("@Cidade", endereco.Cidade);
            mySqlCommand.Parameters.AddWithValue("@Cep", endereco.Cep);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                if (AffectedRows > 0) {
                    LastInsertId = (int)mySqlCommand.LastInsertedId;
                    mySqlCommand = new MySqlCommand("INSERT INTO PessoasEnderecos() VALUES (@PessoaId, @EnderecoId)", databaseConnection);
                    mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
                    mySqlCommand.Parameters.AddWithValue("@EnderecoId", LastInsertId);
                    AffectedRows = mySqlCommand.ExecuteNonQuery();
                    return AffectedRows > 0;
                }
            }
            return false;
        }

        public bool DeleteEndereco(Endereco endereco) {
            mySqlCommand = new MySqlCommand("UPDATE Enderecos SET enderecoStatus = false WHERE enderecoId = @EnderecoId", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@EnderecoId", endereco.Id.Value);
                                  
            return false;
        }
    }
}