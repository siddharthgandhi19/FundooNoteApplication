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
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL iCollabRL;
        public CollabBL(ICollabRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }

        public CollaborationEntity CreateCollab(long NoteId, CollabEmail email)
        {
            try
            {
                return iCollabRL.CreateCollab(NoteId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCollab(CollabIDModel collabIDModel, long noteId)
        {
            try
            {
                return iCollabRL.DeleteCollab(collabIDModel, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollaborationEntity> RetrieveCollab(long CollabId)
        {
            try
            {
                return iCollabRL.RetrieveCollab(CollabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollaborationEntity> RetrieveCollabThroughNotes(long NoteID)
        {
            try
            {
                return iCollabRL.RetrieveCollabThroughNotes(NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollaborationEntity> RetrieveCollabThroughUsers(long UserId)
        {
            try
            {
                return iCollabRL.RetrieveCollabThroughUsers(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
