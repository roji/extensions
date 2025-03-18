// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Microsoft.Shared.Diagnostics;

#pragma warning disable

namespace Microsoft.Extensions.AI;

public class SampleEmbeddingGenerator : IEmbeddingGenerator
{
    public async Task<GeneratedEmbeddings<Embedding>> GenerateAsync(
        IEnumerable<object> values,
        Type embeddingType,
        EmbeddingGenerationOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        _ = Throw.IfNull(values);
        _ = Throw.IfNull(embeddingType);

        // Handle inputs
        Type? inputType = null;
        foreach (var value in values)
        {
            if (inputType is null)
            {
                inputType = value.GetType();
                if (inputType != typeof(string))
                {
                    throw new ArgumentException("All input values must be of type 'string'.", nameof(values));
                }

                // Serialize the strings into JSON, or whatever is appropriate for the service...
            }
            else if (value.GetType() != inputType)
            {
                throw new ArgumentException("All input values must be of type 'string'.", nameof(values));
            }
        }

        // Handle outputs
        if (embeddingType == typeof(Embedding<float>))
        {
            // Perform service call for string->float embeddings, return the results
            return new GeneratedEmbeddings<Embedding>(/* ... */);
        }
        // Handle other embedding types...
        else
        {
            throw new NotSupportedException($"Embedding type '{embeddingType}' is not supported.");
        }
    }

    public object? GetService(Type serviceType, object? serviceKey = null) => null;

    public void Dispose()
    {
    }
}
