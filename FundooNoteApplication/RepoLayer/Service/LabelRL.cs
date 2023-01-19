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

        public IEnumerable<LabelEntity> RetrieveLabel(long LabelID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == LabelID);
                if (result != null)
                {
                    return result;
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

        public IEnumerable<LabelEntity> RetrieveLabelThroughNoteID(long NoteID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.NoteID == NoteID);
                if (result != null)
                {
                    return result;
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

        public bool DeleteLabel(long LabelID)
        {
            try
            {
                var result = fundooContext.LabelTable.FirstOrDefault(x => x.LabelID == LabelID);
                fundooContext.LabelTable.Remove(result);
                fundooContext.SaveChanges();
                return true;
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

                var labelEntity = fundooContext.LabelTable.FirstOrDefault(x => x.LabelID == LabelID);
                if (labelEntity != null)
                {
                    labelEntity.LabelName = labelName;

                    fundooContext.SaveChanges();
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}