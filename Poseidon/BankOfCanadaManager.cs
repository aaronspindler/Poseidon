using System;
using System.IO;
using System.Net;

namespace Poseidon
{
    public class BankOfCanadaManager
    {
        public void GetBankOfCanadaData()
                {
                    var client = new WebClient();
                    try
                    {
                        var data = client.OpenRead("https://www.bankofcanada.ca/valet/observations/FXAUDCAD,FXBRLCAD,FXCNYCAD,FXEURCAD,FXHKDCAD,FXINRCAD,FXIDRCAD,FXJPYCAD,FXMYRCAD,FXMXNCAD,FXNZDCAD,FXNOKCAD,FXPENCAD,FXRUBCAD,FXSARCAD,FXSGDCAD,FXZARCAD,FXKRWCAD,FXSEKCAD,FXCHFCAD,FXTWDCAD,FXTHBCAD,FXTRYCAD,FXGBPCAD,FXUSDCAD,FXVNDCAD/csv?start_date=2017-01-02&end_date=2019-02-07");
                        var reader = new StreamReader(data);
        
                        while (!reader.EndOfStream)
                        {
                            
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
    }
}