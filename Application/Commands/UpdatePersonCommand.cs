using MediatR;

namespace MediatRSample.Application.Commands
{
    public class UpdatePersonCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public char Sexo { get; set; }
    }
}
