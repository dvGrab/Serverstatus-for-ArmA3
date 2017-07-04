using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Ini
{
    class InitializeINI
    {
        public string ExternPath;
        StringBuilder Temp = new StringBuilder(255);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string Section, string Keyname, string Value, string FilePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string Section, string Keyname, string Default, StringBuilder Return, int Size, string FilePath);

        public InitializeINI(string FilePath)
        {
            string Temp = String.Format("{0}\\{1}", Application.StartupPath, FilePath);

            if (Temp.Length != 0)
                ExternPath = Temp;
        }
    
        public void Write(string Section, string Keyname, string Value)
        {
            if(Value.Length != 0)
                WritePrivateProfileString(Section, Keyname, Value, this.ExternPath);
        }

        public string Read(string Section, string Keyname)
        {       
            GetPrivateProfileString(Section, Keyname, "", Temp, 255, this.ExternPath);
            return Temp.ToString();
        }
    }
}
