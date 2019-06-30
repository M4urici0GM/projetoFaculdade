using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using AplicacaoFaculdade.Enums;
using System.Threading.Tasks;

namespace AplicacaoFaculdade {
    public class UsuarioContext {

        public int? AffectedRows { get; private set; }
        public int? LastInsertId { get; private set; }
        public DataTable LastSelection { get; private set; }
        public Usuario LastCreated { get; private set; }


        private MySqlConnection databaseConn;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;


        public UsuarioContext() {
            databaseConn = Database.GetInstance().GetConnection();
            LastCreated = new Usuario();
            LastSelection = new DataTable();
        }

        public async Task<Usuario> DoLogin(string usuarioLogin, string usuarioSenha) {
            string actualHash = Crypter.Hash(usuarioSenha);

            mySqlCommand = new MySqlCommand("SELECT usuarioId, usuarioLogin, usuarioSenha, pessoaId, pessoaNome FROM Usuarios LEFT JOIN Pessoas on usuarioFkPessoa = pessoaId WHERE usuarioLogin = @Email AND usuarioSenha = @Hash", databaseConn);
            mySqlCommand.Parameters.AddWithValue("@Email", usuarioLogin);
            mySqlCommand.Parameters.AddWithValue("@Hash", actualHash);

            mySqlDataReader = mySqlCommand.ExecuteReader();

            using (mySqlDataReader) {
                if (mySqlDataReader.Read()) {
                    if (mySqlDataReader.HasRows) {
                        Usuario usuario = new Usuario() {
                            Id = mySqlDataReader.GetInt32(0),
                            Email = mySqlDataReader.GetString(1),
                            Senha = mySqlDataReader.GetString(2),
                            FkPessoa = mySqlDataReader.GetInt32(3),
                            PessoaId = mySqlDataReader.GetInt32(3),
                            Nome = mySqlDataReader.GetString(4)
                        };
                        return usuario;
                    }
                }
            }
            return new Usuario();
        }


        public int CreateUsuario(Usuario usuario, Pessoa pessoa) {
            string query = @"
                SELECT * FROM Usuarios
                WHERE usuarioLogin = @UsuarioLogin
            ";
            using (mySqlCommand = new MySqlCommand(query, databaseConn)) {
                mySqlCommand.Parameters.AddWithValue("@UsuarioLogin", usuario.Email);
                mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows) {
                    return 2;
                } else {
                    mySqlDataReader.Close();
                    usuario.Senha = Crypter.Hash(usuario.Senha);
                    query = @"INSERT INTO Usuarios(usuarioLogin, usuarioSenha, usuarioFkNivelAcesso, usuarioFkPessoa, usuarioDataRegistro, usuarioStatus) VALUES (@UsuarioLogin, @UsuarioSenha, @UsuarioFkNivel, @UsuarioFkPessoa, NOW(), true)";
                    mySqlCommand = new MySqlCommand(query, databaseConn);
                    mySqlCommand.Parameters.AddWithValue("@UsuarioLogin", usuario.Email);
                    mySqlCommand.Parameters.AddWithValue("@UsuarioSenha", usuario.Senha);
                    mySqlCommand.Parameters.AddWithValue("@UsuarioFkNivel", usuario.FkNivelAcesso);
                    mySqlCommand.Parameters.AddWithValue("@UsuarioFkPessoa", pessoa.Id);
                    AffectedRows = mySqlCommand.ExecuteNonQuery();
                    return (AffectedRows > 0) ? 1 : 0;
                }
            }
        }

        /*public Usuario CreateUser(Usuario usuario) {
            mySqlCommand = new MySqlCommand("SELECT usuarioId, usuarioLogin FROM Usuarios WHERE usuarioLogin = @UserLogin");
            mySqlCommand.Parameters.AddWithValue("@UserLogin", usuario.Email);
            mySqlDataReader = mySqlCommand.ExecuteReader();

            using (mySqlDataReader) {
                if (mySqlDataReader.Read()) {
                    if (!mySqlDataReader.HasRows) {
                        mySqlCommand = new MySqlCommand("INSERT INTO Usuarios() VALUES (null, @UserLogin, @UserPassword, @UserAccessLevel, null, 1)");
                        mySqlCommand.Parameters.AddWithValue("@UserLogin", usuario.Email);
                        mySqlCommand.Parameters.AddWithValue("@UserPassword", usuario.Senha);
                        mySqlCommand.Parameters.AddWithValue("@UserAccessLevel", usuario.FkNivelAcesso);

                        int result = mySqlCommand.ExecuteNonQuery();
                        int userNewId = (int)mySqlCommand.LastInsertedId;

                        mySqlCommand = new MySqlCommand("SELECT * FROM Usuarios WHERE usuarioId = @UserId");
                        mySqlCommand.Parameters.AddWithValue("@UserId", userNewId);

                        mySqlDataReader = mySqlCommand.ExecuteReader();

                        using (mySqlDataReader) {
                            if (mySqlDataReader.Read()) {

                                return new Usuario() {
                                    Id = mySqlDataReader.GetInt32(0),
                                    Email = mySqlDataReader.GetString(1),
                                    Senha = mySqlDataReader.GetString(2),
                                    FkNivelAcesso = mySqlDataReader.GetInt32(3),
                                    Status = mySqlDataReader.GetBoolean(5)
                                };
                            }
                            throw new Exception("User not found");
                        }
                    }
                    return new Usuario() {
                        Id = mySqlDataReader.GetInt32(0),
                        Email = mySqlDataReader.GetString(1)
                    };
                }
            }
            return new Usuario();
        }*/


        public DataTable GetUsuarios(bool usuarioAtivos = true) {
            DataTable userDataTable = new DataTable();
            mySqlCommand = new MySqlCommand("SELECT * FROM Usuarios left join Pessoas on usuarioFkPessoa = pessoaId WHERE usuarioStatus = @UserStatus AND usuarioId != 0", databaseConn);
            mySqlCommand.Parameters.AddWithValue("@UserStatus", usuarioAtivos);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(userDataTable);
                return userDataTable;
            }
        }

        public bool UpdateUsuario(Usuario usuario) {
            string query = $@"
                UPDATE Usuarios 
                SET 
                    usuarioLogin = @UsuarioLogin
                    { (usuario.Senha.Equals("") ? "," : ", usuarioSenha = @UsuarioSenha,") }
                    usuarioFkNivelAcesso = @NivelAcesso,
                    usuarioStatus = @UsuarioStatus
                WHERE
                    usuarioId = @UsuarioId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConn);
            mySqlCommand.Parameters.AddWithValue("@UsuarioLogin", usuario.Email);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuario.Id);
            mySqlCommand.Parameters.AddWithValue("@NivelAcesso", usuario.FkNivelAcesso);
            mySqlCommand.Parameters.AddWithValue("@UsuarioStatus", usuario.Status);
            if (!usuario.Senha.Equals(""))
                mySqlCommand.Parameters.AddWithValue("@UsuarioSenha", Crypter.Hash(usuario.Senha));
            AffectedRows = mySqlCommand.ExecuteNonQuery();
             return AffectedRows > 0;
        }

        public Usuario GetUsuarios(int usuarioId) {
            string query = @"
                SELECT * FROM Usuarios
                INNER JOIN Pessoas
                    ON usuarioFkPessoa = pessoaId
                WHERE usuarioId = @UsuarioId
            ";
            mySqlCommand = new MySqlCommand(query, databaseConn);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuarioId);

            mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.HasRows) {
                if (!mySqlDataReader.Read())
                    throw new Exception("teste");
                else
                    return new Usuario() {
                        Id = mySqlDataReader.GetInt32(0),
                        Email = mySqlDataReader.GetString(1),
                        Senha = mySqlDataReader.GetString(2),
                        FkNivelAcesso = mySqlDataReader.GetInt32(3),
                        FkPessoa = mySqlDataReader.GetInt32(4),
                        Status = mySqlDataReader.GetBoolean(5),
                    };
            }
            return new Usuario();
        }

        public DataTable GetUsuarios(Pessoa pessoa) {
            string query = @"
                SELECT
                    usuarioId, usuarioLogin, usuarioSenha, usuarioFkNivelAcesso, nivelAcessoNome, usuarioStatus, usuarioDataRegistro
                FROM Usuarios 
                    INNER JOIN Pessoas
                        ON usuarioFkPessoa = pessoaId
                    INNER JOIN NivelAcesso
                        ON usuarioFkNivelAcesso = nivelAcessoId
                    WHERE usuarioStatus = true AND pessoaId  = @PessoaId
            ";
            using (mySqlCommand = new MySqlCommand(query, databaseConn)) {
                mySqlCommand.Parameters.AddWithValue("@PessoaId", pessoa.Id);
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlDataAdapter.Fill(LastSelection);
            }
            return LastSelection;
        }

        public bool DeleteUser(Usuario usuario) {
            mySqlCommand = new MySqlCommand("UPDATE Usuarios SET usuarioStatus = @UserStatus WHERE usuarioId = @UserId", databaseConn);
            mySqlCommand.Parameters.AddWithValue("@UserStatus", 0);
            mySqlCommand.Parameters.AddWithValue("@UserId", usuario.Id.Value);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }

        public DataTable GetNivelAcesso(bool ativos = true) {
            DataTable nivelAcessos = new DataTable();
            string query = @"
                SELECT * FROM NivelAcesso
                WHERE
                    nivelAcessoStatus = @NivelAcessoStatus
                AND
                    nivelAcessoId != 0
            ";
            mySqlCommand = new MySqlCommand(query, databaseConn);
            mySqlCommand.Parameters.AddWithValue("@NivelAcessoStatus", ativos);
            using (mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand)) {
                mySqlDataAdapter.Fill(nivelAcessos);
            }
            return nivelAcessos;
        }

        public NivelAcesso GetNivelAcesso(NivelAcesso nivelAcesso) {
            string query = @"SELECT * FROM NivelAcesso WHERE nivelAcessoId = @NivelAcessoId";
            mySqlCommand = new MySqlCommand(query, databaseConn);
            mySqlCommand.Parameters.AddWithValue("@NivelAcessoId", nivelAcesso.Id.Value);
            using (mySqlDataReader = mySqlCommand.ExecuteReader()) {
                if (mySqlDataReader.HasRows && mySqlDataReader.Read()) {
                    return new NivelAcesso() {
                        Id = mySqlDataReader.GetInt32(0),
                        Nome = mySqlDataReader.GetString(1),
                        Status = mySqlDataReader.GetBoolean(2)
                    };
                }
            }
            return new NivelAcesso();
        }

        public bool CreateNivelAcesso(NivelAcesso nivelAcesso) {
            string query = "INSERT INTO NivelAcesso() VALUES (null, @NivelAcessoNome, true)";
            mySqlCommand = new MySqlCommand(query, databaseConn);
            mySqlCommand.Parameters.AddWithValue("@NivelAcessoNome", nivelAcesso.Nome);
            using (mySqlCommand) {
                AffectedRows = mySqlCommand.ExecuteNonQuery();
            }
            return (AffectedRows > 0);
        }

        public void DisposeConnection() {
            databaseConn.Close();
            databaseConn.Dispose();
        }
    }
}