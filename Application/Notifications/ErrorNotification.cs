using MediatR;

namespace MediatRSample.Application.Notifications
{
    public class ErrorNotification:INotification
    {
        public string Excecao { get; set; }
        public string PilhaErro { get; set; }
    }
}
