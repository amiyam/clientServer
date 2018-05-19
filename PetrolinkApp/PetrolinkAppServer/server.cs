using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;

namespace PetrolinkAppServer
{
    public partial class server : Form
    {

        private int no_curves;                      //no. of curves
        private int no_index;                       //no. of indices
        private int data_rate;                      //data rate of transfer

        private TcpListener listener;
        
        private List<string> curvesList;            //list of curves
        private List<long> indices;                 //list of indices
        private string urlFile = "test.csv";        //url of curves data file
        private double[,] curvesData;               //curve data points
        private string[] dataPoints;                //data structure to send data points

        private char stopTransfer;                  //flag to stop data transfer : 'Y' - stop transfer
        private char tcpListenerActive;             //flag to check if TCP listener is active
        private char fileLoaded;                    //flag to check if data file is loaded and parsed, to check before starting listening
        private char loadPage;                      //flag to check if page loading : 'N' page loading completed

        private string logFilePath;                 
        private string logFileName = "logServer.txt";

        public server()
        {
            InitializeComponent();
        }

        private void server_Load(object sender, EventArgs e)
        {
            txtPort.Text = "8910";       //for testing

            txtFile.Text = "test.csv";
            txtDataRate.Text = "1000";

            logFilePath = Properties.Settings.Default.logFilePath + logFileName;  //path name saved in settings attribute 'logFilePath'

            //getting host IP
            try
            {
                string hostName = Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
                for (int i = 0; i < ipHostInfo.AddressList.Length; ++i)
                {
                    if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        txtHost.Text = ipHostInfo.AddressList[i].ToString();
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                    " Error in server_Load : " + ex.Message + Environment.NewLine);
            }
            ////

            parseDataFile();
            loadPage = 'N';
        }

        //method to parse the data file
        private void parseDataFile()
        {
            try
            {
                curvesList = new List<string>();
                indices = new List<long>();

                urlFile = txtFile.Text;

                if (urlFile != "")
                {
                    if (rdXML.Checked == true)                  //checking of XML filetype selected
                    {
                        if (urlFile.ToUpper().Contains(".XML"))
                        {
                            parseXMLFile();
                        }
                        else
                        {
                            MessageBox.Show("Please upload file in the correct format");
                        }
                    }
                    else if (rdText.Checked == true)         //text file
                    {
                        parseTextFile();
                    }
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in parseDataFile : " + ex.Message + Environment.NewLine);
            }
        }

        //parse XML file
        private void parseXMLFile()
        {
            List<string> curvesDataList = new List<string>();

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(urlFile);

                XmlNodeList nodeList = doc.GetElementsByTagName("header");

                foreach (XmlNode node in nodeList)
                {
                    string header = node.InnerText.ToString();

                    var values = header.Split(',');
                    for (int i = 1; i < values.Length; i++)    //ignoring first column as it is 'index'
                        curvesList.Add(values[i]);
                }

                nodeList = doc.GetElementsByTagName("data");

                foreach (XmlNode node in nodeList)
                {
                    string data = node.InnerText.ToString();
                    curvesDataList.Add(data);
                }

                processCurveDataList(curvesDataList);

            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in parseXMLFile : " + ex.Message + Environment.NewLine);
            }

        }

        //parse text file
        private void parseTextFile()
        {
            List<string> curvesDataList = new List<string>();
            int first_row = 0;

            try
            {
                var reader = new StreamReader(urlFile);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (first_row == 0)                //first row in the file contains list of curves
                    {
                        var values = line.Split(',');
                        for (int i = 1; i < values.Length; i++)    //ignoring first column as it is 'index'
                            curvesList.Add(values[i]);
                    }
                    else
                    {
                        curvesDataList.Add(line);
                    }
                    first_row = first_row + 1;
                }

                processCurveDataList(curvesDataList);

            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in parseTextFile : " + ex.Message + Environment.NewLine);
            }
        }

        //process the list of datapoints obtained from file to store in a 2d array
        private void processCurveDataList(List<string> curvesDataList)
        {
            no_index = curvesDataList.Count;        //setting variables no. of index and no. of curves
            no_curves = curvesList.Count;

            curvesData = new double[no_index, no_curves];   //declaring dynamic 2d array to store the datapoints
            int row = 0;

            try
            {
                foreach (var ele in curvesDataList)
                {
                    int col = 0;
                    var values = ele.Split(',');
                    indices.Add(long.Parse(values[0]));

                    for (int i = 1; i < values.Length; i++)
                    {
                        col = i - 1;

                        if (values[i].Equals(""))               //to avoid error while parsing null string
                            values[i] = "0";

                        curvesData[row, col] = double.Parse(values[i]);
                    }
                    row = row + 1;
                }

                fileLoaded = 'Y';
                if(loadPage.Equals('N'))
                    MessageBox.Show("File loaded successfully");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in processCurveDataList : " + ex.Message + Environment.NewLine);
            }
        }

        //start listening for incoming connections
        private async void btnListen_ClickAsync(object sender, EventArgs e)
        {
            if (!txtHost.Text.Trim().Equals("") && !txtPort.Text.Trim().Equals(""))
            {
                if (fileLoaded.Equals('Y'))      //checking if file is loaded
                {
                    if (!tcpListenerActive.Equals('Y'))
                    {
                        try
                        {
                            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(txtHost.Text), int.Parse(txtPort.Text));
                            listener = new TcpListener(ep);
                            listener.Start();                   //started listening
                            tcpListenerActive = 'Y';            //setting flag to indicate server is active

                            txtStatus.AppendText("\n Listening at..." + ep.Address + " : " + ep.Port + "\n");

                            while (tcpListenerActive.Equals('Y'))
                            {
                                try
                                {
                                    await Task.Run(async () =>
                                    {
                                        TcpClient client = await listener.AcceptTcpClientAsync();    //accepting client
                                        sendInitialData(client);                                     //send index and curves list to client
                                        receiveDataAsync(client);                                    //receive data
                                    });                         
                                }
                                catch (Exception ex)
                                {
                                    File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                                        " Error in btnListen_Click : " + ex.Message + Environment.NewLine);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                                " Error in btnListen_ClickOuter : " + ex.Message + Environment.NewLine);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Server already in active state");
                    }
                }
                else
                    MessageBox.Show("Server not started. Please load the data file before starting the server.");
            }
        }

        //method to receive data
        private async Task receiveDataAsync(TcpClient client)
        {
            while (client.Connected)
            {
                try
                {
                    List < string > curvesRet = new List<string>();

                    curvesRet = await waitForData(client);

                    processReceivedDataAsync(curvesRet,client);

                }
                catch (Exception ex)
                {
                    File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                        " Error in receiveData : " + ex.Message + Environment.NewLine);
                }
            }
        }

        //send index and curves list
        private void sendInitialData(TcpClient client)
        {

            string clientEndPoint = client.Client.RemoteEndPoint.ToString();

            this.txtStatus.Invoke(new MethodInvoker(delegate ()
            {
                txtStatus.AppendText("\n Received connection request from : " + clientEndPoint + " \n");
            }));

            try
            {
                NetworkStream netStream = client.GetStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(netStream, indices);                    //sending index list

                bf = new BinaryFormatter();
                bf.Serialize(netStream, curvesList);                 //sending list of curves

            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in sendInitialData : " + ex.Message + Environment.NewLine);
            }

        }

        
        private async Task<List<string>> waitForData(TcpClient client)
        {
            List<string> curvesRet = new List<string>();

            await Task.Run(async () =>
            {
                var nst = client.GetStream();
                BinaryFormatter formatter = new BinaryFormatter();
                curvesRet = (List<string>)formatter.Deserialize(nst);
            });

            return curvesRet;
        }

        //process the received data
        private async void processReceivedDataAsync(List<string> curvesRet,TcpClient client)
        {
            try
            {
                string clientEndPoint = client.Client.RemoteEndPoint.ToString();

                if (curvesRet[0].ToString().Equals("Stop"))    //command sent from client to stop data transfer
                {
                    stopTransfer = 'Y';
                    this.txtStatus.Invoke(new MethodInvoker(delegate ()
                    {
                        txtStatus.AppendText("\n Stop data transfer request from client : " + clientEndPoint + "\n");
                    }));
                }
                else if (curvesRet[0].ToString().Equals("Disconnect"))    //command sent from client to stop data transfer
                {
                    client.Close();
                    this.txtStatus.Invoke(new MethodInvoker(delegate ()
                    {
                        txtStatus.AppendText("\n Disconnect request from client : " + clientEndPoint +"\n");
                    }));
                }
                else
                {
                    stopTransfer = 'N';
                    string curves = "";
                    foreach (var ele in curvesRet)
                        curves += ele.ToString() + "  ";

                    this.txtStatus.Invoke(new MethodInvoker(delegate ()
                    {
                        txtStatus.AppendText("\n Retrieval request from client: " + clientEndPoint + " - "
                            + curves + "\n");
                    }));

                    setDataRate();

                    await getCurvePointsAsync(curvesRet, client);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in processReceivedData : " + ex.Message + Environment.NewLine);
            }

        }

        //set the rate of data transfer
        private void setDataRate()
        {
            try
            {
                data_rate = int.Parse(txtDataRate.Text);
            }
            catch (Exception ex)
            {
                txtDataRate.Text = "1000";
                data_rate = 1000;
                txtStatus.AppendText("\n Error setting data rate. Data rate set to default. \n");
            }
        }

        //get the data for transfer
        private async Task getCurvePointsAsync(List<string> curvesRet,TcpClient client)
        {
            string min, min_found, max;
            dataPoints = new string[no_index + 3];

            try
            {
                foreach (var item in curvesRet)
                {
                    if (stopTransfer != 'Y')
                    {
                        int i = curvesList.IndexOf(item);
                        int j;

                        dataPoints[0] = item;  //adding curve as first column

                        min = indices[0].ToString();
                        max = indices[0].ToString();
                        min_found = "N";

                        for (j = 0; j < no_index; j++)
                        {
                            dataPoints[j + 1] = curvesData[j, i].ToString();

                            //finding min max indices
                            if (curvesData[j, i].ToString().Equals("0"))
                            {
                                if (min_found.Equals("N"))
                                {
                                    min = indices[j + 1].ToString();
                                }
                            }
                            else
                            {
                                max = indices[j].ToString();
                                min_found = "Y";
                            }
                            ////
                        }

                        dataPoints[j + 1] = min;
                        dataPoints[j + 2] = max;

                        await sendCurveDataAsync(dataPoints,client); 

                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in getCurvePoints : " + ex.Message + Environment.NewLine);
            }
        }

        //send the data
        private async Task sendCurveDataAsync(string[] dataPoints,TcpClient client)
        {
            string message = "";

            await Task.Delay(data_rate);

            NetworkStream netStream = client.GetStream();
            try
            {
                if (!stopTransfer.Equals('Y'))      //checking if stop flag set
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(netStream, dataPoints);
                    message = "\n Curve sent to client:" + dataPoints[0] + "\n";
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in sendCurveData : " + ex.Message + Environment.NewLine);
            }

            this.txtStatus.Invoke(new MethodInvoker(delegate ()
            {
                txtStatus.AppendText(message);
            }));

        }

        //to stop server
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                    tcpListenerActive = 'N';
                    txtStatus.AppendText("\n Stopped listening \n");
                }
                else
                {
                    txtStatus.AppendText("\n Listener not initialized \n");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in btnStop_Click : " + ex.Message + Environment.NewLine);
            }
        }

        //to upload data file
        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileData.FileName = "test.csv";
            DialogResult result = openFileData.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFile.Text = openFileData.FileName;
                parseDataFile();
            }
        }
    }
}
