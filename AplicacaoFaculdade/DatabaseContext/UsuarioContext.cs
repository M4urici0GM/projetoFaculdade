using AplicacaoFaculdade.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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




    }
}