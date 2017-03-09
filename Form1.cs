using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bulanıkmantıkprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
                      Sürtünme//
                        Az        Orta      Çok
         Eğim//  Az     Hızlı     Hızlı     Orta
                 Orta   Hızlı     Orta      Yavaş
                 Çok    Orta      Yavaş     Yavaş
         */

        private void button1_Click(object sender, EventArgs e)
        {

            // EĞİM AÇISI HESAPLAMALARI
            double[] egim = bulanık(0, 35, 70, Convert.ToDouble(textBox1.Text));
            label14.Text = egim[0].ToString();
            label15.Text = egim[1].ToString();
            label16.Text = egim[2].ToString();

            //SURTUNME KATSAYISI HESAPLAMALARI
            double surtKatsayı = Convert.ToDouble(textBox2.Text);
            if (surtKatsayı > 10)
            {
                surtKatsayı = surtKatsayı / 100;

            }

            else 
            { 
                surtKatsayı = surtKatsayı / 10; 
            }

            double[] surtunme = bulanık(0.3, 0.6, 0.9, surtKatsayı);
            label20.Text = surtunme[0].ToString();
            label21.Text = surtunme[1].ToString();
            label22.Text = surtunme[2].ToString();


            double[,] array = new double[3, 3];

            array[0, 0] = Math.Min(egim[0], surtunme[0]);
            array[1, 0] = Math.Min(egim[1], surtunme[0]);
            array[2, 0] = Math.Min(egim[2], surtunme[0]);
            array[0, 1] = Math.Min(egim[0], surtunme[1]);
            array[1, 1] = Math.Min(egim[1], surtunme[1]);
            array[2, 1] = Math.Min(egim[2], surtunme[1]);
            array[0, 2] = Math.Min(egim[0], surtunme[2]);
            array[1, 2] = Math.Min(egim[1], surtunme[2]);
            array[2, 2] = Math.Min(egim[2], surtunme[2]);

            //Hız durumu hesaplanıyor

            double hızlı, orta, yavas, Hız;

            hızlı = Math.Max(Math.Max(array[0, 0], array[0, 1]), array[1, 0]);
            orta  = Math.Max(Math.Max(array[2, 0], array[0, 2]), array[1, 1]);
            yavas = Math.Max(Math.Max(array[2, 1], array[2, 2]), array[1, 2]);


            //hız hesaplaması

            Hız = Math.Round((yavas * 35 + orta * 70 + hızlı * 110)/(yavas+orta+hızlı));
            
            if (comboBox6.SelectedItem == "OTOMOBİL")
            {
                sonuc.Text = (Hız + 5).ToString();

            }
            else if (comboBox6.SelectedItem == "OTOBÜS")
            {
                sonuc.Text = (Hız + 3).ToString();

            }

            else if (comboBox6.SelectedItem == "KAMYON")
            {
                sonuc.Text = Hız.ToString();

            }
        }

        
        public double[] bulanık(double altSinir, double ortaSinir, double ustSinir, double sonuc)
            {
                double[] aidiyet = new double[3];

                if (sonuc <= altSinir)
                    aidiyet[0] = 1;

                else if (sonuc > altSinir && sonuc < ortaSinir)
                {
                    aidiyet[0] = Math.Round((ortaSinir - sonuc) / (ortaSinir - altSinir), 3);
                    aidiyet[1] = Math.Round((sonuc - altSinir) / (ortaSinir - altSinir), 3);
                }
                else if (sonuc == ortaSinir)
                    aidiyet[1] = 1;
                else if (sonuc > ortaSinir && sonuc < ustSinir)
                {
                    aidiyet[1] = Math.Round((ustSinir - sonuc) / (ustSinir - ortaSinir), 3);
                    aidiyet[2] = Math.Round((sonuc - ortaSinir) / (ustSinir - ortaSinir), 3);
                }
                else if (sonuc >= ustSinir)
                    aidiyet[2] = 1;

                return aidiyet;
            }
        }
    }

