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
    private InputStreamReader isr;
    private DataOutputStream dos;
    private List<Packet> packetBuffer;

    private INetworking eventHandeler;

    public client(String a_hostname, int a_port, INetworking eventHandeler){
        this.eventHandeler = eventHandeler;
        packetBuffer = new ArrayList<>();

        this.hostname = a_hostname;
        this.port = a_port;
    }

    public void start(){
        System.out.println("Starting" );



    }
    public void sendData(Packet packet) throws IOException {
        if (dos != null){
            dos.writeInt(packet.getData().length);
            dos.write(packet.getData());
        }

    }




    public void run() {
        System.out.println("thread running");

        try (Socket socket = new Socket(hostname, port)) {

            InputStream input = socket.getInputStream();
            isr = new InputStreamReader(input);
            OutputStream output = socket.getOutputStream();
            dos = new DataOutputStream(output);

            while (true) {
                this.eventHandeler.OnRecievePacket("yeet");
                System.out.println(isr.read());
            }
        } catch (UnknownHostException ex) {

            System.out.println("Server not found: " + ex.getMessage());

        } catch (IOException ex) {

            System.out.println("I/O error: " + ex.getMessage());
        }
    }
}
