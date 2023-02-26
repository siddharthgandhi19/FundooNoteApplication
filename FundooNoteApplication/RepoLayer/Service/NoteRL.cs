using CommonLayer.ModelClass;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteRegistration"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteIdModel"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteIDModel"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool ArchieveNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.UserId == UserId && x.NoteID == noteIDModel.NoteID).FirstOrDefault();
                if (result != null)
                {
                    if (!result.Archive == true)
                    {
                        result.Archive = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        result.Archive = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
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

        public bool PinnedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.UserId == UserId && x.NoteID == noteIDModel.NoteID).FirstOrDefault();
                if (result != null)
                {
                    if (!result.Pin == true)
                    {
                        result.Pin = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        result.Pin = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteIDModel"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool TrashedNotes(NoteIDModel noteIDModel, long UserId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.UserId == UserId && x.NoteID == noteIDModel.NoteID).FirstOrDefault();
                if (!result.Trash == true)
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="NoteID"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="noteColor"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="noteId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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