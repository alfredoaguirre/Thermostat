using System;
using System.Collections.Generic;

namespace Themostat
{
    [Serializable]
    public class Current
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string _base { get; set; }
        public Main_ Main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public double id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }


    }

    public class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Weather
    {
        public string id { get; set; }
        public string main_ { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }
    public class Main_
    {
        public float temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float temp_max { get; set; }
        public float temp_min { get; set; }

    }
    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
        public float gust { get; set; }


    }
    public class Cloud
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public float massage { get; set; }
        public string county { get; set; }
        public Double sunrise { get; set; }
        public Double suncet { get; set; }
    }


    public class Sun
    {
        public DateTime set { get; set; }
        public DateTime rise { get; set; }
    }

    public class Temperature
    {

        public double value { get; set; }
        public string unit { get; set; }
        public double max { get; set; }
        public double min { get; set; }
    }
}