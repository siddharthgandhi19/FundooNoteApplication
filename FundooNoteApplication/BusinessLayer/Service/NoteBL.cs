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

        public bool MoveToArchive(NoteTrashed deleteNote, long UserId)
        {
            try
            {
                return iNoteRL.MoveToArchive(deleteNote, UserId);
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

        public bool PinnedNotes(NoteTrashed deleteNote, long UserId)
        {
            try
            {
                return iNoteRL.PinnedNotes(deleteNote, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity RemoveNotes(NoteRemove noteRemove, long noteId)
        {
            try
            {
                return iNoteRL.RemoveNotes(noteRemove, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NoteEntity> RetrieveAllNotes(long userId)
        {
            try
            {
                return iNoteRL.RetrieveAllNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                return iNoteRL.RetrieveNotes(userId, noteId);
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
