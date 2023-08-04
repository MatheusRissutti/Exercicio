using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2
{
    internal class JsonMockFootball
    {
        public int Page { get; set; }

        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<FootballMatch> Data { get; set; }
    }

    internal class FootballMatch
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team1Goals { get; set; }
        public string Team2Goals { get; set; }
    }
}
