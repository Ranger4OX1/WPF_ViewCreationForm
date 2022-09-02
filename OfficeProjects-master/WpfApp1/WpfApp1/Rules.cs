using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Windows;
using System.Data;

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
    /// Bussiness Layer 
    /// Data Validation Rules 
    /// </summary>
    internal class Rules
    {
        DAL dal = new DAL();
        public bool modtreeValidation(modtree mods)
        {
            //s100 = childTypeComboBox.Text,
            //s101 = statusComboBox.Text,
            //s102 = docTypeComboBox.Text,
            //s105 = productCodeTextBox.Text,
            //s1 = moduleCodeTextBox.Text,
            //s2 = moduleNameTextBox.Text,
            //s3 = parentComboBox.Text,
            //s8 = moduleImgTextBox.Text,
            //s39 = treeLvlTextBox.Text,
            //s40 = prevLvlTextBox.Text,
            //n1 = Convert.ToInt32(displayOrderTextBox.Text)
            
            if(dal.Search(mods.s1.ToString())>0)
            {
                MessageBox.Show("Module code already exits!", "", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            else if (dal.Search(mods.s2.ToString()) > 0)
            {
                MessageBox.Show("Module Name already exits!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (mods.s100.ToString() != "MOD")
            {
                MessageBox.Show("Invalid DocType for Module Creation!\n Select \"MOD\"", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public bool IsUnique(string modS1, string secCode, int secLvl)
        {
            string code = modS1.Substring(0, 2);
            string len;
            if (secLvl == 1)
                len = "4";
            else
                len = "6";

            string sql = "select n100,s100,s102,s1,s2 from modtree where s1 = '"+ secCode +"' and LEN(s1)= " + len;

            DataTable result = dal.Exec(sql);

            if (result.Rows.Count <= 0)
            {
                return true;
            }

            MessageBox.Show("Section Code already exists in this module", "", MessageBoxButton.OK, MessageBoxImage.Error);

            return false;
        }

        public bool ScreenValidation(modtree screen)
        {
            string s1 = "'"+ screen.s1.ToString() +"'";
            if ( dal.IsUnique("modtree", "s1", s1) )
            {
                MessageBox.Show("true");
                return true;
            }
            return false;
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
}
