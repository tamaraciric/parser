using System;
using System.Collections.Generic;
using System.Text;

namespace EventusParser
{
    public class OtherEvent : Event
    {
        private Subject _subject;

        public Subject Subject { get => _subject; set => _subject = value; }
    }
}
