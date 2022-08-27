using MediatR;

namespace MediatRSample.Application.Notifications
{
    public class UpdatedPersonNotification: INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public char Sexo { get; set; }
        public bool IsEfetivado { get; set; }
    }
}
