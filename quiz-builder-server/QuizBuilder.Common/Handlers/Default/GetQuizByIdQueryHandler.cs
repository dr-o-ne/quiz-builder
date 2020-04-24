using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default
{
    public class GetQuizByIdDto
    {
        public long Id { get; }
        public string Name { get; }
        public DateTime CreatedDate { get; }
        public DateTime UpdatedDate { get; }

        public GetQuizByIdDto(long id, string name, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Name = name;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }

    public class GetQuizByIdQuery : IQuery<GetQuizByIdDto>
    {
        public long Id { get; set; }
    }

    public class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, GetQuizByIdDto>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;

        public GetQuizByIdQueryHandler( IGenericRepository<Quiz> quizRepository )
        {
            _quizRepository = quizRepository;
        }

        public async Task<GetQuizByIdDto> HandleAsync(GetQuizByIdQuery query)
        {
            var entity = await _quizRepository.GetByIdAsync( query.Id );

            return entity is null
                ? default
                : new GetQuizByIdDto(
                    id: entity.Id,
                    name: entity.Name,
                    createdDate: entity.CreatedDate,
                    updatedDate: entity.UpdatedDate);
        }
    }
}
