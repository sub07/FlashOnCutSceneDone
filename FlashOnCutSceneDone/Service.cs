using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashOnCutSceneDone
{
    public class Service
    {
        [PluginService] internal static IPluginLog PluginLog { get; private set; } = null!;
        [PluginService] public static ICondition Condition { get; private set; } = null!;
    }
}