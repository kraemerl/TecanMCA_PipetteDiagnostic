using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TecanMCA_PipetteDiagnostic
{
    class WriteData
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// 
        /// </summary>
        private int m_retVal = 2;

        private Int32 m_normalize = 0; // boolean: 0 or 1
        private string m_plate_96well = "";
        private string m_plate_384well_1 = "";
        private string m_plate_384well_2 = "";
        private string m_plate_96well_platform = "";
        private string m_plate_96well_layout = "";
        private Int32 m_plate_96well_number = 0;
        private string m_plate_384well_1_platform = "";
        private string m_plate_384well_1_layout = "";
        private Int32 m_plate_384well_1_number = 0;
        private string m_plate_384well_2_platform = "";
        private string m_plate_384well_2_layout = "";
        private Int32 m_plate_384well_2_number = 0;
        private Int32 m_target_96well_volume = 0; // in ng/µl
        private Int32 m_target_96well_concentration = 0; // in µl
        private Int32 m_target_384well_volume = 0; // in µl

        private Dictionary<string, CPlateWell> new96WellDict = new Dictionary<string, CPlateWell>();
        private Dictionary<string, CPlateWell> new384WellDict = new Dictionary<string, CPlateWell>();

        private string[] m_args;

        public WriteData(string[] args)
        {
            m_args = args;
            try
            {
                loadVariables(m_args[1]);
                backupFiles();
                loadLastJobFile(m_args[2]);
                writeToIBDbase();
                m_retVal = 0;
            }
            catch (Exception e)
            {
                m_retVal = 2;
                MessageBox.Show(e.Message);
                Application.Exit();
            }
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void backupFiles()
        {
            if (Directory.Exists(m_args[4]) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(m_args[4]);
            }

            string prefix = m_plate_96well + "_";
            File.Copy(m_args[1], prefix + "variables.txt", true);
            File.Copy(m_args[2], prefix + "jobInfo.txt", true);
            File.Copy(m_args[3], prefix + "worklist.txt", true);
        }

        private void deleteFiles()
        {
            File.Delete(m_args[1]);
            File.Delete(m_args[2]);
            File.Delete(m_args[3]);
        }

        private void loadLastJobFile(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    if ((line = streamReader.ReadLine()) != null)
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            String[] tokenized = line.Split('\t');
                            for (int i = 0; i < tokenized.Length; i++)
                                tokenized[i] = tokenized[i].Trim(' ');
                            if (tokenized[0] != "source" && tokenized[0] != "")
                            {
                                new96WellDict[tokenized[8]] = new CPlateWell(int.Parse(tokenized[1]), int.Parse(tokenized[2]), tokenized[8], tokenized[8][0], int.Parse(tokenized[8].Substring(1)));
                                new96WellDict[tokenized[8]].setSampleVol(Convert.ToDouble(tokenized[10]));

                                for (int i = 1; i <= 4; i++) {
                                    String well = ScanReport.get384Well(tokenized[8], i);
                                    new384WellDict[well] = new CPlateWell(int.Parse(tokenized[1]), int.Parse(tokenized[2]), well, well[0], int.Parse(well.Substring(1)));
                                    new384WellDict[well].setSampleVol(Convert.ToDouble(tokenized[10]));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error importing last job file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void loadVariables(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        String[] tokenized = line.Split(',');
                        if (tokenized.Length != 3)
                        {
                            throw new Exception("Invalid entries in import variables file: \"" + file + "\" with entries:\r\n" + line);
                        }
                        else
                        {
                            if (tokenized[1] == "plate_96well")
                            {
                                m_plate_96well = tokenized[2].Trim();
                                String[] plate_tokenized = m_plate_96well.Split('-');
                                m_plate_96well_platform = plate_tokenized[0];
                                m_plate_96well_layout = plate_tokenized[1];
                                m_plate_96well_number = int.Parse(plate_tokenized[2]);
                            }
                            else if (tokenized[1] == "plate_384well_1")
                            {
                                m_plate_384well_1 = tokenized[2].Trim();
                                String[] plate_tokenized = m_plate_384well_1.Split('-');
                                m_plate_384well_1_platform = plate_tokenized[0];
                                m_plate_384well_1_layout = plate_tokenized[1];
                                m_plate_384well_1_number = int.Parse(plate_tokenized[2]);
                            }
                            else if (tokenized[1] == "plate_384well_2")
                            {
                                m_plate_384well_2 = tokenized[2].Trim();
                                String[] plate_tokenized = m_plate_384well_2.Split('-');
                                m_plate_384well_2_platform = plate_tokenized[0];
                                m_plate_384well_2_layout = plate_tokenized[1];
                                m_plate_384well_2_number = int.Parse(plate_tokenized[2]);
                            }
                            else if (tokenized[1] == "target_volume")
                            {
                                m_target_96well_volume = int.Parse(tokenized[2].Trim());
                            }
                            else if (tokenized[1] == "target_concentration")
                            {
                                m_target_96well_concentration = int.Parse(tokenized[2].Trim());
                            }
                            else if (tokenized[1] == "normalize")
                            {
                                m_normalize = int.Parse(tokenized[2].Trim());
                                if (m_normalize == 0)
                                {
                                    m_target_96well_concentration = 0;
                                }
                            }
                        }
                    }
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error importing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error importing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void writeToIBDbase()
        {
            try
            {
                CSQL.addSamplesToPlate(
                    m_plate_96well_platform,
                    m_plate_96well_layout,
                    m_plate_96well_number,
                    m_target_96well_volume,
                    m_target_96well_concentration,
                    new96WellDict
                );
                if (m_plate_384well_1 != "")
                {
                    CSQL.addSamplesToPlate(
                        m_plate_384well_1_platform,
                        m_plate_384well_1_layout,
                        m_plate_384well_1_number,
                        m_target_384well_volume,
                        m_target_96well_concentration,
                        new384WellDict
                    );
                }
                if (m_plate_384well_2 != "")
                {
                    CSQL.addSamplesToPlate(
                        m_plate_384well_2_platform,
                        m_plate_384well_2_layout,
                        m_plate_384well_2_number,
                        m_target_384well_volume,
                        m_target_96well_concentration,
                        new384WellDict
                    );
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error while writing to IBDbase:" + e.Message);
            }
        }
    }
}
