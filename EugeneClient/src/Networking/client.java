package Networking;

import com.sun.xml.internal.bind.v2.runtime.reflect.Lister;
import sceneswitcher.IEventPane;

import java.io.*;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;

/**
 * This program is a socket client application that connects to a time server
 * to get the current date time.
 *
 * @author www.codejava.net
 */
public class client extends Thread {
    private String hostname;
    private int port;
    private DataInputStream dis;
    private DataOutputStream dos;
    private List<Packet> packetBuffer;

    public INetworking eventHandeler;

    public INetworking getEventHandeler() {
        return eventHandeler;
    }

    public client(String a_hostname, int a_port){
        packetBuffer = new ArrayList<>();

        this.hostname = a_hostname;
        this.port = a_port;
    }

    public void start() {
    }
    public void sendData(Packet packet) throws IOException {
        if (dos != null){
            dos.write(packet.getData());
        }

    }

    public void run() {
        System.out.println("thread running");

        try (Socket socket = new Socket(hostname, port)) {

            InputStream input = socket.getInputStream();
            dis = new DataInputStream(input);
            OutputStream output = socket.getOutputStream();
            dos = new DataOutputStream(output);
            byte[] data;
            byte[] reading;
            while (true) {

                data = new byte[32];
                for(int i = 0; i < data.length; i++)
                    data[i] = dis.readByte();
                eventHandeler.OnRecievePacket( new Packet(data));
            }
        } catch (UnknownHostException ex) {

            System.out.println("Server not found: " + ex.getMessage());

        } catch (IOException ex) {

            System.out.println("I/O error: " + ex.getMessage());
        }
    }
}
