package Objects;

import Networking.NetworkSingleton;
import Networking.Packet;
import com.sun.xml.internal.bind.v2.runtime.reflect.Lister;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import sun.nio.ch.Net;

import javax.imageio.IIOException;
import java.io.IOException;

public class Grid extends BaseGameObject {
    public class Cell extends BaseGameObject implements IClickable
    {
        private double xGridPos;
        private double yGridPos;

        private static final int cellWidth = 10;
        private static final int cellHeight = 10;

        public Cell(double xGridPos, double yGridPos, Image sprite, double offsetX, double offsetY) {
            super(xGridPos * cellWidth + offsetY, yGridPos * cellHeight + offsetX);
            this.sprite = sprite;
        }

        @Override
        public void draw(GraphicsContext gc) {
            drawImage(gc);
        }

        @Override
        public void onClick() {

            System.out.println("clicked cell at: " + (int)this.getPosX()/cellWidth+ " " + (int) this.getPosY()/cellHeight);
            try{
                byte[] b = { Byte.valueOf((byte) (this.getPosX() /cellWidth)),Byte.valueOf((byte) (this.getPosY()/cellHeight))};
                Packet p = new Packet(b);
                NetworkSingleton.getInstance().sendData(p);
            } catch (IOException e){

            }

        }

    }
    private Cell[][] cells;
    private int gridWidth = 10;
    private int gridHeight = 15;

    public Grid(double posY, double posX, int a_gridHeight, int a_gridWidth) {
        super(posY, posX);
        this.gridHeight = a_gridHeight;
        this.gridWidth = a_gridWidth;
        cells = new Cell[gridWidth][gridHeight];
        for(int i = 0; i < cells.length; i++)
        {
            for (int j = 0; j < cells[i].length; j++)
            {
                cells[i][j] = new Cell(i, j,new Image("sea.png",10,10,false,true), this.getPosX(), this.getPosY());
            }
        }
    }
    public Cell getCell(int y, int x) {
        return cells[x][y];
    }

    public void getClickedCell(double clickposX, double clickposY){
        for(int i = 0; i < cells.length; i++)
        {
            for(int j = 0; j < cells[i].length; j++)
            {
                if(cells[i][j] instanceof IClickable){
                    //if so run the function onclick
                    if(cells[i][j].clickedOn(clickposX,clickposY)) {
                        ((IClickable) cells[i][j]).onClick();
                    }
                }
//                System.out.println(cells[i][j].clickedOn(clickposX,clickposY));
            }
        }
    }

    @Override
    public void draw(GraphicsContext gc) {
        super.draw(gc);
        for(int i = 0; i < cells.length; i++)
        {
            for(int j = 0; j < cells[i].length; j++)
            {
                cells[i][j].draw(gc);
            }
        }
    }
}


