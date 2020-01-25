using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using BE;


namespace DAL
{
    partial class DALXML //: IDAL
    {
        #region Singleton
        private static readonly DALXML instance = new DALXML();
        public static DALXML Instance
        {
            get { return instance; }
        }

        private DALXML()
        {
            DownloadBank();
            //open xml files (creates if don't exit) and load items
        }
        static DALXML() { }

        #endregion

        #region banks
        //save banks
        public static volatile bool bankDownloaded=false;//flag if bank was downloaded
        void DownloadBank()
        {
            #region downloadBank
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch (Exception ex)
            {
                try
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    bankDownloaded = true;
                }
                catch(Exception exeption)
                {
                    //tries again if the connection didn't allow to download it
                }
            }
            finally
            {
                wc.Dispose();
            }
            #endregion

        }
        //List<BankAccount> GetBankAccounts()
        //{

        //}
        #endregion

       
    }
}
