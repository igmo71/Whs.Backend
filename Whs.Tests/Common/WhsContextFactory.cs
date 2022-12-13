using Microsoft.EntityFrameworkCore;
using Whs.Domain;
using Whs.Persistence;

namespace Whs.Tests.Common
{
    public class WhsContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteForDelete = Guid.NewGuid();
        public static Guid NoteForUpdate = Guid.NewGuid();

        public static WhsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<WhsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new WhsDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("B67C4C7E-575D-468C-9743-C5428E14F040"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("F30DD6A2-D613-42CE-A84E-F566719A32C2"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = NoteForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = NoteForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(WhsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
