using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System.Data;
using AplicacaoFaculdade.Enums;

namespace AplicacaoFaculdade.DatabaseContext {
    public class PessoaContext {

        public int? AffectedRows { get; private set; }
        public int? LastInserted { get; private set; }
        public DataTable LastSelection { get; set; }

        private readonly MySqlConnection database;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public PessoaContext() {
            database = Database.GetInstance().GetConnection();
            LastSelection = new DataTable();
        }

        public bool CadastrarPessoa(Pessoa pessoa) {
            string query = @"
                INSERT INTO Pessoas() VALUES
                (
                    null, @PessoaNome, @PessoaSobrenome, true, @PessoaSexo, @PessoaNascimento, @PessoaTelefone,
                    @PessoaCelular, @PessoaCNPJ, @PessoaCPF, @PessoaRG, @PessoaEndereco, @PessoaCep,
                    @PessoaNumero, @PessoaComplemento, @PessoaCidade, @PessoaEstado
                );
            ";

            using (mySqlCommand = new MySqlCommand(query, database)) {
                mySqlCommand.Parameters.AddWithValue("@PessoaNome", pessoa.Nome);
                mySqlCommand.Parameters.AddWithValue("@PessoaSobrenome", pessoa.Sobrenome);
                mySqlCommand.Parameters.AddWithValue("@PessoaSexo", pessoa.Sexo);
                mySqlCommand.Parameters.AddWithValue("@PessoaNascimento", pessoa.Nascimento);
                mySqlCommand.Parameters.AddWithValue("@PessoaTelefone", pessoa.Telefone);
                mySqlCommand.Parameters.AddWithValue("@PessoaCelular", pessoa.Celular);
                mySqlCommand.Parameters.AddWithValue("@PessoaCNPJ", pessoa.Cnpj);
                mySqlCommand.Parameters.AddWithValue("@PessoaCPF", pessoa.Cpf);
                mySqlCommand.Parameters.AddWithValue("@PessoaRG", pessoa.Rg);
                mySqlCommand.Parameters.AddWithValue("@PessoaEndereco", pessoa.Endereco);
                mySqlCommand.Parameters.AddWithValue("@PessoaCep", pessoa.Cep);
                mySqlCommand.Parameters.AddWithValue("@PessoaNumero", pessoa.Numero);
                mySqlCommand.Parameters.AddWithValue("@PessoaComplemento", pessoa.Complemento);
                mySqlCommand.Parameters.AddWithValue("@PessoaCidade", pessoa.Cidade);
                mySqlCommand.Parameters.AddWithValue("@PessoaEstado", pessoa.Estado);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInserted = (int)mySqlCommand.LastInsertedId;
            }
            return AffectedRows > 0;
        }

        public bool UpdatePessoa(Pessoa pessoa) {
            string query = @"
                UPDATE Pessoas SET
                    pessoaNome = @PessoaNome, pessoaSobrenome = @PessoaSobrenome, pessoaStatus = @PessoaStatus, 
                    pessoaSexo = @PessoaSexo, pessoaNascimento = @PessoaNascimento, pessoaTelefone = @PessoaTelefone,
                    pessoaCelular = @PessoaCelular, pessoaCnpj = @PessoaCNPJ, pessoaCpf = @PessoaCPF, 
                    pessoaRg = @PessoaRG, pessoaEndereco = @PessoaEndereco, pessoaCep = @PessoaCep,
                    pessoaNumeroRua = @PessoaNumero, pessoaComplemento = @PessoaComplemento, pessoaCidade = @PessoaCidade, 
                    pessoaEstado = @PessoaEstado
                WHERE pessoaId = @PessoaId
            ";
            using (mySqlCommand = new MySqlCommand(query, database)) {
                mySqlCommand.Parameters.AddWithValue("@PessoaNome", pessoa.Nome);
                mySqlCommand.Parameters.AddWithValue("@PessoaSobrenome", pessoa.Sobrenome);
                mySqlCommand.Parameters.AddWithValue("@PessoaSexo", pessoa.Sexo);
                mySqlCommand.Parameters.AddWithValue("@PessoaNascimento", pessoa.Nascimento);
                mySqlCommand.Parameters.AddWithValue("@PessoaTelefone", pessoa.Telefone);
                mySqlCommand.Parameters.AddWithValue("@PessoaCelular", pessoa.Celular);
                mySqlCommand.Parameters.AddWithValue("@PessoaCNPJ", pessoa.Cnpj);
                mySqlCommand.Parameters.AddWithValue("@PessoaCPF", pessoa.Cpf);
                mySqlCommand.Parameters.AddWithValue("@PessoaRG", pessoa.Rg);
                mySqlCommand.Parameters.AddWithValue("@PessoaEndereco", pessoa.Endereco);
                mySqlCommand.Parameters.AddWithValue("@PessoaCep", pessoa.Cep);
                mySqlCommand.Parameters.AddWithValue("@PessoaNumero", pessoa.Numero);
                mySqlCommand.Parameters.AddWithValue("@PessoaComplemento", pessoa.Complemento);
                mySqlCommand.Parameters.AddWithValue("@PessoaCidade", pessoa.Cidade);
                mySqlCommand.Parameters.AddWithValue("@PessoaEstado", pessoa.Estado);
                mySqlCommand.Parameters.AddWithValue("@PessoaStatus", pessoa.Status);
                mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                LastInserted = (int)mySqlCommand.LastInsertedId;
            }
            return AffectedRows > 0;
        }

        public DataTable GetPessoas(bool pessoaStatus = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaStatus = @PessoaStatus AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("PessoaStatus", pessoaStatus);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public Pessoa GetPessoa(int pessoaId) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaId = @PessoaId AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoaId);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                        return new Pessoa() {
                            Id = mySqlDataReader.GetInt32(0),
                            Nome = mySqlDataReader.GetString(1),
                            Sobrenome = mySqlDataReader.GetString(2),
                            Status = mySqlDataReader.GetBoolean(3),
                            Sexo = mySqlDataReader.GetInt32(4),
                            Nascimento = mySqlDataReader.GetDateTime(5),
                            Telefone = mySqlDataReader.IsDBNull(6) ? (long?)null : mySqlDataReader.GetInt64(6),
                            Celular = mySqlDataReader.IsDBNull(7) ? (long?)null : mySqlDataReader.GetInt64(7),
                            Cnpj = mySqlDataReader.IsDBNull(8) ? (long?)null : mySqlDataReader.GetInt64(8),
                            Cpf = mySqlDataReader.GetInt64(9),
                            Rg = mySqlDataReader.IsDBNull(10) ? (long?)null : mySqlDataReader.GetInt64(10),
                            Endereco = mySqlDataReader.GetString(11),
                            Cep = mySqlDataReader.GetInt32(12),
                            Numero = mySqlDataReader.GetInt32(13),
                            Complemento = mySqlDataReader.IsDBNull(14) ? null : mySqlDataReader.GetString(11),
                            Cidade = mySqlDataReader.GetString(15),
                            Estado = mySqlDataReader.GetString(16)
                        };
                    }
                    return new Pessoa();
                }
            }
        }

        public Pessoa GetPessoa(Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaId = @PessoaId AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                        return new Pessoa() {
                            Id = mySqlDataReader.GetInt32(0),
                            Nome = mySqlDataReader.GetString(1),
                            Sobrenome = mySqlDataReader.GetString(2),
                            Status = mySqlDataReader.GetBoolean(3),
                            Sexo = mySqlDataReader.GetInt32(4),
                            Nascimento = mySqlDataReader.GetDateTime(5),
                            Telefone = mySqlDataReader.IsDBNull(6) ? (long?)null : mySqlDataReader.GetInt64(6),
                            Celular = mySqlDataReader.IsDBNull(7) ? (long?)null : mySqlDataReader.GetInt64(7),
                            Cnpj = mySqlDataReader.IsDBNull(8) ? (long?)null : mySqlDataReader.GetInt64(8),
                            Cpf = mySqlDataReader.GetInt64(9),
                            Rg = mySqlDataReader.IsDBNull(10) ? (long?)null : mySqlDataReader.GetInt64(10),
                            Endereco = mySqlDataReader.GetString(11),
                            Cep = mySqlDataReader.GetInt32(12),
                            Numero = mySqlDataReader.GetInt32(13),
                            Complemento = mySqlDataReader.IsDBNull(14) ? null : mySqlDataReader.GetString(11),
                            Cidade = mySqlDataReader.GetString(15),
                            Estado = mySqlDataReader.GetString(16)
                        };
                    }
                    return new Pessoa();
                }
            }
        }

        public Funcionario GetPessoa(Funcionario funcionario) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas INNER JOIN Funcionarios ON pessoaId = funcionarioId WHERE funcionarioId = @FuncionarioId AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioId", funcionario.Id);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new Funcionario() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Sobrenome = mySqlDataReader.GetString(2),
                        Status = mySqlDataReader.GetBoolean(3),
                        Sexo = mySqlDataReader.GetInt32(4),
                        Nascimento = mySqlDataReader.GetDateTime(5),
                        Telefone = mySqlDataReader.IsDBNull(6) ? (long?)null : mySqlDataReader.GetInt64(6),
                        Celular = mySqlDataReader.IsDBNull(7) ? (long?)null : mySqlDataReader.GetInt64(7),
                        Cnpj = mySqlDataReader.IsDBNull(8) ? (long?)null : mySqlDataReader.GetInt64(8),
                        Cpf = mySqlDataReader.GetInt64(9),
                        Rg = mySqlDataReader.IsDBNull(10) ? (long?)null : mySqlDataReader.GetInt64(10),
                        Endereco = mySqlDataReader.GetString(11),
                        Cep = mySqlDataReader.GetInt32(12),
                        Numero = mySqlDataReader.GetInt32(13),
                        Complemento = mySqlDataReader.IsDBNull(14) ? null : mySqlDataReader.GetString(11),
                        Cidade = mySqlDataReader.GetString(15),
                        Estado = mySqlDataReader.GetString(16)
                    };
                }
            }
            return new Funcionario();
        }

        public Aluno GetPessoa(Aluno aluno) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas INNER JOIN Alunos ON pessoaId = alunoId WHERE alunoID = @AlunoId AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new Aluno() {
                        FkPessoa = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Sobrenome = mySqlDataReader.GetString(2),
                        PessoaStatus = mySqlDataReader.GetBoolean(3),
                        Sexo = mySqlDataReader.GetInt32(4),
                        Nascimento = mySqlDataReader.GetDateTime(5),
                        Telefone = mySqlDataReader.GetInt32(6),
                        Celular = mySqlDataReader.GetInt32(7),
                        Cnpj = mySqlDataReader.GetInt64(8),
                        Cpf = mySqlDataReader.GetInt32(9),
                        Numero = mySqlDataReader.GetInt32(10),
                        Complemento = mySqlDataReader.GetString(11),
                        Cidade = mySqlDataReader.GetString(12),
                        Estado = mySqlDataReader.GetString(13),
                        Id = mySqlDataReader.GetInt32(14),
                        Status = mySqlDataReader.GetBoolean(16)
                    };
                }
            }
            return new Aluno();
        }

        public DataTable GetAlunos(Turma turma) {
            string query = @"
                SELECT * FROM Alunos
                INNER JOIN Pessoas 
                    ON alunoFkPessoa = pessoaId
                INNER JOIN TurmaAlunos
                    ON alunoId = fkAluno
                INNER JOIN Turmas
                    ON fkTurma = turmaId
                WHERE turmaId = @TurmaId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@TurmaId", turma.Id);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(LastSelection);
            return LastSelection;
        }

        public DataTable GetAlunos(bool ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Alunos INNER JOIN Pessoas ON alunoFkPessoa = pessoaId WHERE alunoStatus = @AlunoStatus AND pessoaId != 0", database);
            mySqlCommand.Parameters.AddWithValue("@AlunoStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public Aluno GetAluno(Aluno aluno) {
            string query = @"
                SELECT * FROM Alunos WHERE alunoId = @AlunoId
            ";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                return new Aluno() {
                    Id = mySqlDataReader.GetInt32(0),
                    FkPessoa = mySqlDataReader.GetInt32(1),
                    Status = mySqlDataReader.GetBoolean(2)
                };
            }
            return new Aluno();
        }

        public int CreateAluno(Pessoa pessoa) {
            string query = "SELECT * FROM Alunos WHERE alunoFkPessoa = @FkPessoa AND alunoStatus = true";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FkPessoa", pessoa.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows) {
                return 2;
            } else {
                mySqlDataReader.Close();
                query = "INSERT INTO Alunos() VALUES (null, @FkPessoa, true)";
                mySqlCommand.CommandText = query;
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                return (AffectedRows > 0) ? 1 : 0;
            }
        }

        public bool UpdateAluno(Aluno aluno) {
            string query = @"UPDATE Alunos SET alunoFkPessoa = @FkPessoa, alunoStatus = @AlunoStatus WHERE alunoId = @AlunoId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FkPessoa", aluno.FkPessoa);
            mySqlCommand.Parameters.AddWithValue("@AlunoStatus", aluno.Status);
            mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public Pessoa GetPessoa(long documentoNumero) {
            string query = @"
                SELECT * FROM Pessoas
                WHERE
                    (pessoaRg = @Documento OR pessoaCpf = @Documento OR pessoaCnpj = @Documento) AND pessoaId != 0 AND pessoaStatus = true
            ";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@Documento", documentoNumero);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                return new Pessoa() {
                    Id = mySqlDataReader.GetInt32(0),
                    Nome = mySqlDataReader.GetString(1),
                    Sobrenome = mySqlDataReader.GetString(2),
                    Status = mySqlDataReader.GetBoolean(3),
                    Sexo = mySqlDataReader.GetInt32(4),
                    Nascimento = mySqlDataReader.GetDateTime(5),
                    Telefone = mySqlDataReader.IsDBNull(6) ? (long?)null : mySqlDataReader.GetInt64(6),
                    Celular = mySqlDataReader.IsDBNull(7) ? (long?)null : mySqlDataReader.GetInt64(7),
                    Cnpj = mySqlDataReader.IsDBNull(8) ? (long?)null : mySqlDataReader.GetInt64(8),
                    Cpf = mySqlDataReader.GetInt64(9),
                    Rg = mySqlDataReader.IsDBNull(10) ? (long?)null : mySqlDataReader.GetInt64(10),
                    Endereco = mySqlDataReader.GetString(11),
                    Cep = mySqlDataReader.GetInt32(12),
                    Numero = mySqlDataReader.GetInt32(13),
                    Complemento = mySqlDataReader.IsDBNull(14) ? null : mySqlDataReader.GetString(11),
                    Cidade = mySqlDataReader.GetString(15),
                    Estado = mySqlDataReader.GetString(16)
                };
            }
            return new Pessoa();
        }

        public DataTable GetFuncionarios(bool ativos = true) {
            string query = @"
                SELECT * FROM Funcionarios
                INNER JOIN Pessoas
                    ON funcionarioFkPessoa = pessoaId
                WHERE
                    pessoaId != 0 AND funcionarioStatus = @FuncionarioStatus
            ";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetFuncionarios(NivelAcessoSistema nivelAcesso) {
            DataTable returnDataTable = new DataTable();
            string query = @"
                SELECT * FROM Funcionarios 
                    INNER JOIN Pessoas 
                        ON funcionarioFkPessoa = pessoaId
                    INNER JOIN Usuarios
                        ON pessoaId = usuarioFkPessoa
                    INNER JOIN NivelAcesso
                        ON usuarioFkNivelAcesso = nivelAcessoId
                    WHERE 
                        funcionarioStatus = true
                    AND pessoaId != 0  AND nivelAcessoId = @NivelAcesso AND usuarioStatus = true
            ";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@NivelAcesso", nivelAcesso);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(returnDataTable);
            }
            return returnDataTable;
        }

        public Funcionario GetFuncionario(Funcionario funcionario) {
            string query = "SELECT * FROM Funcionarios WHERE funcionarioId = @FuncionarioId";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioId", funcionario.Id);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                return new Funcionario() {
                    Id = mySqlDataReader.GetInt32(0),
                    FkPessoa = mySqlDataReader.GetInt32(1),
                    Status = mySqlDataReader.GetBoolean(2),
                    FkCargo = mySqlDataReader.GetInt32(3)
                };
            }

            return new Funcionario();
        }

        public bool UpdateFuncionario(Funcionario funcionario) {
            string query = "UPDATE Funcionarios SET funcionarioStatus = @FuncionarioStatus, funcionarioCargo = @FuncionarioCargo";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioStatus", funcionario.Status);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioCargo", funcionario.FkCargo);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public int CreateFuncionario(Funcionario funcionario) {
            string query = "SELECT * FROM Funcionarios WHERE funcionarioFkPessoa = @FkPessoa AND funcionarioStatus = true";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@FkPessoa", funcionario.FkPessoa);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows) {
                mySqlDataReader.Close();
                return 2;
            } else {
                mySqlDataReader.Close();
                query = "INSERT INTO Funcionarios(funcionarioFkPessoa, funcionarioCargo) VALUES (@FkPessoa, @FkCargo)";
                mySqlCommand.CommandText = query;
                mySqlCommand.Parameters.AddWithValue("@FkCargo", funcionario.FkCargo);
                AffectedRows = mySqlCommand.ExecuteNonQuery();
                return (AffectedRows > 0) ? 1 : 0;
            }
        }

        public bool CreateCargo(Cargo cargo) {
            string query = "INSERT INTO Cargos () VALUES (null, @CargoNome, @CargoSalario, true)";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@CargoNome", cargo.Nome);
            mySqlCommand.Parameters.AddWithValue("@CargoSalario", cargo.Salario);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public DataTable GetCargo(bool ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Cargos WHERE cargoStatus = @CargoStatus", database);
            mySqlCommand.Parameters.AddWithValue("@CargoStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public Cargo GetCargo(Cargo cargo) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Cargos WHERE cargoId = @CargoId", database);
            mySqlCommand.Parameters.AddWithValue("@CargoId", cargo.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                    return new Cargo() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Salario = mySqlDataReader.GetFloat(2),
                        Status = mySqlDataReader.GetBoolean(3)
                    };
                }
            }
            return new Cargo();
        }

        public bool UpdateCargo(Cargo cargo) {
            string query = @"
                UPDATE Cargos 
                SET 
                    cargoNome = @CargoNome, cargoSalario = @CargoSalario, cargoStatus = @CargoStatus
                WHERE
                    cargoId = @CargoId
                ";
            mySqlCommand = new MySqlCommand(query, database);
            mySqlCommand.Parameters.AddWithValue("@CargoNome", cargo.Nome);
            mySqlCommand.Parameters.AddWithValue("@CargoSalario", cargo.Salario);
            mySqlCommand.Parameters.AddWithValue("@CargoStatus", cargo.Status);
            mySqlCommand.Parameters.AddWithValue("@Cargoid", cargo.Id);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            return AffectedRows > 0;
        }

        public Cargo GetCargo(Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("SELECT cargoId, cargoNome, cargoSalario, cargoStatus FROM Cargos INNER JOIN Funcionarios ON cargoId = funcionarioCargo INNER JOIN Pessoas ON funcionarioFkPessoa = pessoaId WHERE pessoaId = @PessoaId");
            mySqlCommand.Parameters.AddWithValue("@CargoId", pessoa.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                    return new Cargo() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Salario = mySqlDataReader.GetFloat(2),
                        Status = mySqlDataReader.GetBoolean(3)
                    };
                }
            }
            return new Cargo();
        }

        public Cargo GetCargo(Funcionario funcionario) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Cargos INNER JOIN Funcionarios ON cargoId = funcionarioCargo WHERE funcionarioId = @FuncionarioId");
            mySqlCommand.Parameters.AddWithValue("@CargoId", funcionario.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                    return new Cargo() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Salario = mySqlDataReader.GetFloat(2),
                        Status = mySqlDataReader.GetBoolean(3)
                    };
                }
            }
            return new Cargo();
        }

        public void DisposeConnection() {
            database.Close();
            database.Dispose();
        }
    }
}