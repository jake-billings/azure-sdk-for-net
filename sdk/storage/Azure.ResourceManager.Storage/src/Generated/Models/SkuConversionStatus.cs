// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.ResourceManager.Storage.Models
{
    /// <summary> This property indicates the current sku conversion status. </summary>
    public readonly partial struct SkuConversionStatus : IEquatable<SkuConversionStatus>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="SkuConversionStatus"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public SkuConversionStatus(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string InProgressValue = "InProgress";
        private const string SucceededValue = "Succeeded";
        private const string FailedValue = "Failed";

        /// <summary> InProgress. </summary>
        public static SkuConversionStatus InProgress { get; } = new SkuConversionStatus(InProgressValue);
        /// <summary> Succeeded. </summary>
        public static SkuConversionStatus Succeeded { get; } = new SkuConversionStatus(SucceededValue);
        /// <summary> Failed. </summary>
        public static SkuConversionStatus Failed { get; } = new SkuConversionStatus(FailedValue);
        /// <summary> Determines if two <see cref="SkuConversionStatus"/> values are the same. </summary>
        public static bool operator ==(SkuConversionStatus left, SkuConversionStatus right) => left.Equals(right);
        /// <summary> Determines if two <see cref="SkuConversionStatus"/> values are not the same. </summary>
        public static bool operator !=(SkuConversionStatus left, SkuConversionStatus right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="SkuConversionStatus"/>. </summary>
        public static implicit operator SkuConversionStatus(string value) => new SkuConversionStatus(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is SkuConversionStatus other && Equals(other);
        /// <inheritdoc />
        public bool Equals(SkuConversionStatus other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
