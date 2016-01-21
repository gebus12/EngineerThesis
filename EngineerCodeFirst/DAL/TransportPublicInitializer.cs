using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.DAL
{
    public class TransportPublicInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TransportPublicContext>
    {
        protected override void Seed(TransportPublicContext context)
        {
            var buses = new List<Bus>
            {
                new Bus{RegNum = "SGL A123",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.NO},
                new Bus{RegNum = "SGL B246",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.NO},
                new Bus{RegNum = "SGL C345",Status="ON", Latitude = "50.298375", Longitude = "18.680187", Accessible = Accessible.NO},
                new Bus{RegNum = "SGL D468",Status="ON", Latitude = "50.298474", Longitude = "18.783238", Accessible = Accessible.NO},
                new Bus{RegNum = "SGL E567",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.YES},
                new Bus{RegNum = "SGL F680",Status="ON", Latitude = "50.227779", Longitude = "18.656306", Accessible = Accessible.YES},
                new Bus{RegNum = "SGL G789",Status="ON", Latitude = "50.2234821", Longitude = "18.7242504", Accessible = Accessible.YES},
                new Bus{RegNum = "SGL H808",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.YES},
                new Bus{RegNum = "SGL I901",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.YES},
                new Bus{RegNum = "SGL J012",Status="OFF", Latitude = "50.288697", Longitude = "18.677784", Accessible = Accessible.YES},
                /*
                 * defaultowo AEI:
                 * Latitude = "50.288697", Longitude = "18.677784"
                 * 
                 */
            };
            buses.ForEach(s => context.Buses.Add(s));
            context.SaveChanges();

            var drivers = new List<Driver>
            {
                new Driver{DriverName="Clint", DriverSurname="Eastwood", DriverLogin = "clinte", DriverPass = "clinte", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 3)}},
                new Driver{DriverName="Stephen", DriverSurname="King", DriverLogin = "stephenk", DriverPass = "stephenk", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 4)}},
                new Driver{DriverName="Sherlock", DriverSurname="Holmes", DriverLogin = "scherlockh", DriverPass = "scherlockh", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 7)}},
                new Driver{DriverName="Roman", DriverSurname="Polanski", DriverLogin = "romanp", DriverPass = "romanp",  Status = "OFF"},
                new Driver{DriverName="Harry", DriverSurname="Potter", DriverLogin = "harryp", DriverPass = "harryp", Status = "OFF"},
                new Driver{DriverName="Elton", DriverSurname="John", DriverLogin = "eltonj", DriverPass = "eltonj", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 6)}},
            };

            drivers.ForEach(s => context.Drivers.Add(s));
            context.SaveChanges();

            var lines = new List<Line>
            {
                new Line{LineID = 1, LineNumber=47, Direction="Szczyglowice", Status = "OFF", ScheduleType = ScheduleType.HOLIDAY},
                new Line{LineID = 2, LineNumber=47, Direction="Zabrze", Status = "OFF", ScheduleType = ScheduleType.WEEKDAY},
                new Line{LineID = 3, LineNumber=58, Direction="Knurow", Buses = new List<Bus>{buses.Single(b => b.BusID == 3)}, Status = "ON", ScheduleType = ScheduleType.REGULAR},
                new Line{LineID = 4, LineNumber=58, Direction="Gliwice", Status = "OFF", ScheduleType = ScheduleType.HOLIDAY},
                new Line{LineID = 5, LineNumber=32, Direction="Zabrze", Status = "OFF", ScheduleType = ScheduleType.HOLIDAY},
                new Line{LineID = 6, LineNumber=32, Direction="Labedy", Buses = new List<Bus>{buses.Single(b => b.BusID == 4)}, Status = "ON", ScheduleType = ScheduleType.REGULAR},
                new Line{LineID = 7, LineNumber=197, Direction="Sosnica", Buses = new List<Bus>{buses.Single(b => b.BusID == 6)}, Status = "ON", ScheduleType = ScheduleType.REGULAR},
                new Line{LineID = 8, LineNumber=197, Direction="Labedy", Buses = new List<Bus>{buses.Single(b => b.BusID == 7)}, Status = "ON", ScheduleType = ScheduleType.REGULAR},
            };
            lines.ForEach(s => context.Lines.Add(s));
            context.SaveChanges();

            var stops = new List<Stop>
            {
                new Stop{City="Labedy", StopName="Huta", Latitude = "50.35574", Longitude = "18.61513"}, //1
                new Stop{City="Labedy", StopName="Osiedle", Latitude = "50.33893", Longitude = "18.63160"}, //2
                new Stop{City="Gliwice", StopName="Os. Kopernika Oriona", Latitude = "50.32697", Longitude = "18.65019"}, //3
                new Stop{City="Gliwice", StopName="Os. Kopernika Kapielisko", Latitude = "50.32465", Longitude = "18.66170"}, //4
                new Stop{City="Gliwice", StopName="Szobiszowice", Latitude = "50.31720", Longitude = "18.66565"}, //5
                new Stop{City="Gliwice", StopName="Dworzec PKP", Latitude = "50.30025", Longitude = "18.67536"}, //6
                new Stop{City="Gliwice", StopName="Komag", Latitude = "50.28684", Longitude = "18.67371"}, //7
                new Stop{City="Gliwice", StopName="Lipowa", Latitude = "50.29972", Longitude = "18.68412"}, //8
                new Stop{City="Gliwice", StopName="Mikolowska", Latitude = "50.29195", Longitude = "18.66830"}, //9
                new Stop{City="Gliwice", StopName="Fadom", Latitude = "50.26438", Longitude = "18.69100"}, //10
                new Stop{City="Gliwice", StopName="Plac Piastow", Latitude = "50.29801", Longitude = "18.67700"}, //11
                new Stop{City="Gliwice", StopName="Wroclawska", Latitude = "50.29213", Longitude = "18.67312"}, //12
                new Stop{City="Gliwice", StopName="Zabrska", Latitude = "50.29504", Longitude = "18.68868"}, //13
                new Stop{City="Gliwice", StopName="Lotnikow", Latitude = "50.28247", Longitude = "18.67639"}, //14
                new Stop{City="Sosnica", StopName="Gielda", Latitude = "50.28462", Longitude = "18.71958"}, //15
                new Stop{City="Sosnica", StopName="Kopalnia", Latitude = "50.29036", Longitude = "18.74280"}, //16
                new Stop{City="Sosnica", StopName="Os. Zeromskiego", Latitude = "50.28122", Longitude = "18.73495"}, //17
                new Stop{City="Sosnica", StopName="Plac Mariacki", Latitude = "50.29306", Longitude = "18.72885"}, //18
                new Stop{City="Zabrze", StopName="Os. Wyzwolenia", Latitude = "50.29293", Longitude = "18.75829"}, //19
                new Stop{City="Zabrze", StopName="Komenda Policji", Latitude = "50.30211", Longitude = "18.77838"}, //20
                new Stop{City="Zabrze", StopName="Goethego", Latitude = "50.30556", Longitude = "18.78271"}, //21
                new Stop{City="Zabrze", StopName="Os. Janek", Latitude = "50.28231", Longitude = "18.78859"}, //22
                new Stop{City="Zabrze", StopName="Politechnika Slaska", Latitude = "50.29747", Longitude = "18.78090"}, //23
                new Stop{City="Szczyglowice", StopName="Kopalnia", Latitude = "50.19311", Longitude = "18.63178"}, //24
                new Stop{City="Szczyglowice", StopName="Skrzyzowanie", Latitude = "50.19778", Longitude = "18.64736"}, //25
                new Stop{City="Knurow", StopName="Kino", Latitude = "50.21998", Longitude = "18.66544"}, //26
                new Stop{City="Knurow", StopName="Cegielnia", Latitude = "50.23260", Longitude = "18.66025"}, //27
                new Stop{City="Knurow", StopName="Dworcowa", Latitude = "50.21827", Longitude = "18.68669"}, //28
                new Stop{City="Knurow", StopName="Szpitalna", Latitude = "50.22702", Longitude = "18.65764"}, //29
                new Stop{City="Gieraltowice", StopName="Kosciol", Latitude = "50.22371", Longitude = "18.72747"}, //30
                new Stop{City="Gieraltowice", StopName="Skrzyzowanie", Latitude = "50.22951", Longitude = "18.72958"}, //31
                new Stop{City="Gieraltowice", StopName="Wyzwolenia", Latitude = "50.22013", Longitude = "18.71332"}, //32
            };
            stops.ForEach(s => context.Stops.Add(s));
            context.SaveChanges();

            var schedules = new List<Schedule>
            {
                // 197: Sosnica
                new Schedule{BusOrder = 0, DepartureTime = "11:12", LineID = 7, StopID = 1},
                new Schedule{BusOrder = 1, DepartureTime = "11:19", LineID = 7, StopID = 2},
                new Schedule{BusOrder = 2, DepartureTime = "11:26", LineID = 7, StopID = 4},
                new Schedule{BusOrder = 3, DepartureTime = "11:29", LineID = 7, StopID = 5},
                new Schedule{BusOrder = 4, DepartureTime = "11:35", LineID = 7, StopID = 6},
                new Schedule{BusOrder = 5, DepartureTime = "11:37", LineID = 7, StopID = 11},
                new Schedule{BusOrder = 6, DepartureTime = "11:55", LineID = 7, StopID = 17},

                //197: Labedy
                new Schedule{BusOrder = 0, DepartureTime = "15:59", LineID = 8, StopID = 17},
                new Schedule{BusOrder = 1, DepartureTime = "16:07", LineID = 8, StopID = 18},
                new Schedule{BusOrder = 2, DepartureTime = "16:15", LineID = 8, StopID = 13},
                new Schedule{BusOrder = 3, DepartureTime = "16:19", LineID = 8, StopID = 11},
                new Schedule{BusOrder = 4, DepartureTime = "16:21", LineID = 8, StopID = 6},
                new Schedule{BusOrder = 5, DepartureTime = "16:28", LineID = 8, StopID = 5},
                new Schedule{BusOrder = 6, DepartureTime = "16:31", LineID = 8, StopID = 4},
                new Schedule{BusOrder = 7, DepartureTime = "16:38", LineID = 8, StopID = 2},
                new Schedule{BusOrder = 8, DepartureTime = "16:45", LineID = 8, StopID = 1},

                //47: Zabrze
                new Schedule{BusOrder = 0, DepartureTime = "9:08", LineID = 2, StopID = 24},
                new Schedule{BusOrder = 1, DepartureTime = "9:10", LineID = 2, StopID = 25},
                new Schedule{BusOrder = 2, DepartureTime = "9:18", LineID = 2, StopID = 26},
                new Schedule{BusOrder = 3, DepartureTime = "9:22", LineID = 2, StopID = 29},
                new Schedule{BusOrder = 4, DepartureTime = "9:24", LineID = 2, StopID = 27},
                new Schedule{BusOrder = 5, DepartureTime = "9:33", LineID = 2, StopID = 28},
                new Schedule{BusOrder = 6, DepartureTime = "9:36", LineID = 2, StopID = 32},
                new Schedule{BusOrder = 7, DepartureTime = "9:38", LineID = 2, StopID = 30},
                new Schedule{BusOrder = 8, DepartureTime = "9:40", LineID = 2, StopID = 31},
                new Schedule{BusOrder = 9, DepartureTime = "9:57", LineID = 2, StopID = 22},
                new Schedule{BusOrder = 10, DepartureTime = "10:04", LineID = 2, StopID = 23},
                new Schedule{BusOrder = 11, DepartureTime = "10:05", LineID = 2, StopID = 20},
                new Schedule{BusOrder = 12, DepartureTime = "10:08", LineID = 2, StopID = 21},

                //47: Szczyglowice
                new Schedule{BusOrder = 0, DepartureTime = "13:42", LineID = 1, StopID = 21},
                new Schedule{BusOrder = 1, DepartureTime = "13:46", LineID = 1, StopID = 20},
                new Schedule{BusOrder = 2, DepartureTime = "13:47", LineID = 1, StopID = 23},
                new Schedule{BusOrder = 3, DepartureTime = "13:55", LineID = 1, StopID = 22},
                new Schedule{BusOrder = 4, DepartureTime = "14:13", LineID = 1, StopID = 31},
                new Schedule{BusOrder = 5, DepartureTime = "14:15", LineID = 1, StopID = 30},
                new Schedule{BusOrder = 6, DepartureTime = "14:17", LineID = 1, StopID = 32},
                new Schedule{BusOrder = 7, DepartureTime = "14:21", LineID = 1, StopID = 28},
                new Schedule{BusOrder = 8, DepartureTime = "14:29", LineID = 1, StopID = 27},
                new Schedule{BusOrder = 9, DepartureTime = "14:31", LineID = 1, StopID = 29},
                new Schedule{BusOrder = 10, DepartureTime = "14:33", LineID = 1, StopID = 26},
                new Schedule{BusOrder = 11, DepartureTime = "14:42", LineID = 1, StopID = 25},
                new Schedule{BusOrder = 12, DepartureTime = "14:45", LineID = 1, StopID = 24},

                //32: Zabrze
                new Schedule{BusOrder = 0, DepartureTime = "11:19", LineID = 5, StopID = 1},
                new Schedule{BusOrder = 1, DepartureTime = "11:26", LineID = 5, StopID = 2},
                new Schedule{BusOrder = 2, DepartureTime = "11:33", LineID = 5, StopID = 3},
                new Schedule{BusOrder = 3, DepartureTime = "11:36", LineID = 5, StopID = 4},
                new Schedule{BusOrder = 4, DepartureTime = "11:39", LineID = 5, StopID = 5},
                new Schedule{BusOrder = 5, DepartureTime = "11:45", LineID = 5, StopID = 6},
                new Schedule{BusOrder = 6, DepartureTime = "11:47", LineID = 5, StopID = 11},
                new Schedule{BusOrder = 7, DepartureTime = "11:51", LineID = 5, StopID = 9},
                new Schedule{BusOrder = 8, DepartureTime = "11:54", LineID = 5, StopID = 7},
                new Schedule{BusOrder = 9, DepartureTime = "12:07", LineID = 5, StopID = 15},
                new Schedule{BusOrder = 10, DepartureTime = "12:12", LineID = 5, StopID = 16},
                new Schedule{BusOrder = 11, DepartureTime = "12:14", LineID = 5, StopID = 19},
                new Schedule{BusOrder = 12, DepartureTime = "12:19", LineID = 5, StopID = 23},
                new Schedule{BusOrder = 13, DepartureTime = "12:21", LineID = 5, StopID = 20},
                new Schedule{BusOrder = 14, DepartureTime = "12:24", LineID = 5, StopID = 21},

                //32: Labedy
                new Schedule{BusOrder = 0, DepartureTime = "11:02", LineID = 6, StopID = 21},
                new Schedule{BusOrder = 1, DepartureTime = "11:05", LineID = 6, StopID = 20},
                new Schedule{BusOrder = 2, DepartureTime = "11:07", LineID = 6, StopID = 23},
                new Schedule{BusOrder = 3, DepartureTime = "11:12", LineID = 6, StopID = 19},
                new Schedule{BusOrder = 4, DepartureTime = "11:14", LineID = 6, StopID = 16},
                new Schedule{BusOrder = 5, DepartureTime = "11:18", LineID = 6, StopID = 15},
                new Schedule{BusOrder = 6, DepartureTime = "11:30", LineID = 6, StopID = 7},
                new Schedule{BusOrder = 7, DepartureTime = "11:33", LineID = 6, StopID = 12},
                new Schedule{BusOrder = 8, DepartureTime = "11:38", LineID = 6, StopID = 11},
                new Schedule{BusOrder = 9, DepartureTime = "11:39", LineID = 6, StopID = 6},
                new Schedule{BusOrder = 10, DepartureTime = "11:45", LineID = 6, StopID = 5},
                new Schedule{BusOrder = 11, DepartureTime = "11:47", LineID = 6, StopID = 4},
                new Schedule{BusOrder = 12, DepartureTime = "11:49", LineID = 6, StopID = 3},
                new Schedule{BusOrder = 13, DepartureTime = "11:56", LineID = 6, StopID = 2},
                new Schedule{BusOrder = 14, DepartureTime = "12:03", LineID = 6, StopID = 1},

                //58: Gliwice
                new Schedule{BusOrder = 0, DepartureTime = "6:23", LineID = 4, StopID = 29},
                new Schedule{BusOrder = 1, DepartureTime = "6:25", LineID = 4, StopID = 27},
                new Schedule{BusOrder = 2, DepartureTime = "6:41", LineID = 4, StopID = 10},
                new Schedule{BusOrder = 3, DepartureTime = "6:45", LineID = 4, StopID = 14},
                new Schedule{BusOrder = 4, DepartureTime = "6:47", LineID = 4, StopID = 7},
                new Schedule{BusOrder = 5, DepartureTime = "6:50", LineID = 4, StopID = 12},
                new Schedule{BusOrder = 6, DepartureTime = "6:56", LineID = 4, StopID = 8},

                //58: Knurow
                new Schedule{BusOrder = 0, DepartureTime = "22:36", LineID = 3, StopID = 8},
                new Schedule{BusOrder = 1, DepartureTime = "22:40", LineID = 3, StopID = 11},
                new Schedule{BusOrder = 2, DepartureTime = "22:44", LineID = 3, StopID = 9},
                new Schedule{BusOrder = 3, DepartureTime = "22:46", LineID = 3, StopID = 7},
                new Schedule{BusOrder = 4, DepartureTime = "22:48", LineID = 3, StopID = 14},
                new Schedule{BusOrder = 5, DepartureTime = "22:51", LineID = 3, StopID = 10},
                new Schedule{BusOrder = 6, DepartureTime = "23:00", LineID = 3, StopID = 31},
                new Schedule{BusOrder = 7, DepartureTime = "23:02", LineID = 3, StopID = 30},
                new Schedule{BusOrder = 8, DepartureTime = "23:04", LineID = 3, StopID = 32},
                new Schedule{BusOrder = 9, DepartureTime = "23:08", LineID = 3, StopID = 28},
                new Schedule{BusOrder = 10, DepartureTime = "23:13", LineID = 3, StopID = 29},
            };
            schedules.ForEach(s => context.Schedules.Add(s));
            context.SaveChanges();

            var driverNotif = new List<MsgDriver>
            {
                new MsgDriver{DriverID = 2, BusID = 4, Text = "Traffic jam on Mikolowska Street", TimeStamp = System.DateTime.Now, Status = Status.Unread, Receiver = Receiver.Provider},
                new MsgDriver{DriverID = 3, BusID = 7, Text = "Car accident on Dworcowa Street", TimeStamp = System.DateTime.Now, Status = Status.Unread, Receiver = Receiver.Provider},
                new MsgDriver{DriverID = 3, BusID = 7, Text = "Tarnogórska Street is closed due to protest", TimeStamp = System.DateTime.Now, Status = Status.Unread, Receiver = Receiver.Driver},
            };
            driverNotif.ForEach(s => context.MsgDrivers.Add(s));
            context.SaveChanges();
        }
    }
}