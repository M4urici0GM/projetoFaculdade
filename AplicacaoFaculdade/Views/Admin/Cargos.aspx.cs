using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Cargos : System.Web.UI.Page {

        private PessoaContext pessoaContext;
        private DataTable cargosDataTable;


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                pessoaContext = new PessoaContext();
                LoadCargosData();
            }
        }

        private void LoadCargosData() {
            cargosDataTable = pessoaContext.GetCargo();
            HandleDataToTable();
        }

        private void HandleDataToTable() {
            cargosGridView.DataSource = cargosDataTable;
            cargosGridView.DataBind();
        }

        protected void OnRowCommandClickEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int cargoId = (int)cargosGridView.DataKeys[gvRow.RowIndex].Value;

            pessoaContext = new PessoaContext();
            Cargo cargo = pessoaContext.GetCargo(new Cargo() { Id = cargoId });
            if (e.CommandName == "editarCargo") {
                editarCargoNome.Text = cargo.Nome;
                editarCargoSalario.Text = Convert.ToString(cargo.Salario);
                editarCargoIdHidden.Text = Convert.ToString(cargo.Id);
                ClientScript.RegisterStartupScript(this.GetType(), "openeditarcargomodal", "<script> $('#editarCargoModal').modal('show'); </script>");
                return;
            } else if (e.CommandName == "excluirCargo") {
                cargo.Status = false;
                bool status = new PessoaContext().UpdateCargo(cargo);
                if (status) {
                    ClientScript.RegisterStartupScript(this.GetType(), "successInserted", "<script> Swal.fire({ type: 'success', title: 'Excluir cargo', text: 'Registro excluido com sucesso!' }).then(() => location.href = '/admin/pessoas/cargos'); </script>");
                    return;
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo aconteceu', text: 'Houve um erro,  por favor entre em contato com o administrador do sistema' }); </script>");
            return;
        }

        protected void OnPageIndexChanged(object sender, GridViewPageEventArgs e) {
            pessoaContext = new PessoaContext();
            cargosGridView.DataSource = pessoaContext.GetCargo();
            cargosGridView.PageIndex = e.NewPageIndex;
            cargosGridView.DataBind();
        }

        protected void OnAdicionarCargoClickEventHandler(object sender, EventArgs e) {
            if (cargoNome.Text.Equals("") || cargoSalario.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type:'error', title:'Ops, algo esta faltando!', text: 'Para adicionar um novo cargo, preencha todos os campos!' }); </script>");
                return;
            }
            pessoaContext = new PessoaContext();
            Cargo cargo = new Cargo() {
                Nome = cargoNome.Text,
                Salario = (float)Convert.ToDouble(cargoSalario.Text)
            };
            bool result = pessoaContext.CreateCargo(cargo);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "successInserted", "<script> Swal.fire({ type: 'success', title: 'Inserir novo cargo', text: 'Registro inserido com sucesso!' }).then(() => location.href = '/admin/pessoas/cargos'); </script>");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo aconteceu', text: 'Houve um erro,  por favor entre em contato com o administrador do sistema' }); </script>");
            }
        }

        protected void OnEditarCargoClickEventHandler(object sender, EventArgs e) {
            if (editarCargoNome.Text.Equals("") || editarCargoSalario.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type:'error', title:'Ops, algo esta faltando!', text: 'Para editar um novo cargo, preencha todos os campos!' }); </script>");
                return;
            }
            Cargo cargo = new Cargo() {
                Id = Convert.ToInt32(editarCargoIdHidden.Text),
                Nome = editarCargoNome.Text,
                Salario = (float)Convert.ToDouble(editarCargoSalario.Text),
                Status = true
            };
            bool result = new PessoaContext().UpdateCargo(cargo);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "successInserted", "<script> Swal.fire({ type: 'success', title: 'Editar cargo', text: 'Registrado editado com sucesso!' }).then(() => location.href = '/admin/pessoas/cargos'); </script>");
                return;
            }
        }
    }
}