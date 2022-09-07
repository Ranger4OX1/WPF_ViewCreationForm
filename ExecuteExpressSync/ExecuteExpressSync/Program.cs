using System;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlServerCe;


using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;


namespace ExecuteExpressSync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string serverConnectionString = @"Data Source=JOYDIP\SQLEXPRESS;
            Initial Catalog=ServerDB;
            Trusted_Connection=Yes";
            string clientConnectionString = @"Data Source=JOYDIP\SQLEXPRESS;
            Initial Catalog=ClientDB;
            Trusted_Connection=Yes";
            string tableName = "Products";
            DataSynchronizer.Synchronize(tableName, serverConnectionString, clientConnectionString);
            
            Console.WriteLine("Databases synchronized...");
            Console.Read();
        }
        public static class DataSynchronizer
        {
            private static void Initialize(string table,string serverConnectionString,string clientConnectionString)
            {
                using (SqlConnection serverConnection = new
                    SqlConnection(serverConnectionString))
                {
                    using (SqlConnection clientConnection = new
                        SqlConnection(clientConnectionString))
                    {
                        DbSyncScopeDescription scopeDescription = new
                            DbSyncScopeDescription(table);
                        DbSyncTableDescription tableDescription =
                            SqlSyncDescriptionBuilder.GetDescriptionForTable(table,
                                serverConnection);
                        scopeDescription.Tables.Add(tableDescription);
                        SqlSyncScopeProvisioning serverProvision = new
                            SqlSyncScopeProvisioning(serverConnection,
                                scopeDescription);
                        serverProvision.Apply();
                        SqlSyncScopeProvisioning clientProvision = new
                            SqlSyncScopeProvisioning(clientConnection,
                               scopeDescription);
                        clientProvision.Apply();
                    }
                }
            }

            public static void Synchronize(string tableName,string serverConnectionString, string clientConnectionString)
            {
                Initialize(tableName, serverConnectionString, clientConnectionString);
                Synchronize(tableName, serverConnectionString,
                    clientConnectionString, SyncDirectionOrder.DownloadAndUpload);
                CleanUp(tableName, serverConnectionString, clientConnectionString);
            }

            private static void Synchronize(string scopeName,string serverConnectionString,string clientConnectionString, SyncDirectionOrder syncDirectionOrder)
            {
                using (SqlConnection serverConnection = new
                    SqlConnection(serverConnectionString))
                {
                    using (SqlConnection clientConnection
                        = new SqlConnection(clientConnectionString))
                    {
                        var agent = new SyncOrchestrator
                        {
                            LocalProvider = new
                                SqlSyncProvider(scopeName, clientConnection),
                            RemoteProvider = new SqlSyncProvider(scopeName, serverConnection),
                            Direction = syncDirectionOrder
                        };
                        (agent.RemoteProvider as RelationalSyncProvider).SyncProgress +=
                            new EventHandler<DbSyncProgressEventArgs>
                            (dbProvider_SyncProgress);
                        (agent.LocalProvider as RelationalSyncProvider).ApplyChangeFailed +=
                            new EventHandler<DbApplyChangeFailedEventArgs>(dbProvider_SyncProcessFailed);
                        (agent.RemoteProvider as RelationalSyncProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>
                        (dbProvider_SyncProcessFailed);
                        agent.Synchronize();
                    }
                }
            }

            private static void CleanUp(string scopeName,string serverConnectionString,string clientConnectionString)
            {
                using (SqlConnection serverConnection = new
                    SqlConnection(serverConnectionString))
                {
                    using (SqlConnection clientConnection = new
                        SqlConnection(clientConnectionString))
                    {
                        SqlSyncScopeDeprovisioning serverDeprovisioning = new
                             SqlSyncScopeDeprovisioning(serverConnection);
                        SqlSyncScopeDeprovisioning clientDeprovisioning = new
                            SqlSyncScopeDeprovisioning(clientConnection);
                        serverDeprovisioning.DeprovisionScope(scopeName);
                        serverDeprovisioning.DeprovisionStore();
                        clientDeprovisioning.DeprovisionScope(scopeName);
                        clientDeprovisioning.DeprovisionStore();
                    }
                }
            }
        }

    }
}

//static void Main(string[] args)
//{
//    SqlConnection clientConn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=SyncExpressDB; Trusted_Connection=Yes");

//    SqlConnection serverConn = new SqlConnection("Data Source=localhost; Initial Catalog=SyncDB; Integrated Security=True");

//    //create the sync orhcestrator
//    SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

//    // set local provider of orchestrator to a sync provider associated with the 
//    // ProductsScope in the SyncExpressDB express client database
//    syncOrchestrator.LocalProvider = new SqlSyncProvider("ProductsScope", clientConn);

//    // set the remote provider of orchestrator to a server sync provider associated with
//    // the ProductsScope in the SyncDB server database
//    syncOrchestrator.RemoteProvider = new SqlSyncProvider("ProductsScope", serverConn);

//    // set the direction of sync session to Upload and Download
//    syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;

//    // subscribe for errors that occur when applying changes to the client
//    ((SqlCeSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

//    // execute the synchronization process
//    SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

//    //print statistics
//    Console.WriteLine("Start Time: " + syncStats.SyncStartTime);
//    Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
//    Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
//    Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);
//    Console.WriteLine(String.Empty);
//}

//static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
//{
//    // display conflict type
//    Console.WriteLine(e.Conflict.Type);

//    // display error message 
//    Console.WriteLine(e.Error);
//}