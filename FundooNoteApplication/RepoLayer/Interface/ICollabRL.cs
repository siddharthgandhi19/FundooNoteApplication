using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRL
    {
        public CollaborationEntity CreateCollab(long NoteId, string email);
        public IEnumerable<CollaborationEntity> RetrieveCollab(long noteId, long userId);
        public bool DeleteCollab(long collabId, long noteId);
    }
}
