<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="Projeto02.Views.Admin.Empresas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="myMasterPage" runat="server">
    <div class="container-fluid">

       <div class="row">
           <div class="col">
               <h2 class="text-center text-capitalize mt-4">Gerenciamento de Empresas</h2>
           </div>
       </div>

      <div class="row d-flex justify-content-md-start">

          <div class="col-4 mt-5 p-4 border" style="padding-left:5vh; width:50vh;">
              <div class="mt-1">
              <label for="" class="form-label text-success"><strong>Nome Empresa</strong></label>
              <input type="text" pattern="[a-zA-Z]+$" required="required" title="Só letras minúsculas entre [a-zA-Z]" 
                  placeholder="informe a razao social" autocomplete="off" style="width:350px;" class="form-control" />
              </div>

              <div class="mt-3">
              <label for="" class="form-label text-success"><strong>CNPJ Empresa</strong></label>
              <input type="text" pattern="\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}" title="Informe um CNPJ válido (00.000.000/0000-00)"  
                  placeholder="CNPJ..." autocomplete="off" class="form-control" required="required" style="width:350px;" />
              </div>

               <div class="mt-3">
              <label for="" class="form-label text-success"><strong>Associado vinculado</strong></label>
              <asp:DropDownList ID="DropDownList1" runat="server" style="width:350px;" class="form-control"></asp:DropDownList>
              </div>

              <div class="row">
                <div class="d-flex justify-content-between mt-4">
                    <asp:Button runat="server" Text="Remover" class="btn-outline-secondary btn"/>
                    <asp:Button runat="server" Text="Adicionar" class="btn-outline-secondary btn"/>
                    <asp:Button runat="server" Text="Atualizar" class="btn-outline-secondary btn"/>
                </div>
              </div>

          </div>

          <div class="col-md-8">
              <asp:GridView ID="GridView1" runat="server"></asp:GridView>
          </div>


      </div>

    </div>
</asp:Content>
