using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace DetectorDe8s
{
    public partial class Detector8sForm : Form
    {
        private Graphics screenShot;

        public Detector8sForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {

            this.escanear();
        }

        private void escanear() {
            
                //Sacar una screenShot
                Bitmap bt = this.capturar();
                //Evaluar patron
                int x = Int32.Parse(this.XPos.Text);
                int y = Int32.Parse(this.YPos.Text);
                if (this.esUnOcho(x, y, bt)) {
                    this.result.Text = "YES!!!";
                } else {
                    this.result.Text = "No";
                }
        }

        private Bitmap capturar() {
            Bitmap bt = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            screenShot = Graphics.FromImage(bt);
            screenShot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return bt;
        }

        private bool esUnOcho(int x, int y, Bitmap bt) {
            int[,] blancos = {    //  Matríz de 8col x 10filas
              {0,0,0,1,1,0,0,0},    //---oo---
              {0,0,1,1,1,1,0,0},    //--oooo--
              {0,0,0,0,0,0,0,0},    //--------
              {0,0,0,0,0,0,0,0},    //--------
              {0,0,1,1,1,1,0,0},    //--oooo--
              {0,0,1,1,1,1,0,0},    //--oooo--
              {0,1,0,0,0,0,0,0},    //-o------
              {0,1,0,0,0,0,1,0},    //-o----o-
              {0,0,1,1,1,1,0,0},    //--oooo--
              {0,0,0,1,1,0,0,0}     //---oo---
            };
            for (int fila = 0; fila < 10; fila++) {
                for (int columna = 0; columna < 8; columna++) {
                    if (blancos[fila,columna] == 1 && !this.esBlanco(bt.GetPixel(x + columna, y + fila))){
                        return false;
                    }
                }
            }
            return true;
        }

        private bool esBlanco(Color color) {
            return color.R == 255 && color.G == 255 && color.B == 255;
        }
    }
}
