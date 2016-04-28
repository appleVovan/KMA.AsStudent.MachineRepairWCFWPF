using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;

namespace MachineRepair
{
    public partial class MachineRepairService : ServiceBase
    {
        ServiceHost _host;

        public MachineRepairService()
        {
            InitializeComponent();

            //initiaize logs

            if (!EventLog.SourceExists("MachineRepairLog"))
                EventLog.CreateEventSource("MachineRepair", "MachineRepairLog");
            eventLog.Source = "MachineRepair";
            eventLog.Log = "MachineRepairLog";
            eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
            eventLog.MaximumKilobytes = 8192;


            try
            {
                AppDomain.CurrentDomain.UnhandledException += cd_UnhandledException;
                eventLog.WriteEntry("Trying To Start Service");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(
                    "Service Failed To Start Reason " + ex.Message + "\r\n\r\n ex.StackTrace = " + ex.StackTrace +
                    "\r\n\r\n ex.InnerException.Message = " + ex.InnerException.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _host.Close();
            }
            catch
            {
                // ignored
            }
            try
            {
                _host = new ServiceHost(typeof (MachineRepair));
                _host.Open();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error While Opening Host " + ": " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _host.Close();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error While Trying To Stop Host Listener " + ": " + ex.Message,
                                        EventLogEntryType.Error);
            }

            eventLog.WriteEntry("Recognition Service Stopped", EventLogEntryType.Information);

        }

        public void CloseStationDelegateAtTheFirstStage(string message, bool showmessage)
        {
            eventLog.WriteEntry(message, EventLogEntryType.Error);
            try
            {
                var service = new ServiceController("MachineRepairService");
                ExitCode = 1066;
                service.Stop();
            }
            catch
            {
                // ignored
            }
        }

        void cd_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;

            if (ex != null)
            {
                string message = "UnhandledException Reason: ";
                try
                {
                    message += ex.Message;
                }
                catch
                {
                    // ignored
                }
                message += "\r\n\r\n ex.StackTrace = ";
                try
                {
                    message += ex.StackTrace;
                }
                catch
                {
                    // ignored
                }
                message += "\r\n\r\n ex.InnerException.Message = ";
                try
                {
                    message += ex.InnerException.Message;
                }
                catch
                {
                    // ignored
                }
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
            eventLog.WriteEntry("UnhandledException \r\n ex.Message = \r\n ex.StackTrace = ", EventLogEntryType.Error);

        }
    }
}
