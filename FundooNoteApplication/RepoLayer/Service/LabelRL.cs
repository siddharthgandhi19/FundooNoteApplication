using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRL : ILabelRL
    {

        FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity AddLabel(long NoteID, long userId, string labelName)
        {
            try
            {
                var noteResult = fundooContext.NoteTable.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userResult = fundooContext.UserTable.Where(x => x.UserId == userId).FirstOrDefault();


                if (noteResult != null && userResult != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelName;
                    labelEntity.NoteID = noteResult.NoteID;
                    labelEntity.UserId = userResult.UserId;

                    fundooContext.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
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

    }
}