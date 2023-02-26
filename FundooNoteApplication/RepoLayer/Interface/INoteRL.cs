using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity CreateNote(NoteRegistration noteRegistration, long UserId);
        public NoteEntity RemoveNotes(NoteIDModel noteIdModel, long noteId);
        public NoteEntity UpdateNotes(NoteRegistration noteRegistration, long UserId, long NoteID);
        public IEnumerable<NoteEntity> RetrieveNotes(long userId, long noteId);
        public IEnumerable<NoteEntity> RetrieveAllNotes(long userId);
        public bool ArchieveNotes(NoteIDModel noteIDModel, long UserId);
        public bool PinnedNotes(NoteIDModel noteIDModel, long UserId);
        public bool TrashedNotes(NoteIDModel noteIDModel, long UserId);
        public bool TrashedAllNotes(long UserId);
        public NoteEntity Color(long userId, long NoteID, string backgroundColor, NoteColor noteColor);
        public string UploadImage(IFormFile image, long noteId, long userId);
    }
}
