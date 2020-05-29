package Networking;

import java.io.IOException;

public class NetworkSingleton extends Thread {
    private static final NetworkSingleton instance = new NetworkSingleton();
    private client c;
    private String ip;
    private int port;
    private Thread networkthread;

    private NetworkSingleton(){
        System.out.println("Init");
    }
    public void connect(String a_ip, int a_port){
        this.ip = a_ip;
        this.port = a_port;
        c = new client(ip, port );
        networkthread = new Thread(c);
        networkthread.start();
    }

    public client getC() {
        return c;
    }

    public void sendData(Packet packet) throws IOException {
        c.sendData(packet);
    }
    public static NetworkSingleton getInstance(){
        return instance;
    }
}
