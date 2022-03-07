namespace FantasyResultModel
{
    public abstract class ResultBase<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; protected set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Ok { get;protected set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMsg { get; protected set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int?  Code { get;protected set; }
    }
}