using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 历史记录模型
    /// </summary>
    /// <param name="historyManager">历史管理器</param>
    public class DanceHistoryModel(IDanceHistoryManager historyManager) : DanceModel
    {
        /// <summary>
        /// 缓存池
        /// </summary>
        protected readonly Dictionary<string, object?> CachePool = [];

        /// <summary>
        /// 历史管理器
        /// </summary>
        public IDanceHistoryManager HistoryManager { get; } = historyManager;

        /// <summary>
        /// 属性改变
        /// </summary>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        /// <param name="propertyName">属性名</param>
        /// <typeparam name="T">属性类型</typeparam>
        protected void OnHistoryPropertyChanged<T>(T? oldValue, T? newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!this.HistoryManager.IsExecuting && !string.IsNullOrWhiteSpace(propertyName))
            {
                this.HistoryManager.Append(new DancePropertyChangedHistoryStep(this, propertyName, oldValue, newValue));
            }

            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 属性改变
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="newValue">新值</param>
        /// <param name="propertyName">属性名</param>
        /// <typeparam name="T">属性类型</typeparam>
        protected void OnHistoryPropertyChanged<T>(ref T? field, T? newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!this.HistoryManager.IsExecuting && !string.IsNullOrWhiteSpace(propertyName))
            {
                this.HistoryManager.Append(new DancePropertyChangedHistoryStep(this, propertyName, field, newValue));
            }

            field = newValue;

            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Cache<T>(string key, T? value)
        {
            this.CachePool[key] = value;
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存对象</returns>
        public T? GetCache<T>(string key)
        {
            this.CachePool.TryGetValue(key, out object? value);

            return value == null ? default : (T)value;
        }
    }
}