using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRL
    {
        public CollaborationEntity CreateCollab(long NoteId, string email);
    }
}
