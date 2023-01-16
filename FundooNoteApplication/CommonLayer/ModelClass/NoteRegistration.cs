using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ModelClass
{
    public class NoteRegistration
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime CreateNoteTime { get; set; }
    }
}
