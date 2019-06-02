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

using Plexdata.EnvironmentManager.Models;
using System;

namespace Plexdata.EnvironmentManager.Exceptions
{
    public class EnvironmentException : Exception
    {
        public EnvironmentException()
            : base()
        {
            this.Variable = null;
        }

        public EnvironmentException(EnvironmentVariable variable, String message)
            : base(message)
        {
            this.Variable = variable;
        }

        public EnvironmentException(EnvironmentVariable variable, Exception exception)
            : base(exception != null ? exception.Message : String.Empty, exception)
        {
            this.Variable = variable;
        }

        public EnvironmentVariable Variable { get; private set; }

        public override String ToString()
        {
            return
                $"{base.Message}" +
                $"{Environment.NewLine}" +
                $"{(this.Variable != null ? this.Variable.ToString() : "<null>")}" +
                $"{Environment.NewLine}" +
                $"{base.StackTrace}";
        }
    }
}
