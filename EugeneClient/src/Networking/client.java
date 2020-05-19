package Networking;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * This program is a socket client application that connects to a time server
 * to get the current date time.
 *
 * @author www.codejava.net
 */
public class client extends Thread {
    private String hostname;
    private int port;
    public Thread t;

    public client(String a_hostname, int a_port){

        this.hostname = a_hostname;
        this.port = a_port;
        run();
    }

    public void start(){
        System.out.println("Starting " );
        if (t == null) {
            t = new Thread (this);
            t.start ();
        }
    }


    public void run() {
        System.out.println("MyThread running");


        try (Socket socket = new Socket(hostname, port)) {

            InputStream input = socket.getInputStream();
            InputStreamReader reader = new InputStreamReader(input);
            while (true) {
                System.out.println(reader.read());
            }
        } catch (UnknownHostException ex) {

            System.out.println("Server not found: " + ex.getMessage());

        } catch (IOException ex) {

            System.out.println("I/O error: " + ex.getMessage());
        }
    }
}
