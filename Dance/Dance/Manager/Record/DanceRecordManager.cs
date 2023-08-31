using CsvHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 操作记录管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceRecordManager))]
    public class DanceRecordManager : DanceObject, IDanceRecordManager
    {
        /// <summary>
        /// 总操作记录ID
        /// </summary>
        private static long TOTAL_RECORD_ID;

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lock_object = new();

        /// <summary>
        /// 记录队列
        /// </summary>
        private readonly ConcurrentQueue<DanceRecordInfo> Queue = new();

        /// <summary>
        /// CSV文件写入器
        /// </summary>
        private CsvWriter? CsvWriter;

        /// <summary>
        /// 当前文件记录数
        /// </summary>
        private int CurrentFileCount;

        /// <summary>
        /// 单文件记录最大数量
        /// </summary>
        public int OneFileRecordMaxCount { get; set; } = 1000;

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="content">内容</param>
        public void Log(string content)
        {
            Queue.Enqueue(new(TOTAL_RECORD_ID++, DateTime.Now, content));
        }

        /// <summary>
        /// 将操作日志输出到文件
        /// </summary>
        public void Flush()
        {
            try
            {
                lock (this.lock_object)
                {
                    while (this.Queue.TryDequeue(out DanceRecordInfo? info))
                    {
                        if (this.CsvWriter == null || this.CurrentFileCount >= this.OneFileRecordMaxCount)
                        {
                            this.CsvWriter?.Flush();
                            this.CsvWriter?.Dispose();

                            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordLog");
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }

                            string path = Path.Combine(dir, $"{DateTime.Now.ToString("yyyy_MM_dd__HH_mm_ss__fffffff")}.csv");

                            this.CsvWriter = new CsvWriter(new StreamWriter(path), System.Globalization.CultureInfo.CurrentCulture);
                            this.CsvWriter.WriteHeader<DanceRecordInfo>();
                            this.CsvWriter.NextRecord();
                            this.CurrentFileCount = 0;
                        }

                        this.CsvWriter.WriteRecord(info);
                        this.CsvWriter.NextRecord();
                        ++this.CurrentFileCount;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Flush();
        }
    }
}