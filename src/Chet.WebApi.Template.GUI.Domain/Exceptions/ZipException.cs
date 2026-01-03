using System;
using System.Runtime.Serialization;

namespace Chet.WebApi.Template.GUI.Domain.Exceptions
{
    /// <summary>
    /// ZIP异常
    /// <para>用于表示ZIP文件解压过程中发生的错误</para>
    /// </summary>
    public class ZipException : Exception
    {
        /// <summary>
        /// 初始化<see cref="ZipException"/>类的新实例
        /// </summary>
        public ZipException()
        {
        }

        /// <summary>
        /// 使用指定的错误消息初始化<see cref="ZipException"/>类的新实例
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public ZipException(string message) : base(message)
        {
        }

        /// <summary>
        /// 使用指定的错误消息和对作为此异常原因的内部异常的引用来初始化<see cref="ZipException"/>类的新实例
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public ZipException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 用序列化数据初始化<see cref="ZipException"/>类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象</param>
        /// <param name="context">有关源或目标的上下文信息</param>
        protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
