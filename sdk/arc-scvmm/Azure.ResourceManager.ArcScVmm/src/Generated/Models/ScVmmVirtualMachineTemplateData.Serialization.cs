// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.ArcScVmm.Models;
using Azure.ResourceManager.Models;

namespace Azure.ResourceManager.ArcScVmm
{
    public partial class ScVmmVirtualMachineTemplateData : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("extendedLocation");
            writer.WriteObjectValue(ExtendedLocation);
            if (Optional.IsCollectionDefined(Tags))
            {
                writer.WritePropertyName("tags");
                writer.WriteStartObject();
                foreach (var item in Tags)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            writer.WritePropertyName("location");
            writer.WriteStringValue(Location);
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsDefined(InventoryItemId))
            {
                writer.WritePropertyName("inventoryItemId");
                writer.WriteStringValue(InventoryItemId);
            }
            if (Optional.IsDefined(Uuid))
            {
                writer.WritePropertyName("uuid");
                writer.WriteStringValue(Uuid);
            }
            if (Optional.IsDefined(VmmServerId))
            {
                writer.WritePropertyName("vmmServerId");
                writer.WriteStringValue(VmmServerId);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static ScVmmVirtualMachineTemplateData DeserializeScVmmVirtualMachineTemplateData(JsonElement element)
        {
            ExtendedLocation extendedLocation = default;
            Optional<IDictionary<string, string>> tags = default;
            AzureLocation location = default;
            ResourceIdentifier id = default;
            string name = default;
            ResourceType type = default;
            Optional<SystemData> systemData = default;
            Optional<string> inventoryItemId = default;
            Optional<string> uuid = default;
            Optional<string> vmmServerId = default;
            Optional<OSType> osType = default;
            Optional<string> osName = default;
            Optional<string> computerName = default;
            Optional<int> memoryMB = default;
            Optional<int> cpuCount = default;
            Optional<LimitCpuForMigration> limitCpuForMigration = default;
            Optional<DynamicMemoryEnabled> dynamicMemoryEnabled = default;
            Optional<IsCustomizable> isCustomizable = default;
            Optional<int> dynamicMemoryMaxMB = default;
            Optional<int> dynamicMemoryMinMB = default;
            Optional<string> isHighlyAvailable = default;
            Optional<int> generation = default;
            Optional<IReadOnlyList<NetworkInterfaces>> networkInterfaces = default;
            Optional<IReadOnlyList<VirtualDisk>> disks = default;
            Optional<string> provisioningState = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("extendedLocation"))
                {
                    extendedLocation = ExtendedLocation.DeserializeExtendedLocation(property.Value);
                    continue;
                }
                if (property.NameEquals("tags"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetString());
                    }
                    tags = dictionary;
                    continue;
                }
                if (property.NameEquals("location"))
                {
                    location = new AzureLocation(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = new ResourceType(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("systemData"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    systemData = JsonSerializer.Deserialize<SystemData>(property.Value.ToString());
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("inventoryItemId"))
                        {
                            inventoryItemId = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("uuid"))
                        {
                            uuid = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("vmmServerId"))
                        {
                            vmmServerId = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("osType"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            osType = new OSType(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("osName"))
                        {
                            osName = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("computerName"))
                        {
                            computerName = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("memoryMB"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            memoryMB = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("cpuCount"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            cpuCount = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("limitCpuForMigration"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            limitCpuForMigration = new LimitCpuForMigration(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("dynamicMemoryEnabled"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            dynamicMemoryEnabled = new DynamicMemoryEnabled(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("isCustomizable"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            isCustomizable = new IsCustomizable(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("dynamicMemoryMaxMB"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            dynamicMemoryMaxMB = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("dynamicMemoryMinMB"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            dynamicMemoryMinMB = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("isHighlyAvailable"))
                        {
                            isHighlyAvailable = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("generation"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            generation = property0.Value.GetInt32();
                            continue;
                        }
                        if (property0.NameEquals("networkInterfaces"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<NetworkInterfaces> array = new List<NetworkInterfaces>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(Models.NetworkInterfaces.DeserializeNetworkInterfaces(item));
                            }
                            networkInterfaces = array;
                            continue;
                        }
                        if (property0.NameEquals("disks"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<VirtualDisk> array = new List<VirtualDisk>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(VirtualDisk.DeserializeVirtualDisk(item));
                            }
                            disks = array;
                            continue;
                        }
                        if (property0.NameEquals("provisioningState"))
                        {
                            provisioningState = property0.Value.GetString();
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new ScVmmVirtualMachineTemplateData(id, name, type, systemData.Value, Optional.ToDictionary(tags), location, extendedLocation, inventoryItemId.Value, uuid.Value, vmmServerId.Value, Optional.ToNullable(osType), osName.Value, computerName.Value, Optional.ToNullable(memoryMB), Optional.ToNullable(cpuCount), Optional.ToNullable(limitCpuForMigration), Optional.ToNullable(dynamicMemoryEnabled), Optional.ToNullable(isCustomizable), Optional.ToNullable(dynamicMemoryMaxMB), Optional.ToNullable(dynamicMemoryMinMB), isHighlyAvailable.Value, Optional.ToNullable(generation), Optional.ToList(networkInterfaces), Optional.ToList(disks), provisioningState.Value);
        }
    }
}
