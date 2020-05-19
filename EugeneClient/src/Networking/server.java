package Networking;

import javafx.application.Application;
import javafx.stage.Stage;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Date;

public class server extends Application {

    @Override
    public void start(Stage primaryStage) throws Exception {
        String hostName = "127.0.0.1";
        int portNumber = 69;

        try (
                Socket echoSocket = new Socket(hostName, portNumber);

        ) catch()
    }
}

