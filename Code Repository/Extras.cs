using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using System.IO;
using System.Data;



namespace Code_Repository
{
    class Extras
    {









        private bool DbtoXML(string Location, string DBConnext)
        {
            string dbconn = DBConnext, LOC = Location;
            DataTable MainDt, TempDt;
            DataSet MainDs = new DataSet();
            int j=0, k=0,ver = 0;

            ///this code is to copy all data from a specific SQL database in this case it is the ISIS database
            ///


            if (DBConnext != "")
            {
                dbconn = DBConnext;
            }
            if (Location != "")
            {
                LOC = Location;
            }

            try
            {

                if (SQL.CheckOnline(dbconn))
                {

                    MainDt = SQL.ExecuteDT(@"select * from isis.INFORMATION_SCHEMA.TABLES where table_type <> 'VIEW'", "tables", dbconn);

                    if (MainDt.Rows.Count > 0)
                    {
                        // label1.Text = MainDt.Rows.Count + " Tables found ";
                        //progressBar1.Maximum = MainDt.Rows.Count;
                        //progressBar1.Step = 1 / MainDt.Rows.Count;

                        //Application.DoEvents();

                        foreach (DataRow rw in MainDt.Rows)
                        {
                            //label2.Text = "Getting data for " + rw[2];
                            //progressBar1.Value = ((j + 1) * 100 / MainDt.Rows.Count);
                            //Application.DoEvents();

                            TempDt = SQL.ExecuteDT(@" select * from " + rw[0] + "." + rw[1] + "." + rw[2], rw[2].ToString(), dbconn);
                            
                            MainDs.Tables.Add(TempDt);
                            // progressBar1.PerformStep();
                            j++;
                            k++;

                            if (k > (MainDt.Rows.Count / 10) - 1)
                            {
                                //string dsxml = MainDs.GetXml();

                                using (StreamWriter fs = new StreamWriter(LOC + ver + ".xml"))
                                {
                                    //label2.Text = "writing data to  " + LOC + ver + ".xml";
                                    MainDs.WriteXml(fs);
                                    MainDs = null;
                                    MainDs = new DataSet();
                                }


                                k = 0;
                                ver++;

                            }
                            else if (j == MainDt.Rows.Count)
                            {
                                using (StreamWriter fs = new StreamWriter(LOC + ver + ".xml"))
                                {
                                    //label2.Text = "writing data to  " + LOC + ver + ".xml";
                                    MainDs.WriteXml(fs);
                                    MainDs = null;
                                    MainDs = new DataSet();
                                }
                            }



                        }


                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception exc)
            {

                return false;
                throw exc;
            }

        }
    }
}
