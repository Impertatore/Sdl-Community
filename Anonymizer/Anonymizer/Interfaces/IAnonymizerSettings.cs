﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sdl.Community.Anonymizer.Models;

namespace Sdl.Community.Anonymizer.Interfaces
{
	public interface IAnonymizerSettings
	{
		 List<RegexPattern> RegexPatterns { get; set; }
		string EncryptionKey { get; set; }

		List<RegexPattern> GetRegexPatterns();
		string GetEncryptionKey();
	}
}
