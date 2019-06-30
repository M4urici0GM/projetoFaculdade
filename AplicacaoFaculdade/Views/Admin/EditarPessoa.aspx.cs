using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Models;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class EditarPessoa : System.Web.UI.Page {
        private PessoaContext pessoaContext;
        private UsuarioContext usuarioContext;
        private Pessoa pessoa;
        private DataTable pessoaUsuarios;
        private DataTable usuarioNivelAcesso;
        private int pessoaId;
        private string usuarioIdStr;


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                try {
                    usuarioIdStr = Page.RouteData.Values["pessoaId"] as string;
                } catch (Exception) {
                    Response.RedirectToRoute("adminHome");
                }
                int.TryParse(usuarioIdStr, out pessoaId);
                pessoaContext = new PessoaContext();
                usuarioContext = new UsuarioContext();

                LoadPessoaData();
            }
        }

        private void LoadPessoaData() {
            pessoa = pessoaContext.GetPessoa(pessoaId);
            pessoaUsuarios = usuarioContext.GetUsuarios(pessoa);
            usuarioNivelAcesso = usuarioContext.GetNivelAcesso();
            pessoaUsuariosGridView.DataSource = pessoaUsuarios;
            pessoaUsuariosGridView.DataBind();
            nivelAcessoDropDown.DataSource = usuarioNivelAcesso;
            nivelAcessoDropDown.DataTextField = "nivelAcessoNome";
            nivelAcessoDropDown.DataValueField = "nivelAcessoId";
            editarNivelAcessoDropDown.DataSource = usuarioNivelAcesso;
            editarNivelAcessoDropDown.DataTextField = "nivelAcessoNome";
            editarNivelAcessoDropDown.DataValueField = "nivelAcessoId";
            nivelAcessoDropDown.DataBind();
            editarNivelAcessoDropDown.DataBind();
            HandleDataToFields();
        }

        private void HandleDataToFields() {
            pessoaIdHidden.Text = Convert.ToString(pessoaId);
            pessoaNome.Text = pessoa.Nome;
            pessoaSobrenome.Text = pessoa.Sobrenome;
            pessoaSexo.SelectedIndex = pessoa.Sexo;
            pessoaNascimento.Text = pessoa.Nascimento.ToString("dd/MM/yyyy");
            pessoaTelefone.Text = Convert.ToString(pessoa.Telefone);
            pessoaCelular.Text = Convert.ToString(pessoa.Celular);
            pessoaRg.Text = Convert.ToString(pessoa.Rg);
            pessoaCpf.Text = Convert.ToString(pessoa.Cpf);
            pessoaCnpj.Text = Convert.ToString(pessoa.Cnpj);
            pessoaCep.Text = Convert.ToString(pessoa.Cep);
            pessoaNumeroRua.Text = Convert.ToString(pessoa.Numero);
            pessoaComplemento.Text = pessoa.Complemento;
            pessoaCidade.Text = pessoa.Cidade;
            pessoaEstado.Text = pessoa.Estado;
            pessoaEndereco.Text = pessoa.Endereco;
        }
        protected void EditarPessoaClickEventHandler(object sender, EventArgs e) {
            PessoaContext pessoaContext = new PessoaContext();
            if (pessoaNome.Text.Equals("") || pessoaSobrenome.Text.Equals("") || pessoaSexo.Text.Equals("") || pessoaCpf.Text.Equals("") || pessoaEndereco.Text.Equals("") || pessoaCep.Text.Equals("") || pessoaNumeroRua.Text.Equals("") || pessoaCidade.Text.Equals("") || pessoaEstado.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'error', title: 'Ops, algo está faltando', text: 'Por favor, preencha todos os dados obrigatórios!'}); </script>");
                return;
            }

            Pessoa pessoa = new Pessoa() {
                Id = Convert.ToInt32(pessoaIdHidden.Text),
                Nome = pessoaNome.Text,
                Sobrenome = pessoaSobrenome.Text,
                Sexo = Convert.ToInt32(pessoaSexo.SelectedValue),
                Nascimento = DateTime.ParseExact(pessoaNascimento.Text, "dd/MM/yyyy", null),
                Telefone = string.IsNullOrEmpty(pessoaTelefone.Text) ? (long?)null : Convert.ToInt64(pessoaTelefone.Text),
                Celular = string.IsNullOrEmpty(pessoaCelular.Text) ? (long?)null : Convert.ToInt64(pessoaCelular.Text),
                Rg = string.IsNullOrEmpty(pessoaRg.Text) ? (long?)null : Convert.ToInt64(pessoaRg.Text),
                Cpf = string.IsNullOrEmpty(pessoaCpf.Text) ? (long?)null : Convert.ToInt64(pessoaCpf.Text),
                Cnpj = string.IsNullOrEmpty(pessoaCnpj.Text) ? (long?)null : Convert.ToInt64(pessoaCnpj),
                Cep = Convert.ToInt32(pessoaCep.Text),
                Cidade = pessoaCidade.Text,
                Complemento = pessoaComplemento.Text,
                Endereco = pessoaEndereco.Text,
                Estado = pessoaEstado.Text,
                Numero = string.IsNullOrEmpty(pessoaNumeroRua.Text) ? (int?)null : Convert.ToInt32(pessoaNumeroRua.Text),
                Status = true
            };

            bool editarPessoa = pessoaContext.UpdatePessoa(pessoa);
            Response.Write(pessoaContext.AffectedRows);
            Response.Write(editarPessoa);
            if (editarPessoa && pessoaContext.AffectedRows.HasValue) {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullEdit", "<script> Swal.fire({type: 'success', title: 'Editar pessoa', text: 'Pessoa editada com sucesso!'}).then(() => location.href = '/admin/pessoas/'); </script>");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve algum erro, favor entrar com o administrador do sistema'}); </script>");
            }
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int usuarioId = (int) pessoaUsuariosGridView.DataKeys[gvRow.RowIndex].Value;
            usuarioIdHidden.Text = Convert.ToString(usuarioId);
            
            if (e.CommandName == "editarUsuario") {
                Usuario usuario = new UsuarioContext().GetUsuarios(usuarioId);
                editarNivelAcessoDropDown.SelectedValue = usuario.FkNivelAcesso.ToString();
                editarUsuarioLogin.Text = usuario.Email;
                ClientScript.RegisterStartupScript(this.GetType(), "openEditUserModal", "<script>$('#editarUsuarioModal').modal('show');</script>");
            } else if (e.CommandName == "excluirUsuario") {
                Usuario usuario = new UsuarioContext().GetUsuarios(usuarioId);
                usuario.Status = false;
                bool result = new UsuarioContext().UpdateUsuario(usuario);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "succefullEdit", "<script> Swal.fire({type: 'success', title: 'Editar usuario', text: 'Usuario excluido com sucesso!'}).then(() => location.href = '/admin/pessoas/editar/" + usuarioIdHidden.Text + "'); </script>");
                } else {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorOnDeleteUser", "<script> swal.fire({type: 'error', title: 'ops, algo deu errado', text: 'houve algum erro, favor entrar com o administrador do sistema'}); </script>");
                }
            }
        }

        protected void EditarUsuarioClickEventHandler(object sender, EventArgs e) {
            usuarioContext = new UsuarioContext();
            if (editarUsuarioLogin.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script>Swal.fire({type: 'error', title:'Ops, algo está faltando!', text: 'Para editar um usuario, preencha todos os campos!'});</script>");
                return;
            }
            Usuario usuario = new Usuario() {
                Id = Convert.ToInt32(usuarioIdHidden.Text),
                Email = editarUsuarioLogin.Text,
                Senha = editarUsuarioSenha.Text,
                FkNivelAcesso = Convert.ToInt32(editarNivelAcessoDropDown.SelectedValue),
                Status = true
            };
            bool result = usuarioContext.UpdateUsuario(usuario);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullEdit", "<script> Swal.fire({type: 'success', title: 'Editar usuario', text: 'Usuario editado com sucesso!'}).then(() => location.href = '/admin/pessoas/'); </script>");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "succefullEdit", "<script> Swal.fire({type: 'success', title: 'Editar usuario', text: 'Usuario editado com sucesso!'}).then(() => location.href = '/admin/pessoas/'); </script>");
            }
        }

        protected void AdicionarUsuarioClickEventHandler(object sender, EventArgs e) {

            usuarioContext = new UsuarioContext();
            if (usuarioLogin.Text.Equals("") || usuarioSenha.Text.Equals("") || nivelAcessoDropDown.SelectedIndex.Equals(null)) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script>Swal.fire({type: 'error', title:'Ops, algo está faltando!', text: 'Para adicionar um usuario, preencha todos os campos!'});</script>");
                return;
            }

            DataTable usuariosPessoa = usuarioContext.GetUsuarios(new Pessoa() { Id = Convert.ToInt32(pessoaIdHidden.Text) });

            if (usuariosPessoa.Rows.Count > 0) {
                ClientScript.RegisterStartupScript(this.GetType(), "alreadyHaveAnUser", "<script> Swal.fire({ type: 'error', title: 'Ops, algo está errado', text: 'Essa pessoa ja possui um usuario ativo!' }); </script>");
            }

            Usuario usuario = new Usuario() {
                Email = usuarioLogin.Text,
                Senha = usuarioSenha.Text,
                FkNivelAcesso = Convert.ToInt32(nivelAcessoDropDown.SelectedValue)
            };
            
            int result = usuarioContext.CreateUsuario(usuario, new Pessoa() { Id = Convert.ToInt32(pessoaIdHidden.Text) });
            if (result == 0) {
                ClientScript.RegisterStartupScript(this.GetType(), "erroronadduser", "<script> swal.fire({type: 'error', title: 'ops, algo deu errado', text: 'houve algum erro, favor entrar com o administrador do sistema'}); </script>");
            } else if (result == 1) {
                ClientScript.RegisterStartupScript(this.GetType(), "successadduser", "<script> swal.fire({ type: 'success', title: 'adicionar usuario', text: 'usuario adicionado com sucesso!' }).then(() => { location.href =  '/admin/pessoas/editar/" + pessoaIdHidden.Text + "'}); </script>");
            } else if (result == 2) {
                ClientScript.RegisterStartupScript(this.GetType(), "userexists", "<script> swal.fire({ type: 'error', title:'ops, algo deu errado!', text: 'um usuario com esse email já existe, por favor tente novamente!'}); </script>");
            }
        }
    }
}