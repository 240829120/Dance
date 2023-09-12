using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 日志写入器
    /// </summary>
    public class DanceLogWritter : TextWriter
    {
        /// <summary>
        /// 日志写入器
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="writting">写入</param>
        public DanceLogWritter(Encoding encoding, Action<string>? writting)
        {
            this.Encoding = encoding;
            this.Writing = writting;
        }

        /// <summary>
        /// 编码
        /// </summary>
        public override Encoding Encoding { get; }

        /// <summary>
        /// 写入时触发
        /// </summary>
        public Action<string>? Writing;

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(ulong value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(uint value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(StringBuilder? value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="arg">格式化参数</param>
        public override void Write([StringSyntax("CompositeFormat")] string format, params object?[] arg)
        {
            Writing?.Invoke($"{string.Format(format, arg)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="arg0">格式化参数0</param>
        /// <param name="arg1">格式化参数1</param>
        /// <param name="arg2">格式化参数2</param>
        public override void Write([StringSyntax("CompositeFormat")] string format, object? arg0, object? arg1, object? arg2)
        {
            Writing?.Invoke($"{string.Format(format, arg0, arg1, arg2)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="arg0">格式化参数0</param>
        /// <param name="arg1">格式化参数1</param>
        public override void Write([StringSyntax("CompositeFormat")] string format, object? arg0, object? arg1)
        {
            Writing?.Invoke($"{string.Format(format, arg0, arg1)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="arg0">格式化参数0</param>
        public override void Write([StringSyntax("CompositeFormat")] string format, object? arg0)
        {
            Writing?.Invoke($"{string.Format(format, arg0)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(string? value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public override void Write(ReadOnlySpan<char> buffer)
        {
            Writing?.Invoke($"{new(buffer)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(object? value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(long value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(int value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(double value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(decimal value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="index">开始索引</param>
        /// <param name="count">长度</param>
        public override void Write(char[] buffer, int index, int count)
        {
            Writing?.Invoke($"{new(buffer, index, count)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public override void Write(char[]? buffer)
        {
            Writing?.Invoke($"{new(buffer)}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(char value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(bool value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void Write(float value)
        {
            Writing?.Invoke($"{value}");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="cancellationToken">任务取消标志</param>
        public override async Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Writing?.Invoke($"{value}"), cancellationToken);
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override async Task WriteAsync(string? value)
        {
            await Task.Run(() => Writing?.Invoke($"{value}"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="index">开始索引</param>
        /// <param name="count">长度</param>
        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            await Task.Run(() => Writing?.Invoke($"{new(buffer, index, count)}"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public new async Task WriteAsync(char[]? buffer)
        {
            await Task.Run(() => Writing?.Invoke($"{new(buffer)}"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override async Task WriteAsync(char value)
        {
            await Task.Run(() => Writing?.Invoke($"{value}"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="cancellationToken">取消标志</param>
        public override async Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Writing?.Invoke($"{buffer}"), cancellationToken);
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(ulong value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(uint value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(StringBuilder? value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg">格式化参数</param>
        public override void WriteLine([StringSyntax("CompositeFormat")] string format, params object?[] arg)
        {
            Writing?.Invoke($"{string.Format(format, arg)}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">格式化参数0</param>
        /// <param name="arg1">格式化参数1</param>
        /// <param name="arg2">格式化参数2</param>
        public override void WriteLine([StringSyntax("CompositeFormat")] string format, object? arg0, object? arg1, object? arg2)
        {
            Writing?.Invoke($"{string.Format(format, arg0, arg1, arg2)}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">格式化参数0</param>
        public override void WriteLine([StringSyntax("CompositeFormat")] string format, object? arg0)
        {
            Writing?.Invoke($"{string.Format(format, arg0)}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(string? value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(float value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">格式化参数0</param>
        /// <param name="arg1">格式化参数1</param>
        public override void WriteLine([StringSyntax("CompositeFormat")] string format, object? arg0, object? arg1)
        {
            Writing?.Invoke($"{string.Format(format, arg0, arg1)} \r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(object? value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            Writing?.Invoke($"{buffer}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(bool value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(char value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public override void WriteLine(char[]? buffer)
        {
            Writing?.Invoke($"{new(buffer)}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void WriteLine()
        {
            Writing?.Invoke($"\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(decimal value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(double value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(int value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override void WriteLine(long value)
        {
            Writing?.Invoke($"{value}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="index">开始索引</param>
        /// <param name="count">结束索引</param>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            Writing?.Invoke($"{new(buffer, index, count)}\r\n");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        public override async Task WriteLineAsync(char value)
        {
            await Task.Run(() => Writing?.Invoke($"{value}\r\n"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public new async Task WriteLineAsync(char[]? buffer)
        {
            await Task.Run(() => Writing?.Invoke($"{new(buffer)}\r\n"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="index">开始索引</param>
        /// <param name="count">结束索引</param>
        public override async Task WriteLineAsync(char[] buffer, int index, int count)
        {
            await Task.Run(() => Writing?.Invoke($"{new(buffer, index, count)}\r\n"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        /// <param name="cancellationToken">取消标志</param>
        public override async Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Writing?.Invoke($"{buffer}\r\n"), cancellationToken);
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">值</param>
        public override async Task WriteLineAsync(string? value)
        {
            await Task.Run(() => Writing?.Invoke($"{value}\r\n"));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="cancellationToken">取消标志</param>
        public override async Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Writing?.Invoke($"{value}\r\n"), cancellationToken);
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override async Task WriteLineAsync()
        {
            await Task.Run(() => Writing?.Invoke($"\r\n"));
        }
    }
}
