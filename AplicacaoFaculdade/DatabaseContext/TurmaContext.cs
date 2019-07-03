using System;
using System.Data;
using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;

namespace AplicacaoFaculdade.DatabaseContext {
    public class TurmaContext {

        private MySqlCommand mySqlCommand;
        private MySqlConnection databaseConnection;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public int AffectedRows { get; private set; }
        public int LastInsertedId { get; private set; }

        public TurmaContext() {
            databaseConnection = Database.GetInstance().GetConnection();
            AffectedRows = 0;
        }

        public bool CreateTurma(Turma turma, DataTable horarios) {
            string query = @"
                INSERT INTO Turmas() VALUES (null, @TurmaNome, @TurmaMax, @TurmaServico, true, @TurmaFkFuncionario)
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaNome", turma.Nome);
            mySqlCommand.Parameters.AddWithValue("@TurmaMax", turma.Max);
            mySqlCommand.Parameters.AddWithValue("@TurmaServico", turma.FkServico);
            mySqlCommand.Parameters.AddWithValue("@turmaFkFuncionario", turma.FkFuncionario);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            if (AffectedRows > 0) {
                LastInsertedId = (int) mySqlCommand.LastInsertedId;
                if (horarios.Rows.Count > 0) {
                    foreach (DataRow dataRow in horarios.Rows) {
                        query = "INSERT INTO TurmaHorarios() VALUES (null, @FkTurma, @HorarioInicio, @HorarioFim, true, @Dia)";
                        mySqlCommand = new MySqlCommand(query, databaseConnection);
                        mySqlCommand.Parameters.AddWithValue("@FkTurma", LastInsertedId);
                        mySqlCommand.Parameters.AddWithValue("@HorarioInicio", dataRow["turmaHorarioInicio"]);
                        mySqlCommand.Parameters.AddWithValue("@HorarioFim", dataRow["turmaHorarioFim"]);
                        mySqlCommand.Parameters.AddWithValue("@Dia", dataRow["turmaHorarioDia"]);
                        AffectedRows = mySqlCommand.ExecuteNonQuery();
                    }   
                }
                return (AffectedRows > 0);
            }
            return false;
            ;
        }

        internal DataTable GetHorarios(Turma turma) {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT * FROM Turmas
                INNER JOIN TurmaHorarios
                    ON turmaId = turmaHorarioFkTurma
                WHERE turmaId = @TurmaId
                ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetTurmas(bool ativas = true) {
            DataTable datatable = new DataTable();
            string query = @"
                SELECT * FROM Turmas
                INNER JOIN Funcionarios
                    ON turmaFKFuncionario = funcionarioId
                INNER JOIN Servicos
                    ON turmaServico = servicoId
                INNER JOIN Pessoas
                    ON funcionarioFkPessoa = pessoaId
                WHERE   
                    turmaStatus = @TurmaStatus";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaStatus", ativas);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(datatable);
            return datatable;
        }

        public Turma GetTurmas(Turma turma) {
            string query = @"
                SELECT * FROM Turmas    
                WHERE
                    turmaId = @TurmaId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                Turma _turma = new Turma() {
                    Id = mySqlDataReader.GetInt32(0),
                    Nome = mySqlDataReader.GetString(1),
                    Max = mySqlDataReader.GetInt32(2),
                    FkServico = mySqlDataReader.GetInt32(3),
                    Status = mySqlDataReader.GetBoolean(4),
                    FkFuncionario = mySqlDataReader.GetInt32(5)
                };
                mySqlDataReader.Close();
                return _turma;
            }
            return new Turma();
        }

        public bool UpdateTurma(Turma turma) {
            string query = @"
                UPDATE Turmas SET 
                    turmaNome = @TurmaNome, turmaMax = @TurmaMax, turmaServico = @FkServico, turmaStatus = @TurmaStatus, turmaFkFuncionario = @FkFuncionario
                WHERE
                    turmaId = @TurmaId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaNome", turma.Nome);
            mySqlCommand.Parameters.AddWithValue("@TurmaMax", turma.Max);
            mySqlCommand.Parameters.AddWithValue("@FkServico", turma.FkServico);
            mySqlCommand.Parameters.AddWithValue("@TurmaStatus", turma.Status);
            mySqlCommand.Parameters.AddWithValue("@FkFuncionario", turma.FkFuncionario);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return (AffectedRows > 0);
        }

        public DataTable GetAlunos(Turma turma) {
            string query = @"";
            return new DataTable();
        }
    }
}