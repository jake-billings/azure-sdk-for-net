// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.MediaComposition.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Communication.MediaComposition
{
    internal partial class MediaCompositionRestClient
    {
        private readonly HttpPipeline _pipeline;
        private readonly Uri _endpoint;
        private readonly string _apiVersion;

        /// <summary> The ClientDiagnostics is used to provide tracing support for the client library. </summary>
        internal ClientDiagnostics ClientDiagnostics { get; }

        /// <summary> Initializes a new instance of MediaCompositionRestClient. </summary>
        /// <param name="clientDiagnostics"> The handler for diagnostic messaging in the client. </param>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="endpoint"> server parameter. </param>
        /// <param name="apiVersion"> Api Version. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="clientDiagnostics"/>, <paramref name="pipeline"/> or <paramref name="apiVersion"/> is null. </exception>
        public MediaCompositionRestClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Uri endpoint = null, string apiVersion = "2022-07-16-preview1")
        {
            ClientDiagnostics = clientDiagnostics ?? throw new ArgumentNullException(nameof(clientDiagnostics));
            _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
            _endpoint = endpoint ?? new Uri("");
            _apiVersion = apiVersion ?? throw new ArgumentNullException(nameof(apiVersion));
        }

        internal HttpMessage CreateGetRequest(string mediaCompositionId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            return message;
        }

        /// <summary> Gets a media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response<MediaCompositionBody>> GetAsync(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateGetRequest(mediaCompositionId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Gets a media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response<MediaCompositionBody> Get(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateGetRequest(mediaCompositionId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateCreateRequest(string mediaCompositionId, string id, MediaCompositionLayout layout, IDictionary<string, MediaInput> inputs, IDictionary<string, MediaOutput> outputs, CompositionStreamState? streamState)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Content-Type", "application/json");
            MediaCompositionBody mediaCompositionBody = new MediaCompositionBody()
            {
                Id = id,
                Layout = layout,
                StreamState = streamState
            };
            if (inputs != null)
            {
                foreach (var value in inputs)
                {
                    mediaCompositionBody.Inputs.Add(value);
                }
            }
            if (outputs != null)
            {
                foreach (var value in outputs)
                {
                    mediaCompositionBody.Outputs.Add(value);
                }
            }
            var model = mediaCompositionBody;
            var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteObjectValue(model);
            request.Content = content;
            return message;
        }

        /// <summary> Creates a new media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="id"> Id of the media composition. </param>
        /// <param name="layout"> Configure a layout. </param>
        /// <param name="inputs"> Inputs used in the composition. </param>
        /// <param name="outputs"> Outputs used in the composition. </param>
        /// <param name="streamState"> State of the composition stream. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response<MediaCompositionBody>> CreateAsync(string mediaCompositionId, string id = null, MediaCompositionLayout layout = null, IDictionary<string, MediaInput> inputs = null, IDictionary<string, MediaOutput> outputs = null, CompositionStreamState? streamState = null, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateCreateRequest(mediaCompositionId, id, layout, inputs, outputs, streamState);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Creates a new media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="id"> Id of the media composition. </param>
        /// <param name="layout"> Configure a layout. </param>
        /// <param name="inputs"> Inputs used in the composition. </param>
        /// <param name="outputs"> Outputs used in the composition. </param>
        /// <param name="streamState"> State of the composition stream. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response<MediaCompositionBody> Create(string mediaCompositionId, string id = null, MediaCompositionLayout layout = null, IDictionary<string, MediaInput> inputs = null, IDictionary<string, MediaOutput> outputs = null, CompositionStreamState? streamState = null, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateCreateRequest(mediaCompositionId, id, layout, inputs, outputs, streamState);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateUpdateRequest(string mediaCompositionId, string id, MediaCompositionLayout layout, IDictionary<string, MediaInput> inputs, IDictionary<string, MediaOutput> outputs, CompositionStreamState? streamState)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Patch;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Content-Type", "application/json");
            MediaCompositionBody mediaCompositionBody = new MediaCompositionBody()
            {
                Id = id,
                Layout = layout,
                StreamState = streamState
            };
            if (inputs != null)
            {
                foreach (var value in inputs)
                {
                    mediaCompositionBody.Inputs.Add(value);
                }
            }
            if (outputs != null)
            {
                foreach (var value in outputs)
                {
                    mediaCompositionBody.Outputs.Add(value);
                }
            }
            var model = mediaCompositionBody;
            var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteObjectValue(model);
            request.Content = content;
            return message;
        }

        /// <summary> Updates an existing media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="id"> Id of the media composition. </param>
        /// <param name="layout"> Configure a layout. </param>
        /// <param name="inputs"> Inputs used in the composition. </param>
        /// <param name="outputs"> Outputs used in the composition. </param>
        /// <param name="streamState"> State of the composition stream. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response<MediaCompositionBody>> UpdateAsync(string mediaCompositionId, string id = null, MediaCompositionLayout layout = null, IDictionary<string, MediaInput> inputs = null, IDictionary<string, MediaOutput> outputs = null, CompositionStreamState? streamState = null, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateUpdateRequest(mediaCompositionId, id, layout, inputs, outputs, streamState);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Updates an existing media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="id"> Id of the media composition. </param>
        /// <param name="layout"> Configure a layout. </param>
        /// <param name="inputs"> Inputs used in the composition. </param>
        /// <param name="outputs"> Outputs used in the composition. </param>
        /// <param name="streamState"> State of the composition stream. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response<MediaCompositionBody> Update(string mediaCompositionId, string id = null, MediaCompositionLayout layout = null, IDictionary<string, MediaInput> inputs = null, IDictionary<string, MediaOutput> outputs = null, CompositionStreamState? streamState = null, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateUpdateRequest(mediaCompositionId, id, layout, inputs, outputs, streamState);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        MediaCompositionBody value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = MediaCompositionBody.DeserializeMediaCompositionBody(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateDeleteRequest(string mediaCompositionId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Delete;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            return message;
        }

        /// <summary> Deletes a media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response> DeleteAsync(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateDeleteRequest(mediaCompositionId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 204:
                    return message.Response;
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Deletes a media composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response Delete(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateDeleteRequest(mediaCompositionId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 204:
                    return message.Response;
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateStartRequest(string mediaCompositionId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendPath("/:start", false);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            return message;
        }

        /// <summary> Starts the composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response<CompositionStreamState>> StartAsync(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateStartRequest(mediaCompositionId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        CompositionStreamState value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = new CompositionStreamState(document.RootElement.GetString());
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Starts the composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response<CompositionStreamState> Start(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateStartRequest(mediaCompositionId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        CompositionStreamState value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = new CompositionStreamState(document.RootElement.GetString());
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateStopRequest(string mediaCompositionId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/mediaCompositions/", false);
            uri.AppendPath(mediaCompositionId, true);
            uri.AppendPath("/:stop", false);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            return message;
        }

        /// <summary> Stops the composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public async Task<Response<CompositionStreamState>> StopAsync(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateStopRequest(mediaCompositionId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        CompositionStreamState value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = new CompositionStreamState(document.RootElement.GetString());
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await ClientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Stops the composition. </summary>
        /// <param name="mediaCompositionId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="mediaCompositionId"/> is null. </exception>
        public Response<CompositionStreamState> Stop(string mediaCompositionId, CancellationToken cancellationToken = default)
        {
            if (mediaCompositionId == null)
            {
                throw new ArgumentNullException(nameof(mediaCompositionId));
            }

            using var message = CreateStopRequest(mediaCompositionId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        CompositionStreamState value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = new CompositionStreamState(document.RootElement.GetString());
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw ClientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }
    }
}
