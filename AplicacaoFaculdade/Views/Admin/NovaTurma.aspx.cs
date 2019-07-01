using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class NovaTurma : System.Web.UI.Page {

        private static DataTable localStorageHorarios = new DataTable("teste");
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataColumn coluna = localStorageHorarios.Columns.Add("aa")
                localStorageHorarios.Columns.Add("tesdsadsate");
                localStorageHorarios.Columns.Add("dsadsad");
                gridTeste.DataSource = localStorageHorarios;
                gridTeste.DataBind();
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e) {
            DataRow dr = localStorageHorarios.NewRow();
            dr["tesdsadsate"] = "a";
            dr["dsadsad"] = "b";
            localStorageHorarios.Rows.Add(dr);

            gridTeste.DataSource = localStorageHorarios;
            gridTeste.DataBind();


        }
    }
}