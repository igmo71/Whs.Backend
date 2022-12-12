using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whs.Application.Common.Exceptions;
using Whs.Application.Interfaces;

namespace Whs.Application.Services.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly IWhsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(IWhsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(NoteDetailsVm), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
