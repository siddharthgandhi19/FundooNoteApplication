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
    }
}
