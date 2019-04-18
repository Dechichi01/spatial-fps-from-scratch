// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using Improbable.Gdk.Core;
using Improbable.Worker.CInterop;
using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Improbable.Gdk.Health
{
    public partial class HealthRegenComponent
    {
        public const uint ComponentId = 2041;

        public struct Component : IComponentData, ISpatialComponentData, ISnapshottable<Snapshot>
        {
            public uint ComponentId => 2041;

            // Bit masks for tracking which component properties were changed locally and need to be synced.
            // Each byte tracks 8 component properties.
            private byte dirtyBits0;

            public bool IsDataDirty()
            {
                var isDataDirty = false;
                isDataDirty |= (dirtyBits0 != 0x0);
                return isDataDirty;
            }

            /*
            The propertyIndex argument counts up from 0 in the order defined in your schema component.
            It is not the schema field number itself. For example:
            component MyComponent
            {
                id = 1337;
                bool val_a = 1;
                bool val_b = 3;
            }
            In that case, val_a corresponds to propertyIndex 0 and val_b corresponds to propertyIndex 1 in this method.
            This method throws an InvalidOperationException in case your component doesn't contain properties.
            */
            public bool IsDataDirty(int propertyIndex)
            {
                if (propertyIndex < 0 || propertyIndex >= 6)
                {
                    throw new ArgumentException("\"propertyIndex\" argument out of range. Valid range is [0, 5]. " +
                        "Unless you are using custom component replication code, this is most likely caused by a code generation bug. " +
                        "Please contact SpatialOS support if you encounter this issue.");
                }

                // Retrieve the dirtyBits[0-n] field that tracks this property.
                var dirtyBitsByteIndex = propertyIndex / 8;
                switch (dirtyBitsByteIndex)
                {
                    case 0:
                        return (dirtyBits0 & (0x1 << propertyIndex % 8)) != 0x0;
                }

                return false;
            }

            // Like the IsDataDirty() method above, the propertyIndex arguments starts counting from 0.
            // This method throws an InvalidOperationException in case your component doesn't contain properties.
            public void MarkDataDirty(int propertyIndex)
            {
                if (propertyIndex < 0 || propertyIndex >= 6)
                {
                    throw new ArgumentException("\"propertyIndex\" argument out of range. Valid range is [0, 5]. " +
                        "Unless you are using custom component replication code, this is most likely caused by a code generation bug. " +
                        "Please contact SpatialOS support if you encounter this issue.");
                }

                // Retrieve the dirtyBits[0-n] field that tracks this property.
                var dirtyBitsByteIndex = propertyIndex / 8;
                switch (dirtyBitsByteIndex)
                {
                    case 0:
                        dirtyBits0 |= (byte) (0x1 << propertyIndex % 8);
                        break;
                }
            }

            public void MarkDataClean()
            {
                dirtyBits0 = 0x0;
            }

            public Snapshot ToComponentSnapshot(global::Unity.Entities.World world)
            {
                var componentDataSchema = new ComponentData(new SchemaComponentData(2041));
                Serialization.SerializeComponent(this, componentDataSchema.SchemaData.Value.GetFields(), world);
                var snapshot = Serialization.DeserializeSnapshot(componentDataSchema.SchemaData.Value.GetFields());

                componentDataSchema.SchemaData?.Destroy();
                componentDataSchema.SchemaData = null;

                return snapshot;
            }

            private BlittableBool damagedRecently;

            public BlittableBool DamagedRecently
            {
                get => damagedRecently;
                set
                {
                    MarkDataDirty(0);
                    this.damagedRecently = value;
                }
            }

            private float regenCooldownTimer;

            public float RegenCooldownTimer
            {
                get => regenCooldownTimer;
                set
                {
                    MarkDataDirty(1);
                    this.regenCooldownTimer = value;
                }
            }

            private float cooldownSyncInterval;

            public float CooldownSyncInterval
            {
                get => cooldownSyncInterval;
                set
                {
                    MarkDataDirty(2);
                    this.cooldownSyncInterval = value;
                }
            }

            private float regenPauseTime;

            public float RegenPauseTime
            {
                get => regenPauseTime;
                set
                {
                    MarkDataDirty(3);
                    this.regenPauseTime = value;
                }
            }

            private float regenInterval;

            public float RegenInterval
            {
                get => regenInterval;
                set
                {
                    MarkDataDirty(4);
                    this.regenInterval = value;
                }
            }

            private float regenAmount;

            public float RegenAmount
            {
                get => regenAmount;
                set
                {
                    MarkDataDirty(5);
                    this.regenAmount = value;
                }
            }
        }

        public struct ComponentAuthority : ISharedComponentData, IEquatable<ComponentAuthority>
        {
            public bool HasAuthority;

            public ComponentAuthority(bool hasAuthority)
            {
                HasAuthority = hasAuthority;
            }

            // todo think about whether any of this is necessary
            // Unity does a bitwise equality check so this is just for users reading the struct
            public static readonly ComponentAuthority NotAuthoritative = new ComponentAuthority(false);
            public static readonly ComponentAuthority Authoritative = new ComponentAuthority(true);

            public bool Equals(ComponentAuthority other)
            {
                return this == other;
            }

            public override bool Equals(object obj)
            {
                return obj is ComponentAuthority auth && this == auth;
            }

            public override int GetHashCode()
            {
                return HasAuthority.GetHashCode();
            }

            public static bool operator ==(ComponentAuthority a, ComponentAuthority b)
            {
                return a.HasAuthority == b.HasAuthority;
            }

            public static bool operator !=(ComponentAuthority a, ComponentAuthority b)
            {
                return !(a == b);
            }
        }

        [System.Serializable]
        public struct Snapshot : ISpatialComponentSnapshot
        {
            public uint ComponentId => 2041;

            public BlittableBool DamagedRecently;
            public float RegenCooldownTimer;
            public float CooldownSyncInterval;
            public float RegenPauseTime;
            public float RegenInterval;
            public float RegenAmount;

            public Snapshot(BlittableBool damagedRecently, float regenCooldownTimer, float cooldownSyncInterval, float regenPauseTime, float regenInterval, float regenAmount)
            {
                DamagedRecently = damagedRecently;
                RegenCooldownTimer = regenCooldownTimer;
                CooldownSyncInterval = cooldownSyncInterval;
                RegenPauseTime = regenPauseTime;
                RegenInterval = regenInterval;
                RegenAmount = regenAmount;
            }
        }

        public static class Serialization
        {
            public static void SerializeComponent(Improbable.Gdk.Health.HealthRegenComponent.Component component, global::Improbable.Worker.CInterop.SchemaObject obj, global::Unity.Entities.World world)
            {
                {
                    obj.AddBool(1, component.DamagedRecently);
                }
                {
                    obj.AddFloat(2, component.RegenCooldownTimer);
                }
                {
                    obj.AddFloat(3, component.CooldownSyncInterval);
                }
                {
                    obj.AddFloat(4, component.RegenPauseTime);
                }
                {
                    obj.AddFloat(5, component.RegenInterval);
                }
                {
                    obj.AddFloat(6, component.RegenAmount);
                }
            }

            public static void SerializeUpdate(Improbable.Gdk.Health.HealthRegenComponent.Component component, global::Improbable.Worker.CInterop.SchemaComponentUpdate updateObj)
            {
                var obj = updateObj.GetFields();
                {
                    if (component.IsDataDirty(0))
                    {
                        obj.AddBool(1, component.DamagedRecently);
                    }

                }
                {
                    if (component.IsDataDirty(1))
                    {
                        obj.AddFloat(2, component.RegenCooldownTimer);
                    }

                }
                {
                    if (component.IsDataDirty(2))
                    {
                        obj.AddFloat(3, component.CooldownSyncInterval);
                    }

                }
                {
                    if (component.IsDataDirty(3))
                    {
                        obj.AddFloat(4, component.RegenPauseTime);
                    }

                }
                {
                    if (component.IsDataDirty(4))
                    {
                        obj.AddFloat(5, component.RegenInterval);
                    }

                }
                {
                    if (component.IsDataDirty(5))
                    {
                        obj.AddFloat(6, component.RegenAmount);
                    }

                }
            }

            public static void SerializeUpdate(Improbable.Gdk.Health.HealthRegenComponent.Update update, global::Improbable.Worker.CInterop.SchemaComponentUpdate updateObj)
            {
                var obj = updateObj.GetFields();
                {
                    if (update.DamagedRecently.HasValue)
                    {
                        var field = update.DamagedRecently.Value;
                        obj.AddBool(1, field);
                    }
                }
                {
                    if (update.RegenCooldownTimer.HasValue)
                    {
                        var field = update.RegenCooldownTimer.Value;
                        obj.AddFloat(2, field);
                    }
                }
                {
                    if (update.CooldownSyncInterval.HasValue)
                    {
                        var field = update.CooldownSyncInterval.Value;
                        obj.AddFloat(3, field);
                    }
                }
                {
                    if (update.RegenPauseTime.HasValue)
                    {
                        var field = update.RegenPauseTime.Value;
                        obj.AddFloat(4, field);
                    }
                }
                {
                    if (update.RegenInterval.HasValue)
                    {
                        var field = update.RegenInterval.Value;
                        obj.AddFloat(5, field);
                    }
                }
                {
                    if (update.RegenAmount.HasValue)
                    {
                        var field = update.RegenAmount.Value;
                        obj.AddFloat(6, field);
                    }
                }
            }

            public static void SerializeSnapshot(Improbable.Gdk.Health.HealthRegenComponent.Snapshot snapshot, global::Improbable.Worker.CInterop.SchemaObject obj)
            {
                {
                    obj.AddBool(1, snapshot.DamagedRecently);
                }
                {
                    obj.AddFloat(2, snapshot.RegenCooldownTimer);
                }
                {
                    obj.AddFloat(3, snapshot.CooldownSyncInterval);
                }
                {
                    obj.AddFloat(4, snapshot.RegenPauseTime);
                }
                {
                    obj.AddFloat(5, snapshot.RegenInterval);
                }
                {
                    obj.AddFloat(6, snapshot.RegenAmount);
                }
            }

            public static Improbable.Gdk.Health.HealthRegenComponent.Component Deserialize(global::Improbable.Worker.CInterop.SchemaObject obj, global::Unity.Entities.World world)
            {
                var component = new Improbable.Gdk.Health.HealthRegenComponent.Component();

                {
                    component.DamagedRecently = obj.GetBool(1);
                }
                {
                    component.RegenCooldownTimer = obj.GetFloat(2);
                }
                {
                    component.CooldownSyncInterval = obj.GetFloat(3);
                }
                {
                    component.RegenPauseTime = obj.GetFloat(4);
                }
                {
                    component.RegenInterval = obj.GetFloat(5);
                }
                {
                    component.RegenAmount = obj.GetFloat(6);
                }
                return component;
            }

            public static Improbable.Gdk.Health.HealthRegenComponent.Update DeserializeUpdate(global::Improbable.Worker.CInterop.SchemaComponentUpdate updateObj)
            {
                var update = new Improbable.Gdk.Health.HealthRegenComponent.Update();
                var obj = updateObj.GetFields();

                {
                    if (obj.GetBoolCount(1) == 1)
                    {
                        var value = obj.GetBool(1);
                        update.DamagedRecently = new global::Improbable.Gdk.Core.Option<BlittableBool>(value);
                    }
                    
                }
                {
                    if (obj.GetFloatCount(2) == 1)
                    {
                        var value = obj.GetFloat(2);
                        update.RegenCooldownTimer = new global::Improbable.Gdk.Core.Option<float>(value);
                    }
                    
                }
                {
                    if (obj.GetFloatCount(3) == 1)
                    {
                        var value = obj.GetFloat(3);
                        update.CooldownSyncInterval = new global::Improbable.Gdk.Core.Option<float>(value);
                    }
                    
                }
                {
                    if (obj.GetFloatCount(4) == 1)
                    {
                        var value = obj.GetFloat(4);
                        update.RegenPauseTime = new global::Improbable.Gdk.Core.Option<float>(value);
                    }
                    
                }
                {
                    if (obj.GetFloatCount(5) == 1)
                    {
                        var value = obj.GetFloat(5);
                        update.RegenInterval = new global::Improbable.Gdk.Core.Option<float>(value);
                    }
                    
                }
                {
                    if (obj.GetFloatCount(6) == 1)
                    {
                        var value = obj.GetFloat(6);
                        update.RegenAmount = new global::Improbable.Gdk.Core.Option<float>(value);
                    }
                    
                }
                return update;
            }

            public static Improbable.Gdk.Health.HealthRegenComponent.Update DeserializeUpdate(global::Improbable.Worker.CInterop.SchemaComponentData data)
            {
                var update = new Improbable.Gdk.Health.HealthRegenComponent.Update();
                var obj = data.GetFields();

                {
                    var value = obj.GetBool(1);
                    update.DamagedRecently = new global::Improbable.Gdk.Core.Option<BlittableBool>(value);
                    
                }
                {
                    var value = obj.GetFloat(2);
                    update.RegenCooldownTimer = new global::Improbable.Gdk.Core.Option<float>(value);
                    
                }
                {
                    var value = obj.GetFloat(3);
                    update.CooldownSyncInterval = new global::Improbable.Gdk.Core.Option<float>(value);
                    
                }
                {
                    var value = obj.GetFloat(4);
                    update.RegenPauseTime = new global::Improbable.Gdk.Core.Option<float>(value);
                    
                }
                {
                    var value = obj.GetFloat(5);
                    update.RegenInterval = new global::Improbable.Gdk.Core.Option<float>(value);
                    
                }
                {
                    var value = obj.GetFloat(6);
                    update.RegenAmount = new global::Improbable.Gdk.Core.Option<float>(value);
                    
                }
                return update;
            }

            public static Improbable.Gdk.Health.HealthRegenComponent.Snapshot DeserializeSnapshot(global::Improbable.Worker.CInterop.SchemaObject obj)
            {
                var component = new Improbable.Gdk.Health.HealthRegenComponent.Snapshot();

                {
                    component.DamagedRecently = obj.GetBool(1);
                }

                {
                    component.RegenCooldownTimer = obj.GetFloat(2);
                }

                {
                    component.CooldownSyncInterval = obj.GetFloat(3);
                }

                {
                    component.RegenPauseTime = obj.GetFloat(4);
                }

                {
                    component.RegenInterval = obj.GetFloat(5);
                }

                {
                    component.RegenAmount = obj.GetFloat(6);
                }

                return component;
            }

            public static void ApplyUpdate(global::Improbable.Worker.CInterop.SchemaComponentUpdate updateObj, ref Improbable.Gdk.Health.HealthRegenComponent.Component component)
            {
                var obj = updateObj.GetFields();

                {
                    if (obj.GetBoolCount(1) == 1)
                    {
                        var value = obj.GetBool(1);
                        component.DamagedRecently = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(2) == 1)
                    {
                        var value = obj.GetFloat(2);
                        component.RegenCooldownTimer = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(3) == 1)
                    {
                        var value = obj.GetFloat(3);
                        component.CooldownSyncInterval = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(4) == 1)
                    {
                        var value = obj.GetFloat(4);
                        component.RegenPauseTime = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(5) == 1)
                    {
                        var value = obj.GetFloat(5);
                        component.RegenInterval = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(6) == 1)
                    {
                        var value = obj.GetFloat(6);
                        component.RegenAmount = value;
                    }
                    
                }
            }

            public static void ApplyUpdate(global::Improbable.Worker.CInterop.SchemaComponentUpdate updateObj, ref Improbable.Gdk.Health.HealthRegenComponent.Snapshot snapshot)
            {
                var obj = updateObj.GetFields();

                {
                    if (obj.GetBoolCount(1) == 1)
                    {
                        var value = obj.GetBool(1);
                        snapshot.DamagedRecently = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(2) == 1)
                    {
                        var value = obj.GetFloat(2);
                        snapshot.RegenCooldownTimer = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(3) == 1)
                    {
                        var value = obj.GetFloat(3);
                        snapshot.CooldownSyncInterval = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(4) == 1)
                    {
                        var value = obj.GetFloat(4);
                        snapshot.RegenPauseTime = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(5) == 1)
                    {
                        var value = obj.GetFloat(5);
                        snapshot.RegenInterval = value;
                    }
                    
                }
                {
                    if (obj.GetFloatCount(6) == 1)
                    {
                        var value = obj.GetFloat(6);
                        snapshot.RegenAmount = value;
                    }
                    
                }
            }
        }

        public struct Update : ISpatialComponentUpdate
        {
            internal static Stack<List<Update>> Pool = new Stack<List<Update>>();

            public Option<BlittableBool> DamagedRecently;
            public Option<float> RegenCooldownTimer;
            public Option<float> CooldownSyncInterval;
            public Option<float> RegenPauseTime;
            public Option<float> RegenInterval;
            public Option<float> RegenAmount;
        }

        public struct ReceivedUpdates : IComponentData
        {
            internal uint handle;
            public global::System.Collections.Generic.List<Update> Updates
            {
                get => Improbable.Gdk.Health.HealthRegenComponent.ReferenceTypeProviders.UpdatesProvider.Get(handle);
            }
        }

        internal class HealthRegenComponentDynamic : IDynamicInvokable
        {
            public uint ComponentId => HealthRegenComponent.ComponentId;

            private static Component DeserializeData(ComponentData data, World world)
            {
                var schemaDataOpt = data.SchemaData;
                if (!schemaDataOpt.HasValue)
                {
                    throw new ArgumentException($"Can not deserialize an empty {nameof(ComponentData)}");
                }

                return Serialization.Deserialize(schemaDataOpt.Value.GetFields(), world);
            }

            private static Update DeserializeUpdate(ComponentUpdate update, World world)
            {
                var schemaDataOpt = update.SchemaData;
                if (!schemaDataOpt.HasValue)
                {
                    throw new ArgumentException($"Can not deserialize an empty {nameof(ComponentUpdate)}");
                }

                return Serialization.DeserializeUpdate(schemaDataOpt.Value);
            }

            private static Snapshot DeserializeSnapshot(ComponentData snapshot)
            {
                var schemaDataOpt = snapshot.SchemaData;
                if (!schemaDataOpt.HasValue)
                {
                    throw new ArgumentException($"Can not deserialize an empty {nameof(ComponentData)}");
                }

                return Serialization.DeserializeSnapshot(schemaDataOpt.Value.GetFields());
            }

            private static void SerializeSnapshot(Snapshot snapshot, ComponentData data)
            {
                var schemaDataOpt = data.SchemaData;
                if (!schemaDataOpt.HasValue)
                {
                    throw new ArgumentException($"Can not serialise an empty {nameof(ComponentData)}");
                }

                Serialization.SerializeSnapshot(snapshot, data.SchemaData.Value.GetFields());
            }

            private static Update SnapshotToUpdate(in Snapshot snapshot)
            {
                var update = new Update();
                update.DamagedRecently = new Option<BlittableBool>(snapshot.DamagedRecently);
                update.RegenCooldownTimer = new Option<float>(snapshot.RegenCooldownTimer);
                update.CooldownSyncInterval = new Option<float>(snapshot.CooldownSyncInterval);
                update.RegenPauseTime = new Option<float>(snapshot.RegenPauseTime);
                update.RegenInterval = new Option<float>(snapshot.RegenInterval);
                update.RegenAmount = new Option<float>(snapshot.RegenAmount);
                return update;
            }

            public void InvokeHandler(Dynamic.IHandler handler)
            {
                handler.Accept<Component, Update>(ComponentId, DeserializeData, DeserializeUpdate);
            }

            public void InvokeSnapshotHandler(DynamicSnapshot.ISnapshotHandler handler)
            {
                handler.Accept<Snapshot>(ComponentId, DeserializeSnapshot, SerializeSnapshot);
            }

            public void InvokeConvertHandler(DynamicConverter.IConverterHandler handler)
            {
                handler.Accept<Snapshot, Update>(ComponentId, SnapshotToUpdate);
            }
        }
    }
}
