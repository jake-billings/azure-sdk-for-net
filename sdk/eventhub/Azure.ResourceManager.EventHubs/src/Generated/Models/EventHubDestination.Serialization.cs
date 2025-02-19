// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.EventHubs.Models
{
    public partial class EventHubDestination : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Name))
            {
                writer.WritePropertyName("name");
                writer.WriteStringValue(Name);
            }
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsDefined(StorageAccountResourceId))
            {
                writer.WritePropertyName("storageAccountResourceId");
                writer.WriteStringValue(StorageAccountResourceId);
            }
            if (Optional.IsDefined(BlobContainer))
            {
                writer.WritePropertyName("blobContainer");
                writer.WriteStringValue(BlobContainer);
            }
            if (Optional.IsDefined(ArchiveNameFormat))
            {
                writer.WritePropertyName("archiveNameFormat");
                writer.WriteStringValue(ArchiveNameFormat);
            }
            if (Optional.IsDefined(DataLakeSubscriptionId))
            {
                writer.WritePropertyName("dataLakeSubscriptionId");
                writer.WriteStringValue(DataLakeSubscriptionId.Value);
            }
            if (Optional.IsDefined(DataLakeAccountName))
            {
                writer.WritePropertyName("dataLakeAccountName");
                writer.WriteStringValue(DataLakeAccountName);
            }
            if (Optional.IsDefined(DataLakeFolderPath))
            {
                writer.WritePropertyName("dataLakeFolderPath");
                writer.WriteStringValue(DataLakeFolderPath);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static EventHubDestination DeserializeEventHubDestination(JsonElement element)
        {
            Optional<string> name = default;
            Optional<ResourceIdentifier> storageAccountResourceId = default;
            Optional<string> blobContainer = default;
            Optional<string> archiveNameFormat = default;
            Optional<Guid> dataLakeSubscriptionId = default;
            Optional<string> dataLakeAccountName = default;
            Optional<string> dataLakeFolderPath = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
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
                        if (property0.NameEquals("storageAccountResourceId"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            storageAccountResourceId = new ResourceIdentifier(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("blobContainer"))
                        {
                            blobContainer = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("archiveNameFormat"))
                        {
                            archiveNameFormat = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("dataLakeSubscriptionId"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            dataLakeSubscriptionId = property0.Value.GetGuid();
                            continue;
                        }
                        if (property0.NameEquals("dataLakeAccountName"))
                        {
                            dataLakeAccountName = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("dataLakeFolderPath"))
                        {
                            dataLakeFolderPath = property0.Value.GetString();
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new EventHubDestination(name.Value, storageAccountResourceId.Value, blobContainer.Value, archiveNameFormat.Value, Optional.ToNullable(dataLakeSubscriptionId), dataLakeAccountName.Value, dataLakeFolderPath.Value);
        }
    }
}
