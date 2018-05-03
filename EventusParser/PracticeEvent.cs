using System;
using System.Collections.Generic;
using System.Text;

namespace EventusParser
{
    public class PracticeEvent : Event
    {
        private Subject _subject;
        private List<Group> _groups;

        public Subject Subject { get => _subject; set => _subject = value; }
        public List<Group> Groups { get => _groups; set => _groups = value; }
    }
}
