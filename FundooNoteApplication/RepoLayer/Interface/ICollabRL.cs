﻿using CommonLayer.ModelClass;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRL
    {
        public CollaborationEntity CreateCollab(long NoteId, CollabEmail email);
        public IEnumerable<CollaborationEntity> RetrieveCollab(long CollabId);
        public IEnumerable<CollaborationEntity> RetrieveCollabThroughNotes(long NoteID);
        public IEnumerable<CollaborationEntity> RetrieveCollabThroughUsers(long UserId);
        public bool DeleteCollab(CollabIDModel collabIDModel, long noteId);
    }
}
