using System;
using System.Text.RegularExpressions;

namespace Projeto02.Views.Admin
{
    public partial class Empresas : System.Web.UI.Page
    {

        int key = 0;
        Models.DAL Conx;
        private const string sqlForNnRelationShip = "SELECT e.id, e.cnpj as CNPJ, e.nome as NOME_EMPRESA, ass.nome as NOME_FUNCIONARIO, ass.cpf as CPF FROM Empresas e INNER JOIN AssociadosEmpresas assEmp ON assEmp.idEmpresas = e.Cnpj INNER JOIN Associados ass ON assEmp.idAssociados = ass.Cpf";

        protected void Page_Load(object sender, EventArgs e)
        {

            Conx = new Models.DAL();

            if (!IsPostBack)
            {
                string sqlQuery = "SELECT * FROM Associados";
                formAssociadoVinculado.DataSource = Conx.GetData(sqlQuery);
                formAssociadoVinculado.DataValueField = "Nome";
                formAssociadoVinculado.DataBind();
            }

            this.getAllAssociadoEmpresa();
            
        }

        private void getAllAssociadoEmpresa()
        {
            EmpresasList.DataSource = Conx.GetData(sqlForNnRelationShip);
            EmpresasList.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (formAssociadoVinculado.SelectedIndex == -1 || formCnpjEmpresa.Value == "" || formNomeEmpresa.Value == "")
                {
                    errMessage.Text = "Missing Data";
                }
                else
                {
                    Regex regex = new Regex("[^a-zA-Z0-9 ]", RegexOptions.Compiled);

                    string checagemEmpresa = "";
                    string cnpjAlreadyExists = "";
                    string cnpjSemPontuacao = regex.Replace(formCnpjEmpresa.Value, "");
                    string nomeEmpresa = formNomeEmpresa.Value;
                    string nomeAssociado = formAssociadoVinculado.SelectedValue;

                    //antes de decidir salvar ou nao a empresa, preciso verificar se ela ja existe

                    checagemEmpresa = "select Cnpj from Empresas where Cnpj = '{0}'";
                    checagemEmpresa = string.Format(checagemEmpresa, cnpjSemPontuacao);
                    cnpjAlreadyExists = Conx.getSingleData(checagemEmpresa);

                    if(cnpjAlreadyExists != "" && cnpjAlreadyExists != null)
                    {

                        if (insercaoMultipla.Checked)
                        {
                            formCnpjEmpresa.Value = "";
                            formAssociadoVinculado.SelectedIndex = -1;
                            formNomeEmpresa.Value = "";

                            string queryBuscaAssociados = "select Cpf from Associados where Nome = '{0}'";
                            queryBuscaAssociados = string.Format(queryBuscaAssociados, nomeAssociado);
                            string idAssociadoFK = Conx.getSingleData(queryBuscaAssociados);

                            string querySetarRelacionamento = "insert into AssociadosEmpresas values('{0}','{1}')";
                            querySetarRelacionamento = string.Format(querySetarRelacionamento, idAssociadoFK, cnpjSemPontuacao);
                            Conx.setData(querySetarRelacionamento);
                            this.getAllAssociadoEmpresa();
                            errMessage.Text = "Saved !";
                        }
                        else {

                            string querySetEmpresa = "insert into EMPRESAS values('{0}','{1}')";
                            querySetEmpresa = string.Format(querySetEmpresa, nomeEmpresa, cnpjSemPontuacao);
                            Conx.setData(querySetEmpresa);

                        }

                        
                    }
                    else
                    {

                        string querySetEmpresa = "insert into EMPRESAS values('{0}','{1}')";
                        querySetEmpresa = string.Format(querySetEmpresa, nomeEmpresa, cnpjSemPontuacao);
                        Conx.setData(querySetEmpresa);

                        string queryBuscaAssociados = "select Cpf from Associados where Nome = '{0}'";
                        queryBuscaAssociados = string.Format(queryBuscaAssociados, nomeAssociado);
                        string idAssociadoFK = Conx.getSingleData(queryBuscaAssociados);

                        string querySetarRelacionamento = "insert into AssociadosEmpresas values('{0}','{1}')";
                        querySetarRelacionamento = string.Format(querySetarRelacionamento, idAssociadoFK, cnpjSemPontuacao);
                        Conx.setData(querySetarRelacionamento);
                        this.getAllAssociadoEmpresa();
                        errMessage.Text = "Add new Employee !";

                    }

                }
            }
            catch (Exception ex)
            {
                errMessage.Text = ex.Message;
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            string querySetEmpresa = "delete from AssociadosEmpresas where IdAssociados = '{0}' and idEmpresas = '{1}'";
            querySetEmpresa = string.Format(querySetEmpresa, EmpresasList.SelectedRow.Cells[5].Text.ToString(), EmpresasList.SelectedRow.Cells[2].Text.ToString());
            Conx.setData(querySetEmpresa);

            formNomeEmpresa.Value = "";
            formCnpjEmpresa.Value = "";
            formCnpjEmpresa.Disabled = false;
            formAssociadoVinculado.SelectedIndex = -1;

            this.getAllAssociadoEmpresa();
            errMessage.Text = "Deleted !";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string nomeEmpresa = formNomeEmpresa.Value;
            var x = formAssociadoVinculado.SelectedValue;

            string querySetEmpresa = "update Empresas set Nome = '{0}' where Id = '{1}'";
            querySetEmpresa = string.Format(querySetEmpresa, nomeEmpresa, EmpresasList.SelectedRow.Cells[1].Text);
            Conx.setData(querySetEmpresa);

            string queryBuscaAssociados = "select Cpf from Associados where Nome = '{0}'";
            queryBuscaAssociados = string.Format(queryBuscaAssociados, x);
            string idAssociadoFK = Conx.getSingleData(queryBuscaAssociados);

            string queryUpdateAssociadoEmpresa = "update AssociadosEmpresas set IdAssociados = '{0}' where idEmpresas = '{1}'";
            queryUpdateAssociadoEmpresa = string.Format(queryUpdateAssociadoEmpresa, idAssociadoFK, EmpresasList.SelectedRow.Cells[2].Text.ToString());
            Conx.setData(queryUpdateAssociadoEmpresa);

            formNomeEmpresa.Value = "";
            formCnpjEmpresa.Value = "";
            formCnpjEmpresa.Disabled = false;
            formAssociadoVinculado.SelectedIndex = -1;

            this.getAllAssociadoEmpresa();
            errMessage.Text = "Updated !";
        }

        protected void EmpresasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            formCnpjEmpresa.Value = EmpresasList.SelectedRow.Cells[2].Text.ToString();
            formNomeEmpresa.Value = EmpresasList.SelectedRow.Cells[3].Text.ToString();
            formAssociadoVinculado.SelectedItem.Value = EmpresasList.SelectedRow.Cells[5].Text;
            formCnpjEmpresa.Disabled = true;

            if(formNomeEmpresa.Value == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmpresasList.SelectedRow.Cells[1].Text);
            }

        }
    }
}