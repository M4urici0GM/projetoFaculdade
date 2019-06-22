using AplicacaoFaculdade.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace AplicacaoFaculdade.DatabaseContext {
    public class ServicoContext {

        public int? LastInsertId { get; private set; }
        public int? AffectedRows { get; private set; }
        public Servico LastCreated { get; private set; }
        public DataTable LastSelection { get; private set; }

        private MySqlConnection databaseConnection;
        private MySqlCommand mySqlCommand;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlDataReader mySqlDataReader;

        public ServicoContext () {
            databaseConnection = Database.GetInstance().GetConnection();
            AffectedRows = 0;
            LastInsertId = null;
        }

        public DataTable GetServicos(bool ativos = true) {



            return LastSelection;
        }

    }
}