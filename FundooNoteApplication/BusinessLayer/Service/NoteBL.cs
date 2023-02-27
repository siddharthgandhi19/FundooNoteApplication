using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Http;
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

        public bool ArchieveNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                return iNoteRL.ArchieveNotes(noteIDModel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool NoteColor(NoteColor noteColor, long UserId)
        {
            try
            {
                return iNoteRL.NoteColor(noteColor, UserId);
            }
            catch (Exception)
            {
                throw;
            }
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

        public bool PinnedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                return iNoteRL.PinnedNotes(noteIDModel, UserId);
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

        public bool TrashedAllNotes(long UserId)
        {
            try
            {
                return iNoteRL.TrashedAllNotes(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TrashedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                return iNoteRL.TrashedNotes(noteIDModel, UserId);
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

        public string UploadImage(IFormFile image, long noteId, long userId)
        {
            try
            {
                return iNoteRL.UploadImage(image, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
