using MediatR;
using MediatRSample.Application.Commands;
using MediatRSample.Application.Models;
using MediatRSample.Application.Notifications;
using MediatRSample.Repositories;

namespace MediatRSample.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;

        public DeletePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);

                await _mediator.Publish(new DeletedPersonNotification { Id = request.Id, IsEfetivado = true });

                return await Task.FromResult("Pessoa excluída com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new DeletedPersonNotification { Id = request.Id, IsEfetivado = false });
                await _mediator.Publish(new ErrorNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}
