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

namespace TecanMCA_PipetteDiagnostic
{
    public partial class InitializeForm : Form
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// </summary>
        private int m_retVal = 2;
        private int m_384wellPlateCount = 0;
        private int m_micronic_rack_used = 1;
        private int m_wash_station_primed = 0;

        private string[] m_args;

        public InitializeForm(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void InitializeForm_Load(object sender, EventArgs e)
        {
            cbEppiRacksToScan.SelectedIndex = 1;
            cbDitiPosition.SelectedIndex = 0;
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void chkMicronicRack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMicronicRack.Checked == false)
            {
                m_micronic_rack_used = 0;
                txtMicronicRackBarcode.Enabled = false;
                txtMicronicRackBarcode.Text = "";
            }
            else
            {
                m_micronic_rack_used = 1;
                txtMicronicRackBarcode.Enabled = true;
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            m_retVal = 2;
            Application.Exit();
        }

        private void checkVolume()
        {
            int plate384Count = 0;
            if (txt384wellPlateBarcode1.Text.Trim() != "")
            {
                plate384Count += 1;
            }
            if (txt384wellPlateBarcode2.Text.Trim() != "")
            {
                plate384Count += 1;
            }
            if (plate384Count > 0)
            {
                if (int.Parse(txt96wellTargetVolume.Text.Trim()) < (5 + 4 * plate384Count * int.Parse(txt384wellTargetVolume.Text.Trim())))
                {
                    lblNeedVolume.Visible = true;
                }
                else
                {
                    lblNeedVolume.Visible = false;
                }
            }
            else
            {
                lblNeedVolume.Visible = false;
            }
        }

        private void writeVariables(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("I,eppi_racks_to_scan," + cbEppiRacksToScan.Text + "\r\n");
                    streamWriter.Write("I,micronic_rack_used," + m_micronic_rack_used + "\r\n");
                    streamWriter.Write("S,micronic_rack_barcode," + txtMicronicRackBarcode.Text.Trim() + "\r\n");
                    streamWriter.Write("S,plate_96well," + txt96wellPlateBarcode.Text.Trim() + "\r\n");
                    streamWriter.Write("I,plate_384well_count," + m_384wellPlateCount + "\r\n");
                    streamWriter.Write("S,plate_384well_1," + txt384wellPlateBarcode1.Text.Trim() + "\r\n");
                    streamWriter.Write("S,plate_384well_2," + txt384wellPlateBarcode2.Text.Trim() + "\r\n");
                    streamWriter.Write("I,target_96well_volume," + int.Parse(txt96wellTargetVolume.Text.Trim()) + "\r\n");
                    streamWriter.Write("I,target_96well_concentration," + int.Parse(txt96wellTargetConcentration.Text.Trim()) + "\r\n");
                    streamWriter.Write("I,target_384well_volume," + int.Parse(txt384wellTargetVolume.Text.Trim()) + "\r\n");
                    streamWriter.Write("I,wash_station_primed," + m_wash_station_primed + "\r\n");
                    streamWriter.Write("I,diti_position," + int.Parse(cbDitiPosition.Text.Trim()) + "\r\n");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            m_384wellPlateCount = 0;
            if (chkMicronicRack.Checked)
            {
                if (txtMicronicRackBarcode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a Micronic rack barcode.");
                    txtMicronicRackBarcode.Focus();
                    return;
                }
                else
                {
                    if (CSQL.checkMicronicRackExist(txtMicronicRackBarcode.Text.Trim()) == false)
                    {
                        MessageBox.Show("Rack " + txtMicronicRackBarcode.Text.Trim() + " does not exist.");
                        txtMicronicRackBarcode.Focus();
                        return;
                    }
                }
            }

            if (txt96wellPlateBarcode.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a 96well plate barcode.");
                txt96wellPlateBarcode.Focus();
                return;
            }
            else
            {
                if (CSQL.checkPlate96wellExist(txt96wellPlateBarcode.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt96wellPlateBarcode.Text.Trim() + " does not exist.");
                    txt96wellPlateBarcode.Focus();
                    return;
                }
            }

            if (txt384wellPlateBarcode1.Text.Trim() == "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you don't want to create a 384well plate?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    txt384wellPlateBarcode1.Focus();
                    return;
                }
            }
            else
            {
                if (CSQL.checkPlate384wellExist(txt384wellPlateBarcode1.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt384wellPlateBarcode1.Text.Trim() + " does not exist.");
                    txt384wellPlateBarcode1.Focus();
                    return;
                }

                if (CSQL.checkNeededSubPlatesExist(txt384wellPlateBarcode1.Text.Trim(), txt96wellPlateBarcode.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt384wellPlateBarcode1.Text.Trim() + " cannot be created from sub-plate " + txt96wellPlateBarcode.Text.Trim() + ".");
                    txt384wellPlateBarcode1.Focus();
                    return;
                }
                m_384wellPlateCount += 1;
            }

            if (txt384wellPlateBarcode2.Text.Trim() != "")
            {
                if (CSQL.checkPlate384wellExist(txt384wellPlateBarcode2.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt384wellPlateBarcode2.Text.Trim() + " does not exist.");
                    txt384wellPlateBarcode2.Focus();
                    return;
                }

                if (CSQL.checkNeededSubPlatesExist(txt384wellPlateBarcode1.Text.Trim(), txt96wellPlateBarcode.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt384wellPlateBarcode1.Text.Trim() + " cannot be created from sub-plate " + txt96wellPlateBarcode.Text.Trim() + ".");
                    txt384wellPlateBarcode1.Focus();
                    return;
                }
                m_384wellPlateCount += 1;
            }

            writeVariables(m_args[1]);
            m_retVal = 1;
            Application.Exit();
        }

        private void txt384wellPlateBarcode1_TextChanged(object sender, EventArgs e)
        {
            if (txt384wellPlateBarcode1.Text.Trim() != "")
            {
                txt384wellPlateBarcode2.Enabled = true;
            }
            else
            {
                txt384wellPlateBarcode2.Text = "";
                txt384wellPlateBarcode2.Enabled = false;
            }
        }

        private void txt96wellTargetVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)'0':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'1':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'2':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'3':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'4':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'5':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'6':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'7':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'8':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'9':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)Keys.Back:
                    e.KeyChar = e.KeyChar;
                    break;
                default:
                    e.KeyChar = (char)Keys.None;
                    break;
            }
        }

        private void txt96wellTargetConcentration_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)'0':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'1':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'2':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'3':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'4':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'5':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'6':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'7':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'8':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'9':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)Keys.Back:
                    e.KeyChar = e.KeyChar;
                    break;
                default:
                    e.KeyChar = (char)Keys.None;
                    break;
            }
        }

        private void txt384wellTargetVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)'0':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'1':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'2':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'3':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'4':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'5':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'6':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'7':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'8':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'9':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)Keys.Back:
                    e.KeyChar = e.KeyChar;
                    break;
                default:
                    e.KeyChar = (char)Keys.None;
                    break;
            }
        }

        private void txt96wellTargetVolume_TextChanged(object sender, EventArgs e)
        {
            if (txt96wellTargetVolume.Text.Trim() == "")
            {
                txt96wellTargetVolume.Text = "5";
            }
            checkVolume();
        }

        private void txt96wellTargetConcentration_TextChanged(object sender, EventArgs e)
        {
            if (txt96wellTargetVolume.Text.Trim() == "")
            {
                txt96wellTargetVolume.Text = "1";
            }
        }

        private void txt384wellTargetVolume_TextChanged(object sender, EventArgs e)
        {
            if (txt96wellTargetVolume.Text.Trim() == "")
            {
                txt96wellTargetVolume.Text = "0";
            }
            checkVolume();
        }

        private void chkWashStationPrimed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWashStationPrimed.Checked)
            {
                m_wash_station_primed = 1;
            }
            else
            {
                m_wash_station_primed = 0;
            }
        }
    }
}
