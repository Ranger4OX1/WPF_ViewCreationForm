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
using System.Windows.Navigation;
using System.Windows.Shapes;

// NEW
using System.Data;
using System.Data.Entity;

/// <AUTHOR>                                    ///
/// PROJECT ATHOR::-:Ehtisham M.A.:-::          ///
/// FOR ::-:Pearl-Solutions:-::                 ///
/// PROJECT DESCRIPTION::-:This is th first     ///
///     draft of a View creation system to      ///
///     ease the process of creation of modules ///
///     and screens:-::                         ///
/// </AUTHOR>                                   ///

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        private const Visibility visible = Visibility.Visible;
        DBEntities context = new DBEntities();
        CollectionViewSource modtreeViewSource;
        CollectionViewSource modViewSource;
        CollectionViewSource secL2ViewSource;

        private DAL dal = new DAL();
        private Rules rules = new Rules();

        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();
        private modtree selectedScreen = new modtree();

        public MainWindow()
        {
            InitializeComponent();

            modtreeViewSource = ((CollectionViewSource)(FindResource("modtreeViewSource")));
            modViewSource = ((CollectionViewSource)(FindResource("moduleViewSource")));
            secL2ViewSource = ((CollectionViewSource)(FindResource("secLvl2ViewSource")));

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateModTreGrid();
        }

        //////
        ///GRID DATA GET METHODS
        private void PopulateModTreGrid()
        {
            modtreeViewSource.Source = dal.Exec("select n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 from modtree where s100 = 'MOD'");
            //modtreeViewSource.View.Refresh();         

        }

        private void PopulateModuleGrid()
        { 
            string modCode = selectedModule.s1.ToString().Substring(0, 2);
            string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,2)= '" + modCode + "' AND LEN(s1)= 4 ORDER BY s1 DESC";
            modViewSource.Source = dal.Exec(sql);
        }

        private void PopulatesecLvl2ViewSource()
        {
            string secCode = selectedSecLvl1.s1.ToString();

            string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,4)= '" + secCode + "' AND LEN(s1)=6";
            secL2ViewSource.Source = dal.Exec(sql);
            //modtreeViewSource.View.Refresh();         
            //
        }

        //////
        /// STCAK PANEL COMMAND HANDLERS
        /// </summary>
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToLast();
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToPrevious();
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToNext();
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToFirst();
        }

        private void RefreshCommandCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            PopulateModTreGrid();

            sysStatLbl.Content = "Data Refreshed";
        }
        
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();
            //sectionGrid.Visibility = Visibility.Collapsed;
            ModuleInfoGrid.Visibility = Visibility.Collapsed;
            creationModtreeGrid.Visibility = Visibility.Collapsed;
            existingModtreeGrid.Visibility = visible;
            modtreeDataGrid.Visibility = visible;           
        }
        
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();
            ResetControls();
            sysStatLbl.Content = "System Ready";
        }

        private void SelectModuleCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();

            existingModtreeGrid.Visibility = Visibility.Collapsed;
            creationModtreeGrid.Visibility = visible;    
            modtreeDataGrid.Visibility = visible;

        }

        //////
        ///PRIVATE METHODS
        private void ResetControls()//// Cancels any input into the new customer form  
        {           
            childTypeComboBox.Text = string.Empty;
            statusComboBox.Text = string.Empty;
            docTypeTextBox.Text= string.Empty;
            productCodeTextBox.Text = string.Empty;
            moduleCodeTextBox.Text = string.Empty;
            moduleNameTextBox.Text = string.Empty;
            parentComboBox.Text = string.Empty;
            moduleImgTextBox.Text = string.Empty;
            treeLvlTextBox.Text = string.Empty;
            prevLvlTextBox.Text = string.Empty;
            displayOrderTextBox.Text = string.Empty;
            create_idTextBox.Text = string.Empty;
            create_childTypeTextBox.Text = string.Empty;
            create_statusTextBox.Text = string.Empty;
            create_docTypeTextBox.Text = string.Empty;
            create_productCodeTextBox.Text = string.Empty;
            create_moduleCodeTextBox.Text = string.Empty;
            create_moduleNameTextBox.Text = string.Empty;
            create_parentTextBox.Text = string.Empty;
            create_moduleImgTextBox.Text = string.Empty;
            create_treeLvlTextBox.Text = string.Empty;
            create_prevLvlTextBox.Text = string.Empty;
            create_displayOrderTextBox.Text = string.Empty;

        }

        private void Collapse()//// Collapse Evrything HAHAH!!
        {

            existingModtreeGrid.Visibility = Visibility.Collapsed;
            creationModtreeGrid.Visibility = Visibility.Collapsed;

            ModuleInfoGrid.Visibility = Visibility.Collapsed;
            SectionInfoGrid.Visibility = Visibility.Collapsed;
            newSectionLvl1Grid.Visibility = Visibility.Collapsed;
            newSectionLvl2Grid.Visibility = Visibility.Collapsed;

            selctSecL1Grid.Visibility = Visibility.Collapsed;
            selctSecL2Grid.Visibility = Visibility.Collapsed;
            selSecCntlBtnGrid.Visibility = Visibility.Collapsed;
            scrnCntlBtnGrid.Visibility = Visibility.Collapsed;

            modtreeDataGrid.Visibility = Visibility.Collapsed;
            secLvl2DataGrid.Visibility = Visibility.Collapsed;
            moduleDataGrid.Visibility = Visibility.Collapsed;
        }

        private void LoadScreenForm(string state)
        {
            Window1 window = new Window1(selectedModule, selectedSecLvl1, selectedSecLvl2 ,state);
            window.Show();
        }

        //////
        /// SECTION BUTTON CLICK COMMANDS
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            
            selectedModule = dal.GetRecord(create_moduleCodeTextBox.Text);
            sysStatLbl.Content = create_moduleCodeTextBox.Text;

            create_idTextBox.Text = selectedModule.n100.ToString();
            create_childTypeTextBox.Text = selectedModule.s100.ToString();
            create_statusTextBox.Text = selectedModule.s101.ToString();
            create_docTypeTextBox.Text = selectedModule.s102.ToString();
            create_productCodeTextBox.Text = selectedModule.s105;
            create_moduleCodeTextBox.Text = selectedModule.s1.ToString();
            create_moduleNameTextBox.Text = selectedModule.s2.ToString();
            create_parentTextBox.Text = selectedModule.s3.ToString();
            create_moduleImgTextBox.Text = selectedModule.s8.ToString() ;
            create_treeLvlTextBox.Text = selectedModule.s39.ToString();
            create_prevLvlTextBox.Text = selectedModule.s40.ToString();
            create_displayOrderTextBox.Text = selectedModule.n1.ToString();

            //modtreeDataGrid.Visibility = Visibility.Visible;
            //PopulateGrid();
            
        }

        //Dose not yet delete the whole Module YET
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("All parts of the Module will be deleted\n", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                dal.RemoveMod(Convert.ToDecimal(create_idTextBox.Text));
                sysStatLbl.Content = "Module " + create_moduleNameTextBox.Text + " Deleted";
            }            
        }

        private void StkPnlNavButtons_IsEnabled(bool flag)
        {
            if (flag)
            {
                btnFirst.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                btnLast.IsEnabled = true;
            }
            else
            {
                btnFirst.IsEnabled = false;
                btnPrev.IsEnabled = false;
                btnNext.IsEnabled = false;
                btnLast.IsEnabled = false;
            }
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {

            StkPnlNavButtons_IsEnabled(false);

            if (!string.IsNullOrEmpty(create_childTypeTextBox.Text) && !string.IsNullOrEmpty(create_statusTextBox.Text) && !string.IsNullOrEmpty(create_docTypeTextBox.Text) && !string.IsNullOrEmpty(create_productCodeTextBox.Text) && !string.IsNullOrEmpty(create_moduleCodeTextBox.Text) && !string.IsNullOrEmpty(create_displayOrderTextBox.Text)  && !string.IsNullOrEmpty(create_moduleNameTextBox.Text) && !string.IsNullOrEmpty(create_parentTextBox.Text) && !string.IsNullOrEmpty(create_moduleImgTextBox.Text) && !string.IsNullOrEmpty(create_treeLvlTextBox.Text) && !string.IsNullOrEmpty(create_prevLvlTextBox.Text))
            {
                selectedModule.n100 = Convert.ToDecimal(create_idTextBox.Text);
                selectedModule.s100 = create_childTypeTextBox.Text;
                selectedModule.s101 = create_statusTextBox.Text;
                selectedModule.s102 = create_docTypeTextBox.Text;
                selectedModule.s105 = create_productCodeTextBox.Text;
                selectedModule.s1 = create_moduleCodeTextBox.Text;
                selectedModule.s2 = create_moduleNameTextBox.Text;
                selectedModule.s3 = create_parentTextBox.Text;
                selectedModule.s8 = create_moduleImgTextBox.Text;
                selectedModule.s39 = create_treeLvlTextBox.Text;
                selectedModule.s40 = create_prevLvlTextBox.Text;
                selectedModule.n1 = Convert.ToDecimal(create_displayOrderTextBox.Text);

                PopulateModuleGrid();

                moduleDataGrid.Visibility = Visibility.Collapsed;
                modtreeDataGrid.Visibility = Visibility.Collapsed;
                creationModtreeGrid.Visibility = Visibility.Collapsed;

                edit_idTextBox.Text = selectedModule.n100.ToString();
                edit_moduleCodeTextBox.Text = selectedModule.s1.ToString();
                edit_moduleNameTextBox.Text = selectedModule.s2.ToString();
                edit_docTypeTextBox.Text = selectedModule.s102.ToString();

                ModuleInfoGrid.Visibility = visible;
                newSectionLvl1Grid.Visibility = visible;
                newSectionLvl2Grid.Visibility = visible;
            }
            else
            {
                MessageBox.Show("Please dont leave any fields empty", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void addScreenButton_Click(object sender, RoutedEventArgs e)
        {
            StkPnlNavButtons_IsEnabled(true);
            LoadScreenForm("C");
        }
        
        private void editScreenButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void addSecLvl1Button_Click(object sender, RoutedEventArgs e)
        {
            modViewSource.View.Refresh();
            if (!string.IsNullOrEmpty(newSectionLvl1_sectionNameTextBox.Text) && !string.IsNullOrEmpty(newSectionLvl1_sectionCodeTextBox.Text))
            {
                ///Checks if section lvl 1 code has the first 2 digits of the module code and has a len of 4 and checks if the code already exists in the database
                string modCode = selectedModule.s1.ToString().Substring(0, 2);
                if ( (newSectionLvl1_sectionCodeTextBox.Text.Substring(0,2) == modCode) && (newSectionLvl1_sectionCodeTextBox.Text.Length == 4) && (rules.IsUnique(selectedModule.s1.ToString(), newSectionLvl1_sectionCodeTextBox.Text,1) == true) )
                {
                    dal.InsertSectionLvl1(selectedModule, newSectionLvl1_sectionNameTextBox.Text, newSectionLvl1_sectionCodeTextBox.Text);
                    newSectionLvl2Grid.IsEnabled = true;
                    addScreenButton.Visibility = visible;

                    selectedSecLvl1.s1 = newSectionLvl1_sectionCodeTextBox.Text;
                    selectedSecLvl1.s2 = newSectionLvl1_sectionNameTextBox.Text;

                    PopulatesecLvl2ViewSource();
                    newSectionLvl1Grid.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid code for section", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill the Section Name and Code first", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            secL2ViewSource.View.Refresh();

        }

        private void addSecLvl2Button_Click(object sender, RoutedEventArgs e)
        { 

            if (!string.IsNullOrEmpty(newSectionLvl2_sectionNameTextBox.Text) && !string.IsNullOrEmpty(newSectionLvl2_sectionCodeTextBox.Text))
            {
                ///Checks if section lvl 2 code has the first 4 digits of sec lvl 1 and has a len of 6
                string modCode = newSectionLvl1_sectionCodeTextBox.Text;
                if ( (newSectionLvl2_sectionCodeTextBox.Text.Substring(0, 4) == modCode) && (newSectionLvl2_sectionCodeTextBox.Text.Length == 6 ) && (rules.IsUnique(selectedModule.s1.ToString(), newSectionLvl2_sectionCodeTextBox.Text, 2) == true) )
                {
                    dal.InsertSectionLvl2(selectedModule, newSectionLvl2_sectionNameTextBox.Text, newSectionLvl2_sectionCodeTextBox.Text);
                }
                else
                {
                    MessageBox.Show("Invalid code for section", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill the Section Name and Code first", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void createSectionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addModuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (
                !string.IsNullOrEmpty(productCodeTextBox.Text) && !string.IsNullOrEmpty(childTypeComboBox.Text)
                && !string.IsNullOrEmpty(statusComboBox.Text) && !string.IsNullOrEmpty(docTypeTextBox.Text)
                && !string.IsNullOrEmpty(moduleCodeTextBox.Text) && !string.IsNullOrEmpty(moduleNameTextBox.Text)
                && !string.IsNullOrEmpty(parentComboBox.Text) && !string.IsNullOrEmpty(moduleImgTextBox.Text)
                && !string.IsNullOrEmpty(treeLvlTextBox.Text) && !string.IsNullOrEmpty(prevLvlTextBox.Text)
                && !string.IsNullOrEmpty(displayOrderTextBox.Text)
                )
            {
                try
                {
                    modtree mt = new modtree()
                    {
                        //n100 = Convert.ToInt32(idTextBox.Text),
                        s100 = childTypeComboBox.Text,
                        s101 = statusComboBox.Text,
                        s102 = docTypeTextBox.Text,
                        s105 = productCodeTextBox.Text,
                        s1 = moduleCodeTextBox.Text,
                        s2 = moduleNameTextBox.Text,
                        s3 = parentComboBox.Text,
                        s8 = moduleImgTextBox.Text,
                        s39 = treeLvlTextBox.Text,
                        s40 = prevLvlTextBox.Text,
                        n1 = Convert.ToDecimal(displayOrderTextBox.Text)
                    };
                    if (rules.modtreeValidation(mt) == true)
                    {
                        dal.AddModtree(mt);
                        sysStatLbl.Content = "Record successfully saved.";
                        ResetControls();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured unable to save record!\n" + ex.Message, "", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    PopulateModTreGrid();
                }
            }
            else
            {
                MessageBox.Show("Please dont leave any fields empty", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            modtree mt = new modtree();

            mt.n100 = Convert.ToInt32(idTextBox.Text);
            mt.s100 = childTypeComboBox.Text;
            mt.s101 = statusComboBox.Text;
            mt.s102 = docTypeTextBox.Text;
            mt.s105 = productCodeTextBox.Text;
            mt.s1 = moduleCodeTextBox.Text;
            mt.s2 = moduleNameTextBox.Text;
            mt.s3 = parentComboBox.Text;
            mt.s8 = moduleImgTextBox.Text;
            mt.s39 = treeLvlTextBox.Text;
            mt.s40 = prevLvlTextBox.Text;
            mt.n1 = Convert.ToDecimal(displayOrderTextBox.Text);

            if (dal.UpdateMod(mt))
                sysStatLbl.Content = "Record Updated";
        }

        private void sec_addButton_Click(object sender, RoutedEventArgs e)
        {
            Collapse();

            ModuleInfoGrid.Visibility = visible;
            selctSecL1Grid.Visibility = visible;
            selctSecL2Grid.Visibility = visible;
            selSecCntlBtnGrid.Visibility = visible;
            scrnCntlBtnGrid.Visibility = visible;

            moduleDataGrid.Visibility = visible;


            PopulateModuleGrid();
            //string modCode = selectedModule.s1.ToString().Substring(0, 2);
            //string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,2)= '" + modCode + "' AND LEN(s1)= 4 ORDER BY s1 DESC";
            //modViewSource.Source = dal.Exec(sql);
            //modViewSource.View.Refresh();

        }

        private void selSecCntrl_ssL1Btn_Click(object sender, RoutedEventArgs e)
        {
            selectedSecLvl1.s1 = sec_secCodeTextBox.Text;
            selectedSecLvl1.s2 = sec_secNameTextBox.Text;
            selectedSecLvl1.s101 = selectedModule.s101;
            selectedSecLvl1.s105 = selectedModule.s105;
            selectedSecLvl1.s40 = selectedModule.s1.ToString().Substring(0, 2);

            sysStatLbl.Content = "Section  Selected";

            DataTable temp = dal.Exec("SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,4)= '" + sec_secCodeTextBox.Text + "'");
            if (temp.Rows.Count > 0)
            {
                selctSecL2Grid.IsEnabled = true;
                moduleDataGrid.Visibility = Visibility.Collapsed;
                secLvl2DataGrid.Visibility = visible;
                PopulatesecLvl2ViewSource();
                selSecCntrl_ssL2Btn.IsEnabled = true;
            }else
            {
                sysStatLbl.Content = "No Sub-Section Available";
            }
            scrnCntlBtnGrid.IsEnabled = true;            
        }

        private void selSecCntrl_ssL2Btn_Click(object sender, RoutedEventArgs e)
        {
            selectedSecLvl2.s1 = sec_l2secCodeTextBox.Text;
            selectedSecLvl2.s2 = sec_l2secNameTextBox.Text;
            selectedSecLvl2.s101 = selectedModule.s101;
            selectedSecLvl2.s105 = selectedModule.s105;
            selectedSecLvl2.s40 = selectedSecLvl1.s1.ToString().Substring(0, 4);

            sysStatLbl.Content = "Sub-Section Selected";
        }

        private void screenNav_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadScreenForm("C");
        }

        private void screenNav_editScrnBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadScreenForm("S");
        }

        ///// ednds heree
    }
}

/// <AUTHOR>                                    ///
/// PROJECT ATHOR::-:Ehtisham M.A.:-::          ///
/// FOR ::-:Pearl-Solutions:-::                 ///
/// PROJECT DESCRIPTION::-:This is th first     ///
///     draft of a View creation system to      ///
///     ease the process of creation of modules ///
///     and screens:-::                         ///
/// </AUTHOR>                                   ///