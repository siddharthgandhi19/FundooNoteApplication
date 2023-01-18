﻿using CommonLayer.ModelClass;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace RepoLayer.Service
{
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        IConfiguration configuration; //image configuration
        public NoteRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.configuration = config;
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

        public int ArchieveNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.NoteID == noteIDModel.NoteID && x.UserId == UserId).FirstOrDefault();
                if (result != null)
                {
                    result.Archive = !result.Archive;
                    result.CreateNoteTime = DateTime.Now;
                    fundooContext.SaveChanges();
                    return Convert.ToInt16(true) + Convert.ToInt16(!result.Archive);
                }
                else
                {
                    return Convert.ToInt16(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int PinnedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.NoteID == noteIDModel.NoteID && x.UserId == UserId).FirstOrDefault();
                if (result != null)
                {
                    result.Pin = !result.Pin;
                    result.CreateNoteTime = DateTime.Now;
                    fundooContext.SaveChanges();
                    return Convert.ToInt16(true) + Convert.ToInt16(!result.Pin);
                }
                else
                {
                    return Convert.ToInt16(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int TrashedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.NoteID == noteIDModel.NoteID && x.UserId == UserId).FirstOrDefault();
                if (result != null)
                {
                    result.Trash = !result.Trash;
                    result.CreateNoteTime = DateTime.Now;
                    fundooContext.SaveChanges();
                    return Convert.ToInt16(true) + Convert.ToInt16(!result.Pin);
                }
                else
                {
                    return Convert.ToInt16(false);
                }
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
                var result = fundooContext.NoteTable.Where(x => x.UserId == UserId && x.Trash == true);
                if (result != null)
                {
                    foreach (var data in result)
                    {
                        fundooContext.NoteTable.Remove(data);
                    }
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity Color(long userId, long NoteID, string backgroundColor, NoteColor noteColor)
        {
            try
            {
                var noteEntity = fundooContext.NoteTable.FirstOrDefault(e => e.NoteID == NoteID);
                noteEntity.Color = backgroundColor;
                fundooContext.NoteTable.Update(noteEntity);
                fundooContext.SaveChanges();
                return noteEntity;
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
                var result = fundooContext.NoteTable.FirstOrDefault(e => e.NoteID == noteId && e.UserId == userId);
                if (result != null)
                {
                    Account accounnt = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                       this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(accounnt);
                    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();

                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}