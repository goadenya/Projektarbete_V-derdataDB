using Projektarbete_VäderdataDB.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Projektarbete_VäderdataDB
{   
    class CRUD
    {
        const string filePath = @"E:\OneDrive - IT-Högskolan Sverige AB\source\repos\Projektarbete_VäderdataDB\Projektarbete_VäderdataDB\files\tempdata4.csv";
        static int rowsAffected = 0;

        /// <summary>
        /// Create data - the C in CRUD
        /// </summary>
        public static void CreateItem()
        {
            using (var context = new EFContext())
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var filePosts in lines)
                {
                    rowsAffected++;
                    string[] fields = filePosts.Split(','); //Seperates date and timespan + each post.
                    var databaseInput = new Temperature();

                    DateTime date = DateTime.Parse(fields[0]);
                    string place = fields[1];
                    double? temp = double.Parse(fields[2], CultureInfo.InvariantCulture);
                    int? humidity = int.Parse(fields[3]);

                    databaseInput.Date = date;
                    databaseInput.Place = place;
                    databaseInput.Temp = temp;
                    databaseInput.Humidity = humidity;
                    context.Add(databaseInput);
                }
                context.SaveChanges();
                Console.WriteLine($"Seeding complete. {rowsAffected} rows affected");
            }
        }


        /// <summary>
        /// Read data - the R in CRUD
        /// </summary>
        public static void ReadItem()
        {
            using (var db = new EFContext())
            {
                List<Temperature> temperatures = db.Temperatures.ToList();
                foreach (var temp in temperatures)
                {
                    Console.WriteLine($"{temp.ID}  {temp.Date}  {temp.Place}  {temp.Temp}  {temp.Humidity}");
                }
            }
        }


        /// <summary>
        /// Update data - the U in CRUD
        /// </summary>
        public static void UpdateItem()
        {
            using (var db = new EFContext())
            {
                Temperature temperature = db.Temperatures.Find(1);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Delete data - the D in CRUD
        /// </summary>
        public static void DeleteItem()
        {
            using (var db = new EFContext())
            {
                Temperature product = db.Temperatures.Find(5);
                db.Temperatures.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
