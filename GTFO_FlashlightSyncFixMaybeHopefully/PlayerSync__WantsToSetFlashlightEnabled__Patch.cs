using HarmonyLib;
using Player;
using SNetwork;

namespace FlashlightSyncFixMaybeHopefully;

[HarmonyPatch(typeof(PlayerSync), nameof(PlayerSync.WantsToSetFlashlightEnabled))]
public class PlayerSync__WantsToSetFlashlightEnabled__Patch
{
    public static bool Prefix(PlayerSync __instance, bool enable, bool broadcastOnly)
    {
        var data = new pInventoryStatus
        {
            wieldedSlot = __instance.m_agent.Inventory.WieldedSlot,
            toolEnabled = enable
        };
        
        __instance.m_inventoryStatusPacket.Send(data, SNet_ChannelType.GameOrderCritical);
        if (!broadcastOnly)
        {
            __instance.SyncInventoryStatus(data);
        }

        return false;
    }
}