﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//NEW
using System.Data.SqlClient;
using System.Windows;
using System.Data;

//using Microsoft.Data.SqlClient;
// For ConnString
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Configuration;


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
    /// Database Communication Layer
    /// </summary>
    internal class DAL
    {
        private DBEntities DBContext = null;

        SqlConnection conn = new SqlConnection();

        public DAL()
        {
            DBContext = new DBEntities();
        }

        ///Connection String Controller 
        ///<summary>
        /// Set DBContext.Database.Connection.ConnectionString
        /// </summary>
        /// <param name="initialCatalog"></param>
        /// <param name="dataSource"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="integratedSecuity"></param>
        /// <param name="configConnectionStringName"></param>
        public string setDatabase(string initialCatalog = "", string dataSource = "", string userId = "", string password = "", bool integratedSecuity = true)
        //string configConnectionStringName = "")/* this would be used if the*  connectionString name varied from *  the base EF class name */
        {
            string result;
            try
            {
                // use the const name if it's not null, otherwise
                // using the convention of connection string = EF contextname
                // grab the type name and we're done
                //var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                //    ? DBContext.GetType().Name
                //    : configConnectionStringName;

                // add a reference to System.Configuration
                //var entityCnxStringBuilder = new SqlConnectionStringBuilder(DBContext.Database.Connection.ConnectionString);
                //(System.Configuration.ConfigurationManager.ConnectionStrings[configNameEf].ConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder();

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // now flip the properties that were changed
                DBContext.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;

                result = "Succesfully connected to " + initialCatalog;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DAL Error\n Failed to DB Connection\n" + ex);
                result = "Failed to connecte to " + initialCatalog;
            }
            return result;
        }


        // x.s106.Trim();
        // if (string.IsNullOrEmpty(x.s105)) { } else { x.s105.Trim(); }
        // if (string.IsNullOrEmpty(x.s105)) { } else { x.s105 = x.s105.Trim(); }
        
        //Trim
        private modtree DeSpace(modtree x)
        {
            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s7, s8, s14, s32, s35, s39, s40, n1
            if (string.IsNullOrEmpty(x.s100)) { } else { x.s100 = x.s100.Trim(); }
            if (string.IsNullOrEmpty(x.s101)) { } else { x.s101 = x.s101.Trim(); }
            if (string.IsNullOrEmpty(x.s102)) { } else { x.s102 = x.s102.Trim(); }
            if (string.IsNullOrEmpty(x.s105)) { } else { x.s105 = x.s105.Trim(); }
            if (string.IsNullOrEmpty(x.s1))   { } else { x.s1 = x.s1.Trim(); }
            if (string.IsNullOrEmpty(x.s2))   { } else { x.s2 = x.s2.Trim(); }
            if (string.IsNullOrEmpty(x.s3))   { } else { x.s3 = x.s3.Trim(); }
            if (string.IsNullOrEmpty(x.s4))   { } else { x.s4 = x.s4.Trim(); }
            if (string.IsNullOrEmpty(x.s5))   { } else { x.s5 = x.s5.Trim(); }
            if (string.IsNullOrEmpty(x.s6))   { } else { x.s6 = x.s6.Trim(); }
            if (string.IsNullOrEmpty(x.s7))   { } else { x.s7 = x.s7.Trim(); }
            if (string.IsNullOrEmpty(x.s8))   { } else { x.s8 = x.s8.Trim(); }
            if (string.IsNullOrEmpty(x.s9))   { } else { x.s9 = x.s9.Trim(); }
            if (string.IsNullOrEmpty(x.s10))  { } else { x.s10 = x.s10.Trim(); }
            if (string.IsNullOrEmpty(x.s11))  { } else { x.s11 = x.s11.Trim(); }
            if (string.IsNullOrEmpty(x.s12))  { } else { x.s12 = x.s12.Trim(); }
            if (string.IsNullOrEmpty(x.s13))  { } else { x.s13 = x.s13.Trim(); }
            if (string.IsNullOrEmpty(x.s14))  { } else { x.s14 = x.s14.Trim(); }
            if (string.IsNullOrEmpty(x.s15))  { } else { x.s15 = x.s15.Trim(); }
            if (string.IsNullOrEmpty(x.s30))  { } else { x.s30 = x.s30.Trim(); }
            if (string.IsNullOrEmpty(x.s31))  { } else { x.s31 = x.s31.Trim(); }
            if (string.IsNullOrEmpty(x.s32))  { } else { x.s32 = x.s32.Trim(); }
            if (string.IsNullOrEmpty(x.s33))  { } else { x.s33 = x.s33.Trim(); }
            if (string.IsNullOrEmpty(x.s34))  { } else { x.s34 = x.s34.Trim(); }
            if (string.IsNullOrEmpty(x.s35))  { } else { x.s35 = x.s35.Trim(); }
            if (string.IsNullOrEmpty(x.s36))  { } else { x.s36 = x.s36.Trim(); }
            if (string.IsNullOrEmpty(x.s37))  { } else { x.s37 = x.s37.Trim(); }
            if (string.IsNullOrEmpty(x.s38))  { } else { x.s38 = x.s38.Trim(); }
            if (string.IsNullOrEmpty(x.s39))  { } else { x.s39 = x.s39.Trim(); }
            if (string.IsNullOrEmpty(x.s40))  { } else { x.s40 = x.s40.Trim(); }
            return x;
        }
        private dv DeSpace(dv x)
        {
            //n100, s100, s101, s102, s105, s1, s2, s3, s4, s5, s8, s32, s35, s39, s40, n1
            if (string.IsNullOrEmpty(x.s100)) { } else { x.s100 = x.s100.Trim(); }
            if (string.IsNullOrEmpty(x.s101)) { } else { x.s101 = x.s101.Trim(); }
            if (string.IsNullOrEmpty(x.s102)) { } else { x.s102 = x.s102.Trim(); }
            if (string.IsNullOrEmpty(x.s105)) { } else { x.s105 = x.s105.Trim(); }
            if (string.IsNullOrEmpty(x.s1)) { } else { x.s1 = x.s1.Trim(); }
            if (string.IsNullOrEmpty(x.s2)) { } else { x.s2 = x.s2.Trim(); }
            if (string.IsNullOrEmpty(x.s3)) { } else { x.s3 = x.s3.Trim(); }
            if (string.IsNullOrEmpty(x.s4)) { } else { x.s4 = x.s4.Trim(); }
            if (string.IsNullOrEmpty(x.s5)) { } else { x.s5 = x.s5.Trim(); }
            if (string.IsNullOrEmpty(x.s6)) { } else { x.s6 = x.s6.Trim(); }
            if (string.IsNullOrEmpty(x.s7)) { } else { x.s7 = x.s7.Trim(); }
            if (string.IsNullOrEmpty(x.s8)) { } else { x.s8 = x.s8.Trim(); }
            if (string.IsNullOrEmpty(x.s9)) { } else { x.s9 = x.s9.Trim(); }
            if (string.IsNullOrEmpty(x.s10)) { } else { x.s10 = x.s10.Trim(); }
            if (string.IsNullOrEmpty(x.s11)) { } else { x.s11 = x.s11.Trim(); }
            if (string.IsNullOrEmpty(x.s12)) { } else { x.s12 = x.s12.Trim(); }
            if (string.IsNullOrEmpty(x.s13)) { } else { x.s13 = x.s13.Trim(); }
            if (string.IsNullOrEmpty(x.s14)) { } else { x.s14 = x.s14.Trim(); }
            if (string.IsNullOrEmpty(x.s15)) { } else { x.s15 = x.s15.Trim(); }
            if (string.IsNullOrEmpty(x.s30)) { } else { x.s30 = x.s30.Trim(); }
            if (string.IsNullOrEmpty(x.s31)) { } else { x.s31 = x.s31.Trim(); }
            if (string.IsNullOrEmpty(x.s32)) { } else { x.s32 = x.s32.Trim(); }
            if (string.IsNullOrEmpty(x.s33)) { } else { x.s33 = x.s33.Trim(); }
            if (string.IsNullOrEmpty(x.s34)) { } else { x.s34 = x.s34.Trim(); }
            if (string.IsNullOrEmpty(x.s35)) { } else { x.s35 = x.s35.Trim(); }
            if (string.IsNullOrEmpty(x.s36)) { } else { x.s36 = x.s36.Trim(); }
            if (string.IsNullOrEmpty(x.s37)) { } else { x.s37 = x.s37.Trim(); }
            if (string.IsNullOrEmpty(x.s38)) { } else { x.s38 = x.s38.Trim(); }
            if (string.IsNullOrEmpty(x.s39)) { } else { x.s39 = x.s39.Trim(); }
            if (string.IsNullOrEmpty(x.s40)) { } else { x.s40 = x.s40.Trim(); }
            return x;
        }
        private dvcombo DeSpace(dvcombo x)
        {
            //n100,s1,s2,s3
            if (string.IsNullOrEmpty(x.s1)) { } else { x.s1 = x.s1.Trim(); }
            if (string.IsNullOrEmpty(x.s2)) { } else { x.s2 = x.s2.Trim(); }
            if (string.IsNullOrEmpty(x.s3)) { } else { x.s3 = x.s3.Trim(); }

            x.s1.Trim();
            x.s2.Trim();
            x.s3.Trim();
            return x;
        }


        ///Get Methods get Record from DB Using EF (LINQ)
        public modtree Get(int id)
        {
            return DBContext.modtrees.Find(id);
        }
        public modtree GetRecord(string sVal)
        {
            try
            {
                using (var ctx = new DBEntities())
                {
                    var mods = ctx.modtrees.Where(s => s.s1 == sVal).First();
                    return mods;
                }
            }
            catch (EntityException ex)
            {
                MessageBox.Show("Connection error\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                modtree mod = new modtree();
                return mod;
            }
        }
        public dv GetDV(int id)
        {
            return DBContext.dvs.Find(id);
        }
        public dv GetDV(string sVal)
        {
            try
            {
                using (var ctx = new DBEntities())
                {
                    var mods = ctx.dvs.Where(s => s.s1 == sVal).First();
                    return mods;
                }
            }
            catch (Exception)//here and extension
            {
                MessageBox.Show("DAL Error\n Failed to get Record");
                return null;
            }
        }
        public dv GetDVRecord(dv dvRec)
        {
            try
            {
                using (var ctx = new DBEntities())
                {
                    var mods = ctx.dvs.Where(s => s.s100 == dvRec.s100 && s.s107 == dvRec.s107 && s.s1 == dvRec.s1 && s.s31 == dvRec.s31).First();

                    return mods;
                }
            }
            catch (Exception)
            {
                dv mod = new dv();
                return mod;
            }
        }
        public modtreetran GetMTTRecord(string sVal)
        {
            try
            {
                using (var ctx = new DBEntities())
                {
                    var mods = ctx.modtreetrans
                                    .Where(s => s.s1 == sVal).First();
                    return mods;
                }
            }
            catch (Exception)
            {
                modtreetran mod = new modtreetran();
                return mod;
            }
        }
        public int Search(string sVal)
        {
            using (var ctx = new DBEntities())
            {
                var mods = ctx.modtrees
                                .Where(s => s.s1 == sVal).Count();
                return mods;
            }
        }
        public int SearchDVC(string sVal)
        {
            using (var ctx = new DBEntities())
            {
                var combos = ctx.dvcomboes
                                .Where(s => s.s1 == sVal).Count();
                return combos;
            }
        }
        public dvtran GetDVTRecord(string sVal)
        {
            try
            {
                using (var ctx = new DBEntities())
                {
                    var mods = ctx.dvtrans
                                    .Where(s => s.s1 == sVal).First();
                    return mods;
                }
            }
            catch (Exception)
            {
                dvtran mod = new dvtran();
                return mod;
            }
        }
        public dvcombo GetDVCombo(int id)
        {
            return DBContext.dvcomboes.Find(id);
        }

        ///Get Insert Query <summary>
        /// Get Insert Query
        /// </summary>
        /// <returns></returns>
        public string GetCtrl(decimal n100, string tbl)
        {
            string iQuery = "";

            if (tbl == "dv")
            {
                dv temp = GetDV(Convert.ToInt32(n100));
                iQuery = "INSERT INTO dvctrl (s100, s101, s107, s1, s2, s3, s4, s6, s7, s8, s9, s10, s13, s14, s31, s32, s35 ) " + " VALUES('" + temp.s100 + "','" + temp.s101 + "','" + temp.s107 + "','" + temp.s1 + "','" + temp.s2 + "','" + temp.s3 + "','" + temp.s4 + "','" + temp.s6 + "','" + temp.s7 + "','" + temp.s8 + "','" + temp.s9 + "','" + temp.s10 + "','" + temp.s13 + "','" + temp.s14 + "','" + temp.s31 + "','" + temp.s32 + "','" + temp.s35 + "')";
            }
            else
            {
                modtree temp = Get(Convert.ToInt32(n100));
                temp.n1 = 0;
                iQuery = "INSERT INTO modtreectrl (s100, s101, s102, s105, s1, s2, s3, s4, s5, s7, s8, s14, s32, s35, s39, s40, n1 ) " + " VALUES('" + temp.s100 + "','" + temp.s101 + "','" + temp.s102 + "','" + temp.s105 + "','" + temp.s1 + "','" + temp.s2 + "','" + temp.s3 + "','" + temp.s4 + "','" + temp.s5 + "','" + temp.s7 + "','" + temp.s8 + "','" + temp.s14 + "','" + temp.s32 + "','" + temp.s35 + "','" + temp.s39 + "','" + temp.s40 + "'," + temp.n1.ToString() + ")";
            }
            return iQuery;
        }


        //public void InsertSectionLvl1(modtree module, string secName, string secCode)
        //{
        //    try
        //    {
        //        modtree entry = new modtree();
        //        entry.s1 = secCode;
        //        entry.s2 = secName;
        //        entry.s3 = "1";
        //        entry.s4 = "A";
        //        entry.s39 = "1";
        //        entry.s100 = "SEC";
        //        entry.s102 = "SEC";
        //        entry.s101 = module.s101;
        //        entry.s105 = module.s105;
        //        entry.s40 = module.s1.ToString().Substring(0, 2);

        //        if (entry != null)
        //        {
        //            DBContext.modtrees.Add(entry);
        //            DBContext.SaveChanges();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
        //        throw;
        //    }
        //}

        //public void InsertSectionLvl2(modtree module, string secName, string secCode)
        //{
        //    try
        //    {
        //        modtree entry = new modtree();
        //        entry.s1 = secCode;
        //        entry.s2 = secName;
        //        entry.s3 = "1";
        //        entry.s39 = "2";
        //        entry.s40 = secCode.Substring(0, 4);
        //        entry.s100 = "SEC";
        //        entry.s101 = module.s101;
        //        entry.s102 = "SEC";
        //        entry.s105 = module.s105;

        //        if (entry != null)
        //        {
        //            DBContext.modtrees.Add(entry);
        //            DBContext.SaveChanges();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
        //        throw;
        //    }

        //}

        public List<modtree> GetAll()
        {
            using (var context = new DBEntities())
            {
                //List<modtree> list ;
                var posts = context.modtrees
                                   //.OrderByDescending(p => p.n100)
                                   .Where(p => p.s102 == "MOD").ToList();
                return posts;
            }
        }
        public bool UpdateMod(modtree modtre)
        {
            try
            {
                decimal id = modtre.n100;
                var modtreFind = this.Get(Convert.ToInt32(id));
                if (modtreFind != null)
                {
                    modtreFind.s100 = modtre.s100;
                    modtreFind.s101 = modtre.s101;
                    modtreFind.s102 = modtre.s102;
                    modtreFind.s1 = modtre.s1;
                    modtreFind.s2 = modtre.s2;
                    modtreFind.s3 = modtre.s3;
                    modtreFind.s8 = modtre.s8;
                    modtreFind.s39 = modtre.s39;
                    modtreFind.s40 = modtre.s40;
                    modtreFind.n1 = modtre.n1;
                    DBContext.SaveChanges();
                }
                return true;
                //MessageBox.Show("Sql Connected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //throw;
            }
            return false;
        }
        public bool UpdateScrn(dv dvData)
        {
            try
            {
                decimal id = dvData.n100;
                var dvFind = this.GetDV(Convert.ToInt32(id));
                if (dvFind != null)
                {
                    dvFind.s1 = dvData.s1;
                    dvFind.s2 = dvData.s2;
                    dvFind.s3 = dvData.s3;
                    dvFind.s4 = dvData.s4;
                    dvFind.s6 = dvData.s6;
                    dvFind.s7 = dvData.s7;
                    dvFind.s8 = dvData.s8;
                    dvFind.s9 = dvData.s9;
                    dvFind.s10 = dvData.s10;
                    dvFind.s13 = dvData.s13;
                    dvFind.s14 = dvData.s14;
                    dvFind.s35 = dvData.s35;
                    dvFind.s31 = dvData.s31;
                    dvFind.s32 = dvData.s32;
                    //DBContext.dvs.Local.(Convert.ToInt32(id), dvFind);//  --CHECK--
                    DBContext.SaveChanges();
                }
                return true;
                //MessageBox.Show("Sql Connected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //throw;
            }
            return false;
        }
        public void RemoveMod(decimal id)
        {
            try
            {
                var modtreObj = DBContext.modtrees.Find(id);
                if (modtreObj != null)
                {
                    DBContext.modtrees.Remove(modtreObj);
                    DBContext.SaveChanges();
                }
                MessageBox.Show("Module Deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //throw;
            }

        }

        /// <summary>
        ///  SQL-QUERIES
        public SqlConnection GetConnection()
        {
            string connString = DBContext.Database.Connection.ConnectionString;
            //MessageBox.Show(connString);
            //string sql = @"Data Source = localhost;
            //                Initial Catalog = LocalMaster;
            //                //Integrated Security = true ";
            //string sql = @"Data Source = 172.16.1.10;
            //                Initial Catalog = PearlErpMaster;
            //                UID = sa; Pwd = Pearl@2016;";
            conn = new SqlConnection(connString);
            //conn = new SqlConnection(sql);
            try
            {
                conn.Open();
                //MessageBox.Show("Sql Connected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //MessageBox.Show("Module code already exits!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection error\n" + ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw;
            }
            return conn;
        }

        /// <summary>
        /// "select * from " + tableName + " where " + column1 + " = " + where1
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column1"></param>
        /// <param name="where1"></param>
        /// <returns> TRUE IF NO RECORD FOUND </returns>
        public bool IsUnique(string tableName, string column1, string where1)//, string column2, string where2)
        {
            string sql = "select * from " + tableName + " where " + column1 + " = " + where1;
            //+ " and " + column2 + " = " + where2;

            DataTable result = Exec(sql);

            if (result.Rows.Count >= 1)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Executes the passed query
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Return System.Data.DataTable</returns>
        public DataTable Exec(string sql)
        {
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                conn = GetConnection();
                da = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                da.Fill(dt);
                conn.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw;
            }

            conn.Close();
            return dt;
        }

        public bool[] GetButtonInfo(string code)
        {
            bool[] result = new bool[10];
            string temp;
            for (int i = 0; i < 10; i++)// from 0 to 9
            {
                result[i] = false;//initialisation of element
                temp = code + "0" + (i + 1).ToString();//sets s1 
                if (Search(temp) > 0)//searches for that s1 in the db
                {
                    result[i] = true;// makes that element true if found
                }
            }
            return result;
        }

        ///Database Insertiion Methods
        public void AddModtree(modtree modtre)
        {
            try
            {
                if (modtre != null)
                {
                    modtre = DeSpace(modtre);

                    DBContext.modtrees.Add(modtre);
                    DBContext.SaveChanges();

                    modtree x = new modtree();
                    modtreetran mtt = new modtreetran();

                    x = GetRecord(modtre.s1);

                    mtt.n101 = x.n100;
                    mtt.s100 = "en-US";
                    mtt.s101 = "A";
                    mtt.s1 = x.s2;

                    DBContext.modtreetrans.Add(mtt);
                    DBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("null modtre");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

        }
        public void AddDV(dv dvData)
        {
            //s100,s107,s1,s31
            try
            {
                if (dvData != null)
                {
                    dvData = DeSpace(dvData);

                    DBContext.dvs.Add(dvData);
                    DBContext.SaveChanges();

                    dv x = new dv();
                    dvtran dvt = new dvtran();

                    x = GetDVRecord(dvData);

                    dvt.n101 = x.n100;
                    dvt.s100 = "en-US";
                    dvt.s101 = "A";
                    dvt.s1 = x.s2;

                    DBContext.dvtrans.Add(dvt);
                    DBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Data NUllr", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        public void AddDVCombo(dvcombo dvcData)
        {
            //n100,s1,s2,s3
            try
            {
                if (dvcData != null)
                {
                    dvcData = DeSpace(dvcData);

                    //DBContext.dvcomboes.Add(dvcData);
                    //DBContext.SaveChanges();
                    DataTable temp = Exec("SELECT MAX(n100) FROM dvcombo");
                    DataRow row = temp.Rows[0];
                    string x = (row[0]).ToString();
                    int s = Convert.ToInt32(x) + 1;
                    x = s.ToString();
                    string sql = "insert into dvcombo (n100,s1,s2,s3) values(" + x + ",'" + dvcData.s1 + "','" + dvcData.s2 + "','" + dvcData.s3 + "')";
                    SqlDataAdapter da = new SqlDataAdapter();

                    try
                    {
                        conn = GetConnection();
                        da.InsertCommand = new SqlCommand(sql, conn);
                        da.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("inserted");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Data NUll", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        public void AddModTreeTran(modtreetran mttData)
        {
            try
            {
                if (mttData != null)
                {
                    DBContext.modtreetrans.Add(mttData);
                    DBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Data NUllr", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        public void AddDVTran(dvtran dvtData)
        {
            try
            {
                if (dvtData != null)
                {
                    DBContext.dvtrans.Add(dvtData);
                    DBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Data NUllr", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void InsertDT(DataTable dt)
        {
            try
            {
                //n100,s100,s101,s107,s1,s2,s3,s4,s6,s7,s8,s9,s10,s13,s14,s31,s32,s35
                if (dt == null)
                {
                    MessageBox.Show("DAL Error\n Empty DataTablez");
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
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
                        AddDV(temp);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("DAL Error\n Failed to insert DT Records");
            }
        }

    }
}

//namespace ExtensionMethods
//{
//    using WpfApp1;
//    using System.Configuration;
//    public static class ConnectionTools
//    {
        
//        public sealed class EntityConnectionStringBuilder : System.Data.Common.DbConnectionStringBuilder { }
//        // all params are optional
//        public static void ChangeDatabase(
//            this DBEntities source,
//            string initialCatalog = "",
//            string dataSource = "",
//            string userId = "",
//            string password = "",
//            bool integratedSecuity = true,
//            string configConnectionStringName = "")
//        /* this would be used if the
//        *  connectionString name varied from 
//        *  the base EF class name */
//        {
//            try
//            {
//                // use the const name if it's not null, otherwise
//                // using the convention of connection string = EF contextname
//                // grab the type name and we're done
//                var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
//                    ? source.GetType().Name
//                    : configConnectionStringName;

//                // add a reference to System.Configuration

//                EntityConnectionStringBuilder entityCnxStringBuilder = new EntityConnectionStringBuilder();
//                    //System.Configuration.ConfigurationManager.ConnectionStrings[configNameEf].ConnectionString);

//                // init the sqlbuilder with the full EF connectionstring cargo
//                var sqlCnxStringBuilder = new SqlConnectionStringBuilder(entityCnxStringBuilder.ProviderConnectionString);

//                // only populate parameters with values if added
//                if (!string.IsNullOrEmpty(initialCatalog))
//                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
//                if (!string.IsNullOrEmpty(dataSource))
//                    sqlCnxStringBuilder.DataSource = dataSource;
//                if (!string.IsNullOrEmpty(userId))
//                    sqlCnxStringBuilder.UserID = userId;
//                if (!string.IsNullOrEmpty(password))
//                    sqlCnxStringBuilder.Password = password;

//                // set the integrated security status
//                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

//                // now flip the properties that were changed
//                source.Database.Connection.ConnectionString
//                    = sqlCnxStringBuilder.ConnectionString;
//            }
//            catch (Exception ex)
//            {
//                // set log item if required
//            }
//        }
//    }
//}
/// <AUTHOR>                                    ///
/// PROJECT ATHOR::-:Ehtisham M.A.:-::          ///
/// FOR ::-:Pearl-Solutions:-::                 ///
/// PROJECT DESCRIPTION::-:This is th first     ///
///     draft of a View creation system to      ///
///     ease the process of creation of modules ///
///     and screens:-::                         ///
/// </AUTHOR>                                   ///


