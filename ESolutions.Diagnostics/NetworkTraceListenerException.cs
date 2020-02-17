using System;
using System.Collections.Generic;
using System.Text;

namespace ESolutions.Diagnostics
{
	/// <summary>
	/// Exception that can be thrown by the NetworkTraceListener class.
	/// </summary>
	[global::System.Serializable]
	public class NetworkTraceListenerException : Exception
	{
		#region NetworkListenerException
		/// <summary>
		/// Constructor for an empty NetworkAdapter exception
		/// </summary>
		internal NetworkTraceListenerException ()
		{
		}
		#endregion

		#region NetworkListenerException
		/// <summary>
		/// Constructor for the NetworkAdapter exception, taking the exception text.
		/// </summary>
		/// <param name="message">Text of the exception.</param>
		internal NetworkTraceListenerException (string message) : base (message)
		{
		}
		#endregion

		#region NetworkListenerException
		/// <summary>
		/// Constructor for an NetworkAdapter exception with an inner exception.
		/// </summary>
		/// <param name="message">Text of the exception.</param>
		/// <param name="inner">The inner exception.</param>
		internal NetworkTraceListenerException (
			string message, 
			Exception inner) : base (message, inner)
		{
		}
		#endregion
	}
}
