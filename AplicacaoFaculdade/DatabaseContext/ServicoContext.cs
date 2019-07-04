using AplicacaoFaculdade.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System;

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
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT * FROM Servicos 
                INNER JOIN PrecoServico 
	                ON servicoId = precoServicoFkServico
                WHERE 
	                (precoServicoDataInicial < CURDATE() || precoServicoDataInicial = CURDATE()) 
                AND 
                    precoServicoDataFinal IS NULL
                AND 
                    servicoStatus = @ServicoStatus";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoStatus", ativos);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        internal bool UpdateContrato(Contrato contrato) {
            string query = @"
                UPDATE Contratos SET 
                    contratoAtivo = @ContratoStatus
                WHERE contratoId = @ContratoId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContratoStatus", contrato.Ativo);
            mySqlCommand.Parameters.AddWithValue("@ContratoId", contrato.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            //TODO: Remover Alunos das turmas quando contrato cancelado.
            return (AffectedRows > 0);
        }

        public Servico GetServicos(Servico servico) {
            string query = @"
                SELECT * FROM Servicos 
                INNER JOIN PrecoServico 
	                ON servicoId = precoServicoFkServico
                WHERE 
	                (precoServicoDataInicial < CURDATE() || precoServicoDataInicial = CURDATE()) 
                AND 
                    precoServicoDataFinal IS NULL 
                AND 
                    servicoId = @ServicoId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoId", servico.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                Servico _servico = new Servico() {
                    Id = mySqlDataReader.GetInt32(0),
                    Nome = mySqlDataReader.GetString(1),
                    Status = mySqlDataReader.GetBoolean(2),
                    precoServicoId = mySqlDataReader.GetInt32(3),
                    precoServicoValor = mySqlDataReader.GetFloat(4),
                    precoServicoDataInicial = mySqlDataReader.GetDateTime(5),
                    precoServicoDataFinal = (mySqlDataReader.IsDBNull(6) ? (DateTime?)null : mySqlDataReader.GetDateTime(6)),
                    precoServicoFkServico = mySqlDataReader.GetInt32(7)
                };
                mySqlDataReader.Close();
                return _servico;
            }
            return new Servico();
        }

        public bool CreateServico(Servico servico) {
            string query = "INSERT INTO Servicos () VALUES (null, @ServicoNome, true)";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoNome", servico.Nome);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            LastInsertId = (int) mySqlCommand.LastInsertedId;
            if (AffectedRows > 0) {
                query = "INSERT INTO PrecoServico() VALUES (null, @ServicoValor, CURDATE(), null, @FkServico)";
                mySqlCommand.CommandText = query;
                mySqlCommand.Parameters.AddWithValue("@ServicoValor", servico.precoServicoValor);
                mySqlCommand.Parameters.AddWithValue("@FkServico", LastInsertId);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                return AffectedRows > 0;
            }
            return false;
        }

        public bool UpdateServico(Servico servico) {
            Servico _servico = GetServicos(servico);
            string query = @"
                UPDATE Servicos 
                SET 
                    servicoNome = @ServicoNome, servicoStatus = @ServicoStatus
                WHERE
                    servicoId = @ServicoId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ServicoNome", servico.Nome);
            mySqlCommand.Parameters.AddWithValue("@ServicoStatus", servico.Status);
            mySqlCommand.Parameters.AddWithValue("@ServicoId", servico.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            if (_servico.precoServicoValor != servico.precoServicoValor) {
                query = "UPDATE PrecoServico SET precoServicoDataFinal = CURDATE() WHERE precoServicoId = @PrecoServicoId";
                mySqlCommand.CommandText = query;
                mySqlCommand.Parameters.AddWithValue("@PrecoServicoId", _servico.precoServicoId);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                if (AffectedRows > 0) {
                    query = "INSERT INTO PrecoServico() VALUES (null, @NovoServicoPreco, CURDATE(), null, @ServicoId)";
                    mySqlCommand.CommandText = query;
                    mySqlCommand.Parameters.AddWithValue("@NovoServicoPreco", servico.precoServicoValor);
                    AffectedRows = mySqlCommand.ExecuteNonQuery();
                }
            }
            return AffectedRows > 0;
        }

        public DataTable GetContratos(bool ativos = true) {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT *, sum(case when desconto > 0 then round((contratoValor - desconto), 2) else contratoValor end) as valorTotal FROM
                    (
                        SELECT *,
                        SUM(CASE WHEN servicos > 1 THEN ROUND(((contratoValor * 0.1)), 2) else 0 end) AS desconto
                        FROM (
                            SELECT contratoId, contratoData, contratoDiaVencimento, pessoaId, pessoaNome, pessoaSobreNome, 
                            SUM(precoServicoValor) AS contratoValor,
                            COUNT(servicoId) AS servicos
                             FROM Contratos
	                            INNER JOIN Alunos
		                            ON contratoFkAluno = alunoId
	                            INNER JOIN Pessoas
		                            ON alunoFkPessoa = pessoaId
	                            INNER JOIN ContratoTurma
		                            ON contratoId = contratoTurmaFkContrato
	                            INNER JOIN Turmas
		                            ON contratoTurmaFkTurma = turmaId
	                            INNER JOIN Servicos
		                            ON turmaServico = servicoId
	                            INNER JOIN PrecoServico
		                            ON servicoId = precoServicoFkServico
	                            WHERE 
		                            (precoServicoDataInicial < CURDATE() || precoServicoDataInicial = CURDATE()) 
                                    AND precoServicoDataFinal IS NULL 
                                    AND servicoStatus = true 
                                    AND servicoStatus = true 
                                    AND turmaStatus = true
                                    AND contratoAtivo = @ContratoStatus
	                            GROUP BY
		                            contratoId
                            ) as QUERY GROUP BY contratoId
                    ) AS finalQuery GROUP BY contratoId;";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContratoStatus", ativos);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public Contrato GetContratos(Contrato contrato) {
            string query = @"SELECT * FROM Contratos WHERE contratoId = @ContratoId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@ContratoId", contrato.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                Contrato _contrato = new Contrato() {
                    Id = mySqlDataReader.GetInt32(0),
                    FkAluno = mySqlDataReader.GetInt32(1),
                    Data = mySqlDataReader.GetDateTime(2),
                    Vencimento = mySqlDataReader.GetInt32(3),
                    Ativo = mySqlDataReader.GetBoolean(4)
                };
                return _contrato;
            }
            return new Contrato();
        }
    }
}