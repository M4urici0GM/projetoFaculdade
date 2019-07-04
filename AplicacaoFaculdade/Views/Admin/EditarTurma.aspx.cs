using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Enums;
using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class EditarTurma : System.Web.UI.Page {

        private string turmaIdStr;
        private int turmaId;
        private TurmaContext turmaContext;
        private PessoaContext pessoaContext;
        private ServicoContext servicoContext;
        private Turma turmaData;
        private DataTable turmaHorarios;
        private DataTable turmaAlunos;
        private DataTable funcionariosDataTable;
        private DataTable servicoDataTable;
        private static DataTable localStorageHorarios = new DataTable();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                try {
                    turmaIdStr = Page.RouteData.Values["turmaId"] as string;
                } catch (Exception) {
                    Response.RedirectToRoute("adminHome");
                }
                int.TryParse(turmaIdStr, out turmaId);
                turmaContext = new TurmaContext();
                pessoaContext = new PessoaContext();
                servicoContext = new ServicoContext();
                LoadTurmasData();
                if (localStorageHorarios.Columns.Count <= 0) {
                    DataColumn column = new DataColumn("turmaHorarioInicio", typeof(DateTime));
                    DataColumn column2 = new DataColumn("turmaHorarioFim", typeof(DateTime));
                    DataColumn column3 = new DataColumn("turmaHorarioDia", typeof(int));
                    DataColumn column4 = new DataColumn("turmaHorarioDiaSemana", typeof(string));

                    localStorageHorarios.Columns.Add(column);
                    localStorageHorarios.Columns.Add(column2);
                    localStorageHorarios.Columns.Add(column3);
                    localStorageHorarios.Columns.Add(column4);
                } else if (localStorageHorarios.Rows.Count > 0) {
                    localStorageHorarios.Clear();
                }
            }
        }

        private void LoadTurmasData() {
            turmaData = turmaContext.GetTurmas(new Turma() { Id = turmaId });
            if (turmaData.Id.HasValue) {
                servicoDataTable = servicoContext.GetServicos();
                turmaAlunos = pessoaContext.GetAlunos(new Turma() { Id = turmaId });
                funcionariosDataTable = pessoaContext.GetFuncionarios(NivelAcessoSistema.PROFESSOR);
                turmaHorarios = turmaContext.GetHorarios(new Turma() { Id = turmaId });
                DataColumn diaColumn = new DataColumn("turmaHorarioDiaSemana", typeof(string));
                turmaHorarios.Columns.Add(diaColumn);

                foreach (DataRow dataRow in turmaHorarios.Rows) {
                    dataRow["turmaHorarioDiaSemana"] = GetDiaSemana(Convert.ToInt32(dataRow["turmaHorarioDia"].ToString()));
                }

                horariosGridView.DataSource = turmaHorarios;
                horariosGridView.DataBind();

                alunosGridView.DataSource = turmaAlunos;
                alunosGridView.DataBind();

                funcionarioDropDown.DataSource = funcionariosDataTable;
                funcionarioDropDown.DataValueField = "funcionarioId";
                funcionarioDropDown.DataTextField = "pessoaNome";
                funcionarioDropDown.DataBind();

                servicoDropDown.DataSource = servicoDataTable;
                servicoDropDown.DataValueField = "servicoId";
                servicoDropDown.DataTextField = "servicoNome";
                servicoDropDown.DataBind();

                turmaNome.Text = turmaData.Nome;
                turmaMax.Text = turmaData.Max.ToString();
                funcionarioDropDown.SelectedValue = Convert.ToString(turmaData.FkFuncionario);
                servicoDropDown.SelectedValue = Convert.ToString(turmaData.FkServico);

            } else {
                Response.RedirectToRoute("turmas");
            }
        }

        protected void AdicionarHorariosClickEventHandler(object sender, EventArgs e) {
            if (turmaHorarioDiaList.SelectedValue.Equals(null) || turmaHorarioInicio.Text.Equals("") || turmaHorarioFim.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para cadastrar uma nova turma, preencha todos os dados' }); </script>");
                return;
            }
            TurmaContext turmaContext = new TurmaContext();
            turmaIdStr = Page.RouteData.Values["turmaId"] as string;
            int.TryParse(turmaIdStr, out turmaId);
            DataTable turmaAlunos = turmaContext.GetAlunos(new Turma() { Id = turmaId });
            if (turmaAlunos.Rows.Count > 0) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Nao é possivel adicionar horarios em uma turma que contem alunos, favor remove-los antes de modificar os horarios'}); </script>");
                return;
            }
            DateTime inicioEntrada = DateTime.ParseExact(turmaHorarioInicio.Text, "h:mm tt", CultureInfo.InvariantCulture);
            DateTime horarioSaida = DateTime.ParseExact(turmaHorarioFim.Text, "h:mm tt", CultureInfo.InvariantCulture);
            List<int> diasSelected = new List<int>();
            foreach (ListItem listItem in turmaHorarioDiaList.Items)
                if (listItem.Selected)
                    diasSelected.Add(Convert.ToInt32(listItem.Value));

            diasSelected.ForEach((dia) => {
                DataTable actualData = (DataTable)horariosGridView.DataSource;
                DataRow dataRow = localStorageHorarios.NewRow();
                dataRow["turmaHorarioInicio"] = inicioEntrada;
                dataRow["turmaHorarioFim"] = horarioSaida;
                dataRow["turmaHorarioDia"] = dia;
                dataRow["turmaHorarioDiaSemana"] = GetDiaSemana(dia);

                localStorageHorarios.Rows.Add(dataRow);
            });
            turmaIdStr = Page.RouteData.Values["turmaId"] as string;
            int.TryParse(turmaIdStr, out turmaId);
            bool result = turmaContext.CreateHorarios(localStorageHorarios, new Turma() { Id = turmaId });
            if (result == true) {
                ClientScript.RegisterStartupScript(this.GetType(), "successCreated", "<script> Swal.fire({type: 'success', title: 'Adicionar horarios', text: 'Horarios Cadastrados com sucesso!'}).then(() => location.href = '" + GetRouteUrl("turmas", null) + "'); </script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }

        protected void OnHorariosRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gridviewRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            turmaContext = new TurmaContext();
            int horarioId = (int)horariosGridView.DataKeys[gridviewRow.RowIndex].Value;
            turmaIdStr = Page.RouteData.Values["turmaId"] as string;
            int.TryParse(turmaIdStr, out turmaId);
            DataTable turmaAlunos = turmaContext.GetAlunos(new Turma() { Id = turmaId });
            if (turmaAlunos.Rows.Count > 0) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Nao é possivel excluir horarios de uma turma que contem alunos, favor remove-los antes de modificar os horarios'}); </script>");
                return;
            }
            if (e.CommandName == "excluirHorario") {
                TurmaHorario turmaHorario = turmaContext.GetHorario(new TurmaHorario() { Id = horarioId });
                turmaHorario.Status = false;
                bool result = turmaContext.UpdateHorario(turmaHorario);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "successDeleted", "<script> Swal.fire({type: 'success', title: 'Excluir horario', text: 'Horario excluido com sucesso!'}).then(() => location.href = '" + GetRouteUrl("editarTurma", new { turmaId }) + "'); </script>");
                    return;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
                return;
            }
        }

        protected void OnAlunosRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "removerAluno") {
                GridViewRow gridviewRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int alunoId = (int)alunosGridView.DataKeys[gridviewRow.RowIndex].Value;
                turmaIdStr = Page.RouteData.Values["turmaId"] as string;
                int.TryParse(turmaIdStr, out turmaId);
                bool result = new TurmaContext().RemoverAluno(new Aluno() { Id = alunoId }, new Turma() { Id = turmaId });
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "succefullDeleted", "<script> Swal.fire({ type: 'success', title: 'Remover aluno', text: 'Aluno removido com sucesso!' }).then(() => location.href = '" + GetRouteUrl("editarTurma", new { turmaId }) + "'); </script>");
                    return;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
                return;
            }
        }

        protected void EditarTurmaClickEventHandler(object sender, EventArgs e) {

        }

        protected void OnPesquisarPessoaClickEventHandler(object sender, EventArgs e) {
            if (pessoaPesquisaDocumento.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "pesquisarPessoaInvalidInput", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Preencha o documento para pesquisar um aluno!' }); </script>");
                return;
            }
            pessoaContext = new PessoaContext();
            long pessoaDocumento = Convert.ToInt64(pessoaPesquisaDocumento.Text);
            turmaIdStr = Page.RouteData.Values["turmaId"] as string;
            int.TryParse(turmaIdStr, out turmaId);
            Aluno aluno = pessoaContext.GetAluno(pessoaDocumento, new Turma() { Id = turmaId });
            if (aluno.Id.HasValue) {
                ClientScript.RegisterStartupScript(this.GetType(), "alreadyStudent", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta errado', text: 'Esse aluno ja pertence a essa turma!'}); </script>");
                return;
            } else {
                aluno = new PessoaContext().GetAluno(pessoaDocumento);
                if (aluno.Id.HasValue) {
                    pessoaNome.Text = aluno.Nome + " " + aluno.Sobrenome;
                    pesquisaAlunoId.Text = aluno.Id.ToString();
                    return;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "pesquisarPessoaInvalidInput", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Não foram encontrados registros com esse documento!' }); </script>");
            }
        }

        protected void AdicionarAlunoClickEventHandler(object sender, EventArgs e) {
            if (pesquisaAlunoId.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingValues", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando!', text: 'Para adicionar um novo aluno, por favor, efetue a pesquisa antes!' }); </script>");
                return;
            }
            turmaIdStr = Page.RouteData.Values["turmaId"] as string;
            int.TryParse(turmaIdStr, out turmaId);
            int alunoId = Convert.ToInt32(pesquisaAlunoId.Text);
            bool conflitoHorario = new TurmaContext().GetHorarioConflito(new Turma() { Id = turmaId }, new Aluno() { Id = alunoId });
            if (conflitoHorario) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingValues", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta errado!', text: 'Há um conflito de horario para essse aluno, provavelmente ele está numa turma cujo os horarios batem!' }); </script>");
                return;
            } else {
                bool result = new TurmaContext().AdicionarAluno(new Aluno() { Id= alunoId }, new Turma() { Id = turmaId });
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "succefullAdded", "<script> Swal.fire({ type: 'success', title: 'Adicionar Aluno', text: 'Aluno adicionado com sucesso!' }).then(() => location.href = '"+ GetRouteUrl("editarTurma", new { turmaId }) +"'); </script>");
                    return;
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }


        private string GetDiaSemana(int dia) {
            switch (dia) {
                case 1:
                    return "Domingo";
                case 2:
                    return "Segunda";
                case 3:
                    return "Terça";
                case 4:
                    return "Quarta";
                case 5:
                    return "Quinta";
                case 6:
                    return "Sexta";
                case 7:
                    return "Sabado";
                default:
                    return "";
            }
        }
    }
}