using System;
using System.Collections.Generic;
using System.Text;

namespace EventusParser
{
    public class EventusRow
    {
        private double _id;
        private DateTime _created;
        private double _uidCreated;
        private double? _uidUpdated;
        private DateTime? _updated;
        private double _version;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string _description;
        private string _name;
        private double _status;
        private TimeSpan _timeFrom;
        private TimeSpan _timeTo;
        private double _type;
        private string _externalCode;

        public double Id { get => _id; set => _id = value; }
        public DateTime Created { get => _created; set => _created = value; }
        public double UidCreated { get => _uidCreated; set => _uidCreated = value; }
        public double? UidUpdated { get => _uidUpdated; set => _uidUpdated = value; }
        public DateTime? Updated { get => _updated; set => _updated = value; }
        public double Version { get => _version; set => _version = value; }
        public DateTime DateFrom { get => _dateFrom; set => _dateFrom = value; }
        public DateTime DateTo { get => _dateTo; set => _dateTo = value; }
        public string Description { get => _description; set => _description = value; }
        public string Name { get => _name; set => _name = value; }
        public double Status { get => _status; set => _status = value; }
        public TimeSpan TimeFrom { get => _timeFrom; set => _timeFrom = value; }
        public TimeSpan TimeTo { get => _timeTo; set => _timeTo = value; }
        public double Type { get => _type; set => _type = value; }
        public string ExternalCode { get => _externalCode; set => _externalCode = value; }

        public override string ToString()
        {
            return $"\nID: {_id} Created: {_created} UIDCreated: {_uidCreated} UIDUpdated: {_uidUpdated} Updated: {_updated} Version: {_version} DateFrom: {_dateFrom} DataTo: {_dateTo} Description: {_description} Name: {_name} Status: {_status} TimeFrom: {_timeFrom} TimeTo: {_timeTo} Type: {_type} ExternalCode: {_externalCode}\n";
        }
    }
}
