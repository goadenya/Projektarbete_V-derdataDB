using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projektarbete_VäderdataDB.Models.Tools
{
    class Script
    {
        /// <summary>
        /// Function Six - First day of winter algorithm
        /// </summary>
        /// <returns></returns>
        internal static string FirstDayOfWinterQuery() // Iterates through a list of date average temperatures to meet condition for metrologic winter. Condition: 5 consecutive days of average temperature below 0°C (-SMHI).
        {
            using (var db = new EFContext())
            {
                var temperatures = db.Temperatures.ToList();
                var tempsOfEachDay = temperatures
                    .Where(d => d.Place == "UTE")
                    .GroupBy(d => d.Date.Date);


                var averageTempRecord = new List<Record>();
                foreach (var date in tempsOfEachDay)
                {
                    var record = new Record();
                    record.Date = date.First().Date;
                    record.Place = date.First().Place;
                    record.AvgTemp = Math.Round(date.Average(d => (decimal)d.Temp), 2);
                    averageTempRecord.Add(record);
                }

                var listDateAvgTemps = averageTempRecord.OrderBy(d => d.Date);
                var firstDayOfSeason = new Record();
                int count = 0;
                foreach (var date in listDateAvgTemps)
                {
                    if (date.AvgTemp <= 0)
                    {
                        count++;
                        if (count == 5)
                        {
                            firstDayOfSeason = date;
                            break;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                if (firstDayOfSeason.Place != null)
                {
                    return firstDayOfSeason.Date.ToShortDateString();
                }
                return "Inget datum hittades.";
            }
        }

        /// <summary>
        /// Function Five - First day of fall/autumn season algorithm
        /// </summary> 
        internal static string FirstDayOfFallQuery()// Iterates through a list of date average temperatures to meet condition for metrologic fall. Condition: 5 consecutive days of average temperature above 0 °C and below 10°C (-SMHI).
        {
            var listDateAvgTemps = ListAvgTempQuery("ute", false).OrderBy(d => d.Date); //TODO; Add where query to improve 
            var firstDayOfSeason = new Record();
            int count = 0;
            int elementNr = 0;
            foreach (var item in listDateAvgTemps)
            {
                elementNr++;
                if (item.AvgTemp < 10 && item.AvgTemp >= 0)
                {
                    count++;
                    if (count == 5)
                    {
                        firstDayOfSeason = listDateAvgTemps.ElementAt(elementNr - 5);
                        break;
                    }
                }
                else
                {
                    count = 0;
                }

            }
            return firstDayOfSeason.Date.ToString("yyyy-MM-dd");

        }

        /// <summary>
        /// Funkction 4 - Assesses mold risk and returns a list of days with risk for molding. From highest to lowest.
        /// </summary>
        
        internal static IOrderedEnumerable<Record> MoldRiskAssessment(string place) // Returns a list of dates with calculated risk for mold
        {
            place = place.ToUpper();
            using (var db = new EFContext())
            {
                List<Temperature> temperatures = db.Temperatures.ToList();
                var tempsOfEachDay = temperatures
                    .Where(d => d.Place == place)
                    .GroupBy(d => d.Date.Date);

                List<Record> moldRiskRecords = new List<Record>();
                foreach (var date in tempsOfEachDay)
                {
                    double tempAvg = date.Average(d => (double)d.Temp);
                    double humidityAvg = date.Average(d => (double)d.Humidity);

                    double resultMoldRisk = ((humidityAvg - 78) * (tempAvg / 15)) / 22; // Formula for moldrisk: Moldrisk = (AverageHumidity - 78) * (AverageTemperature -15) / 22

                    double moldRiskPercentage = MoldRiskToPercentage(resultMoldRisk);

                    var moldRiskAssessment = new Record();
                    moldRiskAssessment.Date = date.First().Date.Date;
                    moldRiskAssessment.Place = date.First().Place;
                    moldRiskAssessment.AvgTemp = Math.Round((decimal)tempAvg, 1);
                    moldRiskAssessment.AvgHumidity = Math.Round((decimal)humidityAvg, 1);
                    moldRiskAssessment.MoldRisk = Math.Round(moldRiskPercentage, 1);
                    moldRiskRecords.Add(moldRiskAssessment);
                }
                var moldRiskList = moldRiskRecords.OrderBy(d => d.MoldRisk);
                return moldRiskList;
            }
        } // 

        /// <summary>
        /// Function Four Extension
        /// </summary>
        /// <param name="resultMoldRisk"></param>
        /// <returns></returns>
        private static double MoldRiskToPercentage(double resultMoldRisk) // Converts result of mold risk calculation into a equivalent percentage %.
        {
            double percentage;
            if (resultMoldRisk <= 0)
            {
                percentage = 0;
            }
            else if (resultMoldRisk >= 1)
            {
                percentage = 100;
            }
            else
            {
                percentage = resultMoldRisk * 100;
            }

            return percentage;
        }


        /// <summary>
        /// Function Extra - Legacy Code
        /// </summary>
        /// <returns></returns>

        //internal static IEnumerable<Temperature> TopTemps(string place)
        //{
        //    using (var db = new EFContext())
        //    {
        //        List<Temperature> temperatures = db.Temperatures.ToList();
        //        var tempsOfEachDay = temperatures
        //            .Where(d => d.Place == place)
        //            .GroupBy(d => d.Date.Date)
        //            .Select(g => g.AsEnumerable())
        //            .Select(d => d.OrderByDescending(d => d.Temp));
        //        var daysAvgTemps = new List<Temperature>();
        //        foreach (var day in tempsOfEachDay)
        //        {
        //            Temperature dayHighestTemp = day.First();
        //            daysAvgTemps.Add(dayHighestTemp);
        //        }
        //        var topThreeHottestDays = daysAvgTemps.OrderByDescending(d => d.Temp).Take(3);
        //        return topThreeHottestDays;
        //    }

        //}

        /// <summary>
        /// Function Two - Returns list of date average temperatures. Bool = True: Ordered by highest to lowest.  
        /// </summary>
        /// <returns></returns>

        internal static IOrderedEnumerable<Record> ListAvgTempQuery(string place, bool isHotQuery)
        {
            place = place.ToUpper();
            using (var db = new EFContext())
            {
                List<Temperature> temperatures = db.Temperatures.ToList();
                var tempsOfEachDay = temperatures
                    .Where(d => d.Place == place)
                    .GroupBy(d => d.Date.Date)
                    .Select(g => g.AsEnumerable())
                    .Select(d => d.OrderByDescending(d => d.Temp));

                var averageTempRecord = new List<Record>();
                foreach (var date in tempsOfEachDay)
                {
                    var record = new Record();
                    record.Date = date.First().Date;
                    record.Place = date.First().Place;
                    record.AvgTemp = Math.Round(date.Average(d => (decimal)d.Temp), 2);
                    averageTempRecord.Add(record);
                }

                switch (isHotQuery)
                {
                    case true:
                        var result = averageTempRecord.OrderByDescending(d => d.AvgTemp);
                        return result;
                    case false:
                        result = averageTempRecord.OrderBy(d => d.AvgTemp);
                        return result;
                }
            }
        } // Sorts list of dates in order of temperature readings (Hottest to Coldest, or vice versa).

        /// <summary>
        /// Function One - Searches for a date by user input
        /// </summary>
        /// <returns></returns> 
        internal static double DateAvgTempQuery(string date, string place)
        {
            place = place.ToUpper();
            using (var db = new EFContext())
            {
                List<Temperature> temperatures = db.Temperatures.ToList();
                var dateQuery = temperatures.Where(d => d.Place == place && d.Date.Date == DateTime.Parse(date).Date);
                double dateAvgTemp = dateQuery.Average(d => (double)d.Temp);
                var result = Math.Round((double)dateAvgTemp, 2);
                return result;
            }
        }
        /// <summary>
        /// Function Three - Returns list of date average humidity. Bool = True: Ordered by highest to lowest. 
        /// </summary>
        /// <param name="place"></param>
        internal static IOrderedEnumerable<Record> ListAvgHumidityQuery(string place)
        {
            place = place.ToUpper();
            using (var db = new EFContext())
            {
                List<Temperature> temperatures = db.Temperatures.ToList();
                var tempsOfEachDay = temperatures
                    .Where(d => d.Place == place)
                    .GroupBy(d => d.Date.Date)
                    .Select(g => g.AsEnumerable())
                    .Select(d => d.OrderByDescending(d => d.Temp));

                var averageTempRecord = new List<Record>();
                foreach (var date in tempsOfEachDay)
                {
                    var record = new Record();
                    record.Date = date.First().Date;
                    record.Place = date.First().Place;
                    record.AvgHumidity = Math.Round(date.Average(d => (decimal)d.Humidity), 1);
                    averageTempRecord.Add(record);
                }

                var result = averageTempRecord.OrderBy(d => d.AvgHumidity);
                return result;
            }
        }
    }
}
