using AplicacaoFaculdade.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace AplicacaoFaculdade.DatabaseContext {
    public class ServicoContext {

        public int? LastInsertId { get; private set; }
        public int? AffectedRows { get; private set; }
        public Servico LastCreated { get; private set; }
        public DataTable LastSelection { get; private set; }
        public object LastSingleSelection { get; set; }

        private MySqlConnection databaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public ServicoContext () {
            databaseConnection = Database.GetInstance().GetConnection();
            AffectedRows = 0;
            LastInsertId = null;
            LastSelection = new DataTable();
            LastSingleSelection = new Servico();
        }

        public DataTable GetServicos(bool ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Servicos INNER JOIN Turmas ON servicoId = turmaServico INNER JOIN PrecoServico ON servicoId = precoServicoFkServico WHERE servicoStatus = @ServicoStatus", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }
       
        public Servico GetServicos(Servico servico) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Servicos INNER JOIN PrecoServico ON servicoId = precoServicoFkServico WHERE servicoId = @ServicoId", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoId", servico.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    LastSingleSelection = new Servico() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Status = mySqlDataReader.GetBoolean(2)
                    };
                }
            }
            return (Servico) LastSingleSelection;
        }
        
       public DataTable GetServicos(Funcionario funcionario) {
            string query = @"
                SELECT * FROM Servicos
                INNER JOIN
                    PrecoServico ON servicoId = precoServicoFkServico
                INNER JOIN
                    Turmas ON servicoId = turmaServico
                INNER JOIN 
                    FuncionarioTurmas ON turmaId = fkTurma
                INNER JOIN
                    Funcionarios ON fkFuncionario = funcionarioId
                INNER JOIN 
                    Pessoas ON funcionarioFkPessoa = pessoaId
                WHERE
                    funcionarioId = @FuncionarioId
            ";
           mySqlCommand = new MySqlCommand(query, databaseConnection);
           mySqlCommand.Parameters.AddWithValue("@FuncionarioId", funcionario.Id.Value);
           using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
               mySqlDataAdapter.Fill(LastSelection);
           }
           return LastSelection;
       }

        public bool CreateServico(Servico servico) {
            mySqlCommand = new MySqlCommand("INSERT INTO Servicos () VALUES (NULL, @ServicoNome, true)", databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoNome", servico.Nome);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInsertId = (int) mySqlCommand.LastInsertedId;
                if (AffectedRows > 0) 
                    LastCreated = servico;
            }
            return (AffectedRows > 0);
        }

        public bool UpdateServico(Servico servico) {
            mySqlCommand = new MySqlCommand("UPDATE Servicos SET servicoNome = @ServicoNome, servicoStatus = @ServicoStatus WHERE servicoId = @ServicoId");
            mySqlCommand.Parameters.AddWithValue("@ServicoNome", servico.Nome);
            mySqlCommand.Parameters.AddWithValue("@ServicoStatus", servico.Status);
            mySqlCommand.Parameters.AddWithValue("@ServicoId", servico.Id.Value);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows > 0);
        }

    }
}