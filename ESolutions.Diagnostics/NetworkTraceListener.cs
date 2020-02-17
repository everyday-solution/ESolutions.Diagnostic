using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml;
using ESolutions.Net;

namespace ESolutions.Diagnostics
{
	/// <summary>
	/// Directs the tracing and debug information to a listing network end point.
	/// </summary>
	public class NetworkTraceListener : System.Diagnostics.TraceListener
	{
		#region adpater
		/// <summary>
		/// Networkadapter used to communicate with the far end point
		/// </summary>
		private NetworkAdapter adapter;
		#endregion

		#region withExceptions
		/// <summary>
		/// Indicates wether exceptions are thrown or not.
		/// </summary>
		private Boolean withExceptions = false;
		#endregion

		#region openConnection
		/// <summary>
		/// The connection used to send messages to the far listener.
		/// </summary>
		private Connection openConnection;
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using a custom port.
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <param name="farPortNumber">The port of the listening application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 45678.</remarks>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public NetworkTraceListener (
			IPAddress farIpAddress,
			Int32 farPortNumber)
		{
			this.Initialize (
				farIpAddress,
				farPortNumber);
		}
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using standrad port 46876
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 46876.</remarks>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public NetworkTraceListener (
			IPAddress farIpAddress)
		{
			this.Initialize (farIpAddress);
		}
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using a string with the IP Address. The standard port will be used.
		/// This constructor is called when the the App.config file loads the listener.
		/// </summary>
		/// <param name="ipAddress">The IP-Address on which the Server listens for trace messages.</param>
		public NetworkTraceListener (String ipAddress)
		{
			this.Initialize (ipAddress);
		}
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using a custom port.
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <param name="farPortNumber">The port of the listening application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 45678.</remarks>
		/// <param name="withExceptions">Indicates wether the listener throw exceptions or not.</param>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public NetworkTraceListener (
			IPAddress farIpAddress,
			Int32 farPortNumber,
			Boolean withExceptions)
		{
			this.withExceptions = withExceptions;
			this.Initialize (
				farIpAddress,
				farPortNumber);
		}
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using standrad port 46876
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 46876.</remarks>
		/// <param name="withExceptions">indicates wether the listener throws exceptions or not.</param>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public NetworkTraceListener (
			IPAddress farIpAddress,
			Boolean withExceptions)
		{
			this.withExceptions = withExceptions;
			this.Initialize (farIpAddress);
		}
		#endregion

		#region NetworkTraceListener
		/// <summary>
		/// Constructor setting up the connection using a string with the IP Address. The standard port will be used.
		/// This constructor is called when the the App.config file loads the listener.
		/// <param name="withExceptions">indicates wether the listener throws exceptions or not.</param>
		/// <param name="farIpAddress">The IP-Address on which the Server listens for trace messages.</param>
		/// </summary>
		public NetworkTraceListener (
			String farIpAddress,
			Boolean withExceptions)
		{
			this.withExceptions = withExceptions;
			this.Initialize (farIpAddress);
		}
		#endregion

		#region Initialize
		/// <summary>
		/// Constructor setting up the connection using a custom port.
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <param name="farPortNumber">The port of the listening application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 45678.</remarks>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public void Initialize (
			IPAddress farIpAddress,
			Int32 farPortNumber)
		{
			try
			{
				this.adapter = new NetworkAdapter (Dns.GetHostAddresses ("localhost")[0]);

				IPEndPoint farIpEndPoint = new IPEndPoint (
					farIpAddress,
					farPortNumber);

				this.openConnection = this.adapter.Connect (farIpEndPoint);
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw new NetworkTraceListenerException (
						NetworkTraceListenerExceptions.ConnectionError,
						ex);
				}
			}
		}
		#endregion

		#region Initialize
		/// <summary>
		/// Constructor setting up the connection using standrad port 46876
		/// </summary>
		/// <param name="farIpAddress">The ip address of the listening client application.</param>
		/// <remarks>The standard port of the NetworkSniffer is 46876.</remarks>
		/// <exception cref="NetworkTraceListenerException">Is thrown when the connection can not be established.</exception>
		public void Initialize (
			IPAddress farIpAddress)
		{
			try
			{
				this.adapter = new NetworkAdapter (Dns.GetHostAddresses ("localhost")[0]);

				IPEndPoint farIpEndPoint = new IPEndPoint (
					farIpAddress,
					46876);

				this.openConnection = this.adapter.Connect (farIpEndPoint);
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw new NetworkTraceListenerException (
						NetworkTraceListenerExceptions.ConnectionError,
						ex);
				}
			}
		}
		#endregion

		#region Initialize
		/// <summary>
		/// Constructor setting up the connection using a string with the IP Address. The standard port will be used.
		/// This constructor is called when the the App.config file loads the listener.
		/// </summary>
		/// <param name="ipAddress">The IP-Address on which the Server listens for trace messages.</param>
		public void Initialize (String ipAddress)
		{
			try
			{
				IPAddress farIpAddress = IPAddress.Parse (ipAddress);
				IPEndPoint farIpEndPoint = new IPEndPoint (
					farIpAddress,
					46876);

				this.adapter = new NetworkAdapter (Dns.GetHostAddresses ("localhost")[0]);
				this.openConnection = this.adapter.Connect (farIpEndPoint);
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw new NetworkTraceListenerException (
						NetworkTraceListenerExceptions.ConnectionError,
						ex);
				}
			}
		}
		#endregion

		#region Write
		/// <summary>
		/// Send a single message to the connected message listener.
		/// </summary>
		/// <param name="message"></param>
		public override void Write (String message)
		{
			try
			{
				if (openConnection.IsUsable)
				{
					Package p = new Package ();
					p.Payload = message;
					adapter.SendPackage (
						p,
						openConnection,
						false);
				}
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw ex;
				}
			}
		}
		#endregion

		#region WriteLine
		/// <summary>
		/// Sends a single message to the connected message listener.
		/// </summary>
		/// <param name="message"></param>
		public override void WriteLine (String message)
		{
			try
			{
				if (openConnection.IsUsable)
				{
					Package p = new Package ();
					p.Payload = message;
					adapter.SendPackage (
						p,
						openConnection,
						false);
				}
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw ex;
				}
			}
		}
		#endregion

		#region Close
		/// <summary>
		/// Closes the network connection.
		/// </summary>
		public override void Close ()
		{
			try
			{
				openConnection.Close ();
			}
			catch (Exception ex)
			{
				if (this.withExceptions)
				{
					throw ex;
				}
			}
		}
		#endregion
	}
}
