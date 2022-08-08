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

        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();
        private modtree selectedScreen = new modtree();

        public Window1(modtree mod, modtree secL1, modtree secL2, string state)
        {
            InitializeComponent();

            modtreeViewSource = ((CollectionViewSource)(FindResource("modtreeViewSource")));
            DataContext = this;


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
        //////
        ///GRID DATA GET METHODS
        ///
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

        

        //////
        ///STCAK PANEL COMMAND HANDLERS


        //////
        ///SECTION BUTTON CLICK COMMANDS
        private void sel_ScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (selScr_idTextBox.Text != null)
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
            }            

            screenSelectionGrid.Visibility = Visibility.Collapsed;
            screenCreationGrid.Visibility = visible;

        }

        private void refrehBtn_Click(object sender, RoutedEventArgs e)
        {
            if (screenSelectionGrid.Visibility == Visibility.Visible)
            {
                PopulateModTreGrid();
                sysStatLbl.Content = "Refreshed";
                modtreeViewSource.View.Refresh();
            }
            else
            {
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
    }
}
