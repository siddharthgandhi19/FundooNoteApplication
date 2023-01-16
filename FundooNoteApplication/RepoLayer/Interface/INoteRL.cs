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
        public bool MoveToTrash(NoteTrashed deleteNote, long UserId);
        public IEnumerable<NoteEntity> RetrieveNotes(long userId, long noteId);
    }
}
