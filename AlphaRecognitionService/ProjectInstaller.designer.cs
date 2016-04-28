namespace MachineRepair
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstallerAS = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstallerAS = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstallerAS
            // 
            this.serviceProcessInstallerAS.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.serviceProcessInstallerAS.Password = null;
            this.serviceProcessInstallerAS.Username = null;
            // 
            // serviceInstallerAS
            // 
            this.serviceInstallerAS.ServiceName = "MachineRepairService";// "AlphaRecognitionService";
            this.serviceInstallerAS.DisplayName = "MachineRepair Service";// "docAlpha Recognition";
            this.serviceInstallerAS.Description = "MachineRepairService";//"docAlpha Recognition Service - performs all automatic recognition tasks.";
            this.serviceInstallerAS.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerAS,
            this.serviceInstallerAS});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerAS;
        private System.ServiceProcess.ServiceInstaller serviceInstallerAS;
    }
}