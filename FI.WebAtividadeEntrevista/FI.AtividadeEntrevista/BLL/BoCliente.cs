using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        #region Cliente

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long Id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF, Id);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        #endregion

        #region Beneficiario

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(Beneficiario beneficiario)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.IncluirBeneficiario(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void AlterarBeneficiario(Beneficiario beneficiario)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.AlterarBeneficiario(beneficiario);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void ExcluirBeneficiario(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.ExcluirBeneficiario(id);
        }

        /// <summary>
        /// Consulta um beneficiário
        /// </summary>
        public Beneficiario ConsultarBeneficiario(long IdCliente)
        {            
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.ConsultarBeneficiario(IdCliente);
        }

        /// <summary>
        /// Lista os beneficiários
        /// </summary>
        public List<Beneficiario> ListarBeneficiarios(long IdCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.ListarBeneficiarios(IdCliente);
        }

        public bool VerificarExistenciaBeneficiario(string CPF, long Id, long IdCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistenciaBeneficiario(CPF, Id, IdCliente);
        }

        #endregion
    }
}
