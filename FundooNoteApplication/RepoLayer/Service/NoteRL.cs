using CommonLayer.ModelClass;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
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
    }
}