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

