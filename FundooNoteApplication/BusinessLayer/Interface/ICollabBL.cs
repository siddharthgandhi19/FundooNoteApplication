using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollaborationEntity CreateCollab(long NoteId, string email);
    }
}
