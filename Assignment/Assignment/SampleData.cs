﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        // 1.
        public IEnumerable<string> CsvRows // = the current return ? 2/17 lecture
        {
            get 
            {
                return File.ReadAllLines("People.csv").Skip(1).ToList();//System.Reflection.Assembly.GetExecutingAssembly().Location; current will work but it relies on unsafe assumptions
            }
        }


        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        {

            // Define Query
            IEnumerable<string> stateQuery = (
                from line in CsvRows
                let columnSeparatedLines = line.Split(',')
                orderby columnSeparatedLines[6]
                select columnSeparatedLines[6]
                ).Distinct().ToList();

            //Execute Query
            IEnumerable<string> stateList = stateQuery;

            return stateList;

        }
        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows()
        {
           
            return String.Join( ",",GetUniqueSortedListOfStatesGivenCsvRows());

            
            //string stateString = new("");
            //IEnumerable<string> states = GetUniqueSortedListOfStatesGivenCsvRows(){
            //    from item in states
            //    stateString = stateString.Join(",", item);
            //    select stateString;
            //}
        }
            
        // 4.
        public IEnumerable<IPerson> People
        {
            get
            {
               
                IEnumerable<IPerson> PeopleQuery = (
                   from line in CsvRows
                   let columnSeparatedLines = line.Split(',')
                   let addressArray = columnSeparatedLines[4..]
                   let peopleArray = columnSeparatedLines[0..4]
                   orderby addressArray[2], //State
                      addressArray[1], //city
                      addressArray[3]   //zip
               let tempAddress = new Address(addressArray[0], addressArray[1], addressArray[2], addressArray[3])
                   select new Person(
                        peopleArray[1], // FirstName
                        peopleArray[2], // LastName
                        tempAddress, 
                        peopleArray[3]) // Email 
                   ).ToList();

                //foreach (Person p in PeopleQuery){

                //    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", p.FirstName, p.LastName,
                //        p.Address.StreetAddress, p.Address.City, p.Address.State, p.Address.Zip, p.EmailAddress);

                //}
                return PeopleQuery;
            }
        }
        

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter) => throw new NotImplementedException();

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(IEnumerable<IPerson> People)
        {
            string stateQuery = People.Select(item => item.Address.State).Distinct().Aggregate((current, next) => current + "," + next);

            return stateQuery;
        }

    }
}
