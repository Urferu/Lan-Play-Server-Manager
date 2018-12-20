using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace WindowsFormsApp2
{
    public partial class formCreditos : MaterialForm
    {
        public formCreditos(bool conectado)
        {
            InitializeComponent();
            MaterialSkinManager m = MaterialSkinManager.Instance;
            m.AddFormToManage(this);
            m.Theme = MaterialSkinManager.Themes.LIGHT;
            if (conectado)
            {
                m.ColorScheme = new ColorScheme(Primary.Green400, Primary.Green300, Primary.Green200, Accent.Green100, TextShade.WHITE);
            }
            else
            {
                m.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red700, Primary.Red600, Accent.Red400, TextShade.WHITE);
            }

            lbIpPublica.Text = LenguagesManager.StringsCreditsLenguages.DesignedBy;
            materialLabel1.Text = LenguagesManager.StringsCreditsLenguages.DevelopedBy;
            materialLabel2.Text = LenguagesManager.StringsCreditsLenguages.ListServers;
            materialLabel3.Text = LenguagesManager.StringsCreditsLenguages.SupportedBy;
            materialLabel4.Text = LenguagesManager.StringsCreditsLenguages.FollowOn;

            LinkLabel.Link linkDiscord = new LinkLabel.Link();
            linkDiscord.LinkData = "https://discord.gg/q6hdKc8";
            LinkLabel.Link linkRadikal = new LinkLabel.Link();
            linkRadikal.LinkData = "http://www.radikal-gamez.net/forums/juegos-switch.196/";
            linkLabel2.Links.Add(linkRadikal);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
    }
}
