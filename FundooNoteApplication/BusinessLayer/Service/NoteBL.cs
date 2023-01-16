using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{

    public class NoteBL : INoteBL
    {
        INoteRL iNoteRL;
        public NoteBL(INoteRL iNoteRL)
        {
            this.iNoteRL = iNoteRL;
        }
        public NoteEntity CreateNote(NoteRegistration noteRegistration, long UserId)
        {
            try
            {
                return iNoteRL.CreateNote(noteRegistration, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool MoveToTrash(NoteTrashed deleteNote, long UserId)
        {
            try
            {
                return iNoteRL.MoveToTrash(deleteNote, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
