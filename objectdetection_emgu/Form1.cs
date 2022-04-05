using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace ObjectCapture
{
    public partial class objectCapture : Form
    {

    Capture capture;

        public objectCapture()
        {
            InitializeComponent();
        }

        private void objectCapture_Load(object sender, EventArgs e)
        {

        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                capture = new Emgu.CV.Capture(0);
            }
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try{
                Mat m = new Mat();
                capture.Retrieve(m);
                pictureBox1.Image = m.ToImage<Bgr, byte>().Bitmap;
            }catch (Exception)
            {
               
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(capture != null)
            {
                capture.Dispose();
                capture = null;
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            { 
                capture.Pause();
            }
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Video Files |*.mp4";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    capture = new Emgu.CV.Capture(ofd.FileName);
                }
            }
            capture.ImageGrabbed += Capture_ImageGrabbed1;
            capture.Start();
        }

        private void Capture_ImageGrabbed1(object sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture.Retrieve(m);
                pictureBox1.Image = m.ToImage<Bgr, byte>().Bitmap;
                Thread.Sleep(1);
            }
            catch (Exception)
            {

            }
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(capture != null)
            {
                capture.Stop();
            }
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Pause();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
