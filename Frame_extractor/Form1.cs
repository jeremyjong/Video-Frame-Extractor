using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace GIF_extractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }
                

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string[] files_in_folder = Directory.GetFiles(path);

                  
            
            foreach (string file in files_in_folder)
            {
                // Frame image buffer
                Mat image = new Mat();
                int count = 0;

                string cur_filename = Path.GetFileName(file);
                string cur_foldername = Path.GetDirectoryName(file);
                string newfolder = cur_foldername + "\\" + Path.GetFileNameWithoutExtension(cur_filename);
                if (!Directory.Exists(newfolder))
                {
                    Directory.CreateDirectory(newfolder);
                }

                using (var capture = new VideoCapture(file))
                {
                    var frame_count = capture.Get(CaptureProperty.FrameCount);
                    for(int i = 0; i < frame_count; i++)
                    {
                        capture.Read(image); 
                        if (image.Empty())
                            break;                   


                        image.SaveImage(newfolder + "\\" + Path.GetFileNameWithoutExtension(cur_filename) + "_" + count + ".png");                                                                    
                        count += 1 ;
                    }
                }
                    
                
            }
        }
    }
}
