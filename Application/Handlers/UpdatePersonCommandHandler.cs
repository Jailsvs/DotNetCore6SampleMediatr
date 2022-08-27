using MediatR;
using MediatRSample.Application.Commands;
using MediatRSample.Application.Models;
using MediatRSample.Application.Notifications;
using MediatRSample.Repositories;

namespace MediatRSample.Application.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;

        public UpdatePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person { Id = request.Id, Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };

            try
            {
                await _repository.Edit(person);

                await _mediator.Publish(new UpdatedPersonNotification { Id = person.Id, Nome = person.Nome, Idade = person.Idade, Sexo = person.Sexo, IsEfetivado = true });

                return await Task.FromResult("Pessoa alterada com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new UpdatedPersonNotification { Id = person.Id, Nome = person.Nome, Idade = person.Idade, Sexo = person.Sexo, IsEfetivado = false });
                await _mediator.Publish(new ErrorNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da alteração");
            }
        }
    }
}
