/*--
 * Registry.cs: Provides common utility functions for accessing the Windows Registry.
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

using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace Metashell.Core.Utility {

	/// <summary>
	/// Utility class for accessing the system registry.
	/// </summary>
	public static class RegistryUtils {
		
		/// <summary>
		/// Gets all the values from a given Microsoft.Win32.RegistryKey
		/// object as a System.Collections.Generic.Dictionary&lt;string, object&gt;,
		/// with the dictionary key being the value name, and the dictionary value being the
		/// registry value.
		/// </summary>
		/// <param name="rk">RegistryKey to retrieve values from.</param>
		public static Dictionary<string, object> GetValuesFromRegistryKey(RegistryKey rk) {
			
			Dictionary<string, object> dict = new Dictionary<string, object>();
			
			string[] names = rk.GetValueNames();
			
			foreach (string name in names) {;
				dict.Add(name, rk.GetValue(name));
			}
			
			return dict;
		}
	}
}
