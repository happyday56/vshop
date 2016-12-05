namespace A0CvdP16Is
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    internal class AC2pFbvHCa1v4C1DIZ8SngolueQb
    {
        private static bool AAZ0JeEma(2x58C7QQ)d9L4MH = false;
        private static bool ABOZyE4bfGcbd5Z8N5SO7b2 = false;
        private static bool AC2pFbvHCa1v4C1DIZ8SngolueQb = false;

        [DllImport("nr_native_lib.dll", EntryPoint="nr_nli", CallingConvention=(CallingConvention) 0, CharSet=CharSet.Ansi)]
        public static extern bool AAZ0JeEma(2x58C7QQ)d9L4MH([MarshalAs(UnmanagedType.BStr)] string, int);
        [DllImport("Hidistro.UI.Common.Controls_nat.dll", EntryPoint="nr_nli", CallingConvention=(CallingConvention) 0, CharSet=CharSet.Ansi)]
        public static extern bool ABOZyE4bfGcbd5Z8N5SO7b2([MarshalAs(UnmanagedType.BStr)] string, int);
        [DllImport("Hidistro.UI.Common.Controls_nat.dll", EntryPoint="nr_startup", CallingConvention=(CallingConvention) 0, CharSet=CharSet.Ansi)]
        public static extern void AC2pFbvHCa1v4C1DIZ8SngolueQb([MarshalAs(UnmanagedType.BStr)] string);
        internal static void AD)2Z(NKy()
        {
            if (!ABOZyE4bfGcbd5Z8N5SO7b2)
            {
                ABOZyE4bfGcbd5Z8N5SO7b2 = true;
                AC2pFbvHCa1v4C1DIZ8SngolueQb(typeof(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb).Assembly.Location);
            }
        }

        internal static void AE)AZJK0MajbU(bool)
        {
        }

        internal static string AFCkJKk(string text1)
        {
            string str = "";
            for (int i = 0; i < text1.Length; i++)
            {
                str = str + ((char) (0xff - ((byte) text1[i])));
            }
            return str;
        }

        internal static string AGeIcrrVo5pC(snrJL(string text1)
        {
            byte[] bytes = Convert.FromBase64String(text1);
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
        }

        internal static void AHxCaphXxC5uvu()
        {
            if (!AAZ0JeEma(2x58C7QQ)d9L4MH)
            {
                AAZ0JeEma(2x58C7QQ)d9L4MH = true;
                try
                {
                    MessageBox.Show("", "Lock System");
                }
                catch
                {
                }
            }
        }

        internal static void AIYbPEZFPIB(c7C()
        {
            if (!AC2pFbvHCa1v4C1DIZ8SngolueQb)
            {
                AC2pFbvHCa1v4C1DIZ8SngolueQb = true;
                string str = typeof(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb).Assembly.Location.ToString();
                int num = 0;
                Process currentProcess = Process.GetCurrentProcess();
                for (int i = 0; i < currentProcess.Modules.Count; i++)
                {
                    try
                    {
                        if (currentProcess.Modules[i].FileName.ToString() == str.ToString())
                        {
                            num = currentProcess.Modules[i].BaseAddress.ToInt32();
                            try
                            {
                                bool flag = ABOZyE4bfGcbd5Z8N5SO7b2(str, num);
                            }
                            catch (DllNotFoundException)
                            {
                                try
                                {
                                    bool flag2 = AAZ0JeEma(2x58C7QQ)d9L4MH(str, num);
                                }
                                catch (DllNotFoundException)
                                {
                                    try
                                    {
                                        MessageBox.Show("Can't find native library!  Please install the  native library to your local directory or to your system(32) directory.", "'Hidistro.UI.Common.Controls_nat.dll' not found!");
                                        Application.Exit();
                                        Application.DoEvents();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            break;
                        }
                    }
                    catch (DllNotFoundException)
                    {
                    }
                }
            }
        }
    }
}

