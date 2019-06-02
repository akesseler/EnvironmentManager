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

using Plexdata.EnvironmentManager.Exceptions;
using Plexdata.EnvironmentManager.Models;
using Plexdata.LogWriter.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Plexdata.EnvironmentManager.Serializers
{
    public static class EnvironmentSerializer
    {
        public static IEnumerable<EnvironmentVariable> Load(EnvironmentVariableTarget target)
        {
            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables(target))
            {
                yield return EnvironmentSerializer.LoadSettings(new EnvironmentVariable(target, entry));
            }
        }

        public static Boolean Save(IEnumerable<EnvironmentVariable> variables, ref IList<Exception> exceptions)
        {
            Int32 occurrences = 0;

            foreach (EnvironmentVariable variable in variables)
            {
                try
                {
                    if (variable == null || !variable.IsModified)
                    {
                        continue;
                    }

                    if (variable.IsReadonly)
                    {
                        throw new InvalidOperationException("Unable to process read-only variables.");
                    }

                    EnvironmentSerializer.HandleCreated(variable);

                    EnvironmentSerializer.HandleChanged(variable);

                    EnvironmentSerializer.HandleDeleted(variable);
                }
                catch (Exception exception)
                {
                    occurrences++;

                    if (exceptions != null)
                    {
                        exceptions.Add(new EnvironmentException(variable, exception));
                    }

                    Program.Logger.Error("Error while saving environment variables.", exception);
                }
            }

            return occurrences == 0;
        }

        private static void HandleCreated(EnvironmentVariable variable)
        {
            if (variable.IsCreated)
            {
                // Try creating this new environment variable.
                Environment.SetEnvironmentVariable(variable.Label, variable.Value, variable.Scope);
                variable.SaveSettings(Program.Settings);
            }
        }

        private static void HandleChanged(EnvironmentVariable variable)
        {
            if (variable.IsChanged)
            {
                if (variable.IsRenamed)
                {
                    // Try discarding original environment variable.
                    Environment.SetEnvironmentVariable(variable.Start, String.Empty, variable.Scope);
                    variable.FreeSettings(Program.Settings);
                }

                // Try creating this new environment variable.
                Environment.SetEnvironmentVariable(variable.Label, variable.Value, variable.Scope);
                variable.SaveSettings(Program.Settings);
            }
        }

        private static void HandleDeleted(EnvironmentVariable variable)
        {
            if (variable.IsDeleted)
            {
                if (variable.IsRenamed)
                {
                    // Try discarding original environment variable.
                    Environment.SetEnvironmentVariable(variable.Start, String.Empty, variable.Scope);
                }
                else
                {
                    // Try discarding current environment variable.
                    Environment.SetEnvironmentVariable(variable.Label, String.Empty, variable.Scope);
                }

                variable.FreeSettings(Program.Settings);
            }
        }

        private static EnvironmentVariable LoadSettings(EnvironmentVariable variable)
        {
            variable.LoadSettings(Program.Settings);
            return variable;
        }
    }
}
