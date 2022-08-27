using MediatR;

namespace MediatRSample.Application.Notifications
{
    public class DeletedPersonNotification: INotification
    {
        public int Id { get; set; }
        public bool IsEfetivado { get; set; }
    }
}
