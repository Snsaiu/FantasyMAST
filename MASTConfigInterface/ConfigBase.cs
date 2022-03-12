namespace MASTConfig
{
    public abstract class ConfigBase
    {


        /// <summary>
        /// 初始化一个配置文件，如果首次注册，需要使用此构造函数进行用户配置文件的生成
        /// </summary>
        /// <param name="userName"></param>
        public ConfigBase(string userName)
        {
            
        }

        /// <summary>
        /// 初始化一个配置文件，该构造函数为空，代表不会生成新的配置文件
        /// </summary>
        public ConfigBase()
        {
            
        }
        /// <summary>
        /// 存储路径
        /// </summary>
        public string? StorePath { get;  set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get;  set; }

        /// <summary>
        /// 当前设备信息
        /// </summary>
        public List<DeviceInfo>? Devices { get;  set; }

        /// <summary>
        /// 当前设备信息
        /// </summary>
        public DeviceInfo? CurrentDeviceInfo { get;  set; }

        /// <summary>
        /// 组地址
        /// </summary>
     public string? GroupAddress { get; set; }

        /// <summary>
        /// 接收端口
        /// </summary>
        public string? ReceivePort { get; set; }

        /// <summary>
        /// 发送端口
        /// </summary>
        public string? SendPort { get; set; }

        /// <summary>
        /// 设备发现端口
        /// </summary>
        public string? DiscoverPort { get; set; }

        /// <summary>
        /// 当用户在本设备清除的时候调用此方法将配置文件全部清除
        /// </summary>
        /// <returns>清除成功返回true，否则返回false</returns>
        public abstract bool Unregist();

        /// <summary>
        /// 更新当前设备信息
        /// </summary>
        /// <param name="devices"></param>
        /// <returns>修改成功返回true，否则返回false</returns>
        public abstract bool UpdateDevices(List<DeviceInfo> devices);

        /// <summary>
        /// 更新文件的存储路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>修改成功返回true，否则返回false</returns>
        public abstract bool UpdateStorePath(string? path);

        /// <summary>
        /// 更新发送端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public abstract bool UpdateSendPort(string? port);

        /// <summary>
        /// 更新接收端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public abstract bool UpdateReceivePort(string? port);

        /// <summary>
        /// 更新组端口
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public abstract bool UpdateGroupAddress(string? address);

        /// <summary>
        /// 更新设备发现端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public abstract bool UpdateDiscoverPort(string? port);

    }
}