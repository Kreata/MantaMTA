﻿using System;

namespace OpenManta.Core
{
	/// <summary>
	/// ESMTP 8BITMIMETYPE
	/// This is used to determine what format to send/receive a messages body in.
	/// </summary>
	public enum SmtpTransportMIME : int
	{
		/// <summary>
		/// ASCII (7bit)
		/// </summary>
		_7BitASCII = 1,
		/// <summary>
		/// UTF-8 without byte order mark.
		/// </summary>
		_8BitUTF = 2
	}
}

