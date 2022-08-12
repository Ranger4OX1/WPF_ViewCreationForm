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

        CollectionViewSource modtreeViewSource;
        CollectionViewSource DVViewSource;

        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();
        private modtree selectedScreen = new modtree();

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
            secInfo_subSectionCodeLbl.Content = selectedSecLvl2.s1.ToString();
            secInfo_subSectionNameLbl.Content = selectedSecLvl2.s2.ToString();
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
                default:
                    vt_3000.Background = Brushes.LightSteelBlue;
                    vt_3050.Background = Brushes.LightSteelBlue;
                    vt_3100.Background = Brushes.LightSteelBlue;
                    vt_3200.Background = Brushes.LightSteelBlue;
                    vt_3300.Background = Brushes.LightSteelBlue;
                    vt_3400.Background = Brushes.LightSteelBlue;
                    vt_3450.Background = Brushes.LightSteelBlue;
                    break;
            }
        }

        private void SetStsBtnGrid(string code)
        {
            bool[] btns = new bool[9];
            btns = dal.GetButtonInfo(code);

            if (btns[0]) { btns_addCB.IsChecked = true; }
            if (btns[1]) { btns_editCB.IsChecked = true; }
            if (btns[2]) { btns_deleteCB.IsChecked = true; }
            if (btns[3]) { btns_postCB.Content = true; }
            if (btns[4]) { btns_previewCB.Content = true; }
            if (btns[6]) { sts_postedCB.Content = true; }
            if (btns[7]) { sts_unpostedCB.Content = true; }
        }

        //////
        ///GRID DATA GET METHODS
        private void PopulateModTreGrid()
        {
            string code;
            if(selectedSecLvl2.n100.ToString() == null)
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
            dv field = new dv()
            {
                s100 = selectedScreen.s102.ToString(),
                s101 = selectedScreen.s101.ToString(),
                s107 = tabComboBox.Text.ToString(),
                s2 = dv_nameTextBox.Text.ToString(),
                s1 = dv_fieldTextBox.Text.ToString(),
                s3 = dv_dataTypeTextBox.Text.ToString(),
                s4 = dv_comboBoxTextBox.Text.ToString(),
                s6 =  dv_depCaseTextBox.Text.ToString(),
                s7 = dv_normalisationTextBox.Text.ToString(),
                s8 = dv_whrLeftTextBox.Text.ToString(),
                s9 = dv_whrRightTextBox.Text.ToString(),
                s10 = dv_rigixTextBox.Text.ToString(),
                s13 = dv_listItemTextBox.Text.ToString(),
                s14 = dv_validationTextBox.Text.ToString(),
                s31 = dv_tabCdTextBox.Text.ToString(),
                s32 = dv_tableNameTextBox.Text.ToString(),
                s35 = dv_botstrapTextBox.Text.ToString()
            };

            dal.AddDV(field);
            sysStatLbl.Content = "Field Succesfully Added";
        }

        private void mdtreScreen_AddNewBtn_Click(object sender, RoutedEventArgs e)
        {
            modtree screen = new modtree()
            {
                //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s32, s35, s39, s40, n1

                s100 = "SCR",
                s101 = screen_statusComboBox.Text.ToString(),
                s102 = screen_docTypeTextBox.Text.ToString(),
                s105 = selectedSecLvl1.s105.ToString(),
                s1 = screen_screenCodeTextBox.Text.ToString(),
                s2 = screen_screenNameTextBox.Text.ToString(),
                s4 = screen_urlTextBox.Text.ToString(),
                s32 = screen_tableNameTextBox.Text.ToString(),
                s39 = screen_treelvlTextBox.Text.ToString(),
                s40 = screen_prevLvlTextBox.Text.ToString(),
            };

            dal.AddModtree(screen);
            sysStatLbl.Content = "Field Succesfully Added";

        }

        private void vt_3000_Click(object sender, RoutedEventArgs e)
        {
            SetVTBtn("");
            SetVTBtn("3000");
            selectedScreen.s5 = "3000";
            sysStatLbl.Content = "3000";
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
    }
}
