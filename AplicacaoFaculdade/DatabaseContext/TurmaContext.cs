using System;
using System.Data;
using System.Globalization;
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
                LastInsertedId = (int)mySqlCommand.LastInsertedId;
                if (horarios.Rows.Count > 0) {
                    CreateHorarios(horarios, new Turma() { Id = LastInsertedId });
                }
                return (AffectedRows > 0);
            }
            return false;
            ;
        }

        public bool CreateHorarios(DataTable horarios, Turma turma) {
            foreach (DataRow dataRow in horarios.Rows) {
                string query = "INSERT INTO TurmaHorarios() VALUES (null, @FkTurma, @HorarioInicio, @HorarioFim, true, @Dia)";
                mySqlCommand = new MySqlCommand(query, databaseConnection);
                mySqlCommand.Parameters.AddWithValue("@FkTurma", turma.Id);
                mySqlCommand.Parameters.AddWithValue("@HorarioInicio", dataRow["turmaHorarioInicio"]);
                mySqlCommand.Parameters.AddWithValue("@HorarioFim", dataRow["turmaHorarioFim"]);
                mySqlCommand.Parameters.AddWithValue("@Dia", dataRow["turmaHorarioDia"]);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return AffectedRows > 0;
        }

        internal DataTable GetHorarios(Turma turma) {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT * FROM Turmas
                INNER JOIN TurmaHorarios
                    ON turmaId = turmaHorarioFkTurma
                WHERE turmaId = @TurmaId AND turmaHorarioStatus = true
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

        public TurmaHorario GetHorario(TurmaHorario turmaHorario) {
            string query = @"
            SELECT * FROM TurmaHorarios
            WHERE TurmaHorarioId = @TurmaHorarioId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaHorarioId", turmaHorario.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                TurmaHorario _turmaHorario = new TurmaHorario() {
                    Id = mySqlDataReader.GetInt32(0),
                    FkTurma = mySqlDataReader.GetInt32(1),
                    Inicio = DateTime.ParseExact(mySqlDataReader.GetString(2), "HH:mm:ss", CultureInfo.InvariantCulture),
                    Fim = DateTime.ParseExact(mySqlDataReader.GetString(3), "HH:mm:ss", CultureInfo.InvariantCulture),
                    Status = mySqlDataReader.GetBoolean(4),
                    Dia = mySqlDataReader.GetInt32(5)
                };
                mySqlDataReader.Close();
                return _turmaHorario;
            }
            return new TurmaHorario();
        }

        public bool UpdateHorario(TurmaHorario _turmaHorario) {
            string query = @"
                UPDATE TurmaHorarios
                SET
                    turmaHorarioFkTurma = @FkTurma, turmaHorarioInicio = @TurmaHorarioInicio, turmaHorarioFim = @HorarioFim, turmaHorarioDia = @Dia, turmaHorarioStatus = @TurmaHorarioStatus
                WHERE
                    turmaHorarioId = @TurmaHorarioId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@FkTurma", _turmaHorario.FkTurma);
            mySqlCommand.Parameters.AddWithValue("@TurmaHorarioInicio", _turmaHorario.Inicio);
            mySqlCommand.Parameters.AddWithValue("@HorarioFim", _turmaHorario.Fim);
            mySqlCommand.Parameters.AddWithValue("@Dia", _turmaHorario.Dia);
            mySqlCommand.Parameters.AddWithValue("@TurmaHorarioStatus", _turmaHorario.Status);
            mySqlCommand.Parameters.AddWithValue("@TurmaHorarioId", _turmaHorario.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return (AffectedRows > 0);
        }


        public DataTable GetAlunos(Turma turma) {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT * FROM Turmas
                INNER JOIN TurmaAlunos 
                    ON turmaId = fkTurma
                INNER JOIN Alunos
                    ON fkAluno = alunoId
                INNER JOIN Pessoas
                    ON alunoFkPessoa = pessoaId
                WHERE
                    alunoStatus = true AND pessoaStatus = true AND turmaId = @TurmaId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool GetHorarioConflito(Turma turma, Aluno aluno) {
            string query = @"
                SELECT horarioInicio, horarioSaida FROM	(
	                SELECT turmaHorarioInicio as horarioInicio, count(turmaHorarioId) as Id  FROM Turmas 
	                INNER JOIN TurmaHorarios 
		                ON turmaId = turmaHorarioFkTurma
	                WHERE turmaId = @TurmaId
		                AND turmaHorarioStatus = true
	                GROUP BY turmaHorarioId
	                ORDER BY 
		                turmaHorarioInicio ASC LIMIT 1

	                ) horarioInicioTable
		        LEFT JOIN 
                    (
                    select * FROM	(
	                    SELECT turmaHorarioFim as horarioSaida, count(turmaHorarioId) as Id  FROM Turmas
	                    INNER JOIN TurmaHorarios
		                    ON turmaId = turmaHorarioFkTurma
	                    WHERE turmaId = @TurmaId
		                    AND turmaHorarioStatus = true
	                    GROUP BY turmaHorarioId
		                    ORDER BY 
		                    turmaHorarioFim DESC LIMIT 1
	                    ) horarioFinalTable
                    ) joinSelect
                ON horarioInicioTable.Id = joinSelect.Id";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                DateTime horarioInicio = DateTime.ParseExact(mySqlDataReader.GetString(0), "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime horarioFim = DateTime.ParseExact(mySqlDataReader.GetString(1), "HH:mm:ss", CultureInfo.InvariantCulture);
                mySqlDataReader.Close();
                query = @"
                    SELECT * FROM Alunos 
                    INNER JOIN TurmaAlunos
	                    ON alunoId = fkAluno
                    INNER JOIN Turmas
	                    ON fkTurma = turmaId
                    LEFT JOIN TurmaHorarios 
	                    ON turmaId = turmaHorarioFkturma
                    WHERE 
	                    alunoId = @AlunoId
                        AND turmaHorarioStatus = true
	                    AND (turmaHorarioInicio >= @HorarioInicio OR turmaHorarioFim <= @HorarioFinal)";
                mySqlCommand = new MySqlCommand(query, databaseConnection);
                mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
                mySqlCommand.Parameters.AddWithValue("@HorarioInicio", horarioInicio);
                mySqlCommand.Parameters.AddWithValue("@HorarioFinal", horarioFim);
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable horariosAluno = new DataTable();
                mySqlDataAdapter.Fill(horariosAluno);
                return horariosAluno.Rows.Count > 0;
            }
            return false;
        }

        public bool AdicionarAluno(Aluno aluno, Turma turma) {
            string query = @"INSERT INTO TurmaAlunos() VALUES (@FkAluno, @FkTurma)";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@FkAluno", aluno.Id);
            mySqlCommand.Parameters.AddWithValue("@FkTurma", turma.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public bool RemoverAluno(Aluno aluno, Turma turma) {
            string query = @"
                DELETE FROM TurmaAlunos 
                WHERE 
                    fkAluno = @AlunoId
                    AND fkTurma = @TurmaId";
            mySqlCommand = new MySqlCommand(query, databaseConnection);
            mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }
    }
}