﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sdl.Community.Anonymizer.Interfaces;
using Sdl.Community.Anonymizer.Models;
using Sdl.Core.Settings;
using Sdl.Desktop.IntegrationApi;
using Sdl.FileTypeSupport.Framework.NativeApi;

namespace Sdl.Community.Anonymizer.Ui
{
	public partial class AnonymizerSettingsControl : UserControl, ISettingsAware<AnonymizerSettings>
	{
		private List<RegexPattern> _expressions;
		public AnonymizerSettingsControl()
		{
			InitializeComponent();
			
			expressionsGrid.AutoGenerateColumns = false;
			var exportColumn = new DataGridViewCheckBoxColumn
			{
				HeaderText = @"Enable?"
			};
			var shouldEncryptColumn = new DataGridViewCheckBoxColumn
			{
				HeaderText = @"Encrypt?"
				//Width = 60
			};
			expressionsGrid.Columns.Add(exportColumn);
			var pattern = new DataGridViewTextBoxColumn
			{
				HeaderText = @"Regex Pattern",
				DataPropertyName = "Pattern"
			};
			expressionsGrid.Columns.Add(pattern);
			var description = new DataGridViewTextBoxColumn
			{
				HeaderText = @"Description",
				DataPropertyName = "Description"
			};
			expressionsGrid.Columns.Add(description);
		
			expressionsGrid.Columns.Add(shouldEncryptColumn);

		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ReadExistingExpressions();
			SetSettings(Settings);
		}

		public string EncryptionKey
		{
			get => encryptionBox.Text;
			set => encryptionBox.Text = value;
		}

		public List<RegexPattern> RegexPatterns { get; set; }

		private void ReadExistingExpressions()
		{
			RegexPatterns = new List<RegexPattern>
			{
				new RegexPattern
				{
					Id = "1",
					Description = "email",
					Pattern = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A - Z]{ 2,}\b"
				},

				new RegexPattern
				{
					Id = "2",
					Description = "PCI",
					Pattern = @"\b(?:\d[ -]*?){13,16}\b"
				},
				new RegexPattern
				{
					Id = "3",
					Description = "IP6 Address",
					Pattern = @"(?<![:.\w])(?:[A-F0-9]{1,4}:){7}[A-F0-9]{1,4}(?![:.\w])"
				},
				new RegexPattern
				{
					Id = "4",
					Description ="Social Security Numbers",
					Pattern=@"\b(?!000)(?!666)[0-8][0-9]{2}[- ](?!00)[0-9]{2}[- ](?!0000)[0-9]{4}\b",
				},
				new RegexPattern
				{
					Id = "5",
					Description ="Telephone Numbers",
					Pattern=@"\b\d{4}\s\d+-\d+\b"
				},
				new RegexPattern
				{
					Id = "6",
					Description ="Car Registrations",
					Pattern=@"\b\p{Lu}+\s\p{Lu}+\s\d+\b|\b\p{Lu}+\s\d+\s\p{Lu}+\b|\b\p{Lu}+\d+\s\p{Lu}+\b|\b\p{Lu}+\s\d+\p{Lu}+\b"
				},
				new RegexPattern
				{
					Id = "7",
					Description ="Passport Numbers",
					Pattern=@"\b\d{9}\b"
				},
				new RegexPattern
				{
					Id = "8",
					Description ="National Insurance Number",
					Pattern=@"\b[A-Z]{2}\s\d{2}\s\d{2}\s\d{2}\s[A-Z]\b"
				},
				new RegexPattern
				{
					Id = "9",
					Pattern = "Date of Birth",
					Description = @"\b\d{2}/\d{2}/\d{4}\b"
				},
				new RegexPattern
				{
					Pattern = "",
					Description = ""
				}
			};
		}

		private void SetSettings(AnonymizerSettings settings)
		{
			Settings = settings;
			//RegexPatterns = settings.RegexPatterns;
			//var list = new BindingList<RegexPattern>(Settings.RegexPatterns);

			//var patterns =Settings.GetRegexPatterns();
			foreach (var pattern in RegexPatterns)
			{
				Settings.AddPattern(pattern);
				//if (!Settings.RegexPatterns.Contains(pattern))
				//{
				//	Settings.AddPattern(pattern);
				//}
			}

			SettingsBinder.DataBindSetting<string>(encryptionBox,"Text", Settings, nameof(Settings.EncryptionKey));
			SettingsBinder.DataBindSetting<List<RegexPattern>>(expressionsGrid, "DataSource", Settings,
				nameof(Settings.RegexPatterns));
			UpdateUi(settings);
		}

		private void UpdateUi(AnonymizerSettings settings)
		{
			Settings = settings;
		}

		public AnonymizerSettings Settings { get; set; }

		private void expressionsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//if (e.ColumnIndex == 3 && e.RowIndex >= 0)
			//{
			//	expressionsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
			//	var exppression = _expressions[e.RowIndex];
			//	exppression.ShouldEncrypt = (bool)expressionsGrid.CurrentCell.Value;
			//}
		}
	}
}
