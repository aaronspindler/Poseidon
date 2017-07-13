﻿namespace Poseidon.Models
{
    public class Ticker
    {
        public object[] error { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public GNOUSD GNOUSD { get; set; }
        }

        public class GNOUSD
        {
            public string[] a { get; set; }
            public string[] b { get; set; }
            public string[] c { get; set; }
            public string[] v { get; set; }
            public string[] p { get; set; }
            public int[] t { get; set; }
            public string[] l { get; set; }
            public string[] h { get; set; }
            public string o { get; set; }
        }
    }
}