﻿using BusinessLayer.Interface;
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
