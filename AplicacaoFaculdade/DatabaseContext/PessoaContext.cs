using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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

        public DataTable GetPessoas(bool pessoaStatus = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaStatus = @PessoaStatus", database);
            mySqlCommand.Parameters.AddWithValue("PessoaStatus", pessoaStatus);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public Pessoa GetPessoa(int pessoaId) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoaId);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                        return new Pessoa() {
                            Id = mySqlDataReader.GetInt32(0),
                            Nome = mySqlDataReader.GetString(1),
                            SobreNome = mySqlDataReader.GetString(2),
                            Juridica = mySqlDataReader.GetBoolean(3),
                            Status = mySqlDataReader.GetBoolean(4),
                            Sexo = mySqlDataReader.GetBoolean(5),
                            Nascimento = mySqlDataReader.GetDateTime(6)
                        };
                    }
                    return new Pessoa();
                }
            }
        }

        public Pessoa GetPessoa(Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                using (mySqlDataReader) {
                    if (mySqlDataReader.Read() && mySqlDataReader.HasRows) {
                        return new Pessoa() {
                            Id = mySqlDataReader.GetInt32(0),
                            Nome = mySqlDataReader.GetString(1),
                            SobreNome = mySqlDataReader.GetString(2),
                            Juridica = mySqlDataReader.GetBoolean(3),
                            Status = mySqlDataReader.GetBoolean(4),
                            Sexo = mySqlDataReader.GetBoolean(5),
                            Nascimento = mySqlDataReader.GetDateTime(6)
                        };
                    }
                    return new Pessoa();
                }
            }
        }

        public Funcionario GetPessoa(Funcionario funcionario) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas INNER JOIN Funcionarios ON pessoaId = funcionarioId WHERE funcionarioId = @FuncionarioId", database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioId", funcionario.Id);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new Funcionario() {
                        PessoaId = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        SobreNome = mySqlDataReader.GetString(2),
                        Juridica = mySqlDataReader.GetBoolean(3),
                        PessoaStatus = mySqlDataReader.GetBoolean(4),
                        Sexo = mySqlDataReader.GetBoolean(5),
                        Nascimento = mySqlDataReader.GetDateTime(6),
                        Id = mySqlDataReader.GetInt32(7),
                        Status = mySqlDataReader.GetBoolean(9),
                        FkCargo = mySqlDataReader.GetInt32(10),
                    };
                }
            }
            return new Funcionario();
        }

        public Aluno GetPessoa(Aluno aluno) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Pessoas INNER JOIN Alunos ON pessoaId = alunoId WHERE alunoID = @AlunoId", database);
            mySqlCommand.Parameters.AddWithValue("@AlunoId", aluno.Id);
            using (mySqlCommand) {
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new Aluno() {
                        PessoaId = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        SobreNome = mySqlDataReader.GetString(2),
                        Juridica = mySqlDataReader.GetBoolean(3),
                        PessoaStatus = mySqlDataReader.GetBoolean(4),
                        Sexo = mySqlDataReader.GetBoolean(5),
                        Nascimento = mySqlDataReader.GetDateTime(6),
                        Id = mySqlDataReader.GetInt32(7),
                        Status = mySqlDataReader.GetBoolean(9),
                    };
                }
            }
            return new Aluno();
        }

        public DataTable GetAlunos(bool ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Alunos INNER JOIN Pessoas ON alunoFkPessoa = pessoaId WHERE alunoStatus = @AlunoStatus", database);
            mySqlCommand.Parameters.AddWithValue("@AlunoStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public DataTable GetFuncionarios(bool ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Funcionarios INNER JOIN Pessoas ON funcionarioFkPessoa = pessoaId WHERE funcionarioStatus = @FuncionarioStatus", database);
            mySqlCommand.Parameters.AddWithValue("@FuncionarioStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public bool DeletePessoa(Pessoa pessoa) {
            mySqlCommand = new MySqlCommand("UPDATE Pessoas SET pessoaStatus = 0 WHERE pessoaId = @PessoaId", database);
            mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id.Value);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
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
            mySqlCommand = new MySqlCommand("SELECT * FROM Cargos WHERE cargoId = @CargoId");
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

        //TODO: Implement new SalarioContext
    }
}