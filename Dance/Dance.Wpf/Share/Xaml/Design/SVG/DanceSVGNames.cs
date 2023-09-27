using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// 名称集合
    /// </summary>
    public static class DanceSVGNames
    {
        // =============================================================================================
        // ================== Element ==================

        public static XName Use { get; } = XName.Get("use");

        // =============================================================================================
        // ================== Attribute ==================

        public static XName Id { get; } = XName.Get("id");

        public static XName Href { get; } = XName.Get("href", DanceSVGNamespaces.xlink);

        public static XName Style { get; } = XName.Get("style");
    }
}
