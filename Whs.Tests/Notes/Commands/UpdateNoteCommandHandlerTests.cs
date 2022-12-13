using Microsoft.EntityFrameworkCore;
using Whs.Application.Common.Exceptions;
using Whs.Application.Services.Notes.Commands.UpdateNote;
using Whs.Tests.Common;

namespace Whs.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            var updatedTitle = "New Title";

            // Act
            await handler.Handle(new UpdateNoteCommand
            {
                Id = WhsContextFactory.NoteForUpdate,
                UserId = WhsContextFactory.UserBId,
                Title = updatedTitle
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note => note.Id == WhsContextFactory.NoteForUpdate && note.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new UpdateNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = WhsContextFactory.UserAId
            }, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new UpdateNoteCommand
            {
                Id = WhsContextFactory.NoteForUpdate,
                UserId = WhsContextFactory.UserAId
            }, CancellationToken.None));
        }
    }
}
