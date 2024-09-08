using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        #region Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            var cliente = model;

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistencia(cliente.Cpf, cliente.Id))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado para um cliente.");
            }

            if (!CpfValidacao.Validar(cliente.Cpf))
            {
                Response.StatusCode = 400;
                return Json("Este CPF é inválido.");
            }

            model.Id = bo.Incluir(new Cliente()
            {
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
                Cpf = model.Cpf
            });

            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel cliente)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistencia(cliente.Cpf, cliente.Id))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado para outro cliente.");
            }

            if (!CpfValidacao.Validar(cliente.Cpf))
            {
                Response.StatusCode = 400;
                return Json("Este CPF é inválido.");
            }

            bo.Alterar(new Cliente()
            {
                Id = cliente.Id,
                CEP = cliente.CEP,
                Cidade = cliente.Cidade,
                Email = cliente.Email,
                Estado = cliente.Estado,
                Logradouro = cliente.Logradouro,
                Nacionalidade = cliente.Nacionalidade,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Telefone = cliente.Telefone,
                Cpf = cliente.Cpf
            });

            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Cpf = cliente.Cpf
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region Beneficiario

        [HttpPost]
        public JsonResult IncluirBeneficiario(BeneficiarioModel beneficiario)
        {
            BoCliente bo = new BoCliente();

            if (!ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistenciaBeneficiario(beneficiario.Cpf, beneficiario.Id, beneficiario.IdCliente))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado como beneficiário deste cliente.");
            }

            if (!CpfValidacao.Validar(beneficiario.Cpf))
            {
                Response.StatusCode = 400;
                return Json("Este CPF é inválido.");
            }

            beneficiario.Id = bo.Incluir(new Beneficiario()
            {
                Nome = beneficiario.Nome,
                Cpf = beneficiario.Cpf,
                IdCliente = beneficiario.IdCliente
            });

            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult AlterarBeneficiario(BeneficiarioModel beneficiario)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            if (bo.VerificarExistenciaBeneficiario(beneficiario.Cpf, beneficiario.Id, beneficiario.IdCliente))
            {
                Response.StatusCode = 400;
                return Json("Este CPF já está cadastrado como beneficiário deste cliente.");
            }

            if (!CpfValidacao.Validar(beneficiario.Cpf))
            {
                Response.StatusCode = 400;
                return Json("Este CPF é inválido.");
            }

            bo.AlterarBeneficiario(new Beneficiario()
            {
                Id = beneficiario.Id,
                Nome = beneficiario.Nome,
                Cpf = beneficiario.Cpf,
                IdCliente = beneficiario.IdCliente
            });

            return Json("Cadastro alterado com sucesso");
        }

        [HttpPost]
        public JsonResult ExcluirBeneficiario(long Id)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            var beneficiario = new BoCliente().ConsultarBeneficiario(Id);
            if ( beneficiario == null)
            {
                Response.StatusCode = 400;
                return Json("Beneficiário não encontrado");
            }

            bo.ExcluirBeneficiario(Id);

            return Json("Exclusão realizada com sucesso");
        }

        public ActionResult ListarBeneficiarios(long IdCliente = 1)
        {
            //try
            //{
            //    var beneficiarios = new BoCliente().ListarBeneficiarios(IdCliente);

            //    //Return result to jTable
            //    return Json(new { Result = "OK", Records = beneficiarios});
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
            return RedirectToAction("Index");
        }

        #endregion
    }
}