using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        #region Cliente

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
        {
            new System.Data.SqlClient.SqlParameter("@NOME", cliente.Nome),
            new System.Data.SqlClient.SqlParameter("@SOBRENOME", cliente.Sobrenome),
            new System.Data.SqlClient.SqlParameter("@NACIONALIDADE", cliente.Nacionalidade),
            new System.Data.SqlClient.SqlParameter("@CEP", cliente.CEP),
            new System.Data.SqlClient.SqlParameter("@ESTADO", cliente.Estado),
            new System.Data.SqlClient.SqlParameter("@CIDADE", cliente.Cidade),
            new System.Data.SqlClient.SqlParameter("@LOGRADOURO", cliente.Logradouro),
            new System.Data.SqlClient.SqlParameter("@EMAIL", cliente.Email),
            new System.Data.SqlClient.SqlParameter("@TELEFONE", cliente.Telefone),
            new System.Data.SqlClient.SqlParameter("@CPF", cliente.Cpf)
        };

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consulta cliente pelo Id e retorna todos se o Id for zero
        /// </summary>
        /// <param name="Id">Id do cliente</param>
        internal Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        /// <summary>
        /// Consulta que verifica se um cpf já está cadastrado como cliente
        /// </summary>
        /// <param name="CPF">CPF do cliente</param>

        internal bool VerificarExistencia(string CPF, long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("CPF", CPF),
                new System.Data.SqlClient.SqlParameter("ID", Id)
            };

            DataSet ds = base.Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome),
                new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome),
                new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP),
                new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado),
                new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade),
                new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro),
                new System.Data.SqlClient.SqlParameter("Email", cliente.Email),
                new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone),
                new System.Data.SqlClient.SqlParameter("ID", cliente.Id),
                new System.Data.SqlClient.SqlParameter("CPF", cliente.Cpf)

            };

            base.Executar("FI_SP_AltCliente", parametros);
        }

        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<Cliente> Converter(DataSet ds)
        {
            List<Cliente> lista = new List<Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Cliente cli = new Cliente
                    {
                        Id = row.Field<long>("Id"),
                        CEP = row.Field<string>("CEP"),
                        Cidade = row.Field<string>("Cidade"),
                        Email = row.Field<string>("Email"),
                        Estado = row.Field<string>("Estado"),
                        Logradouro = row.Field<string>("Logradouro"),
                        Nacionalidade = row.Field<string>("Nacionalidade"),
                        Nome = row.Field<string>("Nome"),
                        Sobrenome = row.Field<string>("Sobrenome"),
                        Telefone = row.Field<string>("Telefone"),
                        Cpf = row.Field<string>("Cpf")
                    };
                    lista.Add(cli);
                }
            }

            return lista;
        }

        #endregion

        #region Beneficiarios

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long IncluirBeneficiario(Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
        {
            new System.Data.SqlClient.SqlParameter("@NOME", beneficiario.Nome),
            new System.Data.SqlClient.SqlParameter("@CPF", beneficiario.Cpf),
            new System.Data.SqlClient.SqlParameter("@IDCLIENTE", beneficiario.IdCliente)
        };

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        internal bool VerificarExistenciaBeneficiario(string CPF, long Id, long IdCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("CPF", CPF),
                new System.Data.SqlClient.SqlParameter("ID", Id),
                new System.Data.SqlClient.SqlParameter("IDCLIENTE", IdCliente)
            };

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal Beneficiario ConsultarBeneficiario(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("ID", Id)
            };

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var beneficiario = ConverterBeneficiario(ds);

            return beneficiario;
        }

        internal List<Beneficiario> ListarBeneficiarios(long IdCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("IdCliente", IdCliente)
            };

            DataSet ds = base.Consultar("FI_SP_ListBeneficiariosPorCliente", parametros);
            List<Beneficiario> beneficiarios = new List<Beneficiario>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataSet singleRowDataSet = ds.Clone();
                    singleRowDataSet.Tables[0].ImportRow(row);
                    Beneficiario beneficiario = ConverterBeneficiario(singleRowDataSet);
                    if (beneficiario != null)
                    {
                        beneficiarios.Add(beneficiario);
                    }
                }
            }

            return beneficiarios;
        }


        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de cliente</param>
        internal void AlterarBeneficiario(Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id),
                new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome),
                new System.Data.SqlClient.SqlParameter("Cpf", beneficiario.Cpf),
                new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IdCliente)
            };

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }

        /// <summary>
        /// Excluir Beneficiário
        /// </summary>
        /// <param name="Id">Id do beneficiário</param>
        internal void ExcluirBeneficiario(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", Id)
            };

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private Beneficiario ConverterBeneficiario(DataSet ds)
        {
            Beneficiario beneficiario = null;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                beneficiario = new Beneficiario
                {
                    Id = row.Field<long>("Id"),
                    Nome = row.Field<string>("Nome"),
                    Cpf = row.Field<string>("Cpf"),
                    IdCliente = row.Field<long>("IdCliente"),
                };
            }

            return beneficiario;
        }

        #endregion
    }
}
