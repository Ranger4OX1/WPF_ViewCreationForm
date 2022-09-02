using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//New
using System.Data.Entity;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private const Visibility visible = Visibility.Visible;

        private DAL dal = new DAL();
        private Rules rules = new Rules();
        private dv field = new dv();

        CollectionViewSource modtreeViewSource;
        CollectionViewSource DVViewSource;
        CollectionViewSource dvcpViewSource;

        DBEntities context = new DBEntities();

        DataTable dvcpDT = new DataTable();
        private modtree dvcpFScrn = new modtree();
        private modtree dvcpTScrn = new modtree();


        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();
        private modtree selectedScreen = new modtree();
        private modtree tab = new modtree();

        // Constructor
        public Window1(modtree mod, modtree secL1, modtree secL2, string state)
        {
            InitializeComponent();

            modtreeViewSource = ((CollectionViewSource)(FindResource("modtreeViewSource")));
            DVViewSource = ((CollectionViewSource)(FindResource("screenDVViewSource")));
            dvcpViewSource = ((CollectionViewSource)(FindResource("dvcpViewSource")));
            DataContext = this;

            context.dvs.Load();
            DVViewSource.Source = context.dvs.Local;

            context.modtrees.Load();
            modtreeViewSource.Source = context.modtrees.Local;

            if (mod.n100.ToString() == null || secL1.s1.ToString() == null)
            {
                MessageBox.Show("Module or Section not selected", "", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            else
            {
                selectedModule = mod;
                selectedSecLvl1 = secL1;
                selectedSecLvl2 = secL2;
                FillGrids();

                if (state == "c" || state == "C")
                {
                    screenCreationGrid.Visibility = visible;
                }
                else
                {
                    PopulateModTreGrid();
                    screenSelectionGrid.Visibility = visible;
                }
            }
            
        }


        //////
        /// PRIVATE METHODS
        private void FillGrids()
        {
            mod_idTextBox.Text = selectedModule.n100.ToString();
            mod_docTypeTextBox.Text = selectedModule.s102.ToString();
            mod_moduleCodeTextBox.Text = selectedModule.s1.ToString();
            mod_moduleNameTextBox.Text = selectedModule.s2.ToString();

            secInfo_sectionNameLbl.Content = selectedSecLvl1.s2.ToString();
            secInfo_sectionCodeLbl.Content = selectedSecLvl1.s1.ToString();
            if ( (selectedSecLvl2.n100 >= 1) )
            {
                secInfo_subSectionCodeLbl.Content = selectedSecLvl2.s1.ToString();
                secInfo_subSectionNameLbl.Content = selectedSecLvl2.s2.ToString();
            }
            
        }

        private void ResetControlls()
        {

        }

        /// <summary>
        /// Changes the color of the passed btn default makes all btn blue
        /// </summary>
        /// <param name="vt"></param>
        private void SetVTBtn(string vt)
        {
            switch (vt)
            {
                case "3000":
                    vt_3000.Background = Brushes.LightGreen;
                    break;
                case "3050":
                    vt_3050.Background = Brushes.LightGreen;
                    break;
                case "3100":
                    vt_3100.Background = Brushes.LightGreen;
                    break;
                case "3200":
                    vt_3200.Background = Brushes.LightGreen;
                    break;
                case "3300":
                    vt_3300.Background = Brushes.LightGreen;
                    break;
                case "3400":
                    vt_3400.Background = Brushes.LightGreen;
                    break;
                case "3450":
                    vt_3450.Background = Brushes.LightGreen;
                    break;
                case "rpt":
                case "RPT":
                    vt_rpt.Background = Brushes.LightGreen;
                    break;
                default:
                    vt_3000.Background = Brushes.LightSteelBlue;
                    vt_3050.Background = Brushes.LightSteelBlue;
                    vt_3100.Background = Brushes.LightSteelBlue;
                    vt_3200.Background = Brushes.LightSteelBlue;
                    vt_3300.Background = Brushes.LightSteelBlue;
                    vt_3400.Background = Brushes.LightSteelBlue;
                    vt_3450.Background = Brushes.LightSteelBlue;
                    vt_rpt.Background = Brushes.LightSteelBlue;
                    break;
            }
        }

        /// <summary>
        /// Get the buttons for the screen from the modtree
        /// </summary>
        /// <param name="code"></param>
        private void SetStsBtnGrid(string code)
        {
            bool[] btns = new bool[10];
            btns = dal.GetButtonInfo(code);

            if (btns[0]) { btns_addCB.IsChecked = true; }
            if (btns[1]) { btns_editCB.IsChecked = true; }
            if (btns[2]) { btns_deleteCB.IsChecked = true; }
            if (btns[3]) { btns_postCB.IsChecked = true; }
            if (btns[4]) { btns_previewCB.IsChecked = true; }
            if (btns[6]) { sts_postedCB.IsChecked = true; }
            if (btns[5]) { sts_unpostedCB.IsChecked = true; }
        }

        /// <summary>
        /// Gets the state of all the check boxes
        /// </summary>
        /// <returns>bool []</returns>
        private bool[] GetStsBtnGrid()
        {
            bool[] btns = new bool[10];

            for (int i = 0; i < 10; i++) { btns[i] = false; }

            if ((bool)btns_selAllCB.IsChecked)
            {
                btns[0] = btns[1] = btns[2] = btns[3] = btns[4] = true;
            }
            else
            {
                if ((bool)btns_addCB.IsChecked) { btns[0] = true; }
                if ((bool)btns_editCB.IsChecked) { btns[1] = true; }
                if ((bool)btns_deleteCB.IsChecked) { btns[2] = true; }
                if ((bool)btns_postCB.IsChecked) { btns[3] = true; }
                if ((bool)btns_previewCB.IsChecked) { btns[4] = true; }
            }

            if ((bool)btns_selAllCB.IsChecked)
            {
                btns[5] = btns[6] = true;
            }
            else
            {
                if ((bool)sts_unpostedCB.IsChecked) { btns[5] = true; }
                if ((bool)sts_postedCB.IsChecked) { btns[6] = true; }
            }           

            return btns;
        }

        /// <summary>
        /// Gets the modtree entry for the Button, Status or Tab passed
        /// </summary>
        /// <param name="pram"></param>
        /// <param name="scrn"></param>
        /// <returns>Modtree Object</returns>
        private modtree GetBtnStsTabModtree(string pram, modtree scrn)
        {
            modtree result = new modtree();

            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s8, s32, s35, s39, s40, n1

            //SCR A CMMR  03 | 10  12010301 Machine Reading 0 /CMMS/12/VLst/CMMR/SB01 3000  NULL MTRM  NULL  3 120103 NULL
            //BTN A   CMMR    03 | 10   1201030101  Add 0   2   3000                4   12010301    0.00
            //BTN A   CMMR    03 | 10   1201030102  Edit    0                       4   12010301    0.00
            //BTN A   CMMR    03 | 10   1201030103  Delete  0           glyphicon - trash         4   12010301    0.00
            //BTN A   CMMR    03 | 10   1201030104  Post    0           glyphicon - pushpin           4   12010301    0.00
            //BTN A   CMMR    03 | 10   1201030105  Preview 0                       4   12010301    0.00
            //STS A   CMMR    03 | 10   1201030106  Unposted0   2   3000                4   12010301    0.00
            //STS A   CMMR    03 | 10   1201030107  Posted  0   4   3100                4   12010301    0.00
            //TAB A   CMMR    03 | 10   1201030108  Main    0               MSHDD       4   12010301    0.00
            string temp;
            if (scrn.s1.Length > 6)
            {
                temp = "4";
            }else
            {
                temp = "3";
            }
            switch (pram)
            {
                case "add":
                    {
                        result = new modtree();
                        result.s100 = "BTN";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "01";
                        result.s2 = "Add";
                        result.s3 = "0";
                        result.s4 = "2";
                        result.s5 = scrn.s5;
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }                    
                    break;
                case "edit":
                    {
                        result = new modtree();
                        result.s100 = "BTN";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "02";
                        result.s2 = "Edit";
                        result.s3 = "0";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }
                    break;
                case "delete":
                    {
                        result = new modtree();
                        result.s100 = "BTN";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "03";
                        result.s2 = "Delete";
                        result.s3 = "0";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s8 = "glyphicon-trash";
                        result.s22 = "U";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }
                    break;
                case "post":
                    {
                        result = new modtree();
                        result.s100 = "BTN";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "04";
                        result.s2 = "Post";
                        result.s3 = "0";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s8 = "glyphicon-pushpin";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }
                    break;
                case "preview":
                    {
                        result = new modtree();
                        result.s100 = "BTN";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "05";
                        result.s2 = "Preview";
                        result.s3 = "0"; 
                        result.s39 = temp; 
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s40 = scrn.s1;
                    }
                    break;
                case "unposted":
                    {
                        result = new modtree();
                        result.s100 = "STS";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "06";
                        result.s2 = "Unposted";
                        result.s3 = "0";
                        result.s5 = scrn.s5;//check
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }
                    break;
                case "posted":
                    {
                        result = new modtree();
                        result.s100 = "STS";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "07";
                        result.s2 = "Posted";
                        result.s3 = "0";
                        result.s5 = "3100";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s39 = temp; 
                        result.s40 = scrn.s1;
                    }
                    break;
                case "main":
                    {
                        result = new modtree();
                        result.s100 = "TAB";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s1 = scrn.s1.ToString() + "08";
                        result.s2 = "Main";
                        result.s3 = "0";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        if (!string.IsNullOrEmpty(mains32TextBox.Text))
                            result.s32 = mains32TextBox.Text;
                        else
                        {
                            sysStatLbl.Content = "Pls fill Main s32";
                        }
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }
                    break;
                case "newTab":
                    {
                        result = new modtree();
                        result.s100 = "TAB";
                        result.s101 = scrn.s101;
                        result.s102 = scrn.s102;
                        result.s105 = scrn.s105;
                        result.s3 = "0";
                        result.s7 = scrn.s7;
                        result.s14 = "1";
                        result.s39 = temp;
                        result.s40 = scrn.s1;
                    }                        
                    break;
                default:
                    sysStatLbl.Content = "Error Get btns Modtree";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Inserts all the checked Btns, Tabs, Stss
        /// </summary>
        /// <param name="scrn"></param>
        /// <returns>Num of records inserted</returns>
        private string InsertBtns(modtree scrn)
        {
            int count = 2;
            modtree mod = new modtree();

            bool[] temp = GetStsBtnGrid();

            if (temp[0] == true)
            { 
                mod = GetBtnStsTabModtree("add", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[1] == true)
            {
                mod = GetBtnStsTabModtree("edit", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[2] == true)
            {
                mod = GetBtnStsTabModtree("delete", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[3] == true)
            {
                mod = GetBtnStsTabModtree("post", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[4] == true)
            {
                mod = GetBtnStsTabModtree("preview", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[5] == true)
            {
                mod = GetBtnStsTabModtree("unposted", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }
            if (temp[6] == true)
            {
                mod = GetBtnStsTabModtree("posted", scrn);
                dal.AddModtree(mod);
                count++;
                mod = null;
            }

            return count + " records inserted";
        }
        /// <summary>
        /// Copies fields from one screen to another
        /// </summary>
        /// <param name="fromTable"></param>
        /// <param name="frmScrn"></param>
        /// <param name="toScrn"></param>
        /// <returns>DataTable</returns>
        private void CopyDV()
        {
            if (dvcpFScrn.s100 == "SCR" || dvcpFScrn.s100 == "RPT")
            {
                if (dvcpFScrn.s100 == "SCR")
                {
                    foreach (DataRow row in dvcpDT.Rows)
                    {
                        var temp = new dv()
                        {
                            s100 = row[1].ToString(),
                            s101 = row[2].ToString(),
                            s107 = row[3].ToString(),
                            s1 = row[4].ToString(),
                            s2 = row[5].ToString(),
                            s3 = row[6].ToString(),
                            s4 = row[7].ToString(),
                            s6 = row[8].ToString(),
                            s7 = row[9].ToString(),
                            s8 = row[10].ToString(),
                            s9 = row[11].ToString(),
                            s10 = row[12].ToString(),
                            s13 = row[13].ToString(),
                            s14 = row[14].ToString(),
                            s31 = row[15].ToString(),
                            s32 = row[16].ToString(),
                            s35 = row[17].ToString()
                        };


                        if (temp.s107 == "M")
                        {
                            row[1] = dvcpTScrn.s102;
                        }
                        else if (temp.s107 == "D")//point 12
                        {
                            row[1] = dvcpTScrn.s102;
                            row[15] = dvcpTScrn.s1.ToString() + "08";
                            row[16] = dvcpTScrn.s32; 
                        }
                    }
                }
                else if (dvcpFScrn.s100 == "RPT")
                {
                    foreach (DataRow row in dvcpDT.Rows)
                    {
                        var temp = new dv()
                        {
                            s100 = row[1].ToString(),
                            s101 = row[2].ToString(),
                            s107 = row[3].ToString(),
                            s1 = row[4].ToString(),
                            s2 = row[5].ToString(),
                            s3 = row[6].ToString(),
                            s4 = row[7].ToString(),
                            s6 = row[8].ToString(),
                            s7 = row[9].ToString(),
                            s8 = row[10].ToString(),
                            s9 = row[11].ToString(),
                            s10 = row[12].ToString(),
                            s13 = row[13].ToString(),
                            s14 = row[14].ToString(),
                            s31 = row[15].ToString(),
                            s32 = row[16].ToString(),
                            s35 = row[17].ToString()
                        };
                        if (temp.s2 != "Format")
                        {
                            row[3] = dvcpTScrn.s1;
                        }                        
                    }
                }

                dal.InsertDT(dvcpDT);
                sysStatLbl.Content = "Feilds Succfully Copied!";
            }
            else if (dvcpFScrn.s100 != dvcpTScrn.s100)
            {
                MessageBox.Show("Unhandeled Case \nWindow1.CopyDV \n FromScreen.s100 != ToScreen.s100");
            }
            else
            {
                MessageBox.Show("Unhandeled Case \nWindow1.CopyDV \n S100!= RPT or SCR");
            }
        }


        //////
        ///GRID DATA GET METHODS
        private void PopulateModTreGrid()
        {
            string code;
            if( !string.IsNullOrEmpty( selectedSecLvl2.n100.ToString() ) )
            {
                code = selectedSecLvl1.s1.ToString();
                modtreeViewSource.Source = dal.Exec("SELECT n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s32, s39, s40, n1  FROM modtree WHERE(s100 = 'SCR' OR s100 = 'RPT') AND LEFT(s1,4)= '" + code + "'");
                sysStatLbl.Content = "Sec 1 Screens";
            }
            else
            {
                code = selectedSecLvl2.s1.ToString();
                modtreeViewSource.Source = dal.Exec("SELECT n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s32, s39, s40, n1  FROM modtree WHERE(s100 = 'SCR' OR s100 = 'RPT') AND LEFT(s1,6)= '" + code + "'");
                sysStatLbl.Content = "Sec 2 Screens";                
            }
            //context.SaveChanges();
            modtreeViewSource.View.Refresh();
        }
        private void PopulateDVGrid()
        {
            string code, sql;

            if (selectedScreen.s102 == null)
            {
                sysStatLbl.Content = "NO, Entries found for this screen";
                sql = "select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv ";
                DVViewSource.Source = dal.Exec(sql);
                //context.SaveChanges();
                modtreeViewSource.View.Refresh();
            }
            else if (selectedScreen.s102.ToString() == "RPT")
            {
                code = selectedScreen.s1.ToString();
                sql = "select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv where s100='RPT' AND s107='"+ code +"'";
                DVViewSource.Source = dal.Exec(sql);
                //context.SaveChanges();
                modtreeViewSource.View.Refresh();
            }
            else
            {
                code = selectedScreen.s102.ToString();
                sql = "select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv where s100 = '" + code + "'";
                DVViewSource.Source = dal.Exec(sql);
                //context.SaveChanges();
                modtreeViewSource.View.Refresh();
            }
        }
        private void PopulateDVCPGrid(DataTable dSet)
        {
            dvcpViewSource.Source = dSet;
            dvcpViewSource.View.Refresh();
            
        }

        //////
        /// SECTION BUTTON CLICK COMMANDS
        private void sel_ScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if ( !string.IsNullOrEmpty(selScr_idTextBox.Text) )
            {
                //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s32, s39, s40, n1
                selectedModule.n100 = Convert.ToDecimal(selScr_idTextBox.Text);
                selectedScreen.s100 = selScr_childTypeTextBox.Text;
                selectedScreen.s101 = selScr_statusTextBox.Text;
                selectedScreen.s102 = selScr_docTypeTextBox.Text;
                selectedScreen.s105 = selScr_productCodeTextBox.Text;
                selectedScreen.s1 = selScr_screenCodeTextBox.Text;
                selectedScreen.s2 = selScr_screenNameTextBox.Text;                
                selectedScreen.s3 = selScr_parentTextBox.Text;
                selectedScreen.s4 = selScr_URLTextBox.Text;
                selectedScreen.s5 = selScr_viewTypeTextBox.Text;
                selectedScreen.s32 = selScr_tableNameTextBox.Text;
                selectedScreen.s39 = selScr_treeLvlTextBox.Text;
                selectedScreen.s40 = selScr_prevLvlTextBox.Text;
                if (selScr_displayOrderTextBox.Text == "")
                    selectedScreen.n1 = 0;
                else
                    selectedScreen.n1 = Convert.ToDecimal(selScr_displayOrderTextBox.Text);

                SetVTBtn(selScr_viewTypeTextBox.Text);
                SetStsBtnGrid(selectedScreen.s1.ToString());
            }
            else
            {
                sysStatLbl.Content = "No screen selected.  Create New";
            }         

            screenSelectionGrid.Visibility = Visibility.Collapsed;
            screenCreationGrid.Visibility = visible;

            //screenHeaderGrid.IsEnabled = false;
            //scrnVTGrid.IsEnabled = false;
            screenButtonsGrid.IsEnabled = false;

        }


        private void refrehBtn_Click(object sender, RoutedEventArgs e)
        {
            if (screenSelectionGrid.Visibility == Visibility.Visible)
            {
                PopulateModTreGrid();
                sysStatLbl.Content = "Refreshed";
                modtreeViewSource.View.Refresh();
            }
            else if(screenCreationGrid.Visibility == Visibility.Visible)
            {
                PopulateDVGrid();
                DVViewSource.View.Refresh();
                sysStatLbl.Content = "Control Reset";
                ResetControlls();
            } 


            ////n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s8, s32, s39, s40, n1 

            //selectedScreen.s101 = screen_statusComboBox.Text;
            //selectedScreen.s102 = screen_docTypeTextBox.Text;
            //selectedScreen.s1 = screen_screenCodeTextBox.Text;
            //selectedScreen.s2 = screen_screenNameTextBox.Text;
            //selectedScreen.s4 = screen_urlTextBox.Text;
            //selectedScreen.s5 = screen_viewTypeTextBox.Text;
            //selectedScreen.s32 = screen_tableNameTextBox.Text;
            //selectedScreen.s39 = selScr_treeLvlTextBox.Text;
            //selectedScreen.s40 = selScr_prevLvlTextBox.Text;
        }
        private void copyDVBtn_Click(object sender, RoutedEventArgs e)
        {
            screenCreationGrid.Visibility = Visibility.Collapsed;
            dvCopyGrid.Visibility = visible;
            dvcpDataGrid.Visibility = visible;
        }


        private void field_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35

            bool addScrnFlag = false;

            field.s100 = selectedScreen.s102.ToString();
            field.s101 = dv_statusComboBox.Text;

            field.s1 = dv_fieldTextBox.Text.ToString();
            field.s2 = dv_nameTextBox.Text.ToString();
            field.s3 = dv_dataTypeTextBox.Text.ToString();
            field.s4 = dv_comboBoxTextBox.Text.ToString();
            field.s6 = dv_depCaseTextBox.Text.ToString();
            field.s7 = dv_normalisationTextBox.Text.ToString();
            field.s8 = dv_whrLeftTextBox.Text.ToString();
            field.s9 = dv_whrRightTextBox.Text.ToString();
            field.s10 = dv_rigixTextBox.Text.ToString();
            field.s13 = dv_listItemTextBox.Text.ToString();
            field.s14 = dv_validationTextBox.Text.ToString();

            switch (selectedScreen.s5)
            {
                case "3000":
                case "3050":
                    if (!string.IsNullOrEmpty(field.s107))
                    {
                        if (tab31 != "") { field.s31 = tab31; } else { sysStatLbl.Content = "Pls select a tab"; }

                        field.s32 = dv_tableNameTextBox.Text.ToString();
                        field.s35 = dv_botstrapTextBox.Text.ToString();

                        addScrnFlag = true;
                    }
                    else
                    {
                        sysStatLbl.Content = "Pls Select a tab";
                    }
                    break;
                case "3400":
                    {
                        field.s32 = dv_tableNameTextBox.Text.ToString();
                        field.s35 = dv_botstrapTextBox.Text.ToString();
                        addScrnFlag = true;
                    }
                    break;
                case "RPT":
                    field.s107 = selectedScreen.s1;

                    tab_Detail.IsEnabled = tab_Master.IsEnabled = tab_NTab1.IsEnabled = tab_NTab2.IsEnabled = true;

                    addScrnFlag = true;
                    break;
                default:
                    break;
            }

            if (addScrnFlag)
            {
                dal.AddDV(field);
                sysStatLbl.Content = "Field Succesfully Added";
            }
            if (field.s101 == "S")
            {              
                MessageBox.Show(dal.GetCtrl(field.n100, "dv") , "Ctrl Insert Query (Press Ctrl + C to copy)", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            //if (selectedScreen.s5 == "RPT") { PopulateDVGrid(); } else { PopulateDVGrid("rpt"); }
            PopulateDVGrid();
            dv_nameTextBox.Text = dv_fieldTextBox.Text = dv_dataTypeTextBox.Text = dv_comboBoxTextBox.Text = 
            dv_depCaseTextBox.Text = dv_normalisationTextBox.Text = dv_whrLeftTextBox.Text = dv_whrRightTextBox.Text = dv_rigixTextBox.Text = dv_listItemTextBox.Text = dv_validationTextBox.Text = dv_tabCdTextBox.Text =
            dv_tableNameTextBox.Text = dv_botstrapTextBox.Text = String.Empty;           
        }
        private void field_FinishBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void field_updateBtn_Click(object sender, RoutedEventArgs e)
        {
            dv temp = new dv()
            {
                n100 = Convert.ToDecimal(dv_idTextBox.Text),
                s1 = dv_fieldTextBox.Text.ToString(),
                s2 = dv_nameTextBox.Text.ToString(),
                s3 = dv_dataTypeTextBox.Text.ToString(),
                s4 = dv_comboBoxTextBox.Text.ToString(),
                s6 = dv_depCaseTextBox.Text.ToString(),
                s7 = dv_normalisationTextBox.Text.ToString(),
                s8 = dv_whrLeftTextBox.Text.ToString(),
                s9 = dv_whrRightTextBox.Text.ToString(),
                s10 = dv_rigixTextBox.Text.ToString(),
                s13 = dv_listItemTextBox.Text.ToString(),
                s14 = dv_validationTextBox.Text.ToString(),
                s35 = dv_botstrapTextBox.Text.ToString(),
                s31 = dv_tabCdTextBox.Text.ToString(),
                s32 = dv_tableNameTextBox.Text.ToString()
            };
            if (dal.UpdateScrn(temp))
                sysStatLbl.Content = "Record Updated";
            else
                sysStatLbl.Content = "Update Failed";


        }


        private void mdtreScreen_saveBtn_Click(object sender, RoutedEventArgs e)
        {
        }
        private void mdtreScreen_AddNewBtn_Click(object sender, RoutedEventArgs e)
        {
            bool addScrnFlag = false;

            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s8, s32, s35, s39, s40, n1
            selectedScreen.s101 = screen_statusComboBox.Text.ToString();
            selectedScreen.s102 = screen_docTypeTextBox.Text.ToString();
            selectedScreen.s105 = selectedSecLvl1.s105.ToString();
            selectedScreen.s1 = screen_screenCodeTextBox.Text.ToString();
            selectedScreen.s2 = screen_screenNameTextBox.Text.ToString();
            selectedScreen.s4 = screen_urlTextBox.Text.ToString();
            selectedScreen.s7 = screen_menuTypeComboBox.Text.ToString();
            selectedScreen.s14 = "1";
            selectedScreen.s32 = screen_tableNameTextBox.Text.ToString();
            selectedScreen.s39 = screen_treelvlTextBox.Text.ToString();

            if (selectedSecLvl2.n100 == 0)
                selectedScreen.s40 = screen_screenCodeTextBox.Text.Substring(0, 4);
            else
                selectedScreen.s40 = screen_screenCodeTextBox.Text.Substring(0,6);

            if ( rules.ScreenValidation(selectedScreen) )
            {
                switch (selectedScreen.s5)
                {
                    case "3000":
                        selectedScreen.s100 = "SCR";
                        selectedScreen.s3 = "1";
                        sysStatLbl.Content = InsertBtns(selectedScreen);
                        modtree main = GetBtnStsTabModtree("main", selectedScreen);
                        dal.AddModtree(main);
                        addScrnFlag = true;
                        break; 
                    case "3050":
                        selectedScreen.s100 = "SCR";
                        selectedScreen.s3 = "1";
                        sysStatLbl.Content = InsertBtns(selectedScreen);
                        modtree maintb = GetBtnStsTabModtree("main", selectedScreen);
                        dal.AddModtree(maintb);
                        MessageBox.Show("You Have Made a DISCO Screen.\n Pls Fill modtree.s20 and dv.s21 Appropriatly", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        addScrnFlag = true;
                        break;
                    case "3400":
                        selectedScreen.s100 = "SCR";
                        selectedScreen.s3 = "0"; 
                        addScrnFlag = true;
                        break;
                    case "RPT":
                        selectedScreen.s100 = "RPT";
                        selectedScreen.s102 = "RPT";
                        selectedScreen.s3 = "0";
                        selectedScreen.s4 = "RPT";
                        selectedScreen.s5 = "RPT";
                        selectedScreen.s7 = "RPTS";
                        selectedScreen.s35 = "1";
                        selectedScreen.s21 = screen_reportCaseTextBox.Text.ToString();

                        dv format = new dv()
                        {
                            //n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35
                            //1345765    RPT A   100309  S10 Format  IN05    [{id:"4", name: "Excel"}, { id: "5", name: "Portable Doc Format(PDF)"}, { id: "10", name: "Character Separated Values(CSV)"}, { id: "8", name: "Excel Record"}]	NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL
                            s100 = "RPT",
                            s101 = selectedScreen.s101,
                            s107 = selectedScreen.s1,
                            s1 = "S10",
                            s2 = "Format",
                            s3 = "IN05",
                            s4 = "[{id:\"4\", name: \"Excel\"}, { id: \"5\", name: \"Portable Doc Format(PDF)\"}, { id: \"10\", name: \"Character Separated Values(CSV)\"}, { id: \"8\", name: \"Excel Record\"}]",
                            n1 = 10
                        };
                        dal.AddDV(format);

                        addScrnFlag = true;
                        break;
                    default:
                        sysStatLbl.Content = "No View Type selected";
                        break;
                }
                if (addScrnFlag)
                {
                    dal.AddModtree(selectedScreen);
                    sysStatLbl.Content = "Screen Succesfully Added";
                }
                if (selectedScreen.s101 == "S")
                {                                     
                    Clipboard.SetText(dal.GetCtrl(selectedScreen.n100, "modtree"));
                    MessageBox.Show(dal.GetCtrl(selectedScreen.n100, "modtree"), "Ctrl Insert Query (Press Ctrl + C to copy)", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                sysStatLbl.Content = "Screen code already exists";
            }            
        }
        private void mdtreScreen_nextBtn_Click(object sender, RoutedEventArgs e)
        {
            tabsGrid.IsEnabled = dvGrid.IsEnabled = DVgrid.IsEnabled = true;
        }



        private void vt_3000_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3000");
            selectedScreen.s5 = "3000";
            sysStatLbl.Content = "3000";
            screen_urlTextBox.Text = "/"+ selectedModule.s102.ToString() +"/"+ selectedModule.s1.ToString() +"/VLst/"+ screen_docTypeTextBox.Text +"/SB01";
        }

        private void vt_3100_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3100");
            selectedScreen.s5 = "3100";
            sysStatLbl.Content = "3100";
        }

        private void vt_3050_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3050");
            selectedScreen.s5 = "3050";
            sysStatLbl.Content = "3050";
            // / CMMS / 12 / DISCO / CMPMG / SB01
            screen_urlTextBox.Text = "/" + selectedModule.s102.ToString() + "/" + selectedModule.s1.ToString() + "/DISCO/" + screen_docTypeTextBox.Text + "/SB01";
        }

        private void vt_3200_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3200");
            selectedScreen.s5 = "3200";
            sysStatLbl.Content = "3200";
        }

        private void vt_3300_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3300");
            selectedScreen.s5 = "3300";
            sysStatLbl.Content = "3300";
        }

        private void vt_3400_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3400");
            selectedScreen.s5 = "3400";
            sysStatLbl.Content = "3400";
            ///CMMS/12/CM04C/TEAM
            screen_urlTextBox.Text = "/" + selectedModule.s102.ToString() + "/" + selectedModule.s1.ToString() + "/CM04C/" + screen_docTypeTextBox.Text;

            screenButtonsGrid.IsEnabled = false;
            tabsGrid.IsEnabled = false;
            field_AddBtn.IsEnabled = true;
            field_FinishBtn.IsEnabled = true;
            field_updateBtn.IsEnabled = true;
        }

        private void vt_3450_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3450");
            selectedScreen.s5 = "3450";
            sysStatLbl.Content = "3450";
        }

        private void vt_rpt_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("rpt");
            selectedScreen.s5 = "RPT";
            sysStatLbl.Content = "RPT";
            screen_urlTextBox.Text = "RPT";
            menuTypeGrid.Visibility = Visibility.Collapsed;
            reportCaseGrid.Visibility = visible;
            screen_tableNameTextBox.IsEnabled = false;
            screen_reportCaseTextBox.Text = screen_screenCodeTextBox.Text;
            screen_docTypeTextBox.Text = "RPT";

            screenButtonsGrid.IsEnabled = tabsGrid.IsEnabled = false;
            field_AddBtn.IsEnabled = field_FinishBtn.IsEnabled = field_updateBtn.IsEnabled = true;
        }

        private string tab31;
        private int tempTabBtn = 0;
        private bool btn1ClickFlag = false;
        private bool btn2ClickFlag = false;
        private void tab_Master_Click(object sender, RoutedEventArgs e)
        {
            field.s107 = "M";
            field.s31 = "";
            sysStatLbl.Content = "Master tab Selected";
        }
        private void tab_Detail_Click(object sender, RoutedEventArgs e)
        {
            field.s107 = "D";
            sysStatLbl.Content = "Detail tab Selected";
            tab31 = selectedScreen.s1.ToString() + "08";
        }
        private void tab_NTab1_Click(object sender, RoutedEventArgs e)
        {
            if (btn1ClickFlag == false)
            {
                DVgrid.Visibility = Visibility.Collapsed;
                dvGrid.Visibility = Visibility.Collapsed;
                newTabGrid.Visibility = Visibility.Visible;
                btn1ClickFlag = true;
                tempTabBtn = 1;
            }
            else
            {
                field.s107 = tab_NTab1.Content.ToString();
                sysStatLbl.Content = tab_NTab1.Content.ToString() + "tab Selected";
                tab31 = selectedScreen.s1.ToString() + "099";
            }
        }
        private void tab_NTab2_Click(object sender, RoutedEventArgs e)
        {
            if (btn2ClickFlag == false)
            {
                DVgrid.Visibility = Visibility.Collapsed;
                dvGrid.Visibility = Visibility.Collapsed;
                newTabGrid.Visibility = Visibility.Visible;
                btn2ClickFlag = true;
                tempTabBtn = 2;                
            }
            else
            {
                field.s107 = tab_NTab2.Content.ToString();
                sysStatLbl.Content = tab_NTab2.Content.ToString() + "tab Selected";
                tab31 = selectedScreen.s1.ToString() + "10";
            }
        }
        private void tab_saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s32, s35, s39, s40, n1
            modtree tab = GetBtnStsTabModtree("newTab", selectedScreen);            
            tab.s2 = tab_nameTextBox.Text;
            tab.s32 = tab_s32TextBox.Text;

            if (tempTabBtn == 1)
            {
                tab_NTab1.Content = tab_nameTextBox.Text;
                tab.s1 = selectedScreen.s1 + "09";
            }
            else
            {
                tab_NTab2.Content = tab_nameTextBox.Text;
                tab.s1 = selectedScreen.s1 + "10";
            }

            dal.AddModtree(tab);
            DVgrid.Visibility = Visibility.Visible;
            dvGrid.Visibility = Visibility.Visible;
            newTabGrid.Visibility = Visibility.Collapsed;

            sysStatLbl.Content = "New Tab added";
        }


        private void btns_selAllCB_Checked(object sender, RoutedEventArgs e)
        {
            btns_deleteCB.IsChecked = btns_addCB.IsChecked = btns_editCB.IsChecked = btns_postCB.IsChecked = btns_previewCB.IsChecked = true;
        }
        private void sts_selAllCB_Checked(object sender, RoutedEventArgs e)
        {
            sts_postedCB.IsChecked = sts_unpostedCB.IsChecked = true;
        }
        private void btns_selAllCB_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        //DV Grid Btns
        private void dv_comboBoxCreateBtn_Click(object sender, RoutedEventArgs e)
        {
            dvcComboCreationGrid.Visibility = visible;
            screenCreationGrid.Visibility = Visibility.Collapsed;
        }

        //DVC Grid Btns
        private void dvc_backBtn_Click(object sender, RoutedEventArgs e)
        {
            dvcComboCreationGrid.Visibility = Visibility.Collapsed;
            screenCreationGrid.Visibility = visible;
        }
        private void dvc_addBtn_Click(object sender, RoutedEventArgs e)
        {
            dvcombo entry = new dvcombo();
            string s2Text="";

            //no condition if multiple selctd
            if (dvc_simlpeWCCB.IsChecked == false && dvc_simpleWNCCB.IsChecked == false && dvc_coplexCB.IsChecked == false)
            {
                sysStatLbl.Content = "Pls select a type first";
            }
            else
            {
                if (dvc_simlpeWCCB.IsChecked == true)
                {
                    s2Text = dvc_tableNameTextBox.Text + "," + dvc_selectFieldTextBox.Text + "," + dvc_whereClauseNameTextBox.Text + "," + dvc_orderByTextBox.Text + ";" + dvc_appendFieldTextBox.Text + "," + dvc_appendNameTextBox.Text;
                }
                else if (dvc_simpleWNCCB.IsChecked == true)
                {
                    s2Text = dvc_tableNameTextBox.Text + "," + dvc_selectFieldTextBox.Text + "," + dvc_whereClauseNameTextBox.Text + "," + dvc_orderByTextBox.Text;
                }
                else if (dvc_coplexCB.IsChecked == true)
                {
                    //
                }

                entry.s1 = dvc_comboCodeTextBox.Text;
                entry.s2 = s2Text;
                entry.s3 = dvc_remarrksTextBox.Text;

                dal.AddDVCombo(entry); 
            }
        }

        //DVCP Grid Btns
        private void dvcp_backBtn_Click(object sender, RoutedEventArgs e)
        {
            dvCopyGrid.Visibility = Visibility.Collapsed;
            dvcpDataGrid.Visibility = Visibility.Collapsed;
            screenCreationGrid.Visibility = visible;
        }
        private void dvcp_searchBtn_Click(object sender, RoutedEventArgs e)
        {
            dvcpFScrn = dal.GetRecord(dvcp_screenCodeTextBox.Text);
            dvcp_nameTextBox.Text = dvcpFScrn.s2;
            dvcp_CodeTextBox.Text = dvcpFScrn.s102;

            if(dvcpFScrn.s102 == "RPT" || dvcpFScrn.s100 == "RPT")
            {
                dvcpDT = dal.Exec("select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv where s107='" + dvcpFScrn.s1 + "'");
            }
            else
            {
                if (dvcpFScrn.s5 == "3000" || dvcpFScrn.s5 == "3050")
                {
                    if (dvcp_allCB.IsChecked == true)
                    {
                        dvcpDT = dal.Exec("SELECT n100, s100, s101, s107, s1, s2, s3, s4, s6, s7, s8, s9, s10, s13, s14, s31, s32, s35 FROM dv WHERE s100 = '" + dvcpFScrn.s102 + "'");
                    }
                    else if (dvcp_masterCB.IsChecked == true)
                    {
                        dvcpDT = dal.Exec("SELECT n100, s100, s101, s107, s1, s2, s3, s4, s6, s7, s8, s9, s10, s13, s14, s31, s32, s35 FROM dv WHERE s100 = '" + dvcpFScrn.s102 + "' AND s107 = '" + "M" + "'");
                    }
                    else if (dvcp_detailCB.IsChecked == true)
                    {
                        dvcpDT = dal.Exec("SELECT n100, s100, s101, s107, s1, s2, s3, s4, s6, s7, s8, s9, s10, s13, s14, s31, s32, s35 FROM dv WHERE s100 = '" + dvcpFScrn.s102 + "' AND s107 = '" + "D" + "'");
                    }
                    else
                    {
                        sysStatLbl.Content = "Pls Select an option";
                    }                            
                }
                else
                {
                    MessageBox.Show("exec");
                    dvcpDT = dal.Exec("select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv where s100='" + dvcpFScrn.s102 + "'");
                }
            }
            if (dvcpDT != null)
            {
                PopulateDVCPGrid(dvcpDT);
            }
            else
            {
                sysStatLbl.Content = "Failed to find DV records";
            }
        }
        private void dvcp_detailCB_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void dvcp_masterCB_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void dvcp_allCB_Checked(object sender, RoutedEventArgs e)
        {
            dvcp_allCB.IsChecked = dvcp_masterCB.IsChecked = dvcp_detailCB.IsChecked = true;
        }
        private void dvcp_copyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dvcp_tscreenCodeTextBox.Text))
            {
                dvcpTScrn = dal.GetRecord(dvcp_tscreenCodeTextBox.Text);
                if (dvcpTScrn.n100.ToString() != null)
                {
                    dvcp_tnameTextBox.Text = dvcpTScrn.s2;
                    dvcp_tCodeTextBox.Text = dvcpTScrn.s102;
                    dvcp_tmasterCodeTextBox.Text = dvcpTScrn.s32;
                    CopyDV();
                }
                else
                {
                    sysStatLbl.Content = "Screen not found";
                }
            }
            else
            {
                sysStatLbl.Content = "Pls Enter a screen code to copy to";
            }
        }
    }
}

