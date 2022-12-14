// MIT License.

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.SystemWebAdapters.Compiler;
using Microsoft.AspNetCore.SystemWebAdapters.Compiler.Symbols;
using Microsoft.CodeAnalysis;

namespace Microsoft.AspNetCore.SystemWebAdapters.UI.Generator;

[Generator]
public class PageGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var emptyLookup = new SourceGeneratorLookup();
        var aspxFiles = context.AdditionalTextsProvider
            .Where(text => text.Path.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase));
        var symbols = context.CompilationProvider
            .Select((s, ctx) =>
            {
                var visitor = new ControlInfoVisitor(s);

                if (!visitor.IsValid)
                {
                    return emptyLookup;
                }

                visitor.Visit(s.Assembly);

                return visitor.Lookup;
            });

        context.RegisterSourceOutput(aspxFiles.Combine(symbols), (context, arg) => GenerateSource(context, arg.Left, arg.Right));
    }

    private void GenerateSource(SourceProductionContext context, AdditionalText text, IControlLookup controls)
    {
        // TODO: maybe want to use CopyTo to load into a pooled buffer instead of getting a string each time
        var contents = text.GetText(context.CancellationToken)?.ToString();

        if (contents is null)
        {
            return;
        }

        var generated = GenerateFile(text, contents, controls);

        if (generated is null)
        {
            return;
        }

        var path = text.Path.TrimStart('/') + ".g.cs";

        context.AddSource(path, generated);
    }

    private string? GenerateFile(AdditionalText text, string contents, IControlLookup controls)
    {
        using var stringWriter = new StringWriter();
        using var writer = new IndentedTextWriter(stringWriter);

        var details = AspNetCompiler.ParsePage(text.Path, contents, controls);
        var builder = new CSharpPageWriter(writer, details);

        builder.Write();

        return stringWriter.ToString();
    }
}
