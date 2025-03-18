// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.AI;

/// <summary>Represents a generator of embeddings.</summary>
/// <remarks>
/// This base interface is used to allow for embedding generators to be stored in a non-generic manner.
/// To use the generator to create embeddings, instances typed as this base interface first need to be
/// cast to the generic interface <see cref="IEmbeddingGenerator"/>.
/// </remarks>
public interface IEmbeddingGenerator : IDisposable
{
    /// <summary>Generates embeddings for each of the supplied <paramref name="values"/>.</summary>
    /// <param name="values">The sequence of values for which to generate embeddings.</param>
    /// <param name="embeddingType">The type of the embedding to be returned, e.g. <see cref="Embedding{Single}"/>.</param>
    /// <param name="options">The embedding generation options with which to configure the request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The generated embeddings.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
    Task<GeneratedEmbeddings<Embedding>> GenerateAsync(
        IEnumerable<object> values,
        Type embeddingType,
        EmbeddingGenerationOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>Asks the <see cref="IEmbeddingGenerator"/> for an object of the specified type <paramref name="serviceType"/>.</summary>
    /// <param name="serviceType">The type of object being requested.</param>
    /// <param name="serviceKey">An optional key that can be used to help identify the target service.</param>
    /// <returns>The found object, otherwise <see langword="null"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serviceType"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// The purpose of this method is to allow for the retrieval of strongly typed services that might be provided by the
    /// <see cref="IEmbeddingGenerator"/>, including itself or any services it might be wrapping.
    /// For example, to access the <see cref="EmbeddingGeneratorMetadata"/> for the instance, <see cref="GetService"/> may
    /// be used to request it.
    /// </remarks>
    object? GetService(Type serviceType, object? serviceKey = null);
}
