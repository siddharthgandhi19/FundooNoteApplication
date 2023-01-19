﻿using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long NoteID, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long LabelID);
        public IEnumerable<LabelEntity> RetrieveLabelThroughNoteID(long NoteID);
    }
}
