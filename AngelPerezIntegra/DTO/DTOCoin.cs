using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelPerezIntegra.DTO
{
    public class DTOCoin
    {

        public class Bitcoin
        {
            public Time Time { get; set; }

            public string Disclaimer { get; set; }

            public string ChartName { get; set; }

            public Bpi Bpi { get; set; }
        }

        public class Bpi
        {
            public Eur Usd { get; set; }

            public Eur Gbp { get; set; }

            public Eur Eur { get; set; }
        }

        public class Eur
        {
            public string Code { get; set; }

            public string Symbol { get; set; }

            public string Rate { get; set; }

            public string Description { get; set; }

            public double RateFloat { get; set; }
        }

        public class Time
        {
            public string Updated { get; set; }

            public DateTimeOffset UpdatedIso { get; set; }

            public string Updateduk { get; set; }
        }
    }
}
