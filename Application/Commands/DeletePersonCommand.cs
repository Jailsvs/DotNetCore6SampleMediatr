using MediatR;

namespace MediatRSample.Application.Commands
{
    public class DeletePersonCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
