<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="CadastrarPessoa.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.CadastrarPessoa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container mb-4">
        <div class="row mt-4">
           <div class="col-12 d-flex justify-content-between align-items-center">
               <h2> <i class="fas fa-user-plus"></i> Cadastrar pessoa</h2>
               <div>
                   <a href="<%: GetRouteUrl("pessoas", null) %>" class="btn btn-outline-dark">
                        <i class="fas fa-arrow-left"></i> &nbsp; Voltar
                   </a>
                  <%-- <a href="#" class="btn btn-outline-dark">
                       <i class="fas fa-folder-open"></i> &nbsp; Documentos
                   </a>--%>
               </div>
           </div>
        </div>
        <hr />
        <div class="row mt-4">
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNome">Nome<span class="text-danger">*</span>:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaNome" CssClass="form-control" placeholder="Nome da pessoa" />
                    <small class="text-muted">Qual o nome ?</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaSobrenome">Sobrenome<span class="text-danger">*</span>:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaSobrenome" CssClass="form-control" placeholder="Nome da pessoa" />
                    <small class="text-muted">Qual o sobrenome ?</small>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNome">Sexo<span class="text-danger">*</span>:</label>
                    <asp:DropDownList runat="server" ID="pessoaSexo" CssClass="form-control">
                        <asp:ListItem Text="Masculino" Value="1"/>
                        <asp:ListItem Text="Feminino" Value="0"/>
                        <asp:ListItem Text="Não binário" Value="null"/>
                    </asp:DropDownList>
                    <small class="text-muted">Qual o sexo da pessoa ?</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaNascimento">Nascimento<span class="text-danger">*</span>:</label>
                    <div class="input-group date" id="datetimepicker4" data-target-input="nearest">
                        <input type="text" id="pessoaNascimento" class="form-control datetimepicker-input" data-target="#datetimepicker4" placeholder="Data de nascimento"/>
                        <div class="input-group-append" data-target="#datetimepicker4" data-toggle="datetimepicker">
                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                        </div>
                    </div>
                    <small class="text-muted">Qual a data de nascimento ?</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="text-center">
            <small class="text-muted">Para os itens abaixo, clique em adicionar, preencha os dados e clique em salvar.</small><br />
            <small class="text-danger">É necessário adicionar ao menos 1 telefone, endereço e documento</small>
        </div>
         <hr />
        <div class="row">

            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2> <i class="fas fa-file-alt"></i> Documentos</h2>
                <button class="btn btn-outline-dark" id="adicionarDocumento">
                    <i class="fas fa-plus"></i> &nbsp; Adicionar
                </button>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2> <i class="fas fa-map-marked"></i> Enderecos </h2>
                <button class="btn btn-outline-dark" id="adicionarEndereco">
                    <i class="fas fa-plus"></i> &nbsp; Adicionar
                </button>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2> <i class="fas fa-phone-square"></i> Telefones </h2>
                <button class="btn btn-outline-dark" id="adicionarTelefone">
                    <i class="fas fa-plus"></i> &nbsp; Adicionar
                </button>
            </div>
        </div>
        <hr />
        <div class="row" >
            <div class="col-12 col-md-6 offset-md-6">
                <div class="row">
                    <div class="col-6">
                        <a class="btn btn-outline-dark btn-block" href="#">
                            <i class="fas fa-arrow-left"></i> &nbsp; Voltar
                        </a>
                    </div>
                    <div class="col-6">
                        <a class="btn btn-outline-primary btn-block" href="#">
                            <i class="fas fa-check-square"></i> &nbsp; Salvar
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        window.onload = () => {
            $('#datetimepicker4').datetimepicker({
                format: 'L'
            });

            function addDocument() {
                event.preventDefault();

                return true;
            }
        }
    </script>
</asp:Content>


