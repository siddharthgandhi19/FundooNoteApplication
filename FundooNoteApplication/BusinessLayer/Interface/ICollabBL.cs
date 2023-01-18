using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollaborationEntity CreateCollab(long NoteId, CollabEmail email);
        public IEnumerable<CollaborationEntity> RetrieveCollab(long noteId, long userId);
        public bool DeleteCollab(CollabIDModel collabIDModel, long noteId);
    }
}
