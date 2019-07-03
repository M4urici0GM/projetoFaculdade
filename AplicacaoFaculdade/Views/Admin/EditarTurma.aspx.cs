using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Enums;
using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        }

        protected void OnHorariosRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {

        }

        protected void OnAlunosRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {

        }

        protected void EditarTurmaClickEventHandler(object sender, EventArgs e) {

        }

        protected void OnDataBoundEventHandler(object sender, EventArgs e) {
            Response.Write(e.ToString());
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