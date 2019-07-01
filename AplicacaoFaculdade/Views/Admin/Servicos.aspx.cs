using System;
using System.Web.UI.WebControls;
using System.Data;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade.DatabaseContext;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Servicos : System.Web.UI.Page {

        private ServicoContext servicoContext;
        private DataTable servicosDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                servicoContext = new ServicoContext();
                LoadServicoDados();
            }
        }

        private void LoadServicoDados() {
            servicosDataTable = servicoContext.GetServicos();
            servicosGridView.DataSource = servicosDataTable;
            servicosGridView.DataBind();
        }

        protected void OnRowCommandClickEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int servicoId = (int) servicosGridView.DataKeys[gvRow.RowIndex].Value;
            if (e.CommandName == "excluirServico") {
                Servico servico = new ServicoContext().GetServicos(new Servico() { Id = servicoId });
                servico.Status = false;
                bool result = new ServicoContext().UpdateServico(servico);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "succefullUpdated", "<script> Swal.fire({type: 'success',title: 'Excluir Servico', text: 'Servico excluido com sucesso!'}).then(() => location.href = '/admin/servicos'); </script>");
                    return;
                }
            } else if (e.CommandName == "editarServico") {
                ServicoContext servicoContext = new ServicoContext();
                Servico servico = servicoContext.GetServicos(new Servico() { Id = servicoId });
                editarServicoNome.Text = servico.Nome;
                editarServicoValor.Text = servico.precoServicoValor.ToString();
                editarServicoIdHidden.Text = servicoId.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "openEditModal", "<script>$('#editarServicoModal').modal('show');</script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnInsert", "<script> Swal.fire({ type: 'error', title: 'Ops, algo aconteceu', text: 'Houve um erro, favor entrar em contato com o administrador.' }); </script>");
        }

        protected void OnPageIndexChanged(object sender, GridViewPageEventArgs e) {

        }

        protected void OnAdicionarServicoClickEventHandler(object sender, EventArgs e) {
            if (servicoNome.Text.Equals("") || precoServicovalor.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para adicionar um novo servico, preencha todos os dados!' }); </script>");
            }
            servicoContext = new ServicoContext();
            Servico servico = new Servico() {
                Nome = servicoNome.Text,
                precoServicoValor = (float) Convert.ToDouble(precoServicovalor.Text)
            };
            bool result = servicoContext.CreateServico(servico);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullCreated", "<script> Swal.fire({ type: 'success', title: 'Cadastrar novo servico', text: 'Registro inserido com sucesso!' }).then(() => location.href = '/admin/servicos'); </script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnInsert", "<script> Swal.fire({ type: 'error', title: 'Ops, algo aconteceu', text: 'Houve um erro, favor entrar em contato com o administrador.' }); </script>");
        }

        protected void OnSalvarServicoClickEventHandler(object sender, EventArgs e) {
            if (editarServicoNome.Text.Equals("") || editarServicoValor.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para adicionar um novo servico, preencha todos os dados!' }); </script>");
            }
            ServicoContext servicoContext = new ServicoContext();
            Servico servico = servicoContext.GetServicos(new Servico() { Id = Convert.ToInt32(editarServicoIdHidden.Text) });
            servico.Nome = editarServicoNome.Text;
            servico.precoServicoValor = (float)Convert.ToDouble(editarServicoValor.Text);
            bool result = servicoContext.UpdateServico(servico);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullUpdated", "<script> Swal.fire({type: 'success',title: 'Editar Servico', text: 'Servico editado com sucesso!'}).then(() => location.href = '/admin/servicos'); </script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnInsert", "<script> Swal.fire({ type: 'error', title: 'Ops, algo aconteceu', text: 'Houve um erro, favor entrar em contato com o administrador.' }); </script>");
        }
    }
}