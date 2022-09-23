// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/*
 */

namespace System.Web.UI.WebControls;

/// <devdoc>
///    <para>
///        Used for table header cell scope attribute and property
///    </para>
/// </devdoc>
public enum TableHeaderScope
{

    /// <devdoc>
    ///    <para>
    ///       Property is not set.
    ///    </para>
    /// </devdoc>
    NotSet = 0,

    /// <devdoc>
    ///    <para>
    ///        Row scope   
    ///    </para>
    /// </devdoc>
    Row = 1,

    /// <devdoc>
    ///    <para>
    ///        Column scope
    ///    </para>
    /// </devdoc>
    Column = 2
}
