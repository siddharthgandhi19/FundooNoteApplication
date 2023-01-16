using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity CreateNote(NoteRegistration noteRegistration, long UserId);
    }
}
