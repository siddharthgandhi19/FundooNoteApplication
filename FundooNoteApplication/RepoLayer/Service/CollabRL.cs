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
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;

        public CollabRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        public CollaborationEntity CreateCollab(long NoteId, string email)
        {
            try
            {

                var noteResult = fundooContext.NoteTable.Where(x => x.NoteID == NoteId).FirstOrDefault();
                var emailResult = fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();

                if (emailResult != null && noteResult != null)
                {
                    CollaborationEntity collabEntity = new CollaborationEntity();

                    collabEntity.Email = emailResult.Email;
                    collabEntity.NoteID = noteResult.NoteID;
                    collabEntity.UserId = emailResult.UserId;

                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<CollaborationEntity> RetrieveCollab(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(x => x.NoteID == noteId);
                if (result != null)
                {
                    return result;
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

        public bool DeleteCollab(long collabId, long noteId)
        {
            try
            {
                var result = fundooContext.CollabTable.FirstOrDefault(x => x.CollabId == collabId);
                fundooContext.CollabTable.Remove(result);
                fundooContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
