using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
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
    }
}
