﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 属性改变步骤
    /// </summary>
    public class DancePropertyChangedHistoryStep : DanceObject, IDanceHistoryStep
    {
        /// <summary>
        /// 属性改变步骤
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="property">属性</param>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        /// <param name="detail">描述</param>
        public DancePropertyChangedHistoryStep(object target, string property, object? oldValue, object? newValue, string detail = "属性改变")
        {
            if (target.GetType().GetProperty(property) is not PropertyInfo propertyInfo)
                throw new ArgumentException(property);

            this.Target = target;
            this.Property = propertyInfo;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Detail = detail;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; } = "属性改变";

        /// <summary>
        /// 目标
        /// </summary>
        public object Target { get; }

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// 原始值
        /// </summary>
        public object? OldValue { get; }

        /// <summary>
        /// 新值
        /// </summary>
        public object? NewValue { get; }

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Redo(IDanceHistoryManager manager)
        {
            this.Property.SetValue(this.Target, this.NewValue);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="manager">历史管理器</param>
        public void Undo(IDanceHistoryManager manager)
        {
            this.Property.SetValue(this.Target, this.OldValue);
        }
    }
}
