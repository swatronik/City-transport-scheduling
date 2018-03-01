using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   

    

    public class scheduling {

        public struct Route{
            public string nameTransport;
            public int Time;
            public int TimeR;
            public int TimeInt;
            public Route(String name, int time, int timer, int timeint){
                nameTransport = name;
                Time = time;
                TimeR = timer;
                TimeInt = timeint;
            }
        }

        public struct BusStop{
            public string nameStop;
            public List<Route> Bus;
            public BusStop(String name, List<Route> bus) {
                nameStop = name;
                Bus = bus;
            }
        }

        

        void writeobj(Route obj,int t) {
            int timehelp = obj.Time;
            while (timehelp < t) { timehelp += obj.TimeInt; }
            int timehelp2 = obj.TimeR;
            while (timehelp2 < t) { timehelp2 += obj.TimeInt; }
            Console.WriteLine(obj.nameTransport+" in a "+(timehelp-t)+" minute");
            Console.WriteLine(obj.nameTransport + " reverse in a " + (timehelp2-t) + " minute");
        }


        List <BusStop> road=new List<BusStop>();
        List<int> stoptime =new List<int>(){ 1500, 1530 };

        public scheduling() {  
            Route route1 = new Route("Route 437", 300, 314, 10);
            List<Route> list1 = new List<Route>();
            list1.Add(route1);
            BusStop stop1 = new BusStop("E", list1);

            Route route2 = new Route("Route 104", 306, 308, 10);
            List<Route> list2 = new List<Route>();
            list2.Add(route2);
            Route route3 = new Route("Route 437", 310, 322, 5);
            list2.Add(route3);
            BusStop stop2 = new BusStop("A", list2);

            Route route4 = new Route("Route 437", 319, 315, 5);
            List<Route> list3 = new List<Route>();
            list3.Add(route4);
            Route route5 = new Route("Route 104", 303, 309, 10);
            list3.Add(route5);
            BusStop stop3 = new BusStop("B", list3);

            Route route6 = new Route("Route 437", 314, 300, 5);
            List<Route> list4 = new List<Route>();
            list4.Add(route6);
            BusStop stop4 = new BusStop("F", list4);

            Route route7 = new Route("Route 104", 318, 314, 10);
            List<Route> list5 = new List<Route>();
            list5.Add(route7);
            BusStop stop5 = new BusStop("C", list5);

            Route route8 = new Route("Route 104", 322, 310, 10);
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

        private bool data(string stop, int t) {
            int err = 0;
            foreach (BusStop el in road)
            {
                if (el.nameStop == stop) {
                    err = 1;
                    foreach (Route el1 in el.Bus) {
                        writeobj(el1, t);
                    }
                }
            }
            if (err == 0) return false;
            return true;
        }

         public bool  processing() {
            Console.WriteLine("Input stop and time");
            String str=Console.ReadLine();
            if (String.IsNullOrWhiteSpace(str)) return false;
            string[] array = str.Split(' ');
            string[] array2 = array[1].Split(':');
            int t = Int32.Parse(array2[0]) * 60 + Int32.Parse(array2[1]);
            if (Int32.Parse(array2[0]) < 4) t += 1440;
            if (!data(array[0], t)) { Console.WriteLine("This stop dont exist"); }
            return true;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            scheduling city = new scheduling();

            while (city.processing()) {}
        }
    }
}
