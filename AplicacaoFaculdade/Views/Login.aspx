<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AplicacaoFaculdade.Views.Login" %>

<html lang="en">
    <head>
        <title>Login no sistema - Admin</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <link rel="Stylesheet" href="../Cdn/MainStyle.css" />
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
        <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
        <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@8"></script>
    </head>
    <body>
        <form id="form1" runat="server" class="h-100">
            <div class="container h-100">
			    <div class="row align-items-center h-100">
				    <div class="col-md-6 col-sm-12 col-xs-12 offset-md-3 offset-sm-12 offset-xs-12">
					    <div class="float-center" style="margin-bottom: 20px">
						    <div class="login-headertxt text-center">
							    Login no sistema
						    </div>
						    <hr>
                            <div class="form-group">
  							    <div class="row">
  								    <div class="col-md-12">
  									    <label class="inputLabel" for="txtUser">Usuário:</label>
  									    <asp:TextBox runat="server" ID="usuarioEmail" type="text" class="form-control" placeholder="Digite seu email"></asp:TextBox>
  								    </div>
  							    </div>
  						    </div>
  						    <div class="form-group">
  							    <div class="row">
  								    <div class="col-md-12">
  									    <label class="inputLabel" for="txtPass">Senha:</label>
  									    <asp:TextBox runat="server" ID="usuarioSenha" type="password" class="form-control" placeholder="Digite sua senha"></asp:TextBox>
  								    </div>
  							    </div>
  						    </div>
  						    <a href="#">Esqueceu sua senha?</a>
  						    <div class="float-right">
  							    <asp:Button runat="server" ID="ButtonDoLogin" OnClick="DoLoginEvent" CssClass="btn btn-outline-primary" Text="Login"/>
  						    </div>
					    </div>
				    </div>
			    </div>
		    </div>
        </form>
        <style>
			.login-headertxt{ font-weight: bold; color: gray; font-size: 18px; }
		</style>
    </body>
</html>