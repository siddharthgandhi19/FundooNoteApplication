using CommonLayer.ModelClass;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
        }
        public NoteEntity CreateNote(NoteRegistration noteRegistration, long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteRegistration.Title;
                noteEntity.Description = noteRegistration.Description;
                noteEntity.Reminder = noteRegistration.Reminder;
                noteEntity.Color = noteRegistration.Color;
                noteEntity.Image = noteRegistration.Image;
                noteEntity.Archive = noteRegistration.Archive;
                noteEntity.Pin = noteRegistration.Pin;
                noteEntity.Trash = noteRegistration.Trash;
                noteEntity.CreateNoteTime = noteRegistration.CreateNoteTime;
                noteEntity.UserId = UserId;
                fundooContext.NoteTable.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity RemoveNotes(NoteIDModel noteIdModel, long noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.NoteID = noteIdModel.NoteID;
                fundooContext.NoteTable.Remove(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NoteEntity UpdateNotes(NoteRegistration noteRegistration, long UserId, long NoteID)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteRegistration.Title;
                    result.Description = noteRegistration.Description;
                    result.Reminder = noteRegistration.Reminder;
                    result.Color = noteRegistration.Color;
                    result.Image = noteRegistration.Image;
                    result.Archive = noteRegistration.Archive;
                    result.Pin = noteRegistration.Pin;
                    result.Trash = noteRegistration.Trash;
                    result.CreateNoteTime = noteRegistration.CreateNoteTime;
                    result.UserId = UserId;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<NoteEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.NoteID == noteId);
                return result;
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
                var result = fundooContext.NoteTable.Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}