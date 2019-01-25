using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditHWPidListGUI.Extensions
{
    static class OpenCustomFileDialoge
    {
        
        public static string Show(string FileName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Välj fil: " + FileName + "!";

            string PathToFile=null;

            if (openFileDialog.ShowDialog() == true)
            {
                PathToFile = Path.GetDirectoryName(openFileDialog.FileName);

                //Spar mappväg för nästa gång programmet öppnas
                //Properties.Settings.Default.FolderPath = FolderPath;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();
            }
            else
            {
                //Kill program
                //Environment.Exit(0);
            }
            return PathToFile;
        }
    }
}
