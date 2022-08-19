using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//New

using System.Data.Entity;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private const Visibility visible = Visibility.Visible;
        DBEntities context = new DBEntities();
        private DAL dal = new DAL();
        private Rules rules = new Rules();
        private dv field = new dv();

        CollectionViewSource modtreeViewSource;
        CollectionViewSource DVViewSource;

        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();
        private modtree selectedScreen = new modtree();
        private modtree tab = new modtree();

        public Window1(modtree mod, modtree secL1, modtree secL2, string state)
        {
            InitializeComponent();

            modtreeViewSource = ((CollectionViewSource)(FindResource("modtreeViewSource")));
            DVViewSource = ((CollectionViewSource)(FindResource("screenDVViewSource")));
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
        
        private bool[] GetStsBtnGrid()
        {
            bool[] btns = new bool[10];

            for (int i = 0; i < 10; i++) { btns[i] = false; }

            if ((bool)btns_addCB.IsChecked) { btns[0] = true; }
            if ((bool)btns_editCB.IsChecked) { btns[1] = true; }
            if ((bool)btns_deleteCB.IsChecked ) { btns[2] = true; }
            if ((bool)btns_postCB.IsChecked ) { btns[3] = true; }
            if ((bool)btns_previewCB.IsChecked ) { btns[4] = true; }
            if ((bool)sts_unpostedCB.IsChecked ) { btns[5] = true; }
            if ((bool)sts_postedCB.IsChecked ) { btns[6] = true; }
            

            return btns;
        }

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
                        result.s8 = "glyphicon-trash";
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
            context.SaveChanges();
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
                context.SaveChanges();
                modtreeViewSource.View.Refresh();
            }
            else
            {
                code = selectedScreen.s102.ToString();
                sql = "select n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35 from dv where s100 = '" + code + "'";
                DVViewSource.Source = dal.Exec(sql);
                context.SaveChanges();
                modtreeViewSource.View.Refresh();
            }            
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

        private void mdtreScreen_saveBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void field_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35
            if (!string.IsNullOrEmpty(field.s107))
            {
                field.s100 = selectedScreen.s102.ToString();
                field.s101 = selectedScreen.s101.ToString();
                field.s2 = dv_nameTextBox.Text.ToString();
                field.s1 = dv_fieldTextBox.Text.ToString();
                field.s3 = dv_dataTypeTextBox.Text.ToString();
                field.s4 = dv_comboBoxTextBox.Text.ToString();
                field.s6 = dv_depCaseTextBox.Text.ToString();
                field.s7 = dv_normalisationTextBox.Text.ToString();
                field.s8 = dv_whrLeftTextBox.Text.ToString();
                field.s9 = dv_whrRightTextBox.Text.ToString();
                field.s10 = dv_rigixTextBox.Text.ToString();
                field.s13 = dv_listItemTextBox.Text.ToString();
                field.s14 = dv_validationTextBox.Text.ToString();
                if (tab31 != "") { field.s31 = tab31; } else { sysStatLbl.Content = "Pls select a tab"; }
                field.s32 = dv_tableNameTextBox.Text.ToString();
                field.s35 = dv_botstrapTextBox.Text.ToString();

                dal.AddDV(field);
                sysStatLbl.Content = "Field Succesfully Added";

                PopulateDVGrid();
                dv_nameTextBox.Text = String.Empty;
                dv_fieldTextBox.Text = String.Empty;
                dv_dataTypeTextBox.Text = String.Empty;
                dv_comboBoxTextBox.Text = String.Empty;
                dv_depCaseTextBox.Text = String.Empty;
                dv_normalisationTextBox.Text = String.Empty;
                dv_whrLeftTextBox.Text = String.Empty;
                dv_whrRightTextBox.Text = String.Empty;
                dv_rigixTextBox.Text = String.Empty;
                dv_listItemTextBox.Text = String.Empty;
                dv_validationTextBox.Text = String.Empty;
                dv_tabCdTextBox.Text = String.Empty;
                dv_tableNameTextBox.Text = String.Empty;
                dv_botstrapTextBox.Text = String.Empty;

            }
            else
            {
                sysStatLbl.Content = "Pls Select a tab";
            }             
        }

        private void field_FinishBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void field_updateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mdtreScreen_AddNewBtn_Click(object sender, RoutedEventArgs e)
        {
            //bool addScrnFlag = false;
            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s8, s32, s35, s39, s40, n1
            selectedScreen.s101 = screen_statusComboBox.Text.ToString();
            selectedScreen.s102 = screen_docTypeTextBox.Text.ToString();
            selectedScreen.s105 = selectedSecLvl1.s105.ToString();
            selectedScreen.s1 = screen_screenCodeTextBox.Text.ToString();
            selectedScreen.s2 = screen_screenNameTextBox.Text.ToString();
            selectedScreen.s4 = screen_urlTextBox.Text.ToString();
            selectedScreen.s32 = screen_tableNameTextBox.Text.ToString();
            selectedScreen.s39 = screen_treelvlTextBox.Text.ToString();
            selectedScreen.s40 = screen_prevLvlTextBox.Text.ToString();

            if ( rules.ScreenValidation(selectedScreen) )
            {
                dal.AddModtree(selectedScreen);
                sysStatLbl.Content = "Screen Succesfully Added";

                switch (selectedScreen.s5)
                {
                    case "3000":
                        selectedScreen.s100 = "SCR";
                        selectedScreen.s3 = "1";
                        sysStatLbl.Content = InsertBtns(selectedScreen);
                        modtree main = GetBtnStsTabModtree("main", selectedScreen);
                        dal.AddModtree(main);
                        //addScrnFlag = true;
                        break;
                    default:
                        sysStatLbl.Content = "No ViewType selected";
                        break;
                }
            }
            else
            {
                sysStatLbl.Content = "Screen code already exists";
            }            
        }

        private void vt_3000_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3000");
            selectedScreen.s5 = "3000";
            sysStatLbl.Content = "3000";
            screen_urlTextBox.Text = "/"+ selectedModule.s102.ToString() +"/"+ selectedModule.s1.ToString() +"/VLst/"+ screen_docTypeTextBox.Text +"/SB01";
            screen_prevLvlTextBox.Text = screen_screenCodeTextBox.Text.Substring(0, 4);
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
            selectedScreen.s5 = "rpt";
            sysStatLbl.Content = "RPT";
        }

        private string tab31;
        private void tab_Master_Click(object sender, RoutedEventArgs e)
        {
            field.s107 = "M";
            sysStatLbl.Content = "Master tab Selected";
        }

        private void tab_Detail_Click(object sender, RoutedEventArgs e)
        {
            field.s107 = "D";
            sysStatLbl.Content = "Detail tab Selected";
            tab31 = selectedScreen.s1.ToString() + "08";
        }

        private int tempTabBtn = 0;
        private bool btn1ClickFlag = false;
        private bool btn2ClickFlag = false;
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

        private void mdtreScreen_nextBtn_Click(object sender, RoutedEventArgs e)
        {
            tabsGrid.IsEnabled = dvGrid.IsEnabled = DVgrid.IsEnabled = true;
        }

    }
}
