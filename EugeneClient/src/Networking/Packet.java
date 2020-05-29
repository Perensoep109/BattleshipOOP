package Networking;

import Objects.Grid;
import javafx.scene.image.Image;

public class Packet {
    public byte[] packetdata;

    public Packet(byte[] data){
        this.packetdata = data;
        }

    public byte[] getData() {
        return packetdata;
    }


    public byte getType(){
        return packetdata[0];
    }


    public byte getBoardType(){
        if(packetdata[0] == 1){
            return packetdata[1];
        }
        return 0;
    }
}
