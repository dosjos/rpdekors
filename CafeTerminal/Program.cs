using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CafeTerminal.DataAccess;
using CafeTerminal.UI;

namespace CafeTerminal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Database.SetInitializer(new CreateDatabaseIfNotExists<SalgDbContext>());
            
            Application.Run(new MainWindow());

        }


        
    }
}
