<%@ Page Title="" Language="C#" MasterPageFile="~/Views/template/Admin.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AplicacaoFaculdade.Views.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">


    <div class="container">
        <div class="row mt-4">
           <div class="col-12 d-flex justify-content-between">
               <h2>Usuarios</h2>
               <button class="btn btn-outline-dark">
                   <i class="fas fa-user-plus"></i> &nbsp; Novo
               </button>
           </div>
        </div>
        <hr />
        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="userGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="OnRowCommandEventHandler" 
                    CssClass="table table-borderless table-hover text-center border-0" AllowPaging="true" OnPageIndexChanging="OnPageChangingIndex" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="pessoaNome" HeaderStyle-CssClass="text-center"  HeaderText="Nome"/>
                        <asp:BoundField DataField="usuarioLogin" HeaderStyle-CssClass="text-center"  HeaderText="Usuario" />
                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center" >
                            <ItemTemplate>
                                <asp:LinkButton ID="editButton" runat="server" CommandName="editUser" CssClass="btn btn-outline-primary">
                                    <i class="fas fa-user-cog"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="delButton" runat="server" CommandName="editUser" CssClass="btn btn-outline-danger">
                                    <i class="fas fa-user-minus"></i> Excluir
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
