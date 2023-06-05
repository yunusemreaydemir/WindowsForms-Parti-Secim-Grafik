using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanlı_Parti_Secim_Grafik
{
    public partial class FrmOyGiris : Form
    {
        public FrmOyGiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FIKBBE9;Initial Catalog=DBSECIMPROJE;Integrated Security=True");

        private void BtnOyGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //sql bağlantısı açıyoruz
            SqlCommand komut = new SqlCommand("insert into TBLILCE(ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) values (@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);  // sql command sınıfından nesne türetiyorun insert komutu yazabilmek için 
            komut.Parameters.AddWithValue("@P1", TxtIlceAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtA.Text);
            komut.Parameters.AddWithValue("@P3", TxtB.Text);
            komut.Parameters.AddWithValue("@P4", TxtC.Text);
            komut.Parameters.AddWithValue("@P5", TxtD.Text);
            komut.Parameters.AddWithValue("@P6", TxtE.Text);
            komut.ExecuteNonQuery(); //sorguyu gerçekleştir
            baglanti.Close(); //bağlantıyı kapat
            MessageBox.Show("Oy Girişi Gerçekleşti");
        }

        private void BtnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr = new FrmGrafikler(); //grafikler butonu click olayına ikinci form, frm grafikler formu bir sınıf olarak ekleniyor bu sınıftan fr isimli nesne türetiyoruz
            fr.Show();  //fr nesnesinin show özelliğini kullanarak ikinci formun görüntüleme işlemini gerçekleştirmiş olduk
        }

        private void BtnCikisYap_Click(object sender, EventArgs e)
        {
           // this.Close(); //formu kapat
            Application.Exit(); // uygulamayı kapat
        }
    }
}
