// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Messaging.ServiceBus.Core;
using Azure.Messaging.ServiceBus.Diagnostics;
using Azure.Messaging.ServiceBus.Administration;

namespace Azure.Messaging.ServiceBus
{
    /// <summary>
    /// The <see cref="ServiceBusRuleManager"/> allows rules for a subscription to be managed. The rule manager requires only
    /// Listen claims, whereas the <see cref="ServiceBusAdministrationClient"/> requires Manage claims.
    /// </summary>
    public class ServiceBusRuleManager : IAsyncDisposable
    {
        /// <summary>
        /// The path of the Service Bus subscription that the rule manager is connected to, specific to the
        /// Service Bus namespace that contains it.
        /// </summary>
        public virtual string SubscriptionPath { get; }

        /// <summary>
        /// Gets the ID to identify this client. This can be used to correlate logs and exceptions.
        /// </summary>
        /// <remarks>Every new client has a unique ID.</remarks>
        internal string Identifier { get; }

        /// <summary>
        ///   Indicates whether or not this <see cref="ServiceBusRuleManager"/> has been closed.
        /// </summary>
        ///
        /// <value>
        /// <c>true</c> if the rule manager is closed; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsClosed
        {
            get => _closed;
            private set => _closed = value;
        }

        /// <summary>Indicates whether or not this instance has been closed.</summary>
        private volatile bool _closed;

        /// <summary>
        /// The active connection to the Azure Service Bus service, enabling client communications for metadata
        /// about the associated Service Bus entity and access to transport-aware rule manager.
        /// </summary>
        ///
        private readonly ServiceBusConnection _connection;

        /// <summary>
        /// An abstracted Service Bus transport-specific rule manager that is associated with the
        /// Service Bus entity gateway; intended to perform delegated operations.
        /// </summary>
        internal readonly TransportRuleManager InnerRuleManager;

        /// <summary>
        /// Responsible for creating entity scopes.
        /// </summary>
        private readonly EntityScopeFactory _scopeFactory;

        private const int MaxRulesPerRequest = 100;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ServiceBusRuleManager"/> class.
        /// </summary>
        ///
        /// <param name="connection">The <see cref="ServiceBusConnection" /> connection to use for communication with the Service Bus service.</param>
        /// <param name="subscriptionPath">The path of the Service Bus subscription to which the rule manager is bound.</param>
        ///
        internal ServiceBusRuleManager(
            ServiceBusConnection connection,
            string subscriptionPath)
        {
            Argument.AssertNotNull(connection, nameof(connection));
            Argument.AssertNotNull(connection.RetryOptions, nameof(connection.RetryOptions));
            Argument.AssertNotNullOrWhiteSpace(subscriptionPath, nameof(subscriptionPath));
            connection.ThrowIfClosed();

            Identifier = DiagnosticUtilities.GenerateIdentifier(subscriptionPath);
            _connection = connection;
            SubscriptionPath = subscriptionPath;
            InnerRuleManager = _connection.CreateTransportRuleManager(
                subscriptionPath: SubscriptionPath,
                retryPolicy: connection.RetryOptions.ToRetryPolicy(),
                identifier: Identifier);
            _scopeFactory = new EntityScopeFactory(subscriptionPath, _connection.FullyQualifiedNamespace);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusRuleManager"/> class for mocking.
        /// </summary>
        ///
        protected ServiceBusRuleManager() { }

        /// <summary>
        /// Adds a rule to the current subscription to filter the messages reaching from topic to the subscription.
        /// </summary>
        ///
        /// <param name="ruleName">Name of the rule</param>
        /// <param name="filter">The filter expression against which messages will be matched.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> instance to signal the request to cancel the operation.</param>
        ///
        /// <remarks>
        /// You can add rules to the subscription that decides which messages from the topic should reach the subscription.
        /// A default <see cref="TrueRuleFilter"/> rule named <see cref="RuleProperties.DefaultRuleName"/> is always added while creation of the Subscription.
        /// You can add multiple rules with distinct names to the same subscription.
        /// Multiple filters combine with each other using logical OR condition. i.e., If any filter succeeds, the message is passed on to the subscription.
        /// </remarks>
        ///
        /// <returns>A task instance that represents the asynchronous add rule operation.</returns>
        public virtual async Task CreateRuleAsync(
            string ruleName,
            RuleFilter filter,
            CancellationToken cancellationToken = default)
        {
            await CreateRuleAsync(new CreateRuleOptions(ruleName, filter), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a rule to the current subscription to filter the messages reaching from topic to the subscription.
        /// </summary>
        ///
        /// <param name="options">The options for the rule to add.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> instance to signal the request to cancel the operation.</param>
        ///
        /// <remarks>
        /// You can add rules to the subscription that decides which messages from the topic should reach the subscription.
        /// A default <see cref="TrueRuleFilter"/> rule named <see cref="RuleProperties.DefaultRuleName"/> is always added while creation of the Subscription.
        /// You can add multiple rules with distinct names to the same subscription.
        /// Multiple filters combine with each other using logical OR condition. i.e., If any filter succeeds, the message is passed on to the subscription.
        /// </remarks>
        ///
        /// <returns>A task instance that represents the asynchronous add rule operation.</returns>
        public virtual async Task CreateRuleAsync(
            CreateRuleOptions options,
            CancellationToken cancellationToken = default)
        {
            Argument.AssertNotDisposed(IsClosed, nameof(ServiceBusRuleManager));
            Argument.AssertNotNull(options, nameof(options));
            cancellationToken.ThrowIfCancellationRequested<TaskCanceledException>();
            EntityNameFormatter.CheckValidRuleName(options.Name);
            ServiceBusEventSource.Log.CreateRuleStart(Identifier, options.Name);

            using DiagnosticScope scope = _scopeFactory.CreateScope(
                DiagnosticProperty.CreateRuleActivityName,
                DiagnosticScope.ActivityKind.Client);
            scope.Start();

            try
            {
                await InnerRuleManager.CreateRuleAsync(
                    new RuleProperties(options),
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                ServiceBusEventSource.Log.CreateRuleException(Identifier, exception.ToString(), options.Name);
                scope.Failed(exception);
                throw;
            }

            cancellationToken.ThrowIfCancellationRequested<TaskCanceledException>();
            ServiceBusEventSource.Log.CreateRuleComplete(Identifier, options.Name);
        }

        /// <summary>
        /// Removes the rule on the subscription identified by <paramref name="ruleName" />.
        /// </summary>
        ///
        /// <param name="ruleName">Name of the rule</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> instance to signal the request to cancel the operation.</param>
        ///
        /// <returns>A task instance that represents the asynchronous remove rule operation.</returns>
        public virtual async Task DeleteRuleAsync(
            string ruleName,
            CancellationToken cancellationToken = default)
        {
            Argument.AssertNotDisposed(IsClosed, nameof(ServiceBusRuleManager));
            Argument.AssertNotNullOrEmpty(ruleName, nameof(ruleName));
            cancellationToken.ThrowIfCancellationRequested<TaskCanceledException>();
            ServiceBusEventSource.Log.DeleteRuleStart(Identifier, ruleName);

            using DiagnosticScope scope = _scopeFactory.CreateScope(
                DiagnosticProperty.DeleteRuleActivityName,
                DiagnosticScope.ActivityKind.Client);
            scope.Start();

            try
            {
                await InnerRuleManager.DeleteRuleAsync(
                    ruleName,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                ServiceBusEventSource.Log.DeleteRuleException(Identifier, exception.ToString(), ruleName);
                scope.Failed(exception);
                throw;
            }

            ServiceBusEventSource.Log.DeleteRuleComplete(Identifier, ruleName);
        }

        /// <summary>
        /// Iterates over the rules associated with the subscription.
        /// </summary>
        ///
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> instance to signal the request to cancel the operation.</param>
        /// <returns>Returns each rule on the associated subscription.</returns>
        public virtual async IAsyncEnumerable<RuleProperties> GetRulesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            Argument.AssertNotDisposed(IsClosed, nameof(ServiceBusRuleManager));
            cancellationToken.ThrowIfCancellationRequested<TaskCanceledException>();
            ServiceBusEventSource.Log.GetRulesStart(Identifier);
            int skip = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                List<RuleProperties> ruleProperties;
                using (DiagnosticScope scope = _scopeFactory.CreateScope(
                    DiagnosticProperty.GetRulesActivityName,
                    DiagnosticScope.ActivityKind.Client))
                {
                    scope.Start();
                    try
                    {
                        ruleProperties = await InnerRuleManager.GetRulesAsync(skip, MaxRulesPerRequest, cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception exception)
                    {
                        ServiceBusEventSource.Log.GetRulesException(Identifier, exception.ToString());
                        scope.Failed(exception);
                        throw;
                    }
                }

                skip += ruleProperties.Count;

                foreach (var rule in ruleProperties)
                {
                    yield return rule;
                }

                if (ruleProperties.Count < MaxRulesPerRequest)
                {
                    break;
                }
            }

            ServiceBusEventSource.Log.GetRulesComplete(Identifier);
        }

        /// <summary>
        /// Performs the task needed to clean up resources used by the <see cref="ServiceBusRuleManager" />.
        /// This is equivalent to calling <see cref="CloseAsync"/>.
        /// </summary>
        ///
        /// <returns>A task to be resolved on when the operation has completed.</returns>
        [SuppressMessage("Usage", "AZC0002:Ensure all service methods take an optional CancellationToken parameter.",
            Justification = "This signature must match the IAsyncDisposable interface.")]
        public virtual async ValueTask DisposeAsync()
        {
            await CloseAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs the task needed to clean up resources used by the <see cref="ServiceBusRuleManager" />.
        /// </summary>
        /// <param name="cancellationToken"> An optional<see cref="CancellationToken"/> instance to signal the
        /// request to cancel the operation.</param>
        /// <returns>A task to be resolved on when the operation has completed.</returns>
        public virtual async Task CloseAsync(CancellationToken cancellationToken = default)
        {
            IsClosed = true;

            ServiceBusEventSource.Log.ClientCloseStart(typeof(ServiceBusRuleManager), Identifier);
            try
            {
                await InnerRuleManager.CloseAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ServiceBusEventSource.Log.ClientCloseException(typeof(ServiceBusRuleManager), Identifier, ex);
                throw;
            }

            ServiceBusEventSource.Log.ClientCloseComplete(typeof(ServiceBusRuleManager), Identifier);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        ///
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        ///
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        ///
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Converts the instance to string representation.
        /// </summary>
        ///
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() => base.ToString();
    }
}