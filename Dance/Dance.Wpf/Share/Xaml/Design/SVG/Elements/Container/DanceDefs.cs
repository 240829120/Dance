﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// defs 元素
    /// </summary>
    public class DanceDefs : DanceSVGElement
    {
        /// <summary>
        /// defs 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public DanceDefs(DanceSVG? svg, DanceSVGElement? parent, XElement? element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override DanceSVGElement GetCloneObject()
        {
            return new DanceDefs(this.SVG, this.Parent, this.Element);
        }
    }
}
