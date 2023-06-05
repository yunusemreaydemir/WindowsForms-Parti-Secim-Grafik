using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //bağlantı kütüphanesi

namespace Veri_Tabanlı_Parti_Secim_Grafik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FIKBBE9;Initial Catalog=DBSECIMPROJE;Integrated Security=True");


        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İlçe adlarını comboboxa çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti); // bu komutu çalıştırdığımız zaman sadece ilçe adlarını sorgular
            SqlDataReader dr = komut.ExecuteReader(); //veri okutma işlemi için kullandığmmız bir komut sqldatareader ve bundan bir nesne türetiyoruz ve bunu komuttan gelen değerimizle ilişkilendiriyoruz.
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]); //drden gelen 0. index yani ilçe adı
            }
            baglanti.Close();

            //grafiğe toplam sonuçları getirme 
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBLILCE", baglanti); //toplam sonuç 
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read()) //dr2 komutu okuma yaptığı sürece 
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]); //chart aracı iki tane parametre alıyor x ve y,dr2 0. index
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]); //x e değer verdik
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);
            }
            baglanti.Close() ;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //combobox seçim yapıldığı zaman 
        {
            baglanti.Open() ;
            SqlCommand komut = new SqlCommand("Select * From TBLILCE where ILCEAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                progressBar1.Value = int.Parse(dr[2].ToString()); //string değer integer parse 
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString(); //progressbar verisini label akatarma
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();

            }
            baglanti.Close();
        }
    }
}
