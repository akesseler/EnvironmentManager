/*
 * MIT License
 * 
 * Copyright (c) 2019 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Plexdata.CfgParser.Entities;
using Plexdata.EnvironmentManager.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Plexdata.EnvironmentManager.Models
{
    public class EnvironmentVariable : ICloneable
    {
        private static readonly String ConfigSectionName = "EnvironmentVariables";
        private static readonly String LineSeparator = "[CRLF]";
        private static readonly String[] LineSeparators = new String[] { EnvironmentVariable.LineSeparator };

        private Stages stage;
        private EnvironmentVariableTarget scope;
        private String start;
        private String label;
        private String value;
        private String[] shift;

        public enum Stages
        {
            // Already existing and nothing applied yet.
            Nothing,

            // Created by construction and should never change again.
            Created,

            // Changed by property but should not overwrite an other status
            Changed,

            // Overwrites any other status.
            Deleted,
        };

        public EnvironmentVariable(EnvironmentVariableTarget scope)
        {
            this.scope = scope;
            this.start = String.Empty;
            this.label = "NEW";
            this.value = String.Empty;
            this.shift = new String[0];
            this.stage = Stages.Created;
        }

        public EnvironmentVariable(EnvironmentVariableTarget scope, DictionaryEntry entry)
        {
            String key = entry.Key != null ? entry.Key.ToString() : String.Empty;
            String val = entry.Value != null ? entry.Value.ToString() : String.Empty;

            this.scope = scope;
            this.start = key.Trim();
            this.label = key.Trim();
            this.value = val.Trim();
            this.shift = new String[0];
            this.stage = Stages.Nothing;
        }

        private EnvironmentVariable(EnvironmentVariable other)
        {
            this.scope = other.scope;
            this.start = other.start;
            this.label = other.label;
            this.value = other.value;
            this.shift = other.shift;
            this.stage = other.stage;
        }

        public Stages Stage
        {
            get
            {
                return this.stage;
            }
            set
            {
                this.TryChangeStage(value);
            }
        }

        public EnvironmentVariableTarget Scope
        {
            get
            {
                return this.scope;
            }
            set
            {
                if (this.scope != value)
                {
                    this.scope = value;
                    this.TryChangeStage(Stages.Changed);
                }
            }
        }

        public String Start
        {
            get
            {
                return this.start;
            }
        }

        public String Label
        {
            get
            {
                return this.label;
            }
            set
            {
                value = (value ?? String.Empty).Trim();

                if (this.label != value)
                {
                    this.label = value;
                    this.TryChangeStage(Stages.Changed);
                }
            }
        }

        public String Value
        {
            get
            {
                return this.value;
            }
            set
            {
                value = (value ?? String.Empty).Trim();

                if (this.value != value)
                {
                    this.value = value;
                    this.TryChangeStage(Stages.Changed);
                }
            }
        }

        public String[] Shift
        {
            get
            {
                if (this.shift == null)
                {
                    this.shift = new String[0];
                }
                return this.shift;
            }
            set
            {
                value = this.GetArray(value);

                if (!Enumerable.SequenceEqual(this.shift, value))
                {
                    this.shift = value;
                    this.TryChangeStage(Stages.Changed);
                }
            }
        }

        public Boolean IsCreated
        {
            get
            {
                return this.stage == Stages.Created;
            }
        }

        public Boolean IsChanged
        {
            get
            {
                return this.stage == Stages.Changed;
            }
        }

        public Boolean IsDeleted
        {
            get
            {
                return this.stage == Stages.Deleted;
            }
        }

        public Boolean IsRenamed
        {
            get
            {
                return this.IsModified && this.start.Length > 0 && String.Compare(this.start, this.label) != 0;
            }
        }

        public Boolean IsModified
        {
            get
            {
                return this.stage != Stages.Nothing;
            }
        }

        public Boolean IsReadonly
        {
            get
            {
                return this.scope == EnvironmentVariableTarget.Machine && !PermissionCheck.IsRunAsAdmin;
            }
        }

        public void LoadSettings(ConfigContent content)
        {
            if (content == null)
            {
                return;
            }

            ConfigSection section = content.Find(EnvironmentVariable.ConfigSectionName);

            if (section == null)
            {
                return;
            }

            ConfigValue value = section.Find(this.Label);

            if (value == null)
            {
                return;
            }

            this.shift = this.IntoArray(value.Value);
        }

        public void SaveSettings(ConfigContent content)
        {
            if (content == null)
            {
                return;
            }

            ConfigSection section = content.Find(EnvironmentVariable.ConfigSectionName);

            if (section == null)
            {
                section = content.Append(EnvironmentVariable.ConfigSectionName);
            }

            ConfigValue value = section.Find(this.Label);

            if (value == null)
            {
                value = new ConfigValue(this.Label);
            }

            value.Value = this.FromArray(this.shift);

            section[this.Label] = value;
        }

        public void FreeSettings(ConfigContent content)
        {
            if (content == null)
            {
                return;
            }

            ConfigSection section = content.Find(EnvironmentVariable.ConfigSectionName);

            if (section == null)
            {
                return;
            }

            if (!String.IsNullOrWhiteSpace(this.Start))
            {
                section.Remove(this.Start);
            }

            section.Remove(this.Label);
        }

        public String GetTooltip()
        {
            String scope = this.scope.ToString();
            String stage = this.stage.ToString();
            String label = this.GetString(this.label);
            String value = this.GetString(this.value);

            if (value.Length > 60) { value = value.Substring(0, 57) + "..."; }

            return
                $"{nameof(this.Scope)}:\t{scope}{Environment.NewLine}" +
                $"{nameof(this.Stage)}:\t{stage}{Environment.NewLine}" +
                $"{nameof(this.Label)}:\t{label}{Environment.NewLine}" +
                $"{nameof(this.Value)}:\t{value}";
        }

        public void TryChangeStage(Stages status)
        {
            if (this.stage != status)
            {
                // Delete overwrites any other status.
                if (status == Stages.Deleted)
                {
                    this.stage = Stages.Deleted;
                }

                // May become Created, Changed or Deleted.
                if (this.stage == Stages.Nothing)
                {
                    this.stage = status;
                }
            }
        }

        public Object Clone()
        {
            return new EnvironmentVariable(this);
        }

        public override String ToString()
        {
            return
                $"{nameof(this.Scope)}: {this.scope.ToString()}, " +
                $"{nameof(this.IsCreated)}: {this.IsCreated}, " +
                $"{nameof(this.IsChanged)}: {this.IsChanged}, " +
                $"{nameof(this.IsDeleted)}: {this.IsDeleted}, " +
                $"{nameof(this.IsRenamed)}: {this.IsRenamed}, " +
                $"{nameof(this.IsModified)}: {this.IsModified}, " +
                $"{nameof(this.IsReadonly)}: {this.IsReadonly}, " +
                $"{nameof(this.Start)}: {this.GetString(this.start)}, " +
                $"{nameof(this.Label)}: {this.GetString(this.label)}, " +
                $"{nameof(this.Value)}: {this.GetString(this.value)}, " +
                $"{nameof(this.Shift)}: {this.GetString(this.shift)}";
        }

        private String GetString(String value)
        {
            if (value == null)
            {
                return "<null>";
            }

            if (value.Trim().Length < 1)
            {
                return "<empty>";
            }

            return value;
        }

        private String GetString(String[] value)
        {
            if (value == null)
            {
                return "<null>";
            }

            if (value.Length < 1)
            {
                return "<empty>";
            }

            return String.Join(EnvironmentVariable.LineSeparator, value);
        }

        private String[] GetArray(String[] value)
        {
            return this.IntoArray(this.FromArray(value));
        }

        private String FromArray(String[] value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            if (value.Length < 1)
            {
                return String.Empty;
            }

            return String.Join(EnvironmentVariable.LineSeparator, value);
        }

        private String[] IntoArray(String value)
        {
            List<String> result = new List<String>();

            if (!String.IsNullOrWhiteSpace(value))
            {
                foreach (String current in value.Split(EnvironmentVariable.LineSeparators, StringSplitOptions.None))
                {
                    if (!String.IsNullOrWhiteSpace(current))
                    {
                        result.Add(current.Trim());
                    }
                }
            }

            return result.ToArray();
        }
    }
}
