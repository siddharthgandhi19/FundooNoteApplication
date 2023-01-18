using BusinessLayer.Interface;
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

        public CollaborationEntity CreateCollab(long NoteId, string email)
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

        public bool DeleteCollab(long collabId, long noteId)
        {
            try
            {
                return iCollabRL.DeleteCollab(collabId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollaborationEntity> RetrieveCollab(long noteId, long userId)
        {
            try
            {
                return iCollabRL.RetrieveCollab(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
