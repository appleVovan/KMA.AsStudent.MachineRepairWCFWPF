using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using MachineRepair.Properties;

namespace MachineRepair
{
    static class Program
    {
        static void Main(string[] args)     
        {
            bool showUninstall = true;
            if (args.Length > 0)
            {
                if (args[0] == "-installonly")
                    showUninstall = false;
            }

            bool isInstalled = false;
            bool serviceStarting = false;
            const string serviceName = "MachineRepairService";

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                if (service.ServiceName.Equals(serviceName))
                {
                    isInstalled = true;
                    if (service.Status == ServiceControllerStatus.StartPending)
                    {
                        serviceStarting = true;
                    }
                    break;
                }
            }

            if (!serviceStarting)
            {
                if (isInstalled)
                {
                    if (showUninstall)
                    {
                        var dr = MessageBox.Show($"Do You Really Like To Uninstall {serviceName}", strings.Program_Main_Danger, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr != DialogResult.Yes) return;
                        SelfInstaller.UninstallMe();
                        MessageBox.Show($"Successfully Uninstalled {serviceName}", strings.Program_Main_Status,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Service {serviceName} Already Installed Press Ok ", strings.Program_Main_Status,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    SelfInstaller.InstallMe();
                    MessageBox.Show($"Successfully Installed {serviceName}", strings.Program_Main_Status,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                ServiceBase[] servicesToRun = { new MachineRepairService() };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
    public static class SelfInstaller
    {
        private static readonly string ExePath = Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { "/u", ExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
