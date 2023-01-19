using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }
        public LabelEntity AddLabel(long NoteID, long userId, string labelName)
        {
            try
            {
                return iLabelRL.AddLabel(NoteID, userId, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long LabelID)
        {
            try
            {
                return iLabelRL.DeleteLabel(LabelID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity EditLabel(long NoteID, long LabelID, string labelName)
        {
            try
            {
                return iLabelRL.EditLabel(NoteID, LabelID, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> RetrieveLabel(long LabelID)
        {
            try
            {
                return iLabelRL.RetrieveLabel(LabelID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> RetrieveLabelThroughNoteID(long NoteID)
        {
            try
            {
                return iLabelRL.RetrieveLabelThroughNoteID(NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
