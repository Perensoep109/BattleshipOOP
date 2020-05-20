package Networking;

import java.io.IOException;

public class NetworkSingleton extends Thread {
    private static final NetworkSingleton instance = new NetworkSingleton();
    private client c;
    private String ip;
    private int port;
    private Thread networkthread;
    private INetworking eventHandeler;

    public INetworking getEventHandeler() {
        return eventHandeler;
    }

    private NetworkSingleton(){

    }
    public void connect(String a_ip, int a_port){
        eventHandeler = new INetworking() {
            @Override
            public void OnRecievePacket(String packet) {

            }
        };
        this.ip = a_ip;
        this.port = a_port;
        c = new client(ip, port, this.eventHandeler );
        networkthread = new Thread(c);
        networkthread.start();
    }
    public Thread getNetworkthread(){
        return networkthread;
    }
    public client getClient(){
        return c;
    }
    public void sendData(Packet packet) throws IOException {
        c.sendData(packet);
    }
    public static NetworkSingleton getInstance(){
        return instance;
    }
}
