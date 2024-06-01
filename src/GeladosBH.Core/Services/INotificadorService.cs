using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Core.Services
{
    public interface INotificadorService
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
