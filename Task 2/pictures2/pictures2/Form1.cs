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
        public int epochs;
        public int mode;


        private void Form1_Load(object sender, EventArgs e)
        {
            mode = 1;
            sizeOfFrame = 2;
            epochs = 80000;

            Bitmap image1 = new Bitmap(@"C:\Users\szymo\Documents\munch.jpg", true);
            TrainAndDraw(image1);

            image1.Save(@"C:\Users\szymo\Documents\a1.jpg");
           
        }

        public void TrainAndDraw(Bitmap image)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * image.Height];
            IntPtr scan0 = imageData.Scan0;           

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            FrameOfPixels frame = new FrameOfPixels(sizeOfFrame);

            Random rnd = new Random();

            NeuralNetwork nn = new NeuralNetwork(10, 10, sizeOfFrame, mode);

            //nauka
            int randX = image.Width / sizeOfFrame;
            int randY = image.Height / sizeOfFrame;

            int spawnX = rnd.Next(0, randX);
            int spawnY = rnd.Next(0, randY);

            double quantization_error = 0;

            for (int i = 0; i < epochs; ++i)
            {
                spawnX = rnd.Next(0, randX);
                spawnY = rnd.Next(0, randY);
                for (int p = spawnY; p < spawnY + sizeOfFrame; ++p)
                {
                    for (int z = spawnX; z < spawnX + sizeOfFrame; ++z)
                    {
                        int a = p * image.Width + z;
                        a *= 3;
                        frame.pixels.Add(new Pixel(imageBytes[a + 2], imageBytes[a + 1], imageBytes[a]));
                    }
                }

                quantization_error += nn.Train(frame, epochs, i);
                frame.pixels.Clear();
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
                            int a = p * image.Width + z;
                            a *= 3;
                                
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
                    
                    for (int p = i; p < i + sizeOfFrame; ++p)
                    {
                        for (int z = j; z < j + sizeOfFrame; ++z)
                        {
                            int a = p * image.Width + z;
                            a *= 3;
                            imageBytes[a]     = (byte)BMU.pixels[index].B;
                            imageBytes[a + 1] = (byte)BMU.pixels[index].G;
                            imageBytes[a + 2] = (byte)BMU.pixels[index].R;
                            ++index;
                        }
                    }                    
                }
            }
          
            Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);

            image.UnlockBits(imageData);
        }       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
