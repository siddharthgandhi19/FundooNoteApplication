using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long NoteID, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long LabelID);
        public IEnumerable<LabelEntity> RetrieveLabelThroughNoteID(long NoteID);
        public bool DeleteLabel(long LabelID);       
    }
}
