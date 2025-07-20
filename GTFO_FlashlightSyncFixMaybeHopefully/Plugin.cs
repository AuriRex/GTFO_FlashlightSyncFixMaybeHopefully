using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using FlashlightSyncFixMaybeHopefully;
using HarmonyLib;

[assembly: AssemblyVersion(Plugin.VERSION)]
[assembly: AssemblyFileVersion(Plugin.VERSION)]
[assembly: AssemblyInformationalVersion(Plugin.VERSION)]

namespace FlashlightSyncFixMaybeHopefully;

[BepInPlugin(GUID, MOD_NAME, VERSION)]
public class Plugin : BasePlugin
{
    public const string GUID = "dev.AuriRex.gtfo.FlashlightSyncFixMaybeHopefully";
    public const string MOD_NAME = ManifestInfo.TSName;
    public const string VERSION = ManifestInfo.TSVersion;

    internal static ManualLogSource L;

    private static readonly Harmony _harmony = new(GUID);

    public override void Load()
    {
        L = Log;

        _harmony.PatchAll(Assembly.GetExecutingAssembly());

        L.LogInfo("Plugin loaded!");
    }
}