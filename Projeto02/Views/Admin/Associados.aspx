<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Associados.aspx.cs" Inherits="Projeto02.Views.Admin.Associados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="myMasterPage" runat="server">
    <div class="container-fluid">

       <div class="row">
           <div class="col">
               <h2 class="text-center text-capitalize mt-4">Gerenciamento de Associados</h2>
           </div>
       </div>

      <div class="row d-flex justify-content-md-start">

          <div class="col-4 mt-5 p-4 border" style="padding-left:5vh; width:50vh;">

              <div class="mt-1">
              <label for="" class="form-label text-success"><strong>Nome Associado</strong></label>
              <input type="text" pattern="^[a-zA-Z ]+$" required="required" title="Só letras minúsculas entre [a-zA-Z]" 
                  placeholder="Informe seu nome" autocomplete="off" style="width:350px;" class="form-control" runat="server" id="formNomeAssociado"/>
              </div>

              <div class="mt-3">
              <label for="" class="form-label text-success"><strong>CPF Associado</strong></label>
              <input type="text" pattern="^(\d{3}\.\d{3}\.\d{3}-\d{2})|(\d{11})$" title="Informe um CPF válido (000.000.000-00)"  
                  placeholder="CPF..." autocomplete="off" class="form-control" required="required" style="width:350px;" runat="server" id="formCpfAssociado"/>
              </div>

              <div class="mt-3">
              <label for="" class="form-label text-success"><strong>Data Nascimento Associado</strong></label>
              <input type="text" runat="server" placeholder="dd/mm/aaaa" data-format="00/00/0000" class="form-control" id="formNascimentoAssociado"/>
              <asp:Label runat="server" ID="Label1" class="text-danger"></asp:Label>
              </div>

              <div class="mt-3">
              <label for="" class="form-label text-success"><strong>Empresa Associado vinculado</strong></label>
              <asp:DropDownList ID="EmpresaAssociadoVinculado" runat="server" style="width:350px;" class="form-control"></asp:DropDownList>
              <asp:Label runat="server" ID="errMessage" class="text-danger"></asp:Label>
              </div>

              <div class="mt-3">
              <asp:CheckBox ID="insercaoMultipla" Text="Insercao Multipla" BackColor="Black" class="form-check-input" runat="server"/>
              </div>

                <div class="d-flex justify-content-between mt-4">
                    <asp:Button runat="server" Text="Remover" ID="btnRemover" class="btn-outline-secondary btn" OnClick="btnRemover_Click"/>
                    <asp:Button runat="server" Text="Adicionar" ID="btnAdicionar" class="btn-outline-secondary btn" OnClick="btnAdicionar_Click"/>
                    <asp:Button runat="server" Text="Atualizar" ID="btnAtualizar" class="btn-outline-secondary btn" OnClick="btnAtualizar_Click"/>
                </div>
          </div>

          <div class="col-md-8 mt-4 p-4" style="position: relative; float: right; width: 50%; margin-left: 50px">
              <asp:GridView ID="Relacionamento" class="table table-bordered table-striped table-hover" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Relacionamento_SelectedIndexChanged"></asp:GridView>
          </div>


      </div>

    </div>
    
</asp:Content>
