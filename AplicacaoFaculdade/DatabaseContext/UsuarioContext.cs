using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AplicacaoFaculdade {
    public class UsuarioContext {
        private MySqlConnection databaseConn;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;
      

        public UsuarioContext() {
            databaseConn = Database.GetInstance().GetConnection();
            //databaseConn.Open();
        }

        public Usuario DoLogin(string usuarioLogin, string usuarioSenha) {
            string actualHash = Crypter.Hash(usuarioSenha);

            mySqlCommand = new MySqlCommand("SELECT usuarioId, usuarioLogin, usuarioSenha, pessoaId, pessoaNome FROM Usuarios LEFT JOIN Pessoas on usuarioFkPessoa = pessoaId WHERE usuarioLogin = @Email AND usuarioSenha = @Hash", databaseConn);
            mySqlCommand.Parameters.AddWithValue("@Email", usuarioLogin);
            mySqlCommand.Parameters.AddWithValue("@Hash", actualHash);

            mySqlDataReader = mySqlCommand.ExecuteReader();

            using (mySqlDataReader) {
                if ( mySqlDataReader.Read() ) {
                    if ( mySqlDataReader.HasRows ) {


                        Usuario usuario = new Usuario(new Pessoa() { pessoaId = mySqlDataReader.GetInt32(3), pessoaNome = mySqlDataReader.GetString(4) }) {
                            usuarioId = mySqlDataReader.GetInt32(0),
                            usuarioEmail = mySqlDataReader.GetString(1),
                            usuarioSenha = mySqlDataReader.GetString(2),
                            usuarioFkPessoa = mySqlDataReader.GetInt32(3)
                        };

                        return usuario;

                    }
                }
            }
            return new Usuario();
        }


        public Usuario CreateUser(Usuario usuario) {
            mySqlCommand = new MySqlCommand("SELECT usuarioId, usuarioLogin FROM Usuarios WHERE usuarioLogin = @UserLogin");
            mySqlCommand.Parameters.AddWithValue("@UserLogin", usuario.usuarioEmail);
            mySqlDataReader = mySqlCommand.ExecuteReader();

            using (mySqlDataReader) {
                if (mySqlDataReader.Read()) {
                    if (!mySqlDataReader.HasRows) {
                        mySqlCommand = new MySqlCommand("INSERT INTO Usuarios() VALUES (null, @UserLogin, @UserPassword, @UserAccessLevel, null, 1)");
                        mySqlCommand.Parameters.AddWithValue("@UserLogin", usuario.usuarioEmail);
                        mySqlCommand.Parameters.AddWithValue("@UserPassword", usuario.usuarioSenha);
                        mySqlCommand.Parameters.AddWithValue("@UserAccessLevel", usuario.usuarioFkNivelAcesso);

                        int result = mySqlCommand.ExecuteNonQuery();
                        int userNewId = (int) mySqlCommand.LastInsertedId;

                        mySqlCommand = new MySqlCommand("SELECT * FROM Usuarios WHERE usuarioId = @UserId");
                        mySqlCommand.Parameters.AddWithValue("@UserId", userNewId);

                        mySqlDataReader = mySqlCommand.ExecuteReader();

                        using (mySqlDataReader) {
                            if (mySqlDataReader.Read()) {
                                return new Usuario() {
                                    usuarioId = mySqlDataReader.GetInt32(0),
                                    usuarioEmail = mySqlDataReader.GetString(1),
                                    usuarioSenha = mySqlDataReader.GetString(2),
                                    usuarioFkNivelAcesso = mySqlDataReader.GetInt32(3),
                                    usuarioStatus = mySqlDataReader.GetBoolean(5)
                                };
                            }
                            throw new Exception("User not found");
                        }
                    } else {
                        return new Usuario() {
                            usuarioId = mySqlDataReader.GetInt32(0),
                            usuarioEmail = mySqlDataReader.GetString(1)
                        };
                    }
                }
            }
            return new Usuario();
        }


        public DataTable GetUsuarios(bool usuarioAtivos = true) {
            DataTable userDataTable = new DataTable();
            mySqlCommand = new MySqlCommand("SELECT * FROM Usuarios left join pessoas on usuarioFkPessoa = pessoaId WHERE usuarioStatus = @UserStatus", databaseConn);
            mySqlCommand.Parameters.AddWithValue("@UserStatus", usuarioAtivos);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            using (mySqlDataAdapter) {
                mySqlDataAdapter.Fill(userDataTable);
                return userDataTable;
            }
        }

        public Usuario GetUsuarios(int usuarioId) {
            mySqlCommand = new MySqlCommand("SELECT * FROM Usuarios left join pessoas on usuarioFkPessoa = pessoaId WHERE usuarioId = @UserId");
            mySqlCommand.Parameters.AddWithValue("@UserId", usuarioId);

            mySqlDataReader = mySqlCommand.ExecuteReader();

            using (mySqlDataReader) {
                if (mySqlDataReader.Read()) {
                    return new Usuario() {
                        usuarioId = mySqlDataReader.GetInt32(0),
                        usuarioEmail = mySqlDataReader.GetString(1),
                        usuarioSenha = mySqlDataReader.GetString(2),
                        usuarioFkNivelAcesso = mySqlDataReader.GetInt32(3),
                        usuarioStatus = mySqlDataReader.GetBoolean(5)
                    };
                }
                throw new KeyNotFoundException();
            }
        }

        public bool DeleteUser(Usuario usuario) {
            mySqlCommand = new MySqlCommand("UPDATE Usuarios SET usuarioStatus = @UserStatus WHERE usuarioId = @UserId");
            mySqlCommand.Parameters.AddWithValue("@UserStatus", 0);
            mySqlCommand.Parameters.AddWithValue("@UserId", usuario.usuarioId);
            using (mySqlCommand) {
                return (mySqlCommand.ExecuteNonQuery() > 0);
            }
        }
    }
}