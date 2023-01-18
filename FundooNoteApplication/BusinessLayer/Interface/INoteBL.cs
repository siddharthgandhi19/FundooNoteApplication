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
        public NoteEntity RemoveNotes(NoteIDModel noteIdModel, long noteId);
        public NoteEntity UpdateNotes(NoteRegistration noteRegistration, long UserId, long NoteID);


    }
}
