<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Contratos.aspx.cs" Inherits="AplicacaoFaculdade.Views.Admin.Contratos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="row mt-4">
               <div class="col-12 d-flex justify-content-between align-items-center">
                   <h2>Contratos</h2>
                   <a href="<%: GetRouteUrl("novoContrato", null) %>" class="btn btn-outline-dark">
                       <i class="fas fa-file-alt"></i> &nbsp; Novo Contrato
                   </a>
               </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="contratosGridView" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Ainda não há contratos no sistema."
                AutoGenerateColumns="false" DataKeyNames="contratoId" CssClass="table table-hover table-borderless border-0 mt-3 text-center align-middle" OnRowCommand ="OnRowCommandEventHandler">
                <Columns>
                    <asp:BoundField HeaderText="Data" DataField="contratoData" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Vencimento" DataField="contratoDiaVencimento" />
                    <asp:BoundField HeaderText="Contratante" DataField="pessoaNome" />
                    <asp:BoundField HeaderText="Sobrenome" DataField="pessoaSobrenome" />
                    <asp:BoundField HeaderText="Desconto" DataField="desconto"  DataFormatString="R$ {0:F}" />
                    <asp:BoundField HeaderText="Mensalidade" DataField="valorTotal"  DataFormatString="R$ {0:F}" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="btn btn-outline-primary">
                                <i class="fas fa-eye"></i> Serviços
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-outline-danger" CommandName="CancelarContrato" OnClientClick="return confirmDelete(this)">
                                <i class="fas fa-times"></i> Cancelar
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
