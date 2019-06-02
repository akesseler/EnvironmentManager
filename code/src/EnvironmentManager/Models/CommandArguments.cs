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

using Plexdata.ArgumentParser.Attributes;
using System;

namespace Plexdata.EnvironmentManager.Models
{
    [HelpLicense]
    [HelpUtilize]
    [HelpPreface]
    [ParametersGroup]
    public class CommandArguments
    {
        [SwitchParameter(BriefLabel = "t", SolidLabel = "tray-icon", IsExclusive = true)]
        [HelpSummary("Use this argument to initially start the program minimized and as tray icon.")]
        public Boolean IsMinimized { get; set; }

        [SwitchParameter(BriefLabel = "i", SolidLabel = "install", IsExclusive = true)]
        [HelpSummary("Use this option to register the program for auto-launch on user login. This option requires a program startup with administrator privileges.")]
        public Boolean IsInstall { get; set; }

        [SwitchParameter(BriefLabel = "r", SolidLabel = "remove", IsExclusive = true)]
        [HelpSummary("Use this option to remove the program registration for auto-launch on user login. This option requires a program startup with administrator privileges.")]
        public Boolean IsRemove { get; set; }

        [SwitchParameter(BriefLabel = "?", SolidLabel = "help")]
        [HelpSummary("Show this arguments help screen.")]
        public Boolean IsHelp { get; set; }
    }
}
