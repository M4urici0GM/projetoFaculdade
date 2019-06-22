using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade;

namespace AplicacaoFaculdade.DatabaseContext {
    public class LogContext {

        public int? AffectedRows { get; private set; }
        public int? LastInsertId { get; private set; }
        public DataTable LastSelection { get; set; }
        public DataLog LastInsert { get; set; }

        private MySqlConnection DatabaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        

        public LogContext() {
            DatabaseConnection = Database.GetInstance().GetConnection();
        }

        public DataTable GetLogs(bool _ativos = true) {
            mySqlCommand = new MySqlCommand("SELECT * FROM DataLogs WHERE dataLogStatus = @LogStatus", DatabaseConnection);
            mySqlCommand.Parameters.AddWithValue("@LogStatus", _ativos);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(LastSelection);
                }
            }
            return LastSelection;
        }

        public DataTable GetLogs(Usuario usuario) {
            mySqlCommand = new MySqlCommand("SELECT * FROM DataLogs INNER JOIN Usuarios on dataLogFkUsuario = usuarioId WHERE usuarioId = @UsuarioId ", DatabaseConnection);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", usuario.Id.Value);
            using (mySqlCommand) {
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                using (mySqlDataAdapter) {
                    mySqlDataAdapter.Fill(LastSelection);
                }
            }
            return LastSelection;
        }

        public bool CreateLog(DataLog _dataLog) {
            mySqlCommand = new MySqlCommand("INSERT INTO DataLogs() VALUES (null, NOW(),  @UsuarioId, @Atividade)", DatabaseConnection);
            mySqlCommand.Parameters.AddWithValue("@UsuarioId", _dataLog.FkUsuario);
            mySqlCommand.Parameters.AddWithValue("@Atividade", _dataLog.Atividade);
            AffectedRows = mySqlCommand.ExecuteNonQuery();
            if (AffectedRows > 0) {
                LastInsertId = (int) mySqlCommand.LastInsertedId;
                LastInsert = _dataLog;
            }
            return AffectedRows > 0;
        }
    }
}