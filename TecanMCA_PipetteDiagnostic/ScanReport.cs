using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace TecanMCA_PipetteDiagnostic
{
    public partial class ScanReport : Form
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// 
        /// scan_report C:\Lars\Git\C#_projects\TecanMCA_PipetteDiagnostic\Data\variables.csv C:\Lars\Git\C#_projects\TecanMCA_PipetteDiagnostic\Data\source_eppi_barcode.csv C:\Lars\Git\C#_projects\TecanMCA_PipetteDiagnostic\Data\lastJobInfo.txt C:\Lars\Git\C#_projects\TecanMCA_PipetteDiagnostic\Data\worklist.gwl
        /// </summary>
        private int m_retVal = 2;

        private double m_max_diluter_volume = 400.0;

        private Int32 m_normalize = 1;
        private Int32 m_target_96well_volume = 0; // in ng/µl
        private Int32 m_target_96well_concentration = 0; // in µl
        private Int32 m_target_384well_volume = 0; // in µl
        private Int32 m_eppi_racks_to_scan = 0; // number of eppi racks to scan
        private Int32 m_micronic_rack_used = 0; // boolean: 0 or 1
        private Int32 m_plate_384well_count = 0; // number of 384well plates to create
        private string m_plate_96well = "";
        private string m_plate_384well_1 = "";
        private string m_plate_384well_2 = "";
        private string m_plate_384well_3 = "";
        private string m_micronic_rack_barcode = "";
        private Dictionary<string, CSample> sourceBarcodesDict = new Dictionary<string, CSample>();
        private Dictionary<string, CPlateWell> destinationWellDict = new Dictionary<string, CPlateWell>();

        private string[] m_args;

        public ScanReport(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void ScanReport_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_args.Length != 5)
                {
                    throw new Exception("Invalid argument list. Need exactly 4 parameters: \r\n" +
                                     "1. Variable file path,\r\n" +
                                     "2. Eppi 2ml Barcode file path,\r\n" +
                                     "3. JobInfo output file path,\r\n" +
                                     "4. Worklist output file path,\r\n");
                }
                loadVariables(m_args[1]);
                loadSourceBarcodes();
                loadDestinationLayout();
                getTransferVolumes();

                bool all_okay = showTransferOverview();

                if (all_okay)
                {
                    butContinue.Enabled = true;
                }
            }
            catch (Exception exc)
            {
                m_retVal = 2;
                MessageBox.Show("Error initializing dialog:\r\n" + exc.Message + "\r\n\r\nClosing application ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void loadVariables(string file)
        {
            try
            {
                lblSampleSource.Text = "";
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
                            if (tokenized[1] == "eppi_racks_to_scan")
                            {
                                m_eppi_racks_to_scan = int.Parse(tokenized[2].Trim());
                                if (m_eppi_racks_to_scan >= 1)
                                {
                                    lblSampleSource.Text += "Eppi 2ml; ";
                                }
                                
                            }
                            else if (tokenized[1] == "micronic_rack_used")
                            {
                                m_micronic_rack_used = int.Parse(tokenized[2].Trim());
                                if (m_micronic_rack_used == 1)
                                {
                                    lblSampleSource.Text += "Micronic Tube 1.4ml; ";
                                }
                            }
                            else if (tokenized[1] == "micronic_rack_barcode")
                            {
                                m_micronic_rack_barcode = tokenized[2].Trim();
                            }
                            else if (tokenized[1] == "plate_96well")
                            {
                                m_plate_96well = tokenized[2].Trim();
                                if (m_plate_96well != "")
                                {
                                    lblSampleDestination.Text = m_plate_96well + "; ";
                                }
                            }
                            else if (tokenized[1] == "plate_384well_count")
                            {
                                m_plate_384well_count = int.Parse(tokenized[2].Trim());
                            }
                            else if (tokenized[1] == "plate_384well_1")
                            {
                                m_plate_384well_1 = tokenized[2].Trim();
                                if (m_plate_384well_1 != "")
                                {
                                    lblSampleDestination.Text += m_plate_384well_1 + "; ";
                                }
                            }
                            else if (tokenized[1] == "plate_384well_2")
                            {
                                m_plate_384well_2 = tokenized[2].Trim();
                                if (m_plate_384well_2 != "")
                                {
                                    lblSampleDestination.Text += m_plate_384well_2 + "; ";
                                }
                            }
                            else if (tokenized[1] == "plate_384well_3")
                            {
                                m_plate_384well_3 = tokenized[2].Trim();
                                if (m_plate_384well_3 != "")
                                {
                                    lblSampleDestination.Text += m_plate_384well_3 + "; ";
                                }
                            }
                            else if (tokenized[1] == "target_96well_volume")
                            {
                                m_target_96well_volume = int.Parse(tokenized[2].Trim());
                                lblTarget96Volume.Text = m_target_96well_volume.ToString();
                            }
                            else if (tokenized[1] == "target_96well_concentration")
                            {
                                m_target_96well_concentration = int.Parse(tokenized[2].Trim());
                                lblTarget96Concentration.Text = m_target_96well_concentration.ToString();
                            }
                            else if (tokenized[1] == "target_384well_volume")
                            {
                                m_target_384well_volume = int.Parse(tokenized[2].Trim());
                                lblTarget384Volume.Text = m_target_384well_volume.ToString();
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

        private void loadSourceBarcodes()
        {
            if (m_eppi_racks_to_scan >= 1)
            {
                readEppendorfScanFile(m_args[2], sourceBarcodesDict);
            }
            if (m_micronic_rack_used == 1)
            {
                CSQL.loadMicronicLayout(m_micronic_rack_barcode, sourceBarcodesDict);
            }

            if (sourceBarcodesDict.Count() <= 0)
            {
                m_retVal = 2;
                throw new Exception("No source samples defined");
            }
            else
            {
                CSQL.setPatients(sourceBarcodesDict);
                CSQL.addVolAndConc(sourceBarcodesDict);
            }
        }

        private void loadDestinationLayout()
        {
            String[] tokenized = m_plate_96well.Split('-');
            if (tokenized.Length != 3)
            {
                MessageBox.Show("Error, unknown barcode type! (96 well)");
            }
            else
            {
                CSQL.loadLayout(tokenized[1], destinationWellDict);
            }
            
        }

        private bool showTransferOverview()
        {
            bool all_okay = true;
            string sample_barcode = "";
            foreach (string key in destinationWellDict.Keys)
            {
                sample_barcode = destinationWellDict[key].getSampleBarcode();
                DataGridViewRow row = (DataGridViewRow)grdTransferOverview.Rows[0].Clone();
                if (sample_barcode == "NC")
                {
                    row.Cells[0].Style.BackColor = Color.Gray;
                    row.Cells[1].Style.BackColor = Color.Gray;
                    row.Cells[2].Style.BackColor = Color.Gray;
                    row.Cells[3].Style.BackColor = Color.Gray;
                    row.Cells[4].Style.BackColor = Color.Gray;
                    row.Cells[5].Style.BackColor = Color.Gray;
                    row.Cells[6].Style.BackColor = Color.Gray;
                    row.Cells[7].Style.BackColor = Color.Gray;
                }
                else if (sample_barcode != "")
                {
                    row.Cells[0].Value = sourceBarcodesDict[sample_barcode].getSource();
                    row.Cells[1].Value = sourceBarcodesDict[sample_barcode].getPatient();
                    if (sourceBarcodesDict[sample_barcode].getPatient() == -1)
                    {
                        row.Cells[1].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[2].Value = sourceBarcodesDict[sample_barcode].getSample();
                    if (sourceBarcodesDict[sample_barcode].getSample() == -1)
                    {
                        row.Cells[2].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[3].Value = sourceBarcodesDict[sample_barcode].getBarcode();
                    if (sourceBarcodesDict[sample_barcode].getBarcode() == "***" || sourceBarcodesDict[sample_barcode].getBarcode() == "$$$")
                    {
                        row.Cells[3].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[4].Value = sourceBarcodesDict[sample_barcode].getCategory();
                    row.Cells[5].Value = sourceBarcodesDict[sample_barcode].isControl();
                    row.Cells[6].Value = sourceBarcodesDict[sample_barcode].getVolume();
                    if (sourceBarcodesDict[sample_barcode].getVolume() <= 0)
                    {
                        row.Cells[6].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                    row.Cells[7].Value = sourceBarcodesDict[sample_barcode].getConcentration();
                    if (sourceBarcodesDict[sample_barcode].getConcentration() <= 0)
                    {
                        row.Cells[7].Style.BackColor = Color.Red;
                        if (m_normalize == 1)
                        {
                            all_okay = false;
                        }
                    }
                    else if (sourceBarcodesDict[sample_barcode].getConcentration() < m_target_96well_concentration)
                    {
                        row.Cells[7].Style.BackColor = Color.Orange;
                    }
                }
                else
                {
                    row.Cells[0].Style.BackColor = Color.Red;
                    row.Cells[1].Style.BackColor = Color.Red;
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[3].Style.BackColor = Color.Red;
                    row.Cells[4].Style.BackColor = Color.Red;
                    row.Cells[5].Style.BackColor = Color.Red;
                    row.Cells[6].Style.BackColor = Color.Red;
                    row.Cells[7].Style.BackColor = Color.Red;
                    all_okay = false;
                }

                row.Cells[8].Value = key;
                row.Cells[9].Value = destinationWellDict[key].getPatient();
                row.Cells[10].Value = destinationWellDict[key].getSampleVol();
                if (destinationWellDict[key].getSampleVol() < 4)
                {
                    if (sample_barcode != "NC")
                    {
                        row.Cells[10].Style.BackColor = Color.Red;
                        all_okay = false;
                    }
                }
                row.Cells[11].Value = destinationWellDict[key].getBufferVol();

                grdTransferOverview.Rows.Add(row);
            }

            grdTransferOverview.AllowUserToAddRows = false;
            return all_okay;
        }

        private void getTransferVolumes()
        {
            double bufferDWVol = 0;
            double sampleDWVol = 0;
            double min_sample_vol = 0;
            double DWVol = m_target_96well_volume;
            double DWConc = 0;
            double bufferVol = 0;
            double sampleVol = 0;
            string DWBarcode = "";
            Int32 DWWell = 0;

            foreach (string dkey in destinationWellDict.Keys)
            {
                if (destinationWellDict[dkey].getPatient() == 566977)
                {
                    destinationWellDict[dkey].setSampleVol(0);
                    destinationWellDict[dkey].setBufferVol(m_target_96well_volume);
                    destinationWellDict[dkey].setSampleBarcode("NC");
                }
                else
                {
                    foreach (string skey in sourceBarcodesDict.Keys)
                    {
                        if (destinationWellDict[dkey].getPatient() == sourceBarcodesDict[skey].getPatient())
                        {
                            if (destinationWellDict[dkey].getSampleBarcode() == "")
                            {
                                if (m_normalize == 1)
                                {
                                    // destinationWellDict[dkey].setDWConc(m_target_96well_concentration * m_target_96well_volume / 5);

                                    //if (destinationWellDict[dkey].getDWConc() >= sourceBarcodesDict[skey].getConcentration())
                                    //{
                                    //    destinationWellDict[dkey].setDWSampleVol(DWVol);
                                    //    destinationWellDict[dkey].setDWBufferVol(0);
                                    //}
                                    //else
                                    //{
                                    //    sampleDWVol = Math.Round(DWVol / sourceBarcodesDict[skey].getConcentration() * destinationWellDict[dkey].getDWConc());
                                    //    if (sampleDWVol < 5)
                                    //    {
                                    //        sampleDWVol = 5;
                                    //        DWVol = Math.Round(sampleDWVol * sourceBarcodesDict[skey].getConcentration() / destinationWellDict[dkey].getDWConc());
                                    //    }

                                    //    bufferDWVol = DWVol - sampleDWVol;
                                    //    destinationWellDict[dkey].setDWSampleVol(sampleDWVol);
                                    //    destinationWellDict[dkey].setDWBufferVol(bufferDWVol);
                                    //}

                                    min_sample_vol = Math.Round(m_target_96well_concentration * m_target_96well_volume / sourceBarcodesDict[skey].getConcentration());

                                    if (m_target_96well_concentration >= sourceBarcodesDict[skey].getConcentration())
                                    {
                                        destinationWellDict[dkey].setSampleVol(m_target_96well_volume);
                                        destinationWellDict[dkey].setBufferVol(0);
                                        destinationWellDict[dkey].setSampleBarcode(skey);
                                    }
                                    else if (min_sample_vol >= 5 && min_sample_vol <= m_target_96well_volume)
                                    {
                                        destinationWellDict[dkey].setSampleVol(min_sample_vol);
                                        destinationWellDict[dkey].setBufferVol(m_target_96well_volume - min_sample_vol);
                                        destinationWellDict[dkey].setSampleBarcode(skey);
                                    }
                                    else
                                    {
                                        DWBarcode = "DW_" + skey;
                                        DWWell += 1;
                                        DWConc = m_target_96well_concentration * m_target_96well_volume / 5;
                                        DWVol = m_target_96well_volume;

                                        if (!sourceBarcodesDict.ContainsKey(DWBarcode))
                                        {
                                            sourceBarcodesDict[DWBarcode] = new CSample("DW96well", skey, -1, DWWell, WellNumberToRackPos(DWWell));
                                            sourceBarcodesDict[DWBarcode].setPatientAndSample(sourceBarcodesDict[skey].getPatient(), sourceBarcodesDict[skey].getSample());
                                            sourceBarcodesDict[DWBarcode].setCategory(sourceBarcodesDict[skey].getCategory());
                                            sourceBarcodesDict[DWBarcode].setConcentration(DWConc);
                                            sourceBarcodesDict[DWBarcode].setVolume(0);
                                            DWVol += 5;
                                        }

                                        sampleDWVol = Math.Round(DWVol / sourceBarcodesDict[skey].getConcentration() * DWConc);
                                        if (sampleDWVol < 5)
                                        {
                                            sampleDWVol = 5;
                                            DWVol = Math.Round(sampleDWVol * sourceBarcodesDict[skey].getConcentration() / DWConc);
                                        }

                                        bufferDWVol = DWVol - sampleDWVol;
                                        sourceBarcodesDict[DWBarcode].setVolume(sourceBarcodesDict[DWBarcode].getVolume() + DWVol);


                                        sourceBarcodesDict[DWBarcode].setDWSampleVol(sourceBarcodesDict[DWBarcode].getDWSampleVol() + sampleDWVol);
                                        sourceBarcodesDict[DWBarcode].setDWBufferVol(sourceBarcodesDict[DWBarcode].getDWBufferVol() + bufferDWVol);

                                        destinationWellDict[dkey].setSampleBarcode(DWBarcode);


                                        sampleVol = Math.Round(m_target_96well_volume / DWConc * m_target_96well_concentration);
                                        bufferVol = m_target_96well_volume - sampleVol;
                                        destinationWellDict[dkey].setSampleVol(sampleVol);
                                        destinationWellDict[dkey].setBufferVol(bufferVol);
                                    }
                                }
                                else
                                {
                                    destinationWellDict[dkey].setSampleVol(m_target_96well_volume);
                                    destinationWellDict[dkey].setBufferVol(0);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void readEppendorfScanFile(string file, Dictionary<string, CSample> tubeBarcodes)
        {
            try
            {
                Dictionary<String, String> duplicateEppendorfBarcodes = new Dictionary<string, string>();
                Dictionary<string, string> origEntry = new Dictionary<string, string>();

                int idx_mod = 1;
                var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    if ((line = streamReader.ReadLine()) != null)
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            String[] tokenized = line.Split(';');
                            for (int i = 0; i < tokenized.Length; i++)
                                tokenized[i] = tokenized[i].Trim(' ');
                            if (tokenized.Length == 7)
                            {
                                string barcode = tokenized[6];
                                if (barcode.CompareTo("***") == 0)
                                    continue;
                                int rack = Convert.ToInt32(tokenized[0]);
                                int well = Convert.ToInt32(tokenized[2]);
                                int idx = (rack - idx_mod) * 16 + well;

                                if (duplicateEppendorfBarcodes.Keys.Contains(barcode))
                                    throw new Exception("Duplicate barcode in scan file.\r\nOld entry: " + duplicateEppendorfBarcodes[barcode] + "\r\nNew entry: " + line);
                                else
                                    duplicateEppendorfBarcodes.Add(barcode, line);
                                tubeBarcodes[barcode] = new CSample("Eppi 2ml", barcode, rack, well, "");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_retVal = 2;
                throw new Exception("Error importing eppi barcode file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void writeJobFile(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("source\tpatient_id\tsample_id\tsource BC\tcategory\tcontrol\tvolume (µl)\tconcentration (ng/µl)\tdestination well\tpatient_id\tsample (µl)\tTE (µl)\t\r\n");
                    foreach (DataGridViewRow row in grdTransferOverview.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            streamWriter.Write(cell.Value + "\t");
                        }
                        streamWriter.Write("\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private Boolean writeWorklistFile(string file)
        {

            string sourceLabwareName = "";
            string sourceLabwareType = "";

            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    DateTime localDate = DateTime.Now;
                    int tip = 0;
                    double volume = 0.0;
                    double mixVolume = 0.0;
                    double maxAspirate = m_max_diluter_volume;

                    streamWriter.WriteLine("C; This GWL was generated by TecanEvo_MultilabwareNormalization at " + localDate);
                    streamWriter.WriteLine("B;");
                    streamWriter.WriteLine("C; DNA Source: " + lblSampleSource.Text.Trim() + ", DNA Destination: " + m_plate_96well);
                    streamWriter.WriteLine("B;");

                    if (m_normalize == 1)
                    {
                        streamWriter.WriteLine("C; pipetting DW Buffer");
                        streamWriter.WriteLine("B;");
                        foreach (string key in sourceBarcodesDict.Keys)
                        {
                            if (sourceBarcodesDict[key].getSource() == "DW96well")
                            {
                                volume = sourceBarcodesDict[key].getDWBufferVol();

                                if (volume > 0)
                                {
                                    if (volume <= maxAspirate)
                                    {
                                        streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        streamWriter.WriteLine("D;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        streamWriter.WriteLine("W3;");
                                    }
                                    else
                                    {
                                        double readwriteVolume = volume;
                                        double actPipetVolume = maxAspirate;

                                        while (readwriteVolume > 0)
                                        {
                                            streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                            streamWriter.WriteLine("D;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                            readwriteVolume -= actPipetVolume;
                                            if (readwriteVolume > maxAspirate)
                                                actPipetVolume = maxAspirate;
                                            else
                                                actPipetVolume = readwriteVolume;
                                        }
                                        streamWriter.WriteLine("W3;");
                                    }
                                }
                                if (++tip == 4)
                                {
                                    tip = 0;
                                    streamWriter.WriteLine("B;");
                                }
                            }
                        }

                        streamWriter.WriteLine("C; pipetting Buffer");
                        streamWriter.WriteLine("B;");
                        foreach (string key in destinationWellDict.Keys)
                        {
                            volume = destinationWellDict[key].getBufferVol();

                            if (volume > 0)
                            {
                                if (volume <= maxAspirate)
                                {
                                    streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    streamWriter.WriteLine("D;PCR96well;;96well PCR in Metalladapter;" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    streamWriter.WriteLine("W3;");
                                }
                                else
                                {
                                    double readwriteVolume = volume;
                                    double actPipetVolume = maxAspirate;

                                    while (readwriteVolume > 0)
                                    {
                                        streamWriter.WriteLine("A;Buffer;;Trough 100ml;" + (tip + 1) + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        streamWriter.WriteLine("D;PCR96well;;96well PCR in Metalladapter;" + RackPosTo96wellNumber(key) + ";;" + actPipetVolume.ToString("R", CultureInfo.InvariantCulture) + ";DNA Transfer Job Buffer;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                        readwriteVolume -= actPipetVolume;
                                        if (readwriteVolume > maxAspirate)
                                            actPipetVolume = maxAspirate;
                                        else
                                            actPipetVolume = readwriteVolume;
                                    }
                                    streamWriter.WriteLine("W3;");
                                }
                            }
                            if (++tip == 4)
                            {
                                tip = 0;
                                streamWriter.WriteLine("B;");
                            }
                        }
                    }
                    streamWriter.WriteLine("B;");
                    tip = 0;

                    streamWriter.WriteLine("C; pipetting DW DNA");
                    streamWriter.WriteLine("B;");
                    foreach (string key in sourceBarcodesDict.Keys)
                    {
                        if (sourceBarcodesDict[key].getSource() == "DW96well")
                        {
                            volume = sourceBarcodesDict[key].getDWSampleVol();


                            sourceLabwareName = sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getSource();
                            sourceLabwareType = getLabwareType(sourceLabwareName);

                            if (volume <= maxAspirate)
                            {
                                if (sourceLabwareName == "Eppi 2ml")
                                {
                                    streamWriter.WriteLine("A;" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                else
                                {
                                    streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                streamWriter.WriteLine("D;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            }
                            else
                            {
                                double readwriteVolume = volume;
                                double actPipetVolume = maxAspirate;

                                while (readwriteVolume > 0)
                                {
                                    if (sourceLabwareName == "Eppi 2ml")
                                    {
                                        streamWriter.WriteLine("A;" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    }
                                    else
                                    {
                                        streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[sourceBarcodesDict[key].getBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    }
                                    streamWriter.WriteLine("D;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    readwriteVolume -= actPipetVolume;
                                    if (readwriteVolume > maxAspirate)
                                        actPipetVolume = maxAspirate;
                                    else
                                        actPipetVolume = readwriteVolume;
                                }
                            }
                            // Mix
                            mixVolume = sourceBarcodesDict[key].getVolume();
                            if (mixVolume > m_max_diluter_volume)
                            {
                                mixVolume = m_max_diluter_volume;
                            }
                            streamWriter.WriteLine("A;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + mixVolume + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            streamWriter.WriteLine("D;DW96well;;96 Well HalfDeepWell;" + sourceBarcodesDict[key].getRackPos() + ";;" + mixVolume + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            streamWriter.WriteLine("W;");

                            if (++tip == 4)
                            {
                                tip = 0;
                                streamWriter.WriteLine("B;");
                                for (int tipWash = 0; tipWash < 4; tipWash++)
                                {
                                    streamWriter.WriteLine("A;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                    streamWriter.WriteLine("D;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                    streamWriter.WriteLine("W2;");
                                }
                                streamWriter.WriteLine("B;");
                            }
                        }
                    }

                    streamWriter.WriteLine("C; pipetting DNA");
                    streamWriter.WriteLine("B;");
                    foreach (string key in destinationWellDict.Keys)
                    {
                        if (destinationWellDict[key].getSampleBarcode() != "NC")
                        {
                            volume = destinationWellDict[key].getSampleVol();
                            sourceLabwareName = sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getSource();
                            sourceLabwareType = getLabwareType(sourceLabwareName);

                            if (volume <= maxAspirate)
                            {
                                if (sourceLabwareName == "Eppi 2ml")
                                {
                                    streamWriter.WriteLine("A;" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                else
                                {
                                    streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                }
                                streamWriter.WriteLine("D;PCR96well;;96well PCR in Metalladapter;" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                            }
                            else
                            {
                                double readwriteVolume = volume;
                                double actPipetVolume = maxAspirate;

                                while (readwriteVolume > 0)
                                {
                                    if (sourceLabwareName == "Eppi 2ml")
                                    {
                                        streamWriter.WriteLine("A;" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getEppieRack() + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    }
                                    else
                                    {
                                        streamWriter.WriteLine("A;" + sourceLabwareName + ";;" + sourceLabwareType + ";" + sourceBarcodesDict[destinationWellDict[key].getSampleBarcode()].getRackPos() + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    }
                                    streamWriter.WriteLine("D;PCR96well;;96well PCR in Metalladapter;" + RackPosTo96wellNumber(key) + ";;" + volume.ToString("R", CultureInfo.InvariantCulture) + ";DNA;;" + Convert.ToInt16(Math.Pow(2, tip)) + ";;;");
                                    readwriteVolume -= actPipetVolume;
                                    if (readwriteVolume > maxAspirate)
                                        actPipetVolume = maxAspirate;
                                    else
                                        actPipetVolume = readwriteVolume;
                                }
                            }
                            streamWriter.WriteLine("W;");

                            if (++tip == 4)
                            {
                                tip = 0;
                                streamWriter.WriteLine("B;");
                                for (int tipWash = 0; tipWash < 4; tipWash++)
                                {
                                    streamWriter.WriteLine("A;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                    streamWriter.WriteLine("D;NaClO;;Trough 100ml;" + (tipWash + 1) + ";;" + maxAspirate.ToString("R", CultureInfo.InvariantCulture) + ";Decon Mix In Trough Detect;;" + Convert.ToInt16(Math.Pow(2, tipWash)) + ";;;");
                                    streamWriter.WriteLine("W2;");
                                }
                                streamWriter.WriteLine("B;");
                            }
                        }
                        
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                return false;
            }
        }

        public static int RackPosTo96wellNumber(string well)
        {
            switch (well)
            {
                case "A01": return 1;
                case "B01": return 2;
                case "C01": return 3;
                case "D01": return 4;
                case "E01": return 5;
                case "F01": return 6;
                case "G01": return 7;
                case "H01": return 8;
                case "A02": return 9;
                case "B02": return 10;
                case "C02": return 11;
                case "D02": return 12;
                case "E02": return 13;
                case "F02": return 14;
                case "G02": return 15;
                case "H02": return 16;
                case "A03": return 17;
                case "B03": return 18;
                case "C03": return 19;
                case "D03": return 20;
                case "E03": return 21;
                case "F03": return 22;
                case "G03": return 23;
                case "H03": return 24;
                case "A04": return 25;
                case "B04": return 26;
                case "C04": return 27;
                case "D04": return 28;
                case "E04": return 29;
                case "F04": return 30;
                case "G04": return 31;
                case "H04": return 32;
                case "A05": return 33;
                case "B05": return 34;
                case "C05": return 35;
                case "D05": return 36;
                case "E05": return 37;
                case "F05": return 38;
                case "G05": return 39;
                case "H05": return 40;
                case "A06": return 41;
                case "B06": return 42;
                case "C06": return 43;
                case "D06": return 44;
                case "E06": return 45;
                case "F06": return 46;
                case "G06": return 47;
                case "H06": return 48;
                case "A07": return 49;
                case "B07": return 50;
                case "C07": return 51;
                case "D07": return 52;
                case "E07": return 53;
                case "F07": return 54;
                case "G07": return 55;
                case "H07": return 56;
                case "A08": return 57;
                case "B08": return 58;
                case "C08": return 59;
                case "D08": return 60;
                case "E08": return 61;
                case "F08": return 62;
                case "G08": return 63;
                case "H08": return 64;
                case "A09": return 65;
                case "B09": return 66;
                case "C09": return 67;
                case "D09": return 68;
                case "E09": return 69;
                case "F09": return 70;
                case "G09": return 71;
                case "H09": return 72;
                case "A10": return 73;
                case "B10": return 74;
                case "C10": return 75;
                case "D10": return 76;
                case "E10": return 77;
                case "F10": return 78;
                case "G10": return 79;
                case "H10": return 80;
                case "A11": return 81;
                case "B11": return 82;
                case "C11": return 83;
                case "D11": return 84;
                case "E11": return 85;
                case "F11": return 86;
                case "G11": return 87;
                case "H11": return 88;
                case "A12": return 89;
                case "B12": return 90;
                case "C12": return 91;
                case "D12": return 92;
                case "E12": return 93;
                case "F12": return 94;
                case "G12": return 95;
                case "H12": return 96;
                default: return -1;
            }
        }

        public static string WellNumberToRackPos(Int32 well)
        {
            switch (well)
            {
                case 1: return "A01";
                case 2: return "B01";
                case 3: return "C01";
                case 4: return "D01";
                case 5: return "E01";
                case 6: return "F01";
                case 7: return "G01";
                case 8: return "H01";
                case 9: return "A02";
                case 10: return "B02";
                case 11: return "C02";
                case 12: return "D02";
                case 13: return "E02";
                case 14: return "F02";
                case 15: return "G02";
                case 16: return "H02";
                case 17: return "A03";
                case 18: return "B03";
                case 19: return "C03";
                case 20: return "D03";
                case 21: return "E03";
                case 22: return "F03";
                case 23: return "G03";
                case 24: return "H03";
                case 25: return "A04";
                case 26: return "B04";
                case 27: return "C04";
                case 28: return "D04";
                case 29: return "E04";
                case 30: return "F04";
                case 31: return "G04";
                case 32: return "H04";
                case 33: return "A05";
                case 34: return "B05";
                case 35: return "C05";
                case 36: return "D05";
                case 37: return "E05";
                case 38: return "F05";
                case 39: return "G05";
                case 40: return "H05";
                case 41: return "A06";
                case 42: return "B06";
                case 43: return "C06";
                case 44: return "D06";
                case 45: return "E06";
                case 46: return "F06";
                case 47: return "G06";
                case 48: return "H06";
                case 49: return "A07";
                case 50: return "B07";
                case 51: return "C07";
                case 52: return "D07";
                case 53: return "E07";
                case 54: return "F07";
                case 55: return "G07";
                case 56: return "H07";
                case 57: return "A08";
                case 58: return "B08";
                case 59: return "C08";
                case 60: return "D08";
                case 61: return "E08";
                case 62: return "F08";
                case 63: return "G08";
                case 64: return "H08";
                case 65: return "A09";
                case 66: return "B09";
                case 67: return "C09";
                case 68: return "D09";
                case 69: return "E09";
                case 70: return "F09";
                case 71: return "G09";
                case 72: return "H09";
                case 73: return "A10";
                case 74: return "B10";
                case 75: return "C10";
                case 76: return "D10";
                case 77: return "E10";
                case 78: return "F10";
                case 79: return "G10";
                case 80: return "H10";
                case 81: return "A11";
                case 82: return "B11";
                case 83: return "C11";
                case 84: return "D11";
                case 85: return "E11";
                case 86: return "F11";
                case 87: return "G11";
                case 88: return "H11";
                case 89: return "A12";
                case 90: return "B12";
                case 91: return "C12";
                case 92: return "D12";
                case 93: return "E12";
                case 94: return "F12";
                case 95: return "G12";
                case 96: return "H12";
                default: return "";
            }
        }

        public static Int32 wellToNumber(char row, int col)
        {
            int i_row = 0;
            switch (row)
            {
                case 'A':
                    i_row = 1;
                    break;
                case 'B':
                    i_row = 2;
                    break;
                case 'C':
                    i_row = 3;
                    break;
                case 'D':
                    i_row = 4;
                    break;
                case 'E':
                    i_row = 5;
                    break;
                case 'F':
                    i_row = 6;
                    break;
                case 'G':
                    i_row = 7;
                    break;
                case 'H':
                    i_row = 8;
                    break;
                default:
                    i_row = 0;
                    break;
            }
            if (i_row > 0)
            {
                return i_row + (col - 1) * 8;
            }
            else
            {
                return -1;
            }
        }

        public static Int32 rowToNumber(char row)
        {
            int i_row = 0;
            switch (row)
            {
                case 'A':
                    i_row = 1;
                    break;
                case 'B':
                    i_row = 2;
                    break;
                case 'C':
                    i_row = 3;
                    break;
                case 'D':
                    i_row = 4;
                    break;
                case 'E':
                    i_row = 5;
                    break;
                case 'F':
                    i_row = 6;
                    break;
                case 'G':
                    i_row = 7;
                    break;
                case 'H':
                    i_row = 8;
                    break;
                case 'I':
                    i_row = 9;
                    break;
                case 'J':
                    i_row = 10;
                    break;
                case 'K':
                    i_row = 11;
                    break;
                case 'L':
                    i_row = 12;
                    break;
                case 'M':
                    i_row = 13;
                    break;
                case 'N':
                    i_row = 14;
                    break;
                case 'O':
                    i_row = 15;
                    break;
                case 'P':
                    i_row = 16;
                    break;
                default:
                    i_row = 0;
                    break;
            }
            return i_row;
        }

        public static Char rowToChar(Int32 row)
        {
            Char c_row = '-';
            switch (row)
            {
                case 1:
                    c_row = 'A';
                    break;
                case 2:
                    c_row = 'B';
                    break;
                case 3:
                    c_row = 'C';
                    break;
                case 4:
                    c_row = 'D';
                    break;
                case 5:
                    c_row = 'E';
                    break;
                case 6:
                    c_row = 'F';
                    break;
                case 7:
                    c_row = 'G';
                    break;
                case 8:
                    c_row = 'H';
                    break;
                default:
                    c_row = '-';
                    break;
            }
            return c_row;
        }

        public static string get384Well(string well, Int32 subplate)
        {
            Char row96 = well[0]; 
            Int32 col96 = int.Parse(well.Substring(1));
            Char row384 = 'A'; 
            Int32 col384 = 1;

            if (subplate == 1 || subplate == 3)
            {
                switch (row96)
                {
                    case 'A':
                        row384 = 'A';
                        break;
                    case 'B':
                        row384 = 'C';
                        break;
                    case 'C':
                        row384 = 'E';
                        break;
                    case 'D':
                        row384 = 'G';
                        break;
                    case 'E':
                        row384 = 'I';
                        break;
                    case 'F':
                        row384 = 'K';
                        break;
                    case 'G':
                        row384 = 'M';
                        break;
                    case 'H':
                        row384 = 'O';
                        break;
                }
            }
            else
            {
                switch (row96)
                {
                    case 'A':
                        row384 = 'B';
                        break;
                    case 'B':
                        row384 = 'D';
                        break;
                    case 'C':
                        row384 = 'F';
                        break;
                    case 'D':
                        row384 = 'H';
                        break;
                    case 'E':
                        row384 = 'J';
                        break;
                    case 'F':
                        row384 = 'L';
                        break;
                    case 'G':
                        row384 = 'N';
                        break;
                    case 'H':
                        row384 = 'P';
                        break;
                }
            }

            if (subplate <= 2)
            {
                col384 = col96 * 2 - 1;
            }
            else
            {
                col384 = col96 * 2;
            }

            return row384.ToString() + col384.ToString();
        }

        public static string getWell(char row, int col)
        {
            if (col < 10)
            {
                return row + "0" + col.ToString();
            }
            else
            {
                return row + col.ToString();
            }
        }

        public static string getLabwareType(string labwareName)
        {
            if (labwareName == "Eppi 2ml")
            {
                return "Tube Eppendorf 16 Pos";
            }
            else if (labwareName == "Micronic 1.4ml")
            {
                return "Micronic 1.4ml";
            }
            else if (labwareName == "Micronic 0.7ml")
            {
                return "Micronic 0.7 mL";
            }
            else if (labwareName == "Plate Well")
            {
                return "96well PCR in Metalladapter";
            }
            else if (labwareName == "DW96well")
            {
                return "96 Well HalfDeepWell";
            }
            else
            {
                return "";
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            m_retVal = 2;
            Application.Exit();
        }

        private void butContinue_Click(object sender, EventArgs e)
        {
            writeJobFile(m_args[3]);
            if (writeWorklistFile(m_args[4]) == false)
            {
                m_retVal = 2;
            }
            else
            {
                m_retVal = 0;
            }
            Application.Exit();
        }
    }
}
