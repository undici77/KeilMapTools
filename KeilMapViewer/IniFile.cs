/*
 * This file is part of the PlugnPutty distribution (https://github.com/undici77/PlugnPutty.git).
 * Copyright (c) 2019 Alessandro Barbieri.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
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

using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using System.Diagnostics;

public class IniFile
{
	private ArrayList _Sections;
	private string _File_Name;

	public System.Collections.ICollection SectionsName
	{
		get
		{
			return (_Sections);
		}
	}

	/// @brief Constructor
	///
	public IniFile()
	{
		_Sections = new ArrayList();
	}

	/// @brief Load file in memory
	///
	/// @param file_name init file name
	public void Load(string file_name)
	{
		Load(file_name, false);
	}

	/// @brief Load file in memory
	///
	/// @param file_name init file name
	/// @param merge request merge of file with current data in memory
	public void Load(string file_name, bool merge)
	{
		IniSection temp_section;
		StreamReader stream_reader;
		Regex regex_comment;
		Regex regex_section;
		Regex regex_key_value;
		Regex regex_key_value_comment;
		Regex regex_key_value_string;
		Regex regex_key_value_string_comment;
		string line;
		Match match;

		Debug.WriteLine(string.Format("Loading: {0}", file_name));

		if (!merge)
		{
			RemoveAllSections();
			_File_Name = "";
		}

		_File_Name = _File_Name + "," + file_name;

		temp_section                   = null;
		stream_reader                  = new StreamReader(new FileStream(file_name, FileMode.OpenOrCreate, FileAccess.Read));
		regex_comment                  = new Regex("^([\\s]*;.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
		regex_section                  = new Regex("^[\\s]*\\[[\\s]*([^\\[\\s]*[^\\s\\]])[\\s]*\\][\\s]*$", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
		regex_key_value                = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
		regex_key_value_comment        = new Regex("^\\s*([^=\\s]*)[^=]*=(.*);(.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
		regex_key_value_string         = new Regex("^\\s*([^=\\s]*)[^=]*=\"(.*)\"", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
		regex_key_value_string_comment = new Regex("^\\s*([^=\\s]*)[^=]*=\"(.*)\";(.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));

		while (!stream_reader.EndOfStream)
		{
			line = stream_reader.ReadLine();

			if (line != string.Empty)
			{
				match = null;

				if (regex_comment.Match(line).Success)
				{
					// Comment
					match = regex_comment.Match(line);
					Debug.WriteLine(string.Format("Skipping comment: {0}", match.Groups[0].Value));
				}
				else if (regex_section.Match(line).Success)
				{
					// Section
					match = regex_section.Match(line);
					Debug.WriteLine(string.Format("Section [{0}]", match.Groups[1].Value));
					temp_section = AddSection(match.Groups[1].Value);
				}
				else if (regex_key_value_string_comment.Match(line).Success && (temp_section != null))
				{
					// Key="Value";Comment
					match = regex_key_value_string_comment.Match(line);
					Debug.WriteLine(string.Format("Key {0}={1};{2}", match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
					temp_section.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
					temp_section.AddKey(match.Groups[1].Value).Comment = match.Groups[3].Value;
					temp_section.AddKey(match.Groups[1].Value).Quotes = true;
				}
				else if (regex_key_value_string.Match(line).Success && (temp_section != null))
				{
					// Key="Value"
					match = regex_key_value_string.Match(line);
					Debug.WriteLine(string.Format("Key {0}=\"{1}\"", match.Groups[1].Value, match.Groups[2].Value));
					temp_section.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
					temp_section.AddKey(match.Groups[1].Value).Quotes = true;
				}
				else if (regex_key_value_comment.Match(line).Success && (temp_section != null))
				{
					// Key=Value;Comment
					match = regex_key_value_comment.Match(line);
					Debug.WriteLine(string.Format("Key {0}={1};{2}", match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
					temp_section.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
					temp_section.AddKey(match.Groups[1].Value).Comment = match.Groups[3].Value;
					temp_section.AddKey(match.Groups[1].Value).Quotes = false;
				}
				else if (regex_key_value.Match(line).Success && (temp_section != null))
				{
					// Key=Value
					match = regex_key_value.Match(line);
					Debug.WriteLine(string.Format("Key {0}={1}", match.Groups[1].Value, match.Groups[2].Value));
					temp_section.AddKey(match.Groups[1].Value).Value = match.Groups[2].Value;
					temp_section.AddKey(match.Groups[1].Value).Quotes = false;
				}
				else if (temp_section != null)
				{
					//  Handle Key without value
					Debug.WriteLine(string.Format("Key {0}", line));
					temp_section.AddKey(line);
				}
				else
				{
					Debug.WriteLine(string.Format("unknown type of data: {0}", line));
				}
			}
		}

		stream_reader.Close();

		Debug.WriteLine(string.Format("{0}: loaded", file_name));
	}

	/// @brief Save file from memory to disk
	///
	/// @param file_name init file name
	public void Save(string file_name)
	{
		StreamWriter stream_writer;

		Debug.WriteLine(string.Format("Saving: {0}", file_name));

		stream_writer = new StreamWriter(file_name, false);
		foreach (IniSection s in SectionsName)
		{
			Debug.WriteLine(string.Format("Section: [{0}]", s.Name));
			stream_writer.WriteLine(string.Format("[{0}]", s.Name));

			foreach (IniSection.IniKey k in s.Keys)
			{
				if (k.Comment == null)
				{
					if (k.Quotes == true)
					{
						Debug.WriteLine(string.Format("Key: {0}={1}", k.Name, k.Value));
						stream_writer.WriteLine(string.Format("{0}=\"{1}\"", k.Name, k.Value));
					}
					else
					{
						Debug.WriteLine(string.Format("Key: {0}={1}", k.Name, k.Value));
						stream_writer.WriteLine(string.Format("{0}={1}", k.Name, k.Value));
					}

				}
				else
				{
					if (k.Quotes == true)
					{
						Debug.WriteLine(string.Format("Key: {0}=\"{1}\";{2}", k.Name, k.Value, k.Comment));
						stream_writer.WriteLine(string.Format("{0}=\"{1}\";{2}", k.Name, k.Value, k.Comment));
					}
					else
					{
						Debug.WriteLine(string.Format("Key: {0}={1};{2}", k.Name, k.Value, k.Comment));
						stream_writer.WriteLine(string.Format("{0}={1};{2}", k.Name, k.Value, k.Comment));
					}
				}
			}
		}

		stream_writer.Close();

		Debug.WriteLine(string.Format("{0}: saved", file_name));
	}

	/// @brief Add section to file
	///
	/// @param section_name section name
	/// @retval section
	public IniSection AddSection(string section_name)
	{
		int        id;
		IniSection section;

		section_name = section_name.Trim();

		id = _Sections.IndexOf(section_name.Trim());

		if (id == -1)
		{
			section = new IniSection(this, section_name);
			_Sections.Add(section);
		}
		else
		{
			section = (IniSection)_Sections[id];
		}

		return (section);
	}

	/// @brief Remove section to file
	///
	/// @param section_name section name
	/// @retval true section removed, false error
	public bool RemoveSection(string section_name)
	{
		section_name = section_name.Trim();
		return (RemoveSection(GetSection(section_name)));
	}

	/// @brief Remove section to file
	///
	/// @param section section
	/// @retval true section removed, false error
	public bool RemoveSection(IniSection section)
	{
		if (section != null)
		{
			try
			{
				_Sections.Remove(section.Name);
				return (true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		return (false);
	}

	/// @brief Remove all sections
	///
	/// @retval true sections removed, false error
	public bool RemoveAllSections()
	{
		_Sections.Clear();
		return (_Sections.Count == 0);
	}

	/// @brief Get section from name
	///
	/// @param section_name section name
	/// @retval section
	public IniSection GetSection(string section_name)
	{
		int id;

		id = _Sections.IndexOf(section_name.Trim());

		if (id == -1)
		{
			return (null);
		}
		else
		{
			return ((IniSection)_Sections[id]);
		}

	}

	/// @brief Get a value of a key	in a section.
	///        If key non exsist, empty key will added
	///
	/// @param section_name name of section
	/// @param key_name	name of key
	/// @retval	null of value
	public string GetKeyValue(string section_name, string key_name)
	{
		IniSection        section;
		IniSection.IniKey key;

		section = GetSection(section_name);

		if (section == null)
		{
			section = AddSection(section_name);
			key     = section.AddKey(key_name);
		}
		else
		{
			key = section.GetKey(key_name);

			if (key == null)
			{
				key = section.AddKey(key_name);
			}
			else
			{
				if (key.Value == null)
				{
					key.Value = "";
				}

				return (key.Value);
			}
		}

		return (string.Empty);
	}

	/// @brief Set a value of a key	in a section.
	///        If key non exsist, key will added
	///
	/// @param section_name name of section
	/// @param key_name	name of key
	/// @param value value to set
	/// @retval	true operation done, false operation failed
	public bool SetKeyValue(string section_name, string key_name, string value, bool quotes = true)
	{
		IniSection        section;
		IniSection.IniKey key;

		section = AddSection(section_name);

		if (section != null)
		{
			key = section.AddKey(key_name);

			if (key != null)
			{
				if (quotes)
				{
					key.Quotes = true;
				}

				key.Value = value;
				return (true);
			}
		}

		return (false);
	}

	/// @brief Rename a section
	///
	/// @param old_section_name old section name
	/// @param new_section_name new section name
	/// @retval	true operation done, false operation failed
	public bool RenameSection(string old_section_name, string new_section_name)
	{
		IniSection section;

		section = GetSection(old_section_name);

		if (section != null)
		{
			return (section.SetName(new_section_name));
		}

		return (false);
	}

	/// @brief Rename a key in a section
	///
	/// @param section_name name of section
	/// @param old_key_name old key name
	/// @param new_key_name	new key name
	/// @retval	true operation done, false operation failed
	public bool RenameKey(string section_name, string old_key_name, string new_key_name)
	{
		IniSection        section;
		IniSection.IniKey key;

		section = GetSection(section_name);

		if (section != null)
		{
			key = section.GetKey(old_key_name);
			if (key != null)
			{
				return (key.SetName(new_key_name));
			}
		}

		return (false);
	}

	/// @brief Name of file
	///
	public string FileName
	{
		get
		{
			return (_File_Name);
		}
	}

	public class IniSection
	{
		private IniFile   _Ini_File;
		private string    _Section_Name;
		private ArrayList _Keys_Array;

		/// @brief Constructor
		///
		/// @param parent Ini file name reference
		/// @param section_name name of section
		protected internal IniSection(IniFile parent, string section_name)
		{
			_Ini_File     = parent;
			_Section_Name = section_name;
			_Keys_Array   = new ArrayList();
		}

		/// @brief Override of Equals method
		///
		/// @param obj object to compare
		/// @retval true name are equals, false name are not equals
		public override bool Equals(Object obj)
		{
			return ((string)obj == _Section_Name);
		}

		/// @brief Override of GetHashCode method
		///
		/// @retval	hash code of name
		public override int GetHashCode()
		{
			return (_Section_Name.GetHashCode());
		}

		/// @brief Get collection of keys
		///
		public System.Collections.ICollection Keys
		{
			get
			{
				return (_Keys_Array);
			}
		}

		/// @brief Get AyyaList of keys
		///
		public ArrayList KeysArray
		{
			get
			{
				return (_Keys_Array);
			}
		}

		/// @brief Get section name
		///
		public string Name
		{
			get
			{
				return (_Section_Name);
			}
		}

		/// @brief Add empty key to section
		///
		/// @param key_name	name of key
		/// @retval	reference to related key
		public IniKey AddKey(string key_name)
		{
			int               id;
			IniSection.IniKey key;

			key_name = key_name.Trim();
			key = null;

			if (key_name.Length != 0)
			{
				id = _Keys_Array.IndexOf(key_name);

				if (id == -1)
				{
					key = new IniSection.IniKey(this, key_name);
					_Keys_Array.Add(key);
				}
				else
				{
					key = (IniKey)_Keys_Array[id];
				}
			}

			return (key);
		}

		/// @brief Remove key from section
		///
		/// @param key_name	name of key
		/// @retval	true operation done, false operation failed
		public bool RemoveKey(string key_name)
		{
			return (RemoveKey(GetKey(key_name)));
		}

		/// @brief Remove key from section
		///
		/// @param key reference to key class to remove
		/// @retval	true operation done, false operation failed
		public bool RemoveKey(IniKey key)
		{
			if (key != null)
			{
				try
				{
					_Keys_Array.Remove(key.Name);
					return true;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}

			return (false);
		}

		/// @brief Remove all keys from section
		///
		/// @retval	true operation done, false operation failed
		public bool RemoveAllKeys()
		{
			_Keys_Array.Clear();
			return (_Keys_Array.Count == 0);
		}

		/// @brief Get key reference in section
		///
		/// @param key_name	name of key
		/// @retval	reference of key
		public IniKey GetKey(string key_name)
		{
			int id;

			id = _Keys_Array.IndexOf(key_name);

			if (id == -1)
			{
				return (null);
			}
			else
			{
				return ((IniKey)_Keys_Array[id]);
			}
		}

		/// @brief Set name of section
		///
		/// @param section_name	name of section
		/// @retval	true operation done, false operation failed
		public bool SetName(string section_name)
		{
			IniSection section;

			section_name = section_name.Trim();

			if (section_name.Length != 0)
			{
				section = _Ini_File.GetSection(section_name);

				if ((section != this) && (section != null))
				{
					return (false);
				}

				try
				{
					_Ini_File._Sections.Remove(_Section_Name);
					_Ini_File._Sections.Add(this);
					_Section_Name = section_name;

					return (true);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}

			return (false);
		}

		/// @brief Get section name
		///
		/// @retval	name of section
		public string GetName()
		{
			return (_Section_Name);
		}

		public class IniKey
		{
			private string     Key_Name;
			private string     Key_Value;
			private string     Key_Comment;
			private bool       Key_Quote;
			private IniSection Section;

			/// @brief Constructor
			///
			/// @param parent reference to contaier section
			/// @param key_name	name of key
			protected internal IniKey(IniSection parent, string key_name)
			{
				Section  = parent;
				Key_Name = key_name;
			}

			/// @brief Override of Equals method
			///
			/// @param obj object to compare
			/// @retval true name are equals, false name are not equals
			public override bool Equals(Object obj)
			{
				return ((string)obj == Key_Name);
			}

			/// @brief Override of GetHashCode method
			///
			/// @retval	hash code of name
			public override int GetHashCode()
			{
				return (Key_Name.GetHashCode());
			}

			/// @brief Get name of key
			///
			public string Name
			{
				get
				{
					return (Key_Name);
				}
			}

			/// @brief Get value of key
			///
			public string Value
			{
				get
				{
					return (Key_Value);
				}
				set
				{
					Key_Value = value;
				}
			}

			/// @brief Get comment of key
			///
			public string Comment
			{
				get
				{
					return (Key_Comment);
				}
				set
				{
					Key_Comment = value;
				}
			}

			/// @brief Get and Set if value is surrounded by quotes
			///
			public bool Quotes
			{
				get
				{
					return (Key_Quote);
				}
				set
				{
					Key_Quote = value;
				}
			}

			/// @brief Set value of key
			///
			/// @param value value to set
			public void SetValue(string value)
			{
				Key_Value = value;
			}

			/// @brief Get value of key
			///
			/// @retval value of key
			public string GetValue()
			{
				return (Key_Value);
			}

			/// @brief Set comment of key
			///
			/// @param comment comment to set
			public void SetComment(string comment)
			{
				Key_Comment = comment;
			}

			/// @brief Get comment of key
			///
			/// @retval	comment of key
			public string GetComment()
			{
				return (Key_Comment);
			}

			/// @brief Set name of key
			///
			/// @param key_name name of key
			/// @retval	true operation done, false operation failed
			public bool SetName(string key_name)
			{
				IniKey key;

				key_name = key_name.Trim();

				if (key_name.Length != 0)
				{
					key = Section.GetKey(key_name);

					if ((key != this) && (key != null))
					{
						return (false);
					}

					try
					{
						Section.KeysArray.Remove(Key_Name);
						Section.KeysArray.Add(this);

						Key_Name = key_name;

						return (true);
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
				}

				return (false);
			}

			/// @brief Get name of key
			///
			/// @retval	name of key
			public string GetName()
			{
				return (Key_Name);
			}
		}
	}
}

