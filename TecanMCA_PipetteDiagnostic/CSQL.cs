using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Threading;

namespace TecanMCA_PipetteDiagnostic
{
    class CSQL
    {
        public static void setPatients(Dictionary<string, CSample> tubeBarcodes)
        {
            string lst = "";
            foreach (string key in tubeBarcodes.Keys)
            {
                lst += ", '" + key + "'";
            }
            lst = "(" + lst.Substring(1) + ")";

            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ibdbase.i-kmb.de;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT bar_code, s.patient_id, sample_id, sc.categ_name_id, case when pc.patient_id is null then 0 else 1 end as control " +
                                                " FROM sample s" +
                                                " JOIN sample_category sc ON s.category_id = sc.category_id" +
                                                " LEFT JOIN patient_iscontrol pc ON s.patient_id = pc.patient_id" +
                                                " WHERE bar_code IN " + lst
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    if (tubeBarcodes.ContainsKey(myReader["bar_code"].ToString()))
                    {
                        tubeBarcodes[myReader["bar_code"].ToString()].setPatientAndSample(int.Parse(myReader["patient_id"].ToString()), int.Parse(myReader["sample_id"].ToString()));
                        tubeBarcodes[myReader["bar_code"].ToString()].setCategory(myReader["categ_name_id"].ToString());
                        if (myReader["control"].ToString() == "1")
                        {
                            tubeBarcodes[myReader["bar_code"].ToString()].setControl(true);
                        }
                        else
                        {
                            tubeBarcodes[myReader["bar_code"].ToString()].setControl(false);
                        }
                    }
                }
            myReader.Close();
        }

        /// <summary>
        ///  updates List<CDBSample> dbEntries with concentration and volume and returns a list of sample_ids that are in the DB layout but have no entry
        ///  for concentration and/or value
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="dbEntries"></param>
        /// <returns></returns>
        public static void addVolAndConc(Dictionary<string, CSample> tubeBarcodes)
        {
            string lst = "";
            foreach (string key in tubeBarcodes.Keys)
            {
                lst += ", " + tubeBarcodes[key].getSample();
            }
            lst = "(" + lst.Substring(1) + ")";

            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT case when sc.sample_id is null then sv.sample_id else sc.sample_id end as sample_id, " +
                                                " isnull(volume,-1) as volume, " +
                                                " isnull(concentration,-1) as concentration " +
                                                " from ( " +
                                                "	select s.sample_id, s.property_value as volume " +
                                                "	from sample_prop_dec s " +
                                                "	JOIN ( " +
                                                "		select sample_id, max(date_entered) as date_entered " +
                                                "		from sample_prop_dec " +
                                                "        where property_id = 2 " +
                                                "		and property_value != -1 " +
                                                "		and sample_id in " + lst + " " +
                                                "		group by sample_id " +
                                                "	) dd on dd.sample_id = s.sample_id and property_id = 2 and dd.date_entered = s.date_entered " +
                                                ") sv " +
                                                "FULL OUTER JOIN( " +
                                                "	select s.sample_id, s.property_value as concentration " +
                                                "	from sample_prop_dec s " +
                                                "	join ( " +
                                                "		select sample_id, max(date_entered) as date_entered " +
                                                "		from sample_prop_dec " +
                                                "        where property_id = 1 " +
                                                "		and property_value != -1 " +
                                                "		and sample_id in " + lst + " " +
                                                "		group by sample_id " +
                                                "	) dd on dd.sample_id = s.sample_id and property_id = 1 and dd.date_entered = s.date_entered " +
                                                ") sc on sc.sample_id = sv.sample_id"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    foreach (string key in tubeBarcodes.Keys)
                    {
                        if (tubeBarcodes[key].getSample() == int.Parse(myReader["sample_id"].ToString()))
                        {
                            tubeBarcodes[key].setVolumeAndConcentration(Convert.ToDouble(myReader["volume"].ToString()), Convert.ToDouble(myReader["concentration"].ToString()));
                        }
                    }
                }
            myReader.Close();
        }

        public static bool checkPlate96wellExist(string plate_barcode)
        {
            String[] tokenized = plate_barcode.Split('-');
            if (tokenized.Length != 3)
            {
                MessageBox.Show("Error, unknown barcode type!");
                return false;
            }

            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("select count(*) as num" +
                                                    " from plate_tracking_location" +
                                                    " where plate_id = (" +
                                                    "    select p.plate_id" +
                                                    "    from plate p" +
                                                    "    join plate_layout pl on p.plate_layout_id = pl.plate_layout_id" +
                                                    "    where p.barcode = '" + plate_barcode + "'" +
                                                    "    and pl.plate_type_id = (select plate_type_id from plate_type where plate_type_name_id = '96DNA')" +
                                                    ")"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkPlate384wellExist(string plate_barcode)
        {
            String[] tokenized = plate_barcode.Split('-');
            if (tokenized.Length != 3)
            {
                MessageBox.Show("Error, unknown barcode type!");
                return false;
            }

            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("select count(*) as num" +
                                                    " from plate_tracking_location" +
                                                    " where plate_id = (" +
                                                    "    select p.plate_id" +
                                                    "    from plate p" +
                                                    "    join plate_layout pl on p.plate_layout_id = pl.plate_layout_id" +
                                                    "    where p.barcode = '" + plate_barcode + "'" +
                                                    "    and pl.plate_type_id = (select plate_type_id from plate_type where plate_type_name_id = '384DNA')" +
                                                    ")"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkNeededSubPlatesExist(string plate384_barcode, string plate96_barcode)
        {
            String[] tokenized384 = plate384_barcode.Split('-');
            if (tokenized384.Length != 3)
            {
                MessageBox.Show("Error, unknown barcode type! (384 well)");
                return false;
            }

            String[] tokenized96 = plate96_barcode.Split('-');
            if (tokenized96.Length != 3)
            {
                MessageBox.Show("Error, unknown barcode type! (96 well)");
                return false;
            }

            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("select count(*) as num" +
                                                    " from plate_layout_sublayout" +
                                                    " WHERE plate_layout_id = (select plate_layout_id from plate_layout where name = '" + tokenized384[1] + "')" +
                                                    " AND sub_layout_id != (select plate_layout_id from plate_layout where name = '" + tokenized96[1] + "')"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool checkMicronicRackExist(string rack_barcode)
        {
            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("select count(*) as num" +
                                                    " from sample_micronic_rack_layout" +
                                                    " where micronic_rack_barcode = '" + rack_barcode + "';"
                                                , myConnection);

            if (int.Parse(myCommand.ExecuteScalar().ToString()) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void loadMicronicLayout(string rack_barcode, Dictionary<string, CSample> tubeBarcodes)
        {
            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT row, col, s.bar_code" +
                                                " FROM sample_micronic_rack_layout m" +
                                                " JOIN sample s on s.sample_id = m.sample_id" + 
                                                " WHERE micronic_rack_barcode = '" + rack_barcode + "'" +
                                                " AND m.sample_id > 0" +
                                                " ORDER BY col, row"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            string well = "";
            if (myReader.HasRows)
                while (myReader.Read())
                {

                    well = ScanReport.getWell(ScanReport.rowToChar(int.Parse(myReader["row"].ToString())), int.Parse(myReader["col"].ToString()));
                    tubeBarcodes[myReader["bar_code"].ToString()] = new CSample("Micronic 1.4ml", myReader["bar_code"].ToString(), -1, ScanReport.RackPosTo96wellNumber(well), well);
                }
            myReader.Close();
        }

        public static void loadLayout(string layout, Dictionary<string, CPlateWell> plateWells)
        {
            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");
            myConnection.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("SELECT r.row_c as row, col, patient_id" +
                                                " FROM plate_layout_patient plp" +
                                                " JOIN plate_row_char2int r on plp.row = r.row_i" +
                                                " WHERE plate_layout_id = (select plate_layout_id from plate_layout where name = '" + layout + "')" +
                                                " AND patient_id NOT IN (" +
                                                "   SELECT patient_id" +
                                                "   FROM patient_iscontrol" +
                                                "   WHERE positive = 0" +
                                                "   AND patient_id != 566977" +
                                                " )" +
                                                " ORDER BY col, row"
                                                , myConnection);

            myReader = myCommand.ExecuteReader();
            string well = "";
            if (myReader.HasRows)
                while (myReader.Read())
                {
                    well = ScanReport.getWell(char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString()));
                    plateWells[well] = new CPlateWell(int.Parse(myReader["patient_id"].ToString()), well, char.Parse(myReader["row"].ToString()), int.Parse(myReader["col"].ToString()));
                }
            myReader.Close();
        }

        public static void addSamplesToPlate(string platform, string layout, Int32 number, Int32 volume, Int32 concentration, Dictionary<string, CPlateWell> plateWells,Boolean reduceVolume)
        {
            SqlConnection myConnection = new SqlConnection("User Id=tecan;" +
                                       "Password=tecan;Data Source=ukshikmb-sw049;" +
                                       "Initial Catalog=ibdbase;");

            myConnection.Open();

            SqlTransaction trans;
            // Start a local transaction.
            trans = myConnection.BeginTransaction("SampleTransaction");

            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT plate_id" +
                                                    " FROM plate" +
                                                    " WHERE plate_layout_id = (select plate_layout_id from plate_layout where name = '" + layout + "')" +
                                                    " AND number = " + number +
                                                    " AND plate_platform_id = (" +
                                                    "   SELECT plate_platform_id" +
                                                    "   FROM plate_platform" +
                                                    "   WHERE plate_platform_name_id = '" + platform + "'" +
                                                    " )"
                                                    , myConnection);
                myCommand.Transaction = trans;
                Int32 plate_id = int.Parse(myCommand.ExecuteScalar().ToString());

                myCommand.CommandText = " INSERT INTO plate_prop_dec (property_id, plate_id, property_value, date_entered, created_by)" +
                                        " VALUES (1," + plate_id + "," + volume + ",getdate(),'tecan')";
                myCommand.ExecuteNonQuery();
                if (concentration > 0)
                {
                    myCommand.CommandText = " INSERT INTO plate_prop_dec (property_id, plate_id, property_value, date_entered, created_by)" +
                                        " VALUES (2," + plate_id + "," + concentration + ",getdate(),'tecan')";
                    myCommand.ExecuteNonQuery();
                }

                if (reduceVolume)
                {
                    foreach (string key in plateWells.Keys)
                    {
                        myCommand.CommandText = " INSERT INTO plate_sample (plate_id, row, col, sample_id)" +
                                            " VALUES (" + plate_id + "," + ScanReport.rowToNumber(plateWells[key].getRow()).ToString() + "," + plateWells[key].getCol() + "," + plateWells[key].getSample() + ")";
                        myCommand.ExecuteNonQuery();
                        using (SqlCommand cmd = new SqlCommand("spu_sample_reduce_volume", myConnection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sample_id", SqlDbType.Int).Value = plateWells[key].getSample();
                            cmd.Parameters.Add("@used_vol", SqlDbType.Decimal, 10).Value = plateWells[key].getSampleVol();
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            Thread.Sleep(50);
                        }
                    }
                }
                
                using (SqlCommand cmd = new SqlCommand("spu_plate_track_location_by_plate_id", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@plate_id", SqlDbType.Int).Value = plate_id;
                    cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = 24;
                    cmd.Parameters.Add("@done_by", SqlDbType.VarChar, 25).Value = "tecan";
                    cmd.Parameters.Add("@tracking_location_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                trans.Commit();
                myCommand.Dispose();
            }
            catch (Exception e)
            {
                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Rollback Exception Type: " + ex2.GetType() + "\r\n" + ex2.Message);
                }
                MessageBox.Show("Error writing to IBDbase:\r\n" + e.Message);
            }

            trans.Dispose();
            myConnection.Close();
        }
    }
}
