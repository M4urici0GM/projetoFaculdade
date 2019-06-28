using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class CadastrarPessoa : System.Web.UI.Page {

        public static Dictionary<int, Documento> Documentos { get; private set; }
        public static Dictionary<int, Telefone> Telefones { get; private set; }
        public static Dictionary<int, Endereco> Enderecos { get; private set; }

        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                Documentos = new Dictionary<int, Documento>();
                Telefones = new Dictionary<int, Telefone>();
                Enderecos = new Dictionary<int, Endereco>();
            }
        }

    }
}