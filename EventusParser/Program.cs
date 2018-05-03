using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EventusParser
{
    public enum EventType
    {
        P,
        V,
        N
    }
    class Program
    {
        private static List<EventusRow> eventusData = new List<EventusRow>();
        private static List<Event> events = new List<Event>();
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            String originalFileName = "..\\eventus.xlsx";
            Console.WriteLine("Unesite putanju");
            string filePath = Console.ReadLine();
            if (File.Exists(filePath))
                originalFileName = filePath;

            readFromExcelFile(originalFileName);
            //showDataSample(20);

            convertRowsIntoEvents(eventusData);
            showEvents(events);

            Console.ReadKey();
        }

        private static void showDataSample(int rowNumberToShow)
        {
            for (int i = 0; i < rowNumberToShow; i++)
            {
                Console.WriteLine(eventusData[i].Name);
                Console.WriteLine("======================================================");
            }
        }

        private static void readFromExcelFile(string originalFileName)
        {
            var file = new FileInfo(originalFileName);
            using (
                var stream = File.Open(originalFileName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                if (file.Extension.Equals(".xls"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                else if (file.Extension.Equals(".xlsx"))
                    reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    throw new Exception("Invalid FileName");

                reader.Read();
                while (reader.Read())
                {
                    EventusRow row = new EventusRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                row.Id = reader.GetDouble(i);
                                break;
                            case 1:
                                row.Created = reader.GetDateTime(i);
                                break;
                            case 2:
                                row.UidCreated = reader.GetDouble(i);
                                break;
                            case 3:
                                row.UidUpdated = reader.GetValue(i) != null ? (double?)reader.GetDouble(i) : null;
                                break;
                            case 4:
                                row.Updated = reader.GetValue(i) != null ? (DateTime?)reader.GetDateTime(i) : null;
                                break;
                            case 5:
                                row.Version = reader.GetDouble(i);
                                break;
                            case 6:
                                row.DateFrom = reader.GetDateTime(i).Date;
                                break;
                            case 7:
                                row.DateTo = reader.GetDateTime(i).Date;
                                break;
                            case 8:
                                row.Description = reader.GetString(i);
                                break;
                            case 9:
                                row.Name = reader.GetString(i);
                                break;
                            case 10:
                                row.Status = reader.GetDouble(i);
                                break;
                            case 11:
                                row.TimeFrom = reader.GetDateTime(i).TimeOfDay;
                                break;
                            case 12:
                                row.TimeTo = reader.GetDateTime(i).TimeOfDay;
                                break;
                            case 13:
                                row.Type = reader.GetDouble(i);
                                break;
                            case 14:
                                row.ExternalCode = reader.GetValue(i) != null ? reader.GetString(i) : null;
                                break;
                            default:
                                continue;
                        }
                    }
                    eventusData.Add(row);
                }
            }
        }

        //*******************************************************************************************************************

        private static void convertRowsIntoEvents(List<EventusRow> eventusData)
        {
            foreach (EventusRow row in eventusData)
            {
                Event e = dataAfterProcessing(row);
                if (e != null)
                    events.Add(e);
            }
        }

        private static Event dataAfterProcessing(EventusRow row)
        {
            EventType eventType;
            int index;
            getEventType(row.Name, out index, out eventType);

            switch (eventType)
            {
                case EventType.P:
                    return new LectureEvent()
                    {
                        Id = row.Id,
                        DateFrom = row.DateFrom,
                        DateTo = row.DateTo,
                        TimeFrom = row.TimeFrom,
                        TimeTo = row.TimeTo,
                        Subject = new Subject()
                        {
                            Name = row.Name.Substring(0, index).Trim()
                        },
                        Groups = getGroupForEvent(row.Name, index)
                    };
                case EventType.V:
                    return new PracticeEvent()
                    {
                        Id = row.Id,
                        DateFrom = row.DateFrom,
                        DateTo = row.DateTo,
                        TimeFrom = row.TimeFrom,
                        TimeTo = row.TimeTo,
                        Subject = new Subject()
                        {
                            Name = row.Name.Substring(0, index).Trim()
                        },
                        Groups = getGroupForEvent(row.Name, index)
                    };
                case EventType.N:
                    return new OtherEvent()
                    {
                        Id = row.Id,
                        DateFrom = row.DateFrom,
                        DateTo = row.DateTo,
                        TimeFrom = row.TimeFrom,
                        TimeTo = row.TimeTo,
                        Subject = new Subject()
                        {
                            Name = row.Name.Trim()
                        }
                    };
            }

            return null;
        }

        private static List<Group> getGroupForEvent(string original, int index)
        {
            List<Group> groupList = new List<Group>();

            string newString = original.Substring(index + 2, original.Length - index - 2).Trim();
            string[] groups = newString.Split(',');

            foreach (var group in groups)
                groupList.Add(new Group() { Name = group.Trim(new char[] { ' ', '-' }) });

            return groupList;
        }

        private static void getEventType(string original, out int index, out EventType eventType)
        {
            var i = original.IndexOf(" P ");
            if (i != -1)
            {
                index = i;
                eventType = EventType.P;
                return;
            }
            i = original.IndexOf(" V ");
            if (i != -1)
            {
                index = i;
                eventType = EventType.V;
                return;
            }
            index = -1;
            eventType = EventType.N;
        }

        private static void showEvents(List<Event> events)
        {
            foreach (var ev in events)
            {
                if (ev is LectureEvent lecture)
                    Console.WriteLine($"Date: {lecture.DateFrom.Date} - {lecture.DateTo.Date}\nTime: {lecture.TimeFrom} - {lecture.TimeTo}\nSubject: {lecture.Subject.Name}\nGroups: {getGroupsAsString(lecture)}");

                if (ev is PracticeEvent practice)
                    Console.WriteLine($"Date: {practice.DateFrom.Date} - {practice.DateTo.Date}\nTime: {practice.TimeFrom} - {practice.TimeTo}\nSubject: {practice.Subject.Name}\nGroups: {getGroupsAsString(practice)}");

                if (ev is OtherEvent other)
                    Console.WriteLine($"Date: {other.DateFrom.Date} - {other.DateTo.Date}\nTime: {other.TimeFrom} - {other.TimeTo}\nSubject: {other.Subject.Name}\n");
            }

            Console.WriteLine($"Total events:\t{events.Count}");
            Console.WriteLine($"Total lecture events:\t{events.FindAll(i => i.GetType() == typeof(LectureEvent)).Count}");
            Console.WriteLine($"Total practice events:\t{events.FindAll(i => i.GetType() == typeof(PracticeEvent)).Count}");
            Console.WriteLine($"Total unknown events type:\t{events.FindAll(i => i.GetType() == typeof(OtherEvent)).Count}");
        }

        private static string getGroupsAsString(Event ev)
        {
            string result = "";
            if (ev is LectureEvent lecture)
                lecture.Groups.ForEach(g => result += g.Name + " ");

            if (ev is PracticeEvent practice)
                practice.Groups.ForEach(g => result += g.Name + " ");

            return result;
        }
    }
}
