package Networking;

import java.io.*;
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
    private InputStreamReader isr;
    private OutputStreamWriter osw;


    public client(String a_hostname, int a_port){

        this.hostname = a_hostname;
        this.port = a_port;
    }

    public void start(){
        System.out.println("Starting" );



    }
    public void sendData(String data) throws IOException {
        if (osw != null){
            osw.write(data);
        }

    }




    public void run() {
        System.out.println("thread running");

        try (Socket socket = new Socket(hostname, port)) {

            InputStream input = socket.getInputStream();
            isr = new InputStreamReader(input);
            OutputStream output = socket.getOutputStream();
            osw = new OutputStreamWriter(output);
            while (true) {
                System.out.println(isr.read());
            }
        } catch (UnknownHostException ex) {

            System.out.println("Server not found: " + ex.getMessage());

        } catch (IOException ex) {

            System.out.println("I/O error: " + ex.getMessage());
        }
    }
}
