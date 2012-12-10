﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Challonge.Backend
{
	internal class StringWriterWithEncoding : StringWriter
	{
		Encoding encoding;

		public StringWriterWithEncoding(StringBuilder builder, Encoding encoding)
			: base(builder)
		{
			this.encoding = encoding;
		}

		public override Encoding Encoding
		{
			get { return encoding; }
		}
	}
}
