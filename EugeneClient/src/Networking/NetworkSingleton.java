package Networking;

import java.io.IOException;

public class NetworkSingleton extends Thread {
    private static final NetworkSingleton instance = new NetworkSingleton();
    private client c;
    private String ip;
    private int port;
    public Thread networkthread;

    private NetworkSingleton(){

    }
    public void connect(String a_ip, int a_port){
        this.ip = a_ip;
        this.port = a_port;
        c = new client(ip, port);
        networkthread = new Thread(c);
        networkthread.start();
    }
    public Thread getNetworkthread(){
        return networkthread;
    }
    public client getClient(){
        return c;
    }
    public void sendData(String data) throws IOException {
        c.sendData(data);
    }
    public static NetworkSingleton getInstance(){
        return instance;
    }
}
