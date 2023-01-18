using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
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

        public NoteEntity RemoveNotes(NoteIDModel noteIdModel, long noteId)
        {
            try
            {
                return iNoteRL.RemoveNotes(noteIdModel, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity UpdateNotes(NoteRegistration noteRegistration, long UserId, long NoteID)
        {
            try
            {
                return iNoteRL.UpdateNotes(noteRegistration, UserId, NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
