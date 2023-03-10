//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InterceptsLocationAttribute : Attribute
    {
        public InterceptsLocationAttribute(string filePath, int line, int column)
        {
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;

    using MetadataPopulator = global::System.Func<System.Reflection.MethodInfo, Microsoft.AspNetCore.Http.RequestDelegateFactoryOptions?, Microsoft.AspNetCore.Http.RequestDelegateMetadataResult>;
    using RequestDelegateFactoryFunc = global::System.Func<System.Delegate, Microsoft.AspNetCore.Http.RequestDelegateFactoryOptions, Microsoft.AspNetCore.Http.RequestDelegateMetadataResult?, Microsoft.AspNetCore.Http.RequestDelegateResult>;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.Http.Generators, Version=8.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60", "8.0.0.0")]
    internal class SourceKey
    {
        public string Path { get; init; }
        public int Line { get; init; }

        public SourceKey(string path, int line)
        {
            Path = path;
            Line = line;
        }
    }

    // This class needs to be internal so that the compiled application
    // has access to the strongly-typed endpoint definitions that are
    // generated by the compiler so that they will be favored by
    // overload resolution and opt the runtime in to the code generated
    // implementation produced here.
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.Http.Generators, Version=8.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60", "8.0.0.0")]
    internal static class GenerateRouteBuilderEndpoints
    {
        private static readonly string[] GetVerb = new[] { global::Microsoft.AspNetCore.Http.HttpMethods.Get };
        private static readonly string[] PostVerb = new[] { global::Microsoft.AspNetCore.Http.HttpMethods.Post };
        private static readonly string[] PutVerb = new[]  { global::Microsoft.AspNetCore.Http.HttpMethods.Put };
        private static readonly string[] DeleteVerb = new[] { global::Microsoft.AspNetCore.Http.HttpMethods.Delete };
        private static readonly string[] PatchVerb = new[] { global::Microsoft.AspNetCore.Http.HttpMethods.Patch };

        // File-path, line number, column
        [global::System.Runtime.CompilerServices.InterceptsLocation(@"/Users/captainsafia/tests/TestInterceptors/Program.cs", 3, 4)]
        internal static global::Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapGet1(
            this global::Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints,
            [global::System.Diagnostics.CodeAnalysis.StringSyntax("Route")] string pattern,
            global::System.Delegate handler)
        {
            Console.WriteLine("Hit the interceptor method!");
            MetadataPopulator metadataPopulator = (methodInfo, options) =>
            {
                if (options == null || options.EndpointBuilder == null)
                {
                    return new RequestDelegateMetadataResult { EndpointMetadata = global::System.Collections.ObjectModel.ReadOnlyCollection<object>.Empty };
                }
                options.EndpointBuilder.Metadata.Add(new SourceKey(@"/Users/captainsafia/tests/TestInterceptors/Program.cs", 4));
                return new RequestDelegateMetadataResult { EndpointMetadata = options.EndpointBuilder.Metadata.AsReadOnly() };
            };
            RequestDelegateFactoryFunc requestDelegateFactory = (del, options, inferredMetadataResult) =>
            {
                var handler = (System.Func<string>)del;
                EndpointFilterDelegate? filteredInvocation = null;

                if (options?.EndpointBuilder?.FilterFactories.Count > 0)
                {
                    filteredInvocation = BuildFilterDelegate(ic =>
                    {
                        if (ic.HttpContext.Response.StatusCode == 400)
                        {
                            return ValueTask.FromResult<object?>(Results.Empty);
                        }
                        return ValueTask.FromResult<object?>(handler());
                    },
                    options.EndpointBuilder,
                    handler.Method);
                }

                Task RequestHandler(HttpContext httpContext)
                {
                        var result = handler();
                        return httpContext.Response.WriteAsync(result);
                }
                async Task RequestHandlerFiltered(HttpContext httpContext)
                {
                    var result = await filteredInvocation(new DefaultEndpointFilterInvocationContext(httpContext));
                    await ExecuteObjectResult(result, httpContext);
                }

                RequestDelegate targetDelegate = filteredInvocation is null ? RequestHandler : RequestHandlerFiltered;
                var metadata = inferredMetadataResult?.EndpointMetadata ?? global::System.Collections.ObjectModel.ReadOnlyCollection<object>.Empty;
                return new RequestDelegateResult(targetDelegate, metadata);
            };
            return global::Microsoft.AspNetCore.Routing.RouteHandlerServices.Map(endpoints, pattern, handler, new[] { "GET" }, metadataPopulator, requestDelegateFactory);
            
        }

        internal static EndpointFilterDelegate BuildFilterDelegate(EndpointFilterDelegate filteredInvocation, EndpointBuilder builder, System.Reflection.MethodInfo mi)
        {
            var routeHandlerFilters =  builder.FilterFactories;
            var context0 = new EndpointFilterFactoryContext
            {
                MethodInfo = mi,
                ApplicationServices = builder.ApplicationServices,
            };
            var initialFilteredInvocation = filteredInvocation;
            for (var i = routeHandlerFilters.Count - 1; i >= 0; i--)
            {
                var filterFactory = routeHandlerFilters[i];
                filteredInvocation = filterFactory(context0, filteredInvocation);
            }
            return filteredInvocation;
        }

        internal static Task ExecuteObjectResult(object? obj, HttpContext httpContext)
        {
            if (obj is IResult r)
            {
                return r.ExecuteAsync(httpContext);
            }
            else if (obj is string s)
            {
                return httpContext.Response.WriteAsync(s);
            }
            else
            {
                return httpContext.Response.WriteAsJsonAsync(obj);
            }
        }

    }
}
