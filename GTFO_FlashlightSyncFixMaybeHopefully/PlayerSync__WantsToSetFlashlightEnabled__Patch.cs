using HarmonyLib;
using Player;
using SNetwork;
using UnityEngine;

namespace FlashlightSyncFixMaybeHopefully;

[HarmonyPatch(typeof(PlayerSync), nameof(PlayerSync.WantsToSetFlashlightEnabled))]
[HarmonyPriority(Priority.Last)]
public class PlayerSync__WantsToSetFlashlightEnabled__Patch
{
    private static int _frameCount;
    public static bool Prefix(PlayerSync __instance, bool enable, bool broadcastOnly)
    {
        var currentFrameCount = Time.frameCount;

        // Anti recursion jank for EEC
        if (_frameCount == currentFrameCount)
            return false;
        
        _frameCount = currentFrameCount;
        
        var data = new pInventoryStatus
        {
            wieldedSlot = __instance.m_agent.Inventory.WieldedSlot,
            toolEnabled = enable
        };
        
        __instance.m_inventoryStatusPacket.Send(data, SNet_ChannelType.GameOrderCritical, SNet_SendQuality.Reliable);
        if (!broadcastOnly)
        {
            __instance.SyncInventoryStatus(data);
        }

        return false;
    }
}