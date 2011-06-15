/*--
 * Strings.cs: Provides common utility functions for handling strings.
 *--
 * For more information, please visit the Metashell Project site:
 *   http://code.google.com/p/metashell/
 *--
 * (c) 2006 Christopher E. Granade.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; exactly version 2
 * of the License applies to this software.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;

namespace Metashell.Core.Utility {

	/// <summary>
	/// Provides utility functions for manipulating strings. In particular, several functions
	/// are offered to manipulate parameter strings.
	/// </summary>
	public static class Strings {
		
		/// Defines what is recognized as a long or short option prefix.
		public const string
			LongOpt = "--", ShortOpt = "-";
		
		/// <summary>
		/// Parses a List&lt;string&gt; of options and returns those options which specify
		/// a value as a Dictionary	&lt;string, string&gt;.
		/// </summary>
		/// <param name="opts">Options returned by OptsFromParams.</param>
		public static Dictionary<string, string> OptValsFromOpts(List<string> opts) {
		
			Dictionary<string, string> optvals =
				new Dictionary<string, string>();
		
			foreach (string opt in opts) {
			
				int i = 0;
			
				if ((i = opt.IndexOf("=")) != -1) {
					optvals.Add(opt.Substring(0, i), opt.Substring(i + 1)); 
				}
			
			}
			
			return optvals;
		}
		
		/// <summary>
		/// Parses a parameter string to find arguments that are not also options.
		/// </summary>
		/// <param name="parameters">String of parameters to parse.</param>
		/// <returns>A List&lt;string&gt; of the arguments found.</returns>
		public static List<string> ArgsFromParams(string parameters) {

			string[] ps = parameters.Trim().Split(' ');
			
			List<string> args = new List<string>();
			
			foreach (string p in ps) {
				
				string tp = p.Trim();
				if (!(tp.StartsWith(LongOpt) || tp.StartsWith(ShortOpt))) {
					
					args.Add(tp);
					
				}
				
			}
			
			return args;
		
		}
		
		/// <summary>
		/// Parses a parameter string to find options to a command.
		/// </summary>
		/// <param name="parameters">String of parameters to parse.</param>
		/// <returns>A List&lt;string&gt; of the options found.</returns>
		public static List<string> OptsFromParams(string parameters) {
			
			string[] ps = parameters.Trim().Split(' ');
			
			List<string> opts = new List<string>();
			
			foreach (string p in ps) {
				
				string tp = p.Trim();
				if (tp.StartsWith(LongOpt)) {
					
					opts.Add(tp.Remove(0, LongOpt.Length)); 
					
				} else if (tp.StartsWith(ShortOpt)) {
					
					opts.Add(tp.Remove(0, ShortOpt.Length));
					
				}
				
			}
			
			return opts;
		}
		
		/// <summary>
		/// Converts an array of bytes represented as byte[] to a string of 
		/// hexadecimal digits that represents the same value.
		/// Useful for message digests.
		/// </summary>
		/// <param name="bytes">The array to be converted.</param>
		public static string BytesToHex(byte[] bytes) {
			
			string hex = "";
			
			foreach(byte b in bytes) {
				hex += b.ToString("x");
			}
			
			return hex;	
		}
	}
}
