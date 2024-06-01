using GeladosBH.Business.Intefaces;
using GeladosBH.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificadorService _notificadorService;
        public readonly IUser AppUser;
        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        public MainController(INotificadorService notificadorService, IUser appUser)
        {
            _notificadorService = notificadorService;
            AppUser = appUser;

            if(AppUser.IsAuthenticated())
            {
                UsuarioId = AppUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    sucess = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                sucess = false,
                errors = _notificadorService.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected bool OperacaoValida()
        {
            return !_notificadorService.TemNotificacao();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificadorService.Handle(new Notificacao(mensagem));
        }

    }
}
