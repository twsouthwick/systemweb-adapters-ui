// MIT License.

using System.CodeDom;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>
/// This is an abstract class whose implementation can be used to control or customize the compilation process.
/// A type that extends this class can be registered in web.config using <see cref="System.Web.Configuration.CompilationSection.ControlBuilderInterceptorType"/> property
/// and the compilation sytem calls into it's methods.
/// </summary>
public abstract class ControlBuilderInterceptor
{
    /// <summary>
    /// This methood is called before a <see cref="System.Web.UI.ControlBuilder"/> for an element in the markup is initialized.
    /// </summary>
    /// <param name="controlBuilder">The control builder which is about to be initialized.</param>
    /// <param name="parser">The <see cref="System.Web.UI.TemplateParser"/> which was used to parse the markup.</param>
    /// <param name="parentBuilder">The parent control builder (typically the builder corresponding to the parent element in the markup).</param>
    /// <param name="type">The type of the control that this builder will create.</param>
    /// <param name="tagName">The name of the tag to be built.</param>
    /// <param name="id">ID of the element in the markup.</param>
    /// <param name="attributes">List of attributes of the element in the markup.</param>
    /// <param name="additionalState">This is an additional state which can be used to store/retrive data within several methods of <see cref="System.Web.Compilation.ControlBuilderInterceptor"/>.
    /// The state is per control builder.</param>
    public virtual void PreControlBuilderInit(ControlBuilder controlBuilder,
                                              TemplateParser parser,
                                              ControlBuilder parentBuilder,
                                              Type type,
                                              string tagName,
                                              string id,
                                              IDictionary attributes,
                                              IDictionary additionalState)
    {
    }

    /// <summary>
    /// This method is called after the code generation for this <see cref="System.Web.UI.ControlBuilder"/> is complete.
    /// </summary>
    /// <param name="controlBuilder">The control builder instance.</param>
    /// <param name="codeCompileUnit">This is the entire <see cref="System.CodeDom.CodeCompileUnit"/> that is being generated by the compilation.</param>
    /// <param name="baseType">The type declaration of code behind class. When code behind is not used, this is the same as <paramref name="derivedType"/>.</param>
    /// <param name="derivedType">The type declaration of top level markup element.</param>
    /// <param name="buildMethod">The method with necessary code to create the control and set it's various properties, events, fields.</param>
    /// <param name="dataBindingMethod">The method with code to evaluate data binding expressions within the control.</param>
    /// <param name="additionalState">This is an additional state which can be used to store/retrive data within several methods of <see cref="System.Web.Compilation.ControlBuilderInterceptor"/>.
    /// The state is per control builder.</param>
    public virtual void OnProcessGeneratedCode(ControlBuilder controlBuilder,
                                               CodeCompileUnit codeCompileUnit,
                                               CodeTypeDeclaration baseType,
                                               CodeTypeDeclaration derivedType,
                                               CodeMemberMethod buildMethod,
                                               CodeMemberMethod dataBindingMethod,
                                               IDictionary additionalState)
    {
    }
}
