using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyService
{
	public partial class SimpleService : ServiceBase
	{
		private Thread main;

		public SimpleService()
		{
			InitializeComponent();
		}

		public void writeEvent(string msg)
		{
			string name = "SimpleService";
			string logDest = "Application";
			if (!EventLog.SourceExists(name))
			{
				EventLog.CreateEventSource(name, logDest);
			}
			EventLog.WriteEntry(name, msg);
		}

		protected override void OnStart(string[] args)
		{
			main = new Thread();
			main.Start();
		}

		protected override void OnPause()
		{
			main.Suspend();
			writeEvent("Servicio en Pausa");
		}
		protected override void OnContinue()
		{
			main.Resume();
			writeEvent("Continuando servicio");
		}
		protected override void OnStop()
		{
			main.Abort();
			writeEvent("Deteniendo servicio");
		}

	}
}