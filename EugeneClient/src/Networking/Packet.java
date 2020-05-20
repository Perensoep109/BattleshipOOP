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
}
