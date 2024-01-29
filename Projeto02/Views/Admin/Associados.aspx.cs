using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto02.Views.Admin
{
    public partial class Associados : System.Web.UI.Page
    {

        int key = 0;
        Models.DAL Conx;
        private const string sqlForNnRelationShip = "SELECT ass.id, e.Cnpj as CNPJ, e.nome as NOME_EMPRESA, ass.DataNascimento as DATA_NASCIMENTO, ass.nome as NOME_FUNCIONARIO, ass.cpf as CPF FROM Empresas e INNER JOIN AssociadosEmpresas assEmp ON assEmp.idEmpresas = e.Cnpj INNER JOIN Associados ass ON assEmp.idAssociados = ass.Cpf";

        protected void Page_Load(object sender, EventArgs e)
        {
            Conx = new Models.DAL();

            if (!IsPostBack)
            {
                string sqlQuery = "SELECT * FROM Empresas";
                EmpresaAssociadoVinculado.DataSource = Conx.GetData(sqlQuery);
                EmpresaAssociadoVinculado.DataValueField = "Nome";
                EmpresaAssociadoVinculado.DataBind();
            }

            this.getAllAssociadoEmpresa();
        }

        private void getAllAssociadoEmpresa()
        {
            Relacionamento.DataSource = Conx.GetData(sqlForNnRelationShip);
            Relacionamento.DataBind();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmpresaAssociadoVinculado.SelectedIndex == -1 || formCpfAssociado.Value == "" || formNomeAssociado.Value == "" || formNascimentoAssociado.Value == "")
                {
                    errMessage.Text = "Missing Data";
                }
                else
                {
                    Regex regex = new Regex("[^a-zA-Z0-9 ]", RegexOptions.Compiled);

                    string checagemAssociado = "";
                    string cpfAlreadyExists = "";

                    string cpfSemPontuacao = regex.Replace(formCpfAssociado.Value, "");
                    string nomeAssociado = formNomeAssociado.Value;
                    string dataNascimento = formNascimentoAssociado.Value;
                    string empresaVinculada = EmpresaAssociadoVinculado.SelectedValue;

                    //antes de decidir salvar ou nao a empresa, preciso verificar se ela ja existe

                    checagemAssociado = "select Cpf from Associados where cpf = '{0}'";
                    checagemAssociado = string.Format(checagemAssociado, cpfSemPontuacao);
                    cpfAlreadyExists = Conx.getSingleData(checagemAssociado);

                    if (cpfAlreadyExists != "" && cpfAlreadyExists != null)
                    {

                        if (insercaoMultipla.Checked)
                        {
                            formCpfAssociado.Value = "";
                            EmpresaAssociadoVinculado.SelectedIndex = -1;
                            formNomeAssociado.Value = "";
                            formNascimentoAssociado.Value = "";

                            string queryBuscaEmpresa = "select Cnpj from Empresas where Nome = '{0}'";
                            queryBuscaEmpresa = string.Format(queryBuscaEmpresa, empresaVinculada);
                            string idEmpresaFK = Conx.getSingleData(queryBuscaEmpresa);

                            string querySetarRelacionamento = "insert into AssociadosEmpresas values('{0}','{1}')";
                            querySetarRelacionamento = string.Format(querySetarRelacionamento, cpfSemPontuacao, idEmpresaFK);
                            Conx.setData(querySetarRelacionamento);
                            this.getAllAssociadoEmpresa();
                            errMessage.Text = "Saved !";
                        }
                        else {

                            string querySetEmpresa = "insert into Associados values('{0}','{1}', '{2}')";
                            querySetEmpresa = string.Format(querySetEmpresa, nomeAssociado, cpfSemPontuacao, dataNascimento);
                            Conx.setData(querySetEmpresa);

                        }

                    }

                    else
                    {

                        string querySetEmpresa = "insert into Associados values('{0}','{1}', '{2}')";
                        querySetEmpresa = string.Format(querySetEmpresa, nomeAssociado, cpfSemPontuacao, dataNascimento);
                        Conx.setData(querySetEmpresa);

                        string queryBuscaEmpresa = "select Cnpj from Empresas where Nome = '{0}'";
                        queryBuscaEmpresa = string.Format(queryBuscaEmpresa, empresaVinculada);
                        string idEmpresaFK = Conx.getSingleData(queryBuscaEmpresa);

                        string querySetarRelacionamento = "insert into AssociadosEmpresas values('{0}','{1}')";
                        querySetarRelacionamento = string.Format(querySetarRelacionamento, cpfSemPontuacao, idEmpresaFK);
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

        protected void Relacionamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            formCpfAssociado.Value = Relacionamento.SelectedRow.Cells[6].Text.ToString();
            formNomeAssociado.Value = Relacionamento.SelectedRow.Cells[5].Text.ToString();
            formCpfAssociado.Disabled = true;

            EmpresaAssociadoVinculado.SelectedItem.Value = Relacionamento.SelectedRow.Cells[3].Text;
            formNascimentoAssociado.Value = Relacionamento.SelectedRow.Cells[4].Text;
            formNascimentoAssociado.Disabled = true;

            if (formNomeAssociado.Value == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(Relacionamento.SelectedRow.Cells[1].Text);
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            string nomeFuncionario = formNomeAssociado.Value;
            var x = EmpresaAssociadoVinculado.SelectedValue;
            var y = Relacionamento.SelectedRow.Cells[1].Text;

            string querySetEmpresa = "update Associados set Nome = '{0}' where Id = '{1}'";
            querySetEmpresa = string.Format(querySetEmpresa, nomeFuncionario, Relacionamento.SelectedRow.Cells[1].Text);
            Conx.setData(querySetEmpresa);

            string queryBuscaEmpresas = "select Cnpj from Empresas where Nome = '{0}'";
            queryBuscaEmpresas = string.Format(queryBuscaEmpresas, x);
            string idEmpresaFK = Conx.getSingleData(queryBuscaEmpresas);

            string queryUpdateAssociadoEmpresa = "update AssociadosEmpresas set idEmpresas = '{0}' where IdAssociados = '{1}'";
            queryUpdateAssociadoEmpresa = string.Format(queryUpdateAssociadoEmpresa, idEmpresaFK, Relacionamento.SelectedRow.Cells[6].Text.ToString());
            Conx.setData(queryUpdateAssociadoEmpresa);

            formCpfAssociado.Value = "";
            formNascimentoAssociado.Value = "";
            formNomeAssociado.Value = "";
            EmpresaAssociadoVinculado.SelectedIndex = -1;
            formNascimentoAssociado.Disabled = false;
            formCpfAssociado.Disabled = false;

            this.getAllAssociadoEmpresa();
            errMessage.Text = "Updated !";
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            string querySetEmpresa = "delete from AssociadosEmpresas where idEmpresas = '{0}' and IdAssociados = '{1}'";
            querySetEmpresa = string.Format(querySetEmpresa, Relacionamento.SelectedRow.Cells[2].Text.ToString(), Relacionamento.SelectedRow.Cells[6].Text.ToString());
            Conx.setData(querySetEmpresa);

            formCpfAssociado.Value = "";
            formNascimentoAssociado.Value = "";
            formNomeAssociado.Value = "";
            EmpresaAssociadoVinculado.SelectedIndex = -1;
            formNascimentoAssociado.Disabled = false;
            formCpfAssociado.Disabled = false;

            this.getAllAssociadoEmpresa();
            errMessage.Text = "Deleted !";
        }
    }
}