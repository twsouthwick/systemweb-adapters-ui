// MIT License.

using System.Web.UI.WebControls;

/*
 * Helper class for performing common enumeration range checks.
 * 
 * Copyright (c) 2009 Microsoft Corporation
 */

namespace System.Web.Util;
internal static class EnumerationRangeValidationUtil
{

    public static void ValidateRepeatLayout(RepeatLayout value)
    {
        if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}
