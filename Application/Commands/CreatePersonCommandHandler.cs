using MediatR;
using MediatRSample.Application.Models;
using MediatRSample.Application.Notifications;
using MediatRSample.Repositories;

namespace MediatRSample.Application.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Person> _repository;
        public CreatePersonCommandHandler(IMediator mediator, IRepository<Person> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        public async Task<string> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };

            try
            {
                await _repository.Add(person);

                await _mediator.Publish(new CreatedPersonNotification { Id = person.Id, Nome = person.Nome, Idade = person.Idade, Sexo = person.Sexo });

                return await Task.FromResult("Pessoa criada com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new CreatedPersonNotification { Id = person.Id, Nome = person.Nome, Idade = person.Idade, Sexo = person.Sexo });
                await _mediator.Publish(new ErrorNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
