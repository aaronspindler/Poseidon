using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Poseidon.Models.FiatCurrency.BankOfCanada;

namespace Poseidon
{
    /// <summary>
    /// Manages data and interactions with Bank of Canada
    /// </summary>
    public class BankOfCanadaManager
    {
        /// <summary>
        /// Variable for storing and retrieving the actual response from the bank of canada
        /// </summary>
        private BankOfCanadaResponse _response;
        
        
        /// <summary>
        /// Gets currency price data from The Bank of Canada between two dates
        /// All currencies are in CAD base
        /// </summary>
        /// <param name="start">The data to get data from</param>
        /// <param name="end">The date to get data to</param>
        public void GetFiatRates(DateTime start, DateTime end)
        {
            string startFormatted = start.ToString("yyyy-MM-dd");
            string endFormatted = end.ToString("yyyy-MM-dd");

            
            var client = new WebClient();
            try
            {
                String apiAddress = "https://www.bankofcanada.ca/valet/observations/FXAUDCAD,FXBRLCAD,FXCNYCAD,FXEURCAD,FXHKDCAD,FXINRCAD,FXIDRCAD,FXJPYCAD,FXMYRCAD,FXMXNCAD,FXNZDCAD,FXNOKCAD,FXPENCAD,FXRUBCAD,FXSARCAD,FXSGDCAD,FXZARCAD,FXKRWCAD,FXSEKCAD,FXCHFCAD,FXTWDCAD,FXTHBCAD,FXTRYCAD,FXGBPCAD,FXUSDCAD,FXVNDCAD/csv?start_date=" + startFormatted + "&end_date=" + endFormatted;
                var data = client.OpenRead(apiAddress);
                var reader = new StreamReader(data);

                //Skip over label that just reads "TERMS AND CONDITIONS"
                reader.ReadLine();
                
                //Create URI for terms and conditions
                string termsAndConditions = reader.ReadLine();
                Uri termsURI = new Uri(termsAndConditions);
                
                //Skip empty line
                reader.ReadLine();
                
                //Skip over label that just reads "SERIES"
                reader.ReadLine();
                
                //Skip over column labels "id, label, description"
                reader.ReadLine();
                
                //Create a list to put the series
                List<Series> series = new List<Series>();

                //Loop through, parse, and add all series to list
                string seriesLine = reader.ReadLine();
                while (seriesLine != "")
                {
                    String[] splitLine = seriesLine.Split(",");
                    Series currentSeries = new Series(splitLine[0],splitLine[1],splitLine[2]);
                    series.Add(currentSeries);
                    //This will skip the blank line at the end of the data so theres no need to skip the blank line at the end of it
                    seriesLine = reader.ReadLine();
                }
                
                
                //Skip over label that just reads "OBSERVATIONS"
                reader.ReadLine();

                //Get line with all currency pairs
                string columnNames = reader.ReadLine();
                //Split the line into its respective pair names
                string[] currencyNames = columnNames.Split(",");

                List<Observation> observations = new List<Observation>();
                string observationsLine = reader.ReadLine();

                //Get all of the pair data
                do
                {
                    //Some date ranges will result in 0 observations. Such as on a 1 day range on a weekend where there is no data (Bank is closed)
                    if (observationsLine == "")
                    {
                        break;
                    }
                    string[] observationLineSplit = observationsLine.Split(",");
                    Observation obs = new Observation(Convert.ToDateTime(observationLineSplit[0]));
                    for (int i = 1; i < observationLineSplit.Length; i++)
                    {
                        obs.AddValue(currencyNames[i],Convert.ToDouble(observationLineSplit[i]));
                    }
                    observations.Add(obs);
                    observationsLine = reader.ReadLine();
                } while (observationsLine != "");
                
                //Create the response
                _response = new BankOfCanadaResponse(termsURI, series, observations);
                Logger.WriteLine("Updated Fiat Rates from Bank Of Canada");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Gets currency price data from The Bank Of Canada for the current day
        /// </summary>
        public void GetFiatRates()
        {
            GetFiatRates(DateTime.Today, DateTime.Today);
        }

        /// <summary>
        /// Returns the Bank of Canada response
        /// </summary>
        /// <returns>BankOfCanadaResponse</returns>
        public BankOfCanadaResponse GetResponse()
        {
            return _response;
        }
    }
}