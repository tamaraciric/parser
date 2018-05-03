using System;
using System.Collections.Generic;
using System.Text;

namespace EventusParser
{
    public class Event
    {
        private double _id;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private TimeSpan _timeFrom;
        private TimeSpan _timeTo;
        
        public double Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public TimeSpan TimeTo
        {
            get { return _timeTo; }
            set { _timeTo = value; }
        }
        
        public TimeSpan TimeFrom
        {
            get { return _timeFrom; }
            set { _timeFrom = value; }
        }
        
        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }
        
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
    }
}
