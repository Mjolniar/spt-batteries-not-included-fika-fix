using BatteriesNotIncluded.FikaSync.Managers;
using BatteriesNotIncluded.FikaSync.Pools;
using EFT.InventoryLogic;
using Fika.Core.Main.Players;
using Fika.Core.Networking;
using Fika.Core.Networking.LiteNetLib.Utils;

namespace BatteriesNotIncluded.FikaSync.Packets;

public class BotBatteryPacket : IDevicePoolSubPacket
{
    public Item Battery;
    public bool HasInitialToggleState;
    public bool InitialToggleState;
    public int SlotIndex;

    public static BotBatteryPacket CreateInstance()
    {
        return new BotBatteryPacket();
    }

    public static BotBatteryPacket FromValue(Item battery, int slotIndex, bool? initialToggleState)
    {
        var packet = DeviceSubPacketPoolManager.Instance.GetPacket<BotBatteryPacket>(EDeviceSubPacketType.BotBattery);
        packet.Battery = battery;
        packet.HasInitialToggleState = initialToggleState.HasValue;
        packet.InitialToggleState = initialToggleState.GetValueOrDefault();
        packet.SlotIndex = slotIndex;
        return packet;
    }

    public void Execute(DeviceSyncClientManager manager, int deviceIndex)
    {
        manager.BotBatterySyncSystem.Set(this);
        manager.BotBatterySyncSystem.Run(manager.DeviceManager, deviceIndex);
        manager.BotBatterySyncSystem.Set(null);
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.PutItem(Battery);
        writer.Put(HasInitialToggleState);
        writer.Put(InitialToggleState);
        writer.Put(SlotIndex);
    }

    public void Deserialize(NetDataReader reader)
    {
        Battery = reader.GetItem();
        HasInitialToggleState = reader.GetBool();
        InitialToggleState = reader.GetBool();
        SlotIndex = reader.GetInt();
    }

    public void Dispose()
    {
        Battery = null;
        HasInitialToggleState = false;
        InitialToggleState = false;
        SlotIndex = -1;
    }

    public void Execute(FikaPlayer player = null)
    {
        // Do nothing
    }
}
