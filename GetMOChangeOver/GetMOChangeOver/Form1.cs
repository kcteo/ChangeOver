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
using System.Configuration;

//using OIB.Tutorial;
//using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using System.ServiceModel;



#region DOC_NAMESPACES

using Asm.As.Oib.SiplacePro.Proxy.Types;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Business.Interfaces;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Collections;
using Asm.As.Oib.SiplacePro.Proxy.Business.Types;



#endregion

namespace GetMOChangeOver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public List<compWhereAbt> complocation = new List<compWhereAbt>();

        private void button1_Click(object sender, EventArgs e)
        {

            #region connection string to LES
            //string lesAddr = "PFSG-LES3";
            //string dbName = "SiplaceLES";
            //string userName = "lesreader";
            //string pwd = "read999";
            string setupdata = null;
            #endregion 

            #region connection string to SiplacePro
           //String address = "net.tcp://1202sgn157:9500/Siemens.Siplace.Oib.SiplacePro";
            
            #endregion

            #region connecting to LES

            string connectionString = null;
            SqlConnection cnn;
            SqlCommand command;
            string sql = null;
            SqlDataReader     dataReader;

            //clear setup listing combo box
            cbGetJob.Items.Clear();
            
            connectionString = String.Format("Data Source={0}; Initial Catalog={1};User ID={2};Password={3}",lesAddr,dbName,userName,pwd);
            //MessageBox.Show(connectionString);
            sql = "Select name from SiplaceLESUSR.RecipeCluster where Lineid='SX-Lines\\Epsilon-Line'";
            
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                       // MessageBox.Show(dataReader.GetValue(0) + "-");
                        setupdata = String.Format("{0} {1}\n",setupdata, dataReader.GetValue(0));
                        cbGetJob.Items.Add(dataReader.GetValue(0).ToString());

                    }
                    //MessageBox.Show(setupdata);
                    

                }
                else 
                {
                    MessageBox.Show("No more recorrds");
                }
                    
                dataReader.Close();
                command.Dispose();
                cnn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Cannot Open connection" + ex);
            }
            
#endregion

            


            #region connection to SiplacePro
            
            //this.Cursor = Cursors.WaitCursor;
/*
            try
            {
                SessionManager.CurrentSessionEndpoint = new EndpointAddress(new Uri(address));
                SessionManager.CurrentCallbackEndpointBase = string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);
                Session session = Session.CurrentSession;
                MessageBox.Show("Connected to Siplace Pro..");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting SIPLACE Pro: \n\n" + ex.Message);
                //throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
*/
            #endregion 
  
            #region Populate Job
/*
            List<Identity> identList;

            identList = SessionManager.CurrentSession.GetIdentities("Job:*");
            var query = from x in identList orderby x.FullPath select x.FullPath;
            cbGetJob.DataSource = query.ToList<String>();

            //MessageBox.Show(cbsetuplist.Text);
*/
            #endregion 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cbGetJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selJob;
            Job m_Job;        
            String receipedata = null;

            #region connection string to SiplacePro
            String address = "net.tcp://1202sgn157:9500/Siemens.Siplace.Oib.SiplacePro";

            #endregion


            selJob = cbGetJob.Text;

            //MessageBox.Show (selJob);

            #region connecting to LES

            string connectionString = null;
            SqlConnection cnn;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            
            //string lesAddr = "PFSG-LES3";
            //string dbName = "SiplaceLES";
            //string userName = "lesreader";
            //string pwd = "read999";

            connectionString = String.Format("Data Source={0}; Initial Catalog={1};User ID={2};Password={3}", lesAddr, dbName, userName, pwd);
            //MessageBox.Show(connectionString);
            
            sql = string.Format("SELECT [ParentKey],[ForeignKey],[I] FROM [SiplaceLESUSR].[RC_RE_Binding] WHERE [ParentKey] = '{0}' AND [I] = 0",selJob);
            
            //MessageBox.Show(sql);


            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        //receipedata = String.Format("{0} {1}\n", receipedata, dataReader.GetValue(1));
                        receipedata = dataReader.GetValue(1).ToString();

                    }
                    MessageBox.Show(receipedata);

                }
                else
                {
                    MessageBox.Show("No Receipe found ");
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Cannot Open connection" + ex);
            }

            #endregion

            
            #region connecting to SiplacePro
          
            try
            {
                SessionManager.CurrentSessionEndpoint = new EndpointAddress(new Uri(address));
                SessionManager.CurrentCallbackEndpointBase = string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);
                Session session = Session.CurrentSession;
                MessageBox.Show("Connected to Siplace Pro..");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting SIPLACE Pro: \n\n" + ex.Message);
                //throw;
            }


            #endregion 

            #region get unique component in placement list
            string panelName;
            //int placementlistOID;
            string boardPath;

            boardPath = String.Format("LES\\{0}", receipedata);
            MessageBox.Show(boardPath);

            try
            {
                Session session = SessionManager.CurrentSession;
                Board board = (Board)session.GetObject(@"Board:" + boardPath);
                if (board == null)
                {
                    MessageBox.Show("Board not found");

                }
                else
                {
                    MessageBox.Show("Gathering components placement");
                    //Checcking if placement list EXIST in the board for TOP side
                    if (board.TopSide.PlacementList == null)
                    {
                        MessageBox.Show("Placement List do not exist in this panel...");
                    }
                    else
                    {
                        MessageBox.Show("Processing Placement List....");
                        ProcessPlacementList(board.TopSide.PlacementList);
                    }

                    //Searching for placement list in child panels
                    MessageBox.Show("looking into child panel for placement list");
                    foreach (var panelList in board.TopSide.ChildPanels)
                    {
                        MessageBox.Show("Recursive into each child panel");
                        RecursionSearchPanel(panelList.Value);

                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
            
            #endregion


            #region get all setup object
            /*
            try
            {
                Session session = SessionManager.CurrentSession;
                m_Job = (Job)session.GetObject(@"Job:" + selJob);
                if (m_Job == null)
                {
                    MessageBox.Show("Job not found");
                    //Dictionary <KeyValuePair<KeyValuePair<StationSetup, StationLocation>,int>,IFeeder> dict = new Dictionary<KeyValuePair <KeyValuePair<StationSetup , StationLocation >,int>, IFeeder>();
                    //dict = m_Setup.FeederList;                  

                }
                else
                {
                    MessageBox.Show("Job found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
 */
            #endregion
        }

        private void ProcessPlacementList(PlacementList placeList)
        {

            #region LES Connection variable
            string connectionString = null;
            SqlConnection cnn;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            string compdata = null;
            #endregion

            connectionString = String.Format("Data Source={0}; Initial Catalog={1};User ID={2};Password={3}", lesAddr, dbName, userName, pwd);
            //MessageBox.Show(connectionString);

            

            string temptxt =null;
            //ComponentPlacement currentComPlc = null;
                var compNeeded = placeList.ComponentPlacements
                .Select(cp => new
                {
                    componentPlc = cp.Value.Component.Name
                }).Distinct();

            foreach (var compDict in compNeeded)
            {
                //MessageBox.Show(compDict.componentPlc);
                temptxt = "Epsilon";
                compWhereAbt comp = new compWhereAbt(compDict.componentPlc, 1, temptxt);
                complocation.Add(comp);          
            }

            string strTestComp = null;
            foreach (compWhereAbt item in complocation)
            {
                strTestComp = string.Format("PartNumber : {0} {1}", item.PartNumber, strTestComp);
            }
            MessageBox.Show(strTestComp);
        }

        private void RecursionSearchPanel(PanelMatrix pnlMtrx)
        {
            String plcListName;
            String msg;
            int plcListOID = 0;

            MessageBox.Show("In recursive panel");
            //Set exclusiveness
            if (pnlMtrx.Panel.PlacementList != null)
            {
                PlacementList placeList = pnlMtrx.Panel.PlacementList;

                plcListName = pnlMtrx.Panel.PlacementList.Name;
                plcListOID = pnlMtrx.Panel.PlacementList.OID; 
                
                msg = string.Format("Placement List Name : {0}", plcListName);
                MessageBox.Show(msg);
                msg = string.Format("Placement List OID : {0}", plcListOID);
                MessageBox.Show(msg);

                if (placementListOID == plcListOID)
                {
                    MessageBox.Show("Child Panel - PlacementList Exist");
                }
                else
                {
                    placementListOID = plcListOID;
                    ProcessPlacementList(placeList);
                }
            }
            else
            {
                MessageBox.Show("In recursive function - no placement list");
            }

            plcListName = null;

            foreach (var morePnlMtrxDck in pnlMtrx.Panel.ChildPanels)
            {
                //MessageBox.Show("In searching for inner panel");

                /*
                //display panel Name
                msg = string.Format("Panel Name : {0}",morePnlMtrxDck.Value.Name);
                MessageBox.Show(msg);
                */

                if (plcListName != morePnlMtrxDck.Value.Panel.PlacementList.Name)
                {
                    msg = morePnlMtrxDck.Value.Panel.PlacementList.Name;
                    MessageBox.Show(msg);
                    ProcessPlacementList(morePnlMtrxDck.Value.Panel.PlacementList);
                }
                else
                {
                    MessageBox.Show("Same Placement List");
                }
                plcListName = morePnlMtrxDck.Value.Panel.PlacementList.Name;

                //RecursionSearchPanel(morePnlMtrxDck.Value); //Recursion search on more mtrx
            }


        }

        int placementListOID = 0;

        #region connection string to LES
        string lesAddr = "PFSG-LES3";
        string dbName = "SiplaceLES";
        string userName = "lesreader";
        string pwd = "read999";
        #endregion 
    }
}
