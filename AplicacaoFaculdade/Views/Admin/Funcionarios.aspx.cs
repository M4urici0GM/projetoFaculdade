using System;
using System.Web.UI.WebControls;
using System.Data;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade.DatabaseContext;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Funcionarios : System.Web.UI.Page {

        private PessoaContext pessoaContext;
        private DataTable funcionarioDataTable;
        private DataTable cargosDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                pessoaContext = new PessoaContext();
                LoadFuncionarioData();
            }
        }

        private void LoadFuncionarioData() {
            funcionarioDataTable = pessoaContext.GetFuncionarios();
            funcionarioGridView.DataSource = funcionarioDataTable;
            funcionarioGridView.DataBind();

            cargosDataTable = pessoaContext.GetCargo();
            cargoDropDown.DataSource = cargosDataTable;
            cargoDropDown.DataTextField = "cargoNome";
            cargoDropDown.DataValueField = "cargoId";
            cargoDropDown.DataBind();
            cargoDropDown.SelectedValue = Convert.ToString(-1);

        }

        protected void PesquisarPessoaClickEventHandler(object sender, EventArgs e) {
            if (pessoaPesquisaTxt.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para pesquisar por uma pessoa, preencha todos os campos!' }); </script>");
                return;
            }
            pessoaContext = new PessoaContext();
            long documentoNumero = Convert.ToInt64(pessoaPesquisaTxt.Text);
            Pessoa pessoa = pessoaContext.GetPessoa(documentoNumero);
            if (pessoa.Id.HasValue) {
                pesquisaPessoaIdHidden.Text = Convert.ToString(pessoa.Id.Value);
                pessoaPesquisaNome.Text = pessoa.Nome + " " + pessoa.Sobrenome;
                return;
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "pesquisarPessoaInvalidInput", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Não foram encontrados registros com esse documento!' }); </script>");
            }
        }

        protected void AdicionarFuncionarioSaveClickEventHandler(object sender, EventArgs e) {
            if (pesquisaPessoaIdHidden.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingValues", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando!', text: 'Para adicionar um novo funcionario, por favor, efetue a pesquisa antes!' }); </script>");
                return;
            }
            pessoaContext = new PessoaContext();
            int FkPessoa = Convert.ToInt32(pesquisaPessoaIdHidden.Text);
            int FkCargo =  Convert.ToInt32(cargoDropDown.SelectedValue);
            Funcionario funcionario = new Funcionario() {
                FkCargo = FkCargo,
                FkPessoa = FkPessoa
            };
            int result = pessoaContext.CreateFuncionario(funcionario);
            if (result == 1) {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullCreated", "<script> Swal.fire({ type: 'success', title: 'Inserir novo Funcionario', text: 'Funcionario inserido com sucesso!' }).then(() => location.href = '/admin/pessoas/funcionarios'); </script>");
                return;
            } else if (result == 2) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo está errado', text: 'Essa pessoa já está definida como um funcionario' }); </script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type:'error', title:'Ops, algo aconteceu', text: 'Houve um erro, favor entrar em contato com o administrador do sistema' }); </script>");
        }

        protected void OnRowCommandClickEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int funcionarioId = (int)funcionarioGridView.DataKeys[gvRow.RowIndex].Value;
           
            if (e.CommandName == "editarFuncionario") {

            } else if (e.CommandName == "excluirFuncionario") {
                Funcionario funcionario = new PessoaContext().GetFuncionario(new Funcionario() { Id = funcionarioId });
                funcionario.Status = false;
                bool result = new PessoaContext().UpdateFuncionario(funcionario);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "succefullDelete", "<script> Swal.fire({ type: 'success',  title: 'Excluir Funcionario', text: 'Funcionario excluido com sucesso!' }).then(() => location.href = '/admin/pessoas/funcionarios'); </script>");
                    return;
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type:'error', title:'Ops, algo aconteceu', text: 'Houve um erro, favor entrar em contato com o administrador do sistema' }); </script>");
        }

        protected void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {
            funcionarioDataTable = pessoaContext.GetFuncionarios();
            funcionarioGridView.DataSource = funcionarioDataTable;
            funcionarioGridView.PageIndex = e.NewPageIndex;
            funcionarioGridView.DataBind();
        }
    }
}