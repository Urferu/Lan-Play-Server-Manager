using System;
using System.IO;
using System.Net;
using MaterialSkin;
using System.Drawing;
using System.Net.Cache;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using WindowsFormsApp2.Clases;

namespace WindowsFormsApp2
{
    public partial class FormPrincipal : MaterialForm
    {
        string versionActual = "v0.0.6";
        string identificadorIp = "";
        Process bat;
        MaterialSkinManager m;
        HttpRequestCachePolicy noCachePolicy;
        stdClassCSharp idiomas;

        public FormPrincipal()
        {
            InitializeComponent();
            m = MaterialSkinManager.Instance;
            m.AddFormToManage(this);
            m.Theme = MaterialSkinManager.Themes.LIGHT;
            m.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red700, Primary.Red600, Accent.Red400, TextShade.WHITE);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            bat = new Process();
            ObtenerIdiomas();
            LinkLabel.Link linkRadikal = new LinkLabel.Link();
            linkRadikal.LinkData = "http://www.letmecheck.it/mtu-test.php";
            linkLabel2.Links.Add(linkRadikal);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetActualVersion();
            LoadPmtu();
            txtVersion.Text = versionActual;
            lbStatus.BringToFront();
        }

        private void FormPrincipal_Shown(object sender, EventArgs e)
        {
            GetRecentsServers();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiaIdiomaSeleccionado();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        #region Inicial

        private void ObtenerIdiomas()
        {
            string lenguages = DownloadStringServer("https://raw.githubusercontent.com/Urferu/Lan-Play-Server-Manager/master/Lenguages/LengugesDisp.json");
            idiomas = stdClassCSharp.jsonToStdClass(lenguages);
            int indice = 0;
            int indiceIdioma = 0;
            foreach (stdClassCSharp idioma in idiomas.toArray())
            {
                comboBox1.Items.Add(idioma["language_name"]);
                if(CultureInfo.CurrentCulture.Name.Split('-')[0].ToUpper().Equals(idioma["abbreviation"]))
                {
                    indiceIdioma = indice;
                }
                indice++;
            }
            if(!File.Exists("config.txt"))
                comboBox1.SelectedIndex = indiceIdioma;
        }

        private void CambiaIdiomaSeleccionado()
        {
            string selectedLenguage = "";
            foreach(stdClassCSharp lenguage in idiomas.toArray())
            {
                if(lenguage["language_name"].Contains(comboBox1.Items[comboBox1.SelectedIndex].ToString()))
                {
                    selectedLenguage = lenguage["abbreviation"];
                }
            }
            string lenguageData = DownloadStringServer("https://raw.githubusercontent.com/Urferu/Lan-Play-Server-Manager/master/Lenguages/"+ selectedLenguage + ".json");
            if(!string.IsNullOrWhiteSpace(lenguageData))
            {
                stdClassCSharp datosDelIdioma = stdClassCSharp.jsonToStdClass(lenguageData);
                LenguagesManager.StringsPrincipalLenguages.ButtonConnect = datosDelIdioma["StringsPrincipalLenguages"]["ButtonConnect"];
                LenguagesManager.StringsPrincipalLenguages.ButtonDisconnect = datosDelIdioma["StringsPrincipalLenguages"]["ButtonDisconnect"];
                LenguagesManager.StringsPrincipalLenguages.Servers = datosDelIdioma["StringsPrincipalLenguages"]["Servers"];
                LenguagesManager.StringsPrincipalLenguages.ServersLoading = datosDelIdioma["StringsPrincipalLenguages"]["ServersLoading"];

                LenguagesManager.StringsCreditsLenguages.DesignedBy = datosDelIdioma["StringsCreditsLenguages"]["DesignedBy"];
                LenguagesManager.StringsCreditsLenguages.DevelopedBy = datosDelIdioma["StringsCreditsLenguages"]["DevelopedBy"];
                LenguagesManager.StringsCreditsLenguages.ListServers = datosDelIdioma["StringsCreditsLenguages"]["ListServers"];
                LenguagesManager.StringsCreditsLenguages.SupportedBy = datosDelIdioma["StringsCreditsLenguages"]["SupportedBy"];
                LenguagesManager.StringsCreditsLenguages.FollowOn = datosDelIdioma["StringsCreditsLenguages"]["FollowOn"];

                if(Convert.ToInt32(materialRaisedButton1.Tag).Equals(0))
                    materialRaisedButton1.Text = LenguagesManager.StringsPrincipalLenguages.ButtonConnect;
                else
                    materialRaisedButton1.Text = LenguagesManager.StringsPrincipalLenguages.ButtonDisconnect;
                toolTip1.SetToolTip(this.materialRaisedButton1, datosDelIdioma["StringsPrincipalLenguages"]["ToolTipConnect"]);

                if (labelServers.Text.Contains("..."))
                    labelServers.Text = LenguagesManager.StringsPrincipalLenguages.ServersLoading;
                else
                    labelServers.Text = LenguagesManager.StringsPrincipalLenguages.Servers;

                materialRaisedButton2.Text = datosDelIdioma["StringsPrincipalLenguages"]["ButtonReload"];
                toolTip1.SetToolTip(this.materialRaisedButton2, datosDelIdioma["StringsPrincipalLenguages"]["ToolTipReload"]);
                toolTip1.SetToolTip(linkLabel2, datosDelIdioma["StringsPrincipalLenguages"]["ToolTipMTU"]);
                toolTip1.SetToolTip(txtPmtu, datosDelIdioma["StringsPrincipalLenguages"]["ToolTipMTUValue"]);
                ckConsola.Text = datosDelIdioma["StringsPrincipalLenguages"]["ShowConsole"];
                toolTip1.SetToolTip(ckConsola, datosDelIdioma["StringsPrincipalLenguages"]["ShowConsoleToolTip"]);
                labelDatos.Text = datosDelIdioma["StringsPrincipalLenguages"]["LanPlayConsoleLabel"];
                materialRaisedButton3.Text = datosDelIdioma["StringsPrincipalLenguages"]["ButtonCredits"];
                toolTip1.SetToolTip(materialRaisedButton3, datosDelIdioma["StringsPrincipalLenguages"]["Credits"]);
                colServidor.HeaderText = datosDelIdioma["StringsPrincipalLenguages"]["HeaderServer"];
                colUbicacion.HeaderText = datosDelIdioma["StringsPrincipalLenguages"]["HeaderLocation"];
                colEstatus.HeaderText = datosDelIdioma["StringsPrincipalLenguages"]["HeaderStatus"];
                colConectados.HeaderText = datosDelIdioma["StringsPrincipalLenguages"]["HeaderUsers"];
                colPing.HeaderText = datosDelIdioma["StringsPrincipalLenguages"]["HeaderPing"];
            }
        }

        /// <summary>
        /// Consulta la version actual utilizada de lan-play
        /// </summary>
        private void GetActualVersion()
        {
            if (File.Exists("versionActual.dat"))
            {
                StreamReader srArchivo = new StreamReader("versionActual.dat");
                string leer = srArchivo.ReadLine();
                if (!string.IsNullOrWhiteSpace(leer))
                {
                    versionActual = leer;
                }
            }
        }

        private void LoadPmtu(bool guardar = false)
        {
            if (!guardar)
            {
                int numeroConfiguracion = 0;
                if (File.Exists("config.txt"))
                {
                    StreamReader configFile = new StreamReader("config.txt");
                    string linea = "";
                    do
                    {
                        linea = configFile.ReadLine();
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            switch (numeroConfiguracion)
                            {
                                case 0:
                                    comboBox1.SelectedIndex = Convert.ToInt32(linea);
                                    break;
                                case 1:
                                    if (linea.Trim().Contains("1"))
                                    {
                                        ckInternet.Checked = true;
                                    }
                                    else
                                    {
                                        ckInternet.Checked = false;
                                    }
                                    break;
                                case 2:
                                    if (linea.Trim().Contains("1"))
                                    {
                                        ckConsola.Checked = true;
                                    }
                                    break;
                                case 3:
                                    txtPmtu.Text = linea;
                                    break;
                            }
                            numeroConfiguracion++;
                        }
                    }
                    while (!string.IsNullOrWhiteSpace(linea));
                    configFile.Close();
                }
            }
            else
            {
                StreamWriter configFile = new StreamWriter("config.txt", false);
                configFile.WriteLine(comboBox1.SelectedIndex.ToString());
                configFile.WriteLine(ckInternet.Checked ? "1" : "0");
                configFile.WriteLine(ckConsola.Checked ? "1" : "0");
                configFile.WriteLine(txtPmtu.Text);
                configFile.Close();
            }
        }

        /// <summary>
        /// Consulta los servidores recientes
        /// </summary>
        private void GetRecentsServers()
        {
            dgvRecientes.Rows.Clear();
            materialRaisedButton2.Enabled = false;
            cargaGrids.RunWorkerAsync();
        }

        #endregion

        private void ActivarDesactivarControles(bool accion)
        {
            materialRaisedButton1.Enabled = accion;
            //lbServidoresRecientes.Enabled = accion;
            txtVersion.Enabled = accion;
            dgvRecientes.Enabled = accion;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            bool ejecutar = true;
            ActivarDesactivarControles(false);
            if (Convert.ToInt32(materialRaisedButton1.Tag).Equals(0))
            {
                try
                {
                    if (!File.Exists("lan-play.exe") || (txtVersion.Text.Trim().Length > 0 && !txtVersion.Text.Trim().Equals(versionActual)))
                    {
                        versionActual = txtVersion.Text.Trim();
                        ejecutar = GenerarLanPlay();
                    }

                    if (ejecutar)
                    {
                        GetFunctionalDiviceId();
                        LaunchLanPlay();
                    }
                }
                catch
                {
                    MessageBox.Show("Ocurrio un error al crear lan-play.exe");
                    ActivarDesactivarControles(true);
                }
            }
            else
            {
                lanplayexit(null, null);
            }
        }

        #region ACTIVAR CLIENTE
        /// <summary>
        /// Se encarga de descargar y geberar el archivo lan-play.exe
        /// </summary>
        /// <returns>debuelve si se ejecuto correctamente</returns>
        private bool GenerarLanPlay()
        {
            lbStatus.Text = "Descargando lan-play.exe " + versionActual + "...";
            this.Refresh();
            string nombreLanPlay = "lan-play-win32.exe";
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            bool cerrar = false;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (Environment.Is64BitOperatingSystem)
            {
                nombreLanPlay = "lan-play-win64.exe";
            }

            if (versionActual.Equals("v0.0.3") || versionActual.Equals("v0.0.2") || versionActual.Equals("v0.0.1"))
            {
                nombreLanPlay = "lan-play.exe";
            }

            var webrequest = (HttpWebRequest)WebRequest.Create(
                string.Format("https://github.com/spacemeowx2/switch-lan-play/releases/download/{0}/{1}", versionActual, nombreLanPlay));

            webrequest.Method = WebRequestMethods.Http.Get;
            FileStream fileStream = File.Create("lan-play.exe");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        bytesRead = stream.Read(buffer, 0, bufferSize);
                        if (bytesRead == 0)
                        {
                            cerrar = true;
                        }

                        while (bytesRead != 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);
                            bytesRead = stream.Read(buffer, 0, bufferSize);
                        }
                        stream.Close();
                    }
                    response.Close();
                }
                if (cerrar)
                {
                    MessageBox.Show("No se pudo descargar el archivo, por favor verifique que la versión seleccionada se encuentre disponible en:\n https://github.com/spacemeowx2/switch-lan-play/releases");
                }
                else
                {
                    StreamWriter version = new StreamWriter("versionActual.dat", false);
                    version.WriteLine(versionActual);
                    version.Close();
                }
            }
            catch (WebException wex)
            {
                MessageBox.Show("No se pudo descargar el archivo, por favor verifique que la versión seleccionada se encuentre disponible en:\n https://github.com/spacemeowx2/switch-lan-play/releases" + wex.Message);
                cerrar = true;
            }
            finally
            {
                fileStream.Close();
                if (cerrar)
                {
                    File.Delete("lan-play.exe");
                    ActivarDesactivarControles(true);
                }
            }
            return !cerrar;
        }

        /// <summary>
        /// Obtiene el identificador correcto del dispositivo de red para lan-play
        /// </summary>
        private void GetFunctionalDiviceId()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ni.GetIPProperties().GatewayAddresses.Count > 0)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ni.OperationalStatus == OperationalStatus.Up)
                        {
                            identificadorIp = "\\Device\\NPF_" + ni.Id;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Se encarga de lanzar lan-play
        /// </summary>
        private void LaunchLanPlay()
        {
            string parametros = string.Empty;
            try
            {
                lbStatus.Text = "Lanzando lan-play...";
                this.Refresh();
                StreamWriter swPmtu = new StreamWriter("pmtu.txt", false);
                swPmtu.WriteLine(txtPmtu.Text.Trim());
                swPmtu.Close();
                parametros = "--pmtu "+txtPmtu.Text.Trim()+" --relay-server-addr " + dgvRecientes.CurrentRow.Cells[colServidor.Index].Value.ToString().Trim() + ":11451";
                //Si se encontro identificador lanzamos directamente el identificador a lan-play
                if (!string.IsNullOrWhiteSpace(identificadorIp))
                {
                    parametros = parametros + " --netif " + identificadorIp;
                }

                //Si la versión es la 0.0.5 o mayor se agrega el parametro --fake-internet
                if (!versionActual.Equals("v0.0.3") && !versionActual.Equals("v0.0.2") && !versionActual.Equals("v0.0.1") && !versionActual.Equals("v0.0.4") && ckInternet.Checked)
                {
                    parametros = parametros + " --fake-internet";
                }

                bat.StartInfo = new ProcessStartInfo("lan-play.exe", parametros);

                lbStatus.Text = "Conectando al servidor...";
                this.Refresh();
                bat.EnableRaisingEvents = true;
                bat.Exited += lanplayexit;
                bat.Start();
                materialRaisedButton1.Enabled = true;
                materialRaisedButton1.Tag = 1;
                materialRaisedButton1.Text = LenguagesManager.StringsPrincipalLenguages.ButtonDisconnect;
                m.ColorScheme = new ColorScheme(Primary.Green400, Primary.Green300, Primary.Green200, Accent.Green100, TextShade.WHITE);
                while (bat.MainWindowHandle.ToInt32() == 0)
                {
                }
                if (bat.MainWindowHandle.ToInt32() > 0)
                {
                    WinApi.SetParent(bat.MainWindowHandle, pnDatos.Handle);
                    WinApi.MoveWindow(bat.MainWindowHandle,
                        0, 0,
                        this.pnDatos.Width,
                        this.pnDatos.Height, 1);
                    WinApi.RemoveBorder(bat);
                }
                lbStatus.Text = "";
                this.Refresh();
            }
            catch
            {
                MessageBox.Show("Ocurrio un error al crear lan-play.bat");
                ActivarDesactivarControles(true);
            }
        }

        private IntPtr hWndApp = IntPtr.Zero;

        private void leerDatos()
        {
            
            LanzaStart();
            while (!bat.StandardOutput.EndOfStream)
            {
                bat.StandardOutput.ReadLine();
            }
            bat.WaitForExit();
        }

        private delegate void DelegadoParanoParams();

        private void LanzaStart()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegadoParanoParams(LanzaStart));
            }
            else
            {
                
            }
        }

        private delegate void lanplayexitDelegado(object sender, EventArgs e);

        private void lanplayexit(object sender, EventArgs e)
        {
            try
            {
                if(InvokeRequired)
                {
                    Invoke(new lanplayexitDelegado(lanplayexit), sender, e);
                }
                else
                {
                    try
                    {
                        bat.Kill();
                    }
                    catch
                    {
                    }
                    materialRaisedButton1.Text = LenguagesManager.StringsPrincipalLenguages.ButtonConnect;
                    materialRaisedButton1.Tag = 0;
                    m.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red700, Primary.Red600, Accent.Red400, TextShade.WHITE);
                    ActivarDesactivarControles(true);
                }
                
            }
            catch
            {
            }
        }

        #endregion

        private void ckConsola_CheckedChanged(object sender, EventArgs e)
        {
            if(ckConsola.Checked)
            {
                Width = 1131;
                materialRaisedButton3.Location = new Point(Width - 90, materialRaisedButton3.Location.Y);
            }
            else
            {
                Width = 549;
                materialRaisedButton3.Location = new Point(Width - 90, materialRaisedButton3.Location.Y);
            }
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Convert.ToInt32(materialRaisedButton1.Tag).Equals(1))
            {
                bat.Kill();
            }
            LoadPmtu(true);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            formCreditos formCreditosVer = new formCreditos(Convert.ToInt32(materialRaisedButton1.Tag).Equals(1) ? true : false);
            formCreditosVer.ShowDialog();
        }

        private void ObtenerEstatusDatos(string servidor,ref string estado, ref string latencia)
        {
            if (servidor.Equals("18.191.92.182"))
            {
                estado = "Online";
                latencia = "<1s";
            }
            else
            {
                PingReply ping = PingData.getPing(servidor.Trim());
                if (ping != null && ping.RoundtripTime > 0)
                {
                    estado = "Online";
                    latencia = ping.RoundtripTime + " ms";
                }
                else
                {
                    estado = "Offline";
                    latencia = ">1s";
                }
            }
        }

        private int ObtenerConectados(string servidor)
        {
            int conectados = 0;
            try
            {
                string datos = new WebClienteLanPlay().DownloadString("http://" + servidor + ":11451/info");
                datos = datos.Substring(datos.IndexOf(':') + 1);
                datos = datos.Substring(0, datos.IndexOf(','));
                conectados = Convert.ToInt32(datos);
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            return conectados;
        }

        #region Servidores

        private string DownloadStringServer(string url)
        {
            string responseFromServer = string.Empty;
            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.CachePolicy = noCachePolicy;
            webrequest.Method = WebRequestMethods.Http.Get;
        
            try
            {
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream2))
                        {
                            responseFromServer = reader.ReadToEnd();
                            reader.Close();
                        }
                        stream2.Close();
                    }
                    response.Close();
                }
        
                if (string.IsNullOrWhiteSpace(responseFromServer))
                {
                    responseFromServer = "";
                }
            }
            catch
            {
            }
            return responseFromServer;
        }
        private void cargaGrids_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<string> servers = new List<string>();
            string latencia = "";
            string estado = "";
            string server = "";
            string ubicacion = "";
            int conectados = 0;
            int indice = 0;
            string serversDatos = DownloadStringServer("https://raw.githubusercontent.com/Urferu/Lan-Play-Server-Manager/master/Servers/Servers.txt");
            if (!string.IsNullOrWhiteSpace(serversDatos))
            {
                servers.AddRange(serversDatos.Split('\n'));
            }

            foreach (string serverInfo in servers)
            {
                server = serverInfo.Split(',')[0];
                ubicacion = serverInfo.Split(',')[1];
                ObtenerEstatusDatos(server, ref estado, ref latencia);
                conectados = ObtenerConectados(server);
                if (conectados > 0)
                {
                    estado = "Online";
                }
                object[] datos = new object[] { server, ubicacion, estado, conectados, latencia };
                cargaGrids.ReportProgress(indice, (object)datos);
                indice++;
            }

            if (File.Exists("Servers.txt"))
            {
                StreamReader srArchivo = new StreamReader("Servers.txt");

                string leido = srArchivo.ReadLine();
                while (!string.IsNullOrWhiteSpace(leido))
                {
                    if (!servers.Contains(leido))
                    {
                        servers.Add(leido);
                        if (leido.Split(',').Length > 2)
                        {
                            server = leido.Split(',')[1];
                            ubicacion = leido.Split(',')[3];
                        }
                        else
                        {
                            server = leido.Split(',')[0];
                            ubicacion = leido.Split(',')[1];
                        }
                        ObtenerEstatusDatos(server, ref estado, ref latencia);
                        conectados = ObtenerConectados(server);
                        if (conectados > 0)
                        {
                            estado = "Online";
                        }
                        object[] datos = new object[] { server, ubicacion, estado, conectados, latencia };
                        cargaGrids.ReportProgress(indice, (object)datos);
                        indice++;
                    }
                }
                leido = srArchivo.ReadLine();
                srArchivo.Close();
            }
        }

        private void cargaGrids_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            object[] datos = (object[])e.UserState;
            dgvRecientes.Rows.Add("Server " + (e.ProgressPercentage + 1), datos[0], datos[1], datos[2], datos[3], datos[4]);
            if (datos[2].ToString().Equals("Online"))
            {
                dgvRecientes.Rows[e.ProgressPercentage].DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else
            {
                dgvRecientes.Rows[e.ProgressPercentage].DefaultCellStyle.BackColor = Color.IndianRed;
            }
            this.Refresh();
        }

        private void cargaGrids_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            materialRaisedButton2.Enabled = true;
            labelServers.Text = LenguagesManager.StringsPrincipalLenguages.Servers;
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            dgvRecientes.Rows.Clear();
            materialRaisedButton2.Enabled = false;
            labelServers.Text = LenguagesManager.StringsPrincipalLenguages.ServersLoading; ;
            cargaGrids.RunWorkerAsync();
        }
        #endregion
    }
}
