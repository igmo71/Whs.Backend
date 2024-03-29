﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whs.Tests.Common;
using Whs.Application.Services.Notes.Commands.CreateNote;
using Whs.Application.Services.Notes.Commands.DeleteNote;
using Whs.Application.Common.Exceptions;

namespace Whs.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            //Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = WhsContextFactory.NoteForDelete,
                UserId = WhsContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Notes.SingleOrDefault(note => note.Id == WhsContextFactory.NoteForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new DeleteNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = WhsContextFactory.UserAId
            }, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var createHandler = new CreateNoteCommandHandler(Context);
            var deleteHandler = new DeleteNoteCommandHandler(Context);
            var noteId = await createHandler.Handle(new CreateNoteCommand
            {
                Title = "Note Title",
                UserId = WhsContextFactory.UserAId
            }, CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await deleteHandler.Handle(new DeleteNoteCommand
            {
                Id = noteId,
                UserId = WhsContextFactory.UserBId
            }, CancellationToken.None));
        }
    }
}
