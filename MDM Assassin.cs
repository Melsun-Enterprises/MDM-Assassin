using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace MDM_Assassin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "MDM Assassin - Fuck outta here, your shit water whip";
            this.BackColor = Color.Black;
            this.ForeColor = Color.Red;
            InitializeUI();
        }

        private void InitializeUI()
        {
            Button btnEnableADB = new Button()
            {
                Text = "🔥 Enable ADB",
                Location = new Point(20, 20),
                Size = new Size(200, 50),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEnableADB.Click += async (sender, e) => await btnEnableADB_Click(sender, e);
            this.Controls.Add(btnEnableADB);

            Button btnFactoryReset = new Button()
            {
                Text = "💀 Factory Reset",
                Location = new Point(20, 80),
                Size = new Size(200, 50),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFactoryReset.Click += async (sender, e) => await btnFactoryReset_Click(sender, e);
            this.Controls.Add(btnFactoryReset);

            Button btnFactoryResetBypass = new Button()
            {
                Text = "🔓 Factory Reset (Bypass)",
                Location = new Point(20, 140),
                Size = new Size(200, 50),
                BackColor = Color.Purple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFactoryResetBypass.Click += async (sender, e) => await btnFactoryResetBypass_Click(sender, e);
            this.Controls.Add(btnFactoryResetBypass);

            Button btnRebootDownloadMode = new Button()
            {
                Text = "⚡ Reboot to Download Mode",
                Location = new Point(20, 200),
                Size = new Size(200, 50),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRebootDownloadMode.Click += async (sender, e) => await btnRebootDownloadMode_Click(sender, e);
            this.Controls.Add(btnRebootDownloadMode);

            Button btnSoftBrick = new Button()
            {
                Text = "💣 Trigger Soft Brick",
                Location = new Point(20, 260),
                Size = new Size(200, 50),
                BackColor = Color.Black,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSoftBrick.Click += async (sender, e) => await btnSoftBrick_Click(sender, e);
            this.Controls.Add(btnSoftBrick);

            Button btnGenerateQR = new Button()
            {
                Text = "🌐 Generate ADB QR Code",
                Location = new Point(20, 320),
                Size = new Size(200, 50),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerateQR.Click += async (sender, e) => await btnGenerateQR_Click(sender, e);
            this.Controls.Add(btnGenerateQR);

            PictureBox qrPictureBox = new PictureBox()
            {
                Location = new Point(240, 20),
                Size = new Size(200, 200),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(qrPictureBox);
        }

        private async Task btnGenerateQR_Click(object sender, EventArgs e)
        {
            if (CheckADBExists())
            {
                string uniqueIdentifier = await GetDeviceUniqueIdentifier();
                if (!string.IsNullOrEmpty(uniqueIdentifier))
                {
                    string pairingCode = await GetPairingCode(uniqueIdentifier);
                    if (!string.IsNullOrEmpty(pairingCode))
                    {
                        GenerateQRCode(pairingCode, qrPictureBox);
                    }
                }
            }
        }

        private async Task<string> GetDeviceUniqueIdentifier()
        {
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "adb";
                adbProcess.StartInfo.Arguments = "shell getprop ro.serialno";
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();

                string output = await adbProcess.StandardOutput.ReadToEndAsync();
                adbProcess.WaitForExit();

                return output.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting device unique identifier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private async Task<string> GetPairingCode(string uniqueIdentifier)
        {
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "adb";
                adbProcess.StartInfo.Arguments = "pair";
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();

                string output = await adbProcess.StandardOutput.ReadToEndAsync();
                adbProcess.WaitForExit();

                if (output.Contains("pairing code"))
                {
                    string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        if (line.Contains("pairing code"))
                        {
                            return line.Split(new[] { ':' }, 2)[1].Trim() + uniqueIdentifier;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting pairing code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private void GenerateQRCode(string pairingCode, PictureBox pictureBox)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(pairingCode, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                pictureBox.Image = qrCodeImage;
            }
        }

        private bool CheckADBExists()
        {
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "adb";
                adbProcess.StartInfo.Arguments = "version";
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();
                string output = adbProcess.StandardOutput.ReadToEnd();
                adbProcess.WaitForExit();
                return !string.IsNullOrEmpty(output);
            }
            catch
            {
                MessageBox.Show("ADB is not installed or not found in the system PATH.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task ExecuteADBCommand(string command)
        {
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "adb";
                adbProcess.StartInfo.Arguments = command;
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.RedirectStandardError = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();

                string output = await adbProcess.StandardOutput.ReadToEndAsync();
                string error = await adbProcess.StandardError.ReadToEndAsync();
                adbProcess.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Error executing ADB command: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(output, "ADB Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing ADB command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}