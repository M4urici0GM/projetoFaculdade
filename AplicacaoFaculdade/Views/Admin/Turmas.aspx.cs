using System;
using System.Data;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade.DatabaseContext;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Turmas : System.Web.UI.Page {

        private DataTable turmasDataTable;
        private ServicoContext servicoContext;
        private TurmaContext turmaContext;
        private static DataTable localTurmaDiasStorage = new DataTable();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                servicoContext = new ServicoContext();
                turmaContext = new TurmaContext();
                GetTurmasData();
            }
        }

        private void GetTurmasData() {
            turmasDataTable = turmaContext.GetTurmas(true);
            turmasGridView.DataSource = turmasDataTable;
            turmasGridView.DataBind();
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gridViewRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int turmaId = (int) turmasGridView.DataKeys[gridViewRow.RowIndex].Value;
            if (e.CommandName == "editarTurma") {
                EditarTurma(turmaId);
            } else if (e.CommandName == "excluirTurma") {
                ExcluirTurma(turmaId);
            } else if (e.CommandName == "verificarHorarios") {
                VerificarHorarios(turmaId);
            } else if (e.CommandName == "verificarAlunos") {
                VerificarAlunos(turmaId);
            }
        }

        private void VerificarAlunos(int turmaId) {
            turmaContext = new TurmaContext();
            DataTable alunosDataTable = turmaContext.GetAlunos(new Turma() { Id = turmaId });

        }

        private void VerificarHorarios(int turmaId) {
            
        }

        private void ExcluirTurma(int turmaId) {
            turmaContext = new TurmaContext();
            Turma turma = turmaContext.GetTurmas(new Turma() { Id = turmaId });
            if (turma.Id.HasValue) {
                turma.Status = false;
                bool result = turmaContext.UpdateTurma(turma);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "successDeleted", "<script> Swal.fire({type: 'success', title: 'Excluir turma', text: 'Turma excluida com sucesso!'}).then(() => location.href = '" + GetRouteUrl("turmas", null) + "'); </script>");
                    return;
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }

        private void EditarTurma(int turmaId) {
            Response.RedirectToRoute("editarTurma", new { turmaId });
        }

        protected void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {

        }


    }
}