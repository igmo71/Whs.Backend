using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whs.Application.Services.Notes.Commands.CreateNote;
using Whs.Tests.Common;

namespace Whs.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "Note Name";
            var noteDetails = "Note Details";

            // Act
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = WhsContextFactory.UserAId
                }, CancellationToken.None);

            // Assert
            Assert.NotNull(Context.Notes.SingleOrDefault(note => note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
        }
    }
}
