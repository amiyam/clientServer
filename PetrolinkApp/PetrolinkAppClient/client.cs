using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Threading.Tasks;


namespace PetrolinkAppClient
{
    public partial class client : Form
    {
        private List<string> text_to_send;              //message to be sent to server
        private List<string> curveRet;                  //Curves to be retrieved
        private List<string> curveRec;                  //Curves received
        private List<long> indices;                     //list of indices
        private List<string> curvesList;                //list of curves
        private DataTable dtData;                        //datatable to store curve data points
        private DataTable dtList;                       //datatable to store curves list

        private char receiveData;                       //flag to check which type of data is received : 'Y' - receive datapoints, 'N' - receive curves and index list
        private char firstData;                         //flag to check if the data row received is the first row : 'Y' - first row, 'N' - not first row
        private char receivedIndex;                     //flag to check if the index list is received
        private char receivedCurves;                    //flag to check if curves list is received

        private TcpClient clnt;                         
        private NetworkStream nst;

        private string logFilePath = "logClient.txt";   //log file saved in the root folder

        public client()
        {
            InitializeComponent();
        }

        private void client_Load(object sender, EventArgs e)
        {
            dgdCurves.Visible = false;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (clnt == null || !clnt.Connected)
                {
                    if (!txtHost.Text.Trim().Equals("") && !txtPort.Text.Trim().Equals(""))
                    {
                        clnt = new TcpClient();
                        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(txtHost.Text), int.Parse(txtPort.Text));
                        clnt.Connect(ep);               //attempting to connect to server

                        if (dtData != null)              //resetting the datatable to store the received datapoints
                            dtData.Reset();

                        if (dtList != null)
                            dtList.Reset();

                        if (clnt.Connected)
                        {
                            nst = clnt.GetStream();

                            receiveData = 'N';          //'N' - receive curves list and index list, 'Y' - receive datapoints
                            receivedIndex = 'N';        //'N' - not received index list
                            receivedCurves = 'N';       //'N' - not received curves list

                            await runRecieve();
                        }

                    }
                    else
                        MessageBox.Show("Please enter IP and port address of server");
                }
                else
                    MessageBox.Show("Already connected to server");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection request failed!");
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                        " Error in btnConnect_Click : " + ex.Message + Environment.NewLine);
            }
        }

        private async Task runRecieve()
        {
            while (clnt.Connected)
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    if (receiveData != 'Y')
                    {
                        if (receivedIndex != 'Y')
                        {
                            indices = (List<long>)formatter.Deserialize(nst);       //receiving index list
                            receivedIndex = 'Y';                                    //setting index flag to received
                        }
                        else if (receivedCurves != 'Y')
                        {
                            curvesList = (List<string>)formatter.Deserialize(nst);  //receiving curves list

                            dtList = ToDataTable(curvesList);

                            this.dgdCurves.Invoke(new MethodInvoker(delegate ()
                            {
                                dgdCurves.Visible = true;
                                dgdCurves.DataSource = dtList;                          //setting datagrid datasource to display list of curves
                                dgdCurves.AutoResizeColumns();
                            }));

                            receivedCurves = 'Y';                                               //setting both data and curves flag to received
                            receiveData = 'Y';
                        }
                    }
                    else
                    {
                        
                        await Task.Run(() => {
                            try
                            {
                                string[] dataPoints = (string[])formatter.Deserialize(nst);     //receive datapoints of requested curves
                                //dataPoints structure : first column - curve name; data points ; last two columns - min index, max index
                                curveRec.Add(dataPoints[0]);                                    //adding curve to received list to keep track of the curves received
                                dtData = ToDataTableDataPoints(dataPoints);
                            }
                            catch(Exception ex)
                            {
                                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                        " Error in runRecieve task : " + ex.Message + Environment.NewLine);
                            }
                        });    



                        if (dtData != null)
                        {
                    
                            this.dgdCurves.Invoke(new MethodInvoker(delegate ()
                            {
                                try
                                {
                                    BindingSource bs = new BindingSource();                 //setting datasource of datagrid to display datapoints
                                    bs.DataSource = dtData;
                                    dgdData.DataSource = bs;
                                    dgdData.AutoResizeColumns();

                                    dgdData.Columns["min"].Visible = false;
                                    dgdData.Columns["max"].Visible = false;
                                }
                                catch(Exception ex)
                                {
                                    File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                        " Error in runRecieve : " + ex.Message + Environment.NewLine);
                                }

                            }));

                            this.grpData.Invoke(new MethodInvoker(delegate ()
                            {
                                grpData.Visible = true;
                            }));
                        }

                    }

                }
                catch (Exception ex)
                {
                    File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                        " Error in runRecieve : " + ex.Message + Environment.NewLine);
                }
            }
        }

        //method to convert curves list to datatable
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            try
            {
                dataTable.Columns.Add("Curves");
                foreach (T item in items)
                {
                    dataTable.Rows.Add(item.ToString());
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in ToDataTable : " + ex.Message + Environment.NewLine);
            }
            return dataTable;
        }

        //method to add datapoints received to datatable
        public DataTable ToDataTableDataPoints(string[] dataPoints)
        {
            int i;

            try
            {
                if (firstData != 'N')       //first row, create structure of datatable
                {
                    firstData = 'N';
                    dtData = new DataTable();
                    dtData.Columns.Add("Curve");
                    dtData.Columns.Add("min");
                    dtData.Columns.Add("max");
                    for (i = 0; i < indices.Count; i++)
                    {
                        dtData.Columns.Add(indices[i].ToString());       //add columns corresponding to the indices
                    }
                }

                DataRow dr = dtData.NewRow();
                dr["Curve"] = dataPoints[0];        //first column of datapoint is the curve name

                for (i = 0; i < indices.Count; i++)
                {
                    string indx = indices[i].ToString();

                    if (dataPoints[i + 1].Equals("0"))
                        dataPoints[i + 1] = "";

                    dr[indx] = dataPoints[i + 1];
                }

                dr["min"] = dataPoints[i + 1];      //minimum index
                dr["max"] = dataPoints[i + 2];      //maximum index

                dtData.Rows.Add(dr);
            }
            catch(Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                      " Error in ToDataTableDataPoints : " + ex.Message + Environment.NewLine);
            }

            return dtData;
        }

        private void dgdCurves_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgdCurves.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(true)) //0 is the column number of checkbox
                {
                    row.Selected = true;
                    row.DefaultCellStyle.SelectionBackColor = Color.LightSlateGray;
                }
                else
                    row.Selected = false;
            }
        }
        
        private void dgdCurves_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgdCurves.IsCurrentCellDirty)
            {
                dgdCurves.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //forms list of curves selected and sends to server
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            string curve;
            curveRet = new List<string>();
            curveRec = new List<string>();
            foreach (DataGridViewRow r in dgdCurves.SelectedRows)
            {
                curve = r.Cells[1].Value.ToString();
                curveRet.Add(curve);
            }

            receiveData = 'Y';
            firstData = 'Y';

            if (dtData != null)
                dtData.Reset();

            requestForData();                 //sending request for data retrieval

        }

        private void requestForData()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(nst, curveRet);                        //sending list of curves for which data to be retreived
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                    " Error in requestForData : " + ex.Message + Environment.NewLine);
            }
        }

        //stop data transfer command sent to server while datapoints are being transferred
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                text_to_send = new List<string>();
                text_to_send.Add("Stop");
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(nst, text_to_send);
            }
            catch(Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in btnStop_Click : " + ex.Message + Environment.NewLine);
            }
        }

        //generate XML and download to system in the specified location
        private void btnXml_Click(object sender, EventArgs e)
        {
            string dataRow, headerRow;
            string XMLPath = "curvesXML.xml";
            int no_curves, i=0, no_rows=0;

            //file dialog to get path where to save the file
            saveFileXML.FileName = "curvesXML.xml";
            DialogResult result = saveFileXML.ShowDialog();         
            if (result == DialogResult.OK)
            {
                XMLPath = saveFileXML.FileName;
            }

            //XML generation started
            XmlTextWriter XMLWrite = new XmlTextWriter(XMLPath, System.Text.Encoding.UTF8);

            try
            {

                XMLWrite.WriteStartDocument();
                XMLWrite.WriteStartElement("log");

                XMLWrite.WriteStartElement("logCurveInfo");
                XMLWrite.WriteAttributeString("ID", "index");
                XMLWrite.WriteElementString("typeLogData", "long");
                XMLWrite.WriteEndElement();

                headerRow = "index";

                no_curves = curveRec.Count;

                no_rows = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    no_rows += 1;
                    if (no_rows <= no_curves)
                    {
                        XMLWrite.WriteStartElement("logCurveInfo");
                        XMLWrite.WriteAttributeString("ID", dr["curve"].ToString());
                        XMLWrite.WriteElementString("minIndex", dr["min"].ToString());
                        XMLWrite.WriteElementString("maxIndex", dr["max"].ToString());
                        XMLWrite.WriteElementString("typeLogData", "double");
                        XMLWrite.WriteEndElement();
                        headerRow += "," + dr["curve"].ToString();
                    }
                    else
                        break;
                }

                XMLWrite.WriteStartElement("logData");
                XMLWrite.WriteElementString("header", headerRow);
                for (i = 0; i < indices.Count; i++)
                {
                    dataRow = indices[i].ToString();
                    no_rows = 0;
                    foreach (DataRow dr in dtData.Rows)
                    {
                        no_rows += 1;
                        if (no_rows <= no_curves)
                            dataRow += "," + dr[indices[i].ToString()];
                    }
                    XMLWrite.WriteElementString("data", dataRow);
                }
                XMLWrite.WriteEndElement();

                XMLWrite.WriteEndElement();
                XMLWrite.WriteEndDocument();

                XMLWrite.Flush();
                XMLWrite.Close();

                MessageBox.Show("File downloaded successfully");

            }
            catch(Exception ex)
            {
                XMLWrite.Flush();
                XMLWrite.Close();
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                    " Error in btnXml_Click : " + ex.Message + Environment.NewLine);
            }
        }

        private void dgdCurves_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgdCurves.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(true)) //0 is the column number of checkbox
                {
                    row.Selected = true;
                    row.DefaultCellStyle.SelectionBackColor = Color.LightSlateGray;
                }
                else
                    row.Selected = false;
            }
        }

        //disconnect current connection
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                text_to_send = new List<string>();
                text_to_send.Add("Disconnect");
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(nst, text_to_send);
                clnt.Close();
                if (dtList != null)
                    dtList.Reset();
                if(dtData!=null)
                    dtData.Reset();

                dgdCurves.Visible = false;

                MessageBox.Show("Disconnected successfully");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, Environment.NewLine + DateTime.Now +
                                    " Error in btnDisconnect_Click : " + ex.Message + Environment.NewLine);
            }
        }
    }
}
