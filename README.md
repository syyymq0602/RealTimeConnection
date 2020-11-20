# 实时通讯技术

### 三种实时通讯技术总结：
*****
1. Socket通讯 ：
    - 在Server端要定义套接字，定义要侦听的端口（new Socket（））
    - 在server端要设置最高监听端口数（Listen）
    - server端监听端口数多于1个时最好开启线程，提高CPU的效率（Thread）
    - Socket的通讯类型只能是 byte[] 数组类型，需要用函数处理类型的转换（Encoding）
    - 在Client端根据Server端的套接字定义一样的接口（new Socket）
    - 连接后用永真循环进行一直监听（while（true））
*****
2. SignalR
    - SignalR采用Hub中心传递数据信息
    - Server端主要采用了Hub强类型，可以进行对组、指定用户以及所有用户的广播，可以将连接用户加入组或者移除组。
    - ClientOther端连接Server端后**需要循环**保持监听信息的发送，否则ClientOther不能调用HubConnection.On方法
    - Client端主要采用延时1s永真循环保持信息一直发送，在信息发送过程中可直接传输对话或者序列化。
***** 
3. Grpc
    - Grpc采用自定义的protobuf。
    - 适合轻量级高效的传输。
    - 需要在launchSettings文件中配置接口
    - 可以在传输的对象中加入Metadata类型数据
    - 运用了Asp.net core的依赖注入
    - 在protobuf中需要提前定义传输对象，以及有些类型需要从google.protobuf库导入。
*****

