<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Projeto02.Views.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" />
    <title>Login Page for Technical Exam</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row mt-4 mb-xxl-5">
        </div>
        <div class="row">
            <div class="col-md-4">
 
            </div>
            <div class="col-md-4">
                <form id="form1" runat="server">
                    <div class="text-center mt-3">
                        <img src="../Images/LoginImg.jpg" width="100" height="100" />
                    </div>
                   <div class="mt-3">
                       <label for="" class="form-label">Username</label>
                       <input type="text" placeholder="enter your credencial" autocomplete="off" id="usernameToLogin" class="form-control" />
                   </div>
                    <div class="mt-3">
                       <label for="" class="form-label">Password</label>
                       <input type="password" placeholder="enter your password" autocomplete="off" id="passwordToLogin" class="form-control" />
                   </div>
                    <div class="text-center mt-4 d-grid">
                        <asp:Button class="btn btn-primary px-3 py-2" Text="Login" runat="server" ID="btnLogin" OnClick="btnLogin_Click"></asp:Button>
                    </div>
                </form>
            </div>
            <div class="col-md-4">
 
            </div>
        </div>
    </div>
    
</body>
</html>
