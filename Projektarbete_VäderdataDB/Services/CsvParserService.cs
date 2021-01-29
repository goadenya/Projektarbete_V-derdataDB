using CsvHelper;
using Projektarbete_VäderdataDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Projektarbete_VäderdataDB.Services
{
    class CsvParserService
    {
        /// <summary>
        /// Legacy Code - Alternativt sätt att läsa in CSV filer
        /// </summary>
        /// <param name="Context"></param>
        //public void SeedData(EFContext Context) 
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    const string resourceName = @"E:\OneDrive - IT-Högskolan Sverige AB\source\repos\Projektarbete_VäderdataDB\Projektarbete_VäderdataDB\Models\Temperature.cs";
        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    {
        //        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //        {
        //            CsvReader csvReader = new CsvReader((IParser)reader);
        //            //csvReader.Configuration.WillThrowOnMissingField = false;
        //            var records = csvReader.GetRecords<Temperature>().ToArray();

        //            foreach (Temperature record in records)
        //            {
        //                //record.Datum = 
        //                Context.Temperatures.Add(record);
        //            }
        //        }
        //    }
        //    Context.SaveChanges();
        //}
    }
}
