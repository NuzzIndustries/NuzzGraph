using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Core.Utilities
{
    internal static class RuntimeUtility
    {
        private static readonly string[] _designerProcessNames = new[] { "xdesproc", "devenv" };
        private static bool? _runningFromVisualStudioDesigner = null;
        internal static bool RunningFromVisualStudioDesigner
        {
            get
            {
                if (!_runningFromVisualStudioDesigner.HasValue)
                {
                    using (System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess())
                    {
                        _runningFromVisualStudioDesigner = _designerProcessNames.Contains(currentProcess.ProcessName.ToLower().Trim());
                    }
                }

                return _runningFromVisualStudioDesigner.Value;
            }
        }
    }
}
