using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity CreateNote(NoteRegistration noteRegistration, long UserId);
        public bool MoveToTrash(NoteTrashed deleteNote, long UserId);
    }
}
