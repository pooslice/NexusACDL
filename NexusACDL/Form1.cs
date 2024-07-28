using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.AxHost;

namespace NexusACDL
{
    public partial class NexusACDL : Form
    {
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        public const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private System.Windows.Forms.Timer timer;
        private static Image<Bgr, byte> template;
        private List<Image<Bgr, byte>> templates;
        private const int HOTKEY_ID = 1;

        public NexusACDL()
        {
            InitializeComponent();
            InitializeTemplates();
            InitializeTimer();
            InitializeScreenComboBox();

            // Register hotkey (F8)
            if (!RegisterHotKey(this.Handle, HOTKEY_ID, (uint)KeyModifiers.None, (uint)Keys.F8))
            {
                richTB.Text += "Failed to register hotkey\n";
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == HOTKEY_ID)
                {
                    stopButton_Click(null, null);
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbTimer.SelectedIndex = 2;
        }

        private void InitializeTemplates()
        {
            templates = new List<Image<Bgr, byte>>();
            string templateFolderPath = ".\\res"; // Ange sökvägen till mappen med mallbilder

            try
            {
                // Hämta alla bildfiler i mappen
                string[] templateFiles = Directory.GetFiles(templateFolderPath, "*.png");

                // Läs in varje bildfil och lägg till den i listan
                foreach (string templateFile in templateFiles)
                {
                    Image<Bgr, byte> template = new Image<Bgr, byte>(templateFile);
                    templates.Add(template);

                }

                // Kontrollera att minst en mallbild laddades
                if (templates.Count == 0)
                {
                    richTB.Text += "No template images found in the specified folder.";
                }

            }
            catch (Exception ex)
            {
                richTB.Text += $"Error loading templates: {ex.Message}";
            }
        }

        private void InitializeTimer()
        {

            timer = new System.Windows.Forms.Timer();
            timer.Tick += OnTimedEvent;
        }

        private void InitializeScreenComboBox()
        {
            comboBoxScreens.Items.Clear();
            foreach (var screen in Screen.AllScreens)
            {
                comboBoxScreens.Items.Add(screen.DeviceName);
            }

            if (comboBoxScreens.Items.Count > 0)
            {
                comboBoxScreens.SelectedIndex = 0; // Välj första skärmen som standard
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Interval = int.Parse(cbTimer.SelectedItem.ToString());

            timer.Start();
            richTB.Text += "Autoclicker Started!\n";

            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            richTB.Text += "Autoclicker Stopped!\n";

            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            try
            {
                // Hämta den valda skärmen
                var selectedScreen = Screen.AllScreens[comboBoxScreens.SelectedIndex];

                // Ta en skärmdump av den valda skärmen
                Bitmap screenshot = new Bitmap(selectedScreen.Bounds.Width, selectedScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    g.CopyFromScreen(selectedScreen.Bounds.X, selectedScreen.Bounds.Y, 0, 0, screenshot.Size);
                }

                // Konvertera Bitmap till Image<Bgr, byte>
                Image<Bgr, byte> screenImage = BitmapToImage(screenshot);

                bool matchFound = false;

                // Iterera genom alla mallbilder och utför matchning
                foreach (var template in templates)
                {
                    using (Image<Gray, float> result = screenImage.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
                    {
                        double[] minValues, maxValues;
                        Point[] minLocations, maxLocations;
                        result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                        double bestMatchValue = maxValues[0];
                        Point bestMatchLocation = maxLocations[0];
                        Size bestMatchSize = template.Size;

                        // Kontrollera om en bra matchning hittades
                        double threshold = 0.7; // Tröskelvärde, justera vid behov
                        if (bestMatchValue > threshold)
                        {
                            richTB.Text += $"Template found at location {bestMatchLocation} with match value {bestMatchValue} and size {bestMatchSize}\n";
                            matchFound = true;

                            // Beräkna mitten av matchningen
                            int centerX = selectedScreen.Bounds.X + bestMatchLocation.X + bestMatchSize.Width / 2;
                            int centerY = selectedScreen.Bounds.Y + bestMatchLocation.Y + bestMatchSize.Height / 2;

                            // Utför mus-klick vid mitten av matchningen
                            LeftMouseClick(centerX, centerY);

                            // Avsluta loopen om en matchning hittas (valfritt)
                            break;
                        }
                    }
                }

                if (!matchFound)
                {
                    richTB.Text += "Template not found\n";
                }
            }
            catch (Exception ex)
            {
                richTB.Text += $"Error in timer event: {ex.Message}\n";
            }
        }

        private static Image<Bgr, byte> BitmapToImage(Bitmap bitmap)
        {
            // Skapa ett Image<Bgr, byte> från en Bitmap
            Image<Bgr, byte> img = new Image<Bgr, byte>(bitmap.Width, bitmap.Height);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    System.Drawing.Color color = bitmap.GetPixel(x, y);
                    img.Data[y, x, 0] = color.B;
                    img.Data[y, x, 1] = color.G;
                    img.Data[y, x, 2] = color.R;
                }
            }
            return img;
        }

        private void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBoxAlwaysOnTop.Checked;
        }
    }
}
