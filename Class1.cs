using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public partial class scheduling
    {

        public struct Route
        {
            public string nameTransport;
            public int Time;
            public int TimeEnd;
            public int TimeR;
            public int TimeInt;
            public Route(String name, int time, int timer, int timeint, int timeend)
            {
                nameTransport = name;
                Time = time;
                TimeR = timer;
                TimeInt = timeint;
                TimeEnd = timeend;
            }
        }

        public struct BusStop
        {
            public string nameStop;
            public List<Route> Bus;
            public BusStop(String name, List<Route> bus)
            {
                nameStop = name;
                Bus = bus;
            }
        }



        void writeobj(Route obj, int t)
        {
            int err = 0;
            if (t < obj.Time) t += 1440;
            int timehelp = obj.Time;
            while (timehelp < t) { timehelp += obj.TimeInt; if (timehelp >= obj.TimeEnd) { err = 1; break; } }
            int timehelp2 = obj.TimeR;
            while (timehelp2 < t) { timehelp2 += obj.TimeInt; if (timehelp2 >= obj.TimeEnd) { err = 1; break; } }
            if (err == 1) { Console.WriteLine("City transport dont works in this time"); }
            else
            {
                Console.WriteLine(obj.nameTransport + " in a " + Math.Abs(timehelp - t) + " minute");
                Console.WriteLine(obj.nameTransport + " reverse in a " + Math.Abs(timehelp2 - t) + " minute");
            }
        }


        List<BusStop> road = new List<BusStop>();

        public scheduling()
        {

            if (File.Exists(path: @"input.txt"))
            {
                using (StreamReader fs = new StreamReader(path: @"input.txt"))
                {
                    Console.WriteLine("File is loading");
                    string iterat = fs.ReadLine();
                    int iteratInt = Int32.Parse(iterat);
                    for (int i = 0; i < iteratInt; i++)
                    {
                        string temp = fs.ReadLine();
                        string[] array = temp.Split(' ');
                        string temp2 = fs.ReadLine();
                        string[] array2 = temp2.Split(' ');
                        string temp3 = fs.ReadLine();
                        string[] array3 = temp3.Split(' ');
                        string[] array21 = array[0].Split(':');
                        string[] array22 = array[1].Split(':');
                        int timebegin = Int32.Parse(array21[0]) * 60 + Int32.Parse(array21[1]);
                        int timeend = Int32.Parse(array22[0]) * 60 + Int32.Parse(array22[1]);
                        if (timebegin >= timeend) timeend += 1440;
                        int timebegin1 = timebegin;

                        int sum = 0;
                        for (int j = 0; j < array3.Length; j++)
                        {
                            sum += Int32.Parse(array3[j]);
                        }
                        int timebegin2 = timebegin + sum;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            Route route1 = new Route(array[3], timebegin1, timebegin2, Int32.Parse(array[2]), timeend);
                            List<Route> list1 = new List<Route>();
                            list1.Add(route1);
                            BusStop stop1 = new BusStop(array2[j], list1);
                            if (j != array3.Length)
                            {
                                timebegin1 += Int32.Parse(array3[j]);
                                timebegin2 -= Int32.Parse(array3[j]);
                            }
                            road.Add(stop1);
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("File is not loading");
                Route route1 = new Route("Route 437", 310, 314, 5, 1500);
                List<Route> list1 = new List<Route>();
                list1.Add(route1);
                BusStop stop1 = new BusStop("E", list1);

                Route route2 = new Route("Route 104", 200, 308, 10, 1530);
                List<Route> list2 = new List<Route>();
                list2.Add(route2);
                Route route3 = new Route("Route 437", 310, 322, 5, 1500);
                list2.Add(route3);
                BusStop stop2 = new BusStop("A", list2);

                Route route4 = new Route("Route 437", 319, 315, 5, 1500);
                List<Route> list3 = new List<Route>();
                list3.Add(route4);
                Route route5 = new Route("Route 104", 303, 309, 10, 1530);
                list3.Add(route5);
                BusStop stop3 = new BusStop("B", list3);

                Route route6 = new Route("Route 437", 314, 300, 5, 1500);
                List<Route> list4 = new List<Route>();
                list4.Add(route6);
                BusStop stop4 = new BusStop("F", list4);

                Route route7 = new Route("Route 104", 318, 314, 10, 1530);
                List<Route> list5 = new List<Route>();
                list5.Add(route7);
                BusStop stop5 = new BusStop("C", list5);

                Route route8 = new Route("Route 104", 322, 310, 10, 1530);
                List<Route> list6 = new List<Route>();
                list6.Add(route8);
                BusStop stop6 = new BusStop("D", list6);

                road.Add(stop1);
                road.Add(stop2);
                road.Add(stop3);
                road.Add(stop4);
                road.Add(stop5);
                road.Add(stop6);
            }
            while (this.processing()) { }
        }

        private bool data(string stop, int t)
        {
            int err = 0;
            foreach (BusStop el in road)
            {
                if (el.nameStop == stop)
                {
                    err = 1;
                    foreach (Route el1 in el.Bus)
                    {
                        writeobj(el1, t);
                    }
                }
            }
            if (err == 0) return false;
            return true;
        }

        public bool processing()
        {
            Console.Write("Enter station:");
            String strstation = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(strstation)) return false;
            Console.Write("Enter Time:");
            String str = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(str)) return false;
            string[] array2 = str.Split(':');
            int t = Int32.Parse(array2[0]) * 60 + Int32.Parse(array2[1]);
            if (!data(strstation, t)) { Console.WriteLine("This stop dont exist"); }
            return true;
        }

    }
}
