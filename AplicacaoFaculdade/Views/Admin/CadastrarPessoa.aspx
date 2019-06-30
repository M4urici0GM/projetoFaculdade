<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="CadastrarPessoa.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.CadastrarPessoa" %>
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
                        <asp:TextBox runat="server" id="pessoaNascimento" Cssclass="form-control datetimepicker-input" data-target="#datetimepicker4" placeholder="Data de nascimento"/>
                        <div class="input-group-append" data-target="#datetimepicker4" data-toggle="datetimepicker">
                            <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                        </div>
                    </div>
                    <small class="text-muted">Qual a data de nascimento ?</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4><i class="fas fa-phone"></i> Contato </h4>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaSobrenome">Telefone*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaTelefone" CssClass="form-control" placeholder="Telefone (opcional)" />
                    <small class="text-muted">Insira um telefone para contato.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-6">
                <div class="form-group">
                    <label for="pessoaCelular">Telefone*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCelular" CssClass="form-control" placeholder="Celular (opcional)" />
                    <small class="text-muted">Insira um numero de celular para contato.</small>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <h4> <i class="fas fa-file-alt"></i> Documentação</h4>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaRg">RG*:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaRg" CssClass="form-control" placeholder="RG" required="true"/>
                    <small class="text-muted">Digite o RG (obrigatório).</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCpf">CPF:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCpf" CssClass="form-control" placeholder="CPF" required="true"/>
                    <small class="text-muted">Digite o CPF.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCnpj">CNPJ:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCnpj" CssClass="form-control" placeholder="CNPJ (Opcional)" required="true"/>
                    <small class="text-muted">Digite o CNPJ se for jurídica.</small>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <h4> <i class="fas fa-map-marked-alt"></i> Endereço</h4>
            </div>
            <div class="col-12">
                <div class="form-group">
                    <label for="pessoaCep">CEP:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCep" CssClass="form-control" placeholder="Insira o CEP" required="true" onchange="getCEP(this)"/>
                    <small class="text-muted">Digite o CEP.</small>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-8">
                <div class="form-group">
                    <label for="pessoaEndereco">Logradouro:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaEndereco" CssClass="form-control"   placeholder="Endereço da casa" required="true" Enabled="false"/>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaNumeroRua">Numero:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaNumeroRua" placeholder="Numero da casa" CssClass="form-control" required="true" Enabled="false"/>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaComplemento">Complemento:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaComplemento" placeholder="Complemento (Opcional)" CssClass="form-control" required="true" Enabled="false"/>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaCidade">Cidade:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaCidade"  placeholder="Cidade" CssClass="form-control" required="true" Enabled="false"/>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <div class="form-group">
                    <label for="pessoaEstado">Estado:</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="pessoaEstado"  placeholder="Estado" CssClass="form-control" required="true" Enabled="false"/>
                </div>
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
                        <asp:LinkButton runat="server" OnClick="CadastrarPessoaClickEventHandler" class="btn btn-outline-primary btn-block">
                            <i class="fas fa-check-square"></i> &nbsp; Salvar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>


