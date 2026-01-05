using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Game.ClientState.Conditions;

namespace FlashOnCutSceneDone
{
    public class FlashOnCutSceneDonePlugin : IDalamudPlugin
    {
        public IDalamudPluginInterface Interface;
        public IPluginLog Logger;

        public FlashOnCutSceneDonePlugin(IDalamudPluginInterface pluginInterface)
        {
            Interface = pluginInterface;
            Interface.Create<Service>();

            Logger = Service.PluginLog;

            Service.Condition.ConditionChange += ConditionOnConditionChange;
        }

        void ConditionOnConditionChange(ConditionFlag flag, bool value)
        {
            Logger.Debug($"Condition changed: {flag} is now {value}");
            if (flag != ConditionFlag.OccupiedInCutSceneEvent)
            {
                return;
            }

            Logger.Info($"Condition changed: {flag} is now {value}");

            if (!value && !FlashWindow.ApplicationIsActivated())
            {
                var flashInfo = new FlashWindow.FLASHWINFO
                {
                    cbSize = (uint)Marshal.SizeOf<FlashWindow.FLASHWINFO>(),
                    uCount = uint.MaxValue,
                    dwTimeout = 0,
                    dwFlags = FlashWindow.FLASHW_ALL | FlashWindow.FLASHW_TIMERNOFG,
                    hwnd = Process.GetCurrentProcess().MainWindowHandle,
                };
                FlashWindow.Flash(flashInfo);
            }
        }

        public void Dispose()
        {
            Service.Condition.ConditionChange -= ConditionOnConditionChange;
            GC.SuppressFinalize(this);
        }
    }
}