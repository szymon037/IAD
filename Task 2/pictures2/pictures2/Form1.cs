using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace pictures2
{
    public partial class Form1 : Form
    {       
        public Form1()
        {
            InitializeComponent();
        }

        public int sizeOfFrame;
        public int nnHeight;
        public int nnWidth;
        public int epochs;
        public int mode;
        public string image_path;


        private void Form1_Load(object sender, EventArgs e)
        {
            mode = 1;
            sizeOfFrame = 3;
            image_path = @"C:\Users\szymo\Documents\a.jpg";

            epochs_text.Text = Convert.ToString(5);
            LR_init.Text = Convert.ToString(0.15);
            LR_fin.Text = Convert.ToString(0.005);
            RANGE_init.Text = Convert.ToString(1);
            RANGE_fin.Text = Convert.ToString(0.0001);   
        }

        public void TrainAndDraw(Bitmap image, Bitmap resultImage)
        {
            //source image
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);            
            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * image.Height];
            IntPtr scan0 = imageData.Scan0;  
            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            //result image
            BitmapData resultImageData = resultImage.LockBits(new Rectangle(0, 0, resultImage.Width, resultImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte[] resultImageBytes = new byte[Math.Abs(resultImageData.Stride) * resultImage.Height];
            scan0 = resultImageData.Scan0;
            Marshal.Copy(scan0, resultImageBytes, 0, resultImageBytes.Length);

            FrameOfPixels frame = new FrameOfPixels(sizeOfFrame);

            Random rnd = new Random();

            NeuralNetwork nn = new NeuralNetwork(nnWidth, nnHeight, sizeOfFrame, mode, Convert.ToDouble(RANGE_init.Text), Convert.ToDouble(RANGE_fin.Text), Convert.ToDouble(LR_init.Text), Convert.ToDouble(LR_fin.Text));
            epochs = Convert.ToInt32(epochs_text.Text);

            //nauka
            int randX = image.Width / sizeOfFrame;
            int randY = image.Height / sizeOfFrame;

            int spawnX = rnd.Next(0, randX);
            int spawnY = rnd.Next(0, randY);

            double quantization_error = 0;

            int strideWidthDivided = imageData.Stride / 3;
            int strideWidth = imageData.Stride;
            int resultStrideWidth = resultImageData.Stride;

            int heightDivided = image.Height / sizeOfFrame;
            int widthDivided = image.Width / sizeOfFrame;

            for (int i = 0; i < epochs; ++i)
            {
                /*spawnX = rnd.Next(0, randX);
                spawnY = rnd.Next(0, randY);
                for (int p = spawnY; p < spawnY + sizeOfFrame; ++p)
                {
                    for (int z = spawnX; z < spawnX + sizeOfFrame; ++z)
                    {
                        int a = p * strideWidth + z * 3;

                        frame.pixels.Add(new Pixel(imageBytes[a + 2], imageBytes[a + 1], imageBytes[a]));
                    }
                }*/


                //po kolei
                /*for (int o = 0; o < image.Height - (image.Height % sizeOfFrame); o += sizeOfFrame)
                {
                    for (int j = 0; j < image.Width - (image.Width % sizeOfFrame); j += sizeOfFrame)
                    {
                        for (int p = o; p < o + sizeOfFrame; ++p)
                        {
                            for (int z = j; z < j + sizeOfFrame; ++z)
                            {
                                int a = p * strideWidth + z * 3;

                                frame.pixels.Add(new Pixel(imageBytes[a + 2], imageBytes[a + 1], imageBytes[a]));
                            }
                        }
                        quantization_error += nn.Train(frame, epochs, i);
                        frame.pixels.Clear();
                    }
                }*/

                foreach (int o in Enumerable.Range(0, heightDivided).OrderBy(x => rnd.Next()))
                {
                    foreach (int j in Enumerable.Range(0, widthDivided).OrderBy(x => rnd.Next()))
                    {
                        for (int p = o* sizeOfFrame; p < o* sizeOfFrame + sizeOfFrame; ++p)
                        {
                            for (int z = j* sizeOfFrame; z < j* sizeOfFrame + sizeOfFrame; ++z)
                            {
                                int a = p * strideWidth + z * 3;

                                frame.pixels.Add(new Pixel(imageBytes[a + 2], imageBytes[a + 1], imageBytes[a]));
                            }
                        }
                        quantization_error += nn.Train(frame, epochs, i);
                        frame.pixels.Clear();
                    }
                }



            }
            Console.WriteLine("Quantization error: " + quantization_error / epochs);            

            
            //wypełnianie
           
            for (int i = 0; i < image.Height - (image.Height % sizeOfFrame) ; i += sizeOfFrame)
            {                
                for (int j = 0; j < image.Width - (image.Width % sizeOfFrame); j += sizeOfFrame)
                {
                    for (int p = i; p < i + sizeOfFrame; ++p)
                    {
                        for (int z = j; z < j + sizeOfFrame; ++z)
                        {
                            int a = p * strideWidth + z * 3;

                            if (mode == 0)
                            {
                                int q = (imageBytes[a + 2] + imageBytes[a + 1] + imageBytes[a]) / 3;
                                imageBytes[a + 2] = (byte)q;
                                imageBytes[a + 1] = (byte)q;
                                imageBytes[a] = (byte) q;
                            }
                            frame.pixels.Add(new Pixel(imageBytes[a + 2], imageBytes[a + 1], imageBytes[a]));                      
                        }
                    }                    

                    FrameOfPixels BMU = new FrameOfPixels(sizeOfFrame);
                    BMU = nn.Compare(frame);
                    
                    int index = 0;
                    frame.pixels.Clear();

                    //wypełnienie
                    for (int p = i; p < i + sizeOfFrame; ++p)
                    {
                        for (int z = j; z < j + sizeOfFrame; ++z)
                        {
                            int a = p * resultStrideWidth + z * 3;
                                
                            resultImageBytes[a] = (byte)BMU.pixels[index].B;
                            resultImageBytes[a + 1] = (byte)BMU.pixels[index].G;
                            resultImageBytes[a + 2] = (byte)BMU.pixels[index].R;                             

                            ++index;     
                        }
                    }
                    
                }
            }

           /* Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);
            image.UnlockBits(imageData);*/

            Marshal.Copy(resultImageBytes, 0, scan0, resultImageBytes.Length);
            resultImage.UnlockBits(resultImageData);
        }       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void draw_button_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            UpdateValues();
            Bitmap image1 = new Bitmap(image_path, true);
            Bitmap result_image = new Bitmap(image1.Width - image1.Width % sizeOfFrame, image1.Height - image1.Height % sizeOfFrame);
            
            TrainAndDraw(image1, result_image);
            pictureBox1.Image = result_image;
            result_image.Save(@"C:\Users\szymo\Documents\RESULT.jpg");
           // image1.Save(@"C:\Users\szymo\Documents\RESULT.bmp");
        }

        private void color_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void chromo_Click(object sender, EventArgs e)
        {
            mode = 0;
        }

        private void UpdateValues ()
        {
            sizeOfFrame = Convert.ToInt32(size_of_frame_text.Text);
            nnHeight = Convert.ToInt32(nn_height_text.Text); ;
            nnWidth = Convert.ToInt32(nn_width_text.Text); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                //FileName = "dane", // Default file name
                //DefaultExt = ".txt", // Default file extension
                //Filter = "Text documents (.txt)|*.txt" // Filter files by extension
            };

            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                image_path = dlg.FileName;
                Console.WriteLine(image_path);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
