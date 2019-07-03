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
    public partial class NovaTurma : System.Web.UI.Page {

        private static DataTable localStorageHorarios = new DataTable("teste");
        private DataTable professoresDataTable;
        private DataTable servicosDataTable;
        private PessoaContext pessoaContext;
        private ServicoContext servicoContext;
        private TurmaContext turmaContext;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                pessoaContext = new PessoaContext();
                servicoContext = new ServicoContext();
                LoadProfessoresData();
                LoadServicosData();
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
                horariosGridView.DataSource = localStorageHorarios;
                horariosGridView.DataBind();
            }
        }

        private void LoadServicosData() {
            servicosDataTable = servicoContext.GetServicos();
            servicoDropDown.DataSource = servicosDataTable;
            servicoDropDown.DataValueField = "servicoId";
            servicoDropDown.DataTextField = "servicoNome";
            servicoDropDown.DataBind();
        }

        private void LoadProfessoresData() {
            professoresDataTable = pessoaContext.GetFuncionarios(NivelAcessoSistema.PROFESSOR);
            funcionarioDropDown.DataSource = professoresDataTable;
            funcionarioDropDown.DataValueField = "funcionarioId";
            funcionarioDropDown.DataTextField = "pessoaNome";
            funcionarioDropDown.DataBind();
        }

  
        private string GetDiaSemana(int dia) {
            switch (dia) {
                case 1: return "Domingo";
                case 2: return "Segunda";
                case 3: return "Terça";
                case 4: return "Quarta";
                case 5: return "Quinta";
                case 6: return "Sexta";
                case 7: return "Sabado";
                default: return "";
            }
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            localStorageHorarios.Rows.RemoveAt(gvRow.RowIndex);
            horariosGridView.DataSource = localStorageHorarios;
            horariosGridView.DataBind();
        }

        protected void AdicionarHorariosClickEventHandler(object sender, EventArgs e) {
            if (turmaHorarioDiaList.SelectedValue.Equals(null) || turmaHorarioInicio.Text.Equals("") || turmaHorarioFim.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para adicionar um novo horario, preencha todos os dados' }); </script>");
                return;
            }
            DateTime inicioEntrada = DateTime.ParseExact(turmaHorarioInicio.Text, "h:mm tt", CultureInfo.InvariantCulture);
            DateTime horarioSaida  = DateTime.ParseExact(turmaHorarioFim.Text, "h:mm tt", CultureInfo.InvariantCulture);
            List<int> diasSelected = new List<int>();
            foreach (ListItem listItem in turmaHorarioDiaList.Items) 
                if (listItem.Selected)
                    diasSelected.Add(Convert.ToInt32(listItem.Value));
               
            diasSelected.ForEach((dia) => {
                DataRow dataRow = localStorageHorarios.NewRow();
                dataRow["turmaHorarioInicio"] = inicioEntrada;
                dataRow["turmaHorarioFim"] = horarioSaida;
                dataRow["turmaHorarioDia"] = dia;
                dataRow["turmaHorarioDiaSemana"] = GetDiaSemana(dia);

                localStorageHorarios.Rows.Add(dataRow);
                horariosGridView.DataSource = localStorageHorarios;
                horariosGridView.DataBind();
            });
            turmaHorarioInicio.Text = "";
            turmaHorarioFim.Text = "";
            return;
        }

        protected void CadastrarTurmaClickEventHandler(object sender, EventArgs e) {
            if (turmaNome.Text.Equals("") || turmaMax.Text.Equals("") || servicoDropDown.SelectedValue.Equals(null) || funcionarioDropDown.SelectedValue.Equals(null)) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Para cadastrar uma nova turma, preencha todos os dados' }); </script>");
                return;
            }
            turmaContext = new TurmaContext();
            Turma turma = new Turma() {
                 Nome = turmaNome.Text,
                 Max = Convert.ToInt32(turmaMax.Text),
                 FkServico = Convert.ToInt32(servicoDropDown.SelectedValue),
                 FkFuncionario = Convert.ToInt32(funcionarioDropDown.SelectedValue)
            };

            bool result = turmaContext.CreateTurma(turma, localStorageHorarios);
            if (result == true) {
                ClientScript.RegisterStartupScript(this.GetType(), "successCreated", "<script> Swal.fire({type: 'success', title: 'Cadastrar turma', text: 'Turma Cadastrada com sucesso!'}).then(() => location.href = '" + GetRouteUrl("turmas", null) +"'); </script>");
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }
    }
}