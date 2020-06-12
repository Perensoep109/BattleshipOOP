package Objects;

import Networking.NetworkSingleton;
import Networking.Packet;
import battleship.GameManager;
import com.sun.xml.internal.bind.v2.runtime.reflect.Lister;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import sun.nio.ch.Net;

import javax.imageio.IIOException;
import java.io.IOException;

public class Grid extends BaseGameObject {
    public class Cell extends BaseGameObject implements IClickable
    {
        private int xGridPos;
        private int yGridPos;
        private int gridID;



        public Cell(int xGridPos, int yGridPos, Image sprite, double offsetX, double offsetY, int gridID) {
            super(xGridPos * cellWidth + offsetY, yGridPos * cellHeight + offsetX);
            this.xGridPos = xGridPos;
            this.yGridPos = yGridPos;
            this.gridID = gridID;
            this.sprite = sprite;
        }

        @Override
        public void draw(GraphicsContext gc) {

            drawImage(gc);

        }
        public void changeTile(){
            this.sprite = new Image("land.png",cellWidth,cellHeight,false,true);
        }
        @Override
        public void onClick() {

            System.out.println("clicked cell at grid: " +this.gridID + " at " + this.xGridPos + " " + this.yGridPos);
            GameManager.getInstance().fireShot(this.gridID,this.xGridPos,this.yGridPos);

        }

    }
    private Cell[][] cells;
    private int gridWidth;
    private int gridHeight;
    private int GridID;
    private int cellWidth = 20;
    private int cellHeight = 20;

    public Grid(double posY, double posX, int a_gridHeight, int a_gridWidth, int GridID) {

        super(posY, posX);
        this.GridID = GridID;
        this.gridHeight = a_gridHeight;
        this.gridWidth = a_gridWidth;
        cells = new Cell[gridWidth][gridHeight];
        for(int i = 0; i < cells.length; i++)
        {
            for (int j = 0; j < cells[i].length; j++)
            {
                cells[i][j] = new Cell(i, j,new Image("sea.png",cellWidth,cellHeight,false,true), this.getPosX(), this.getPosY(),GridID);
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
            }
        }
    }

    @Override
    public void draw(GraphicsContext gc) {
        super.draw(gc);

        for(int i = 0; i < cells.length; i++)
        {
            gc.strokeText("" +i, posX -15,posY + (cellHeight * i) + (cellHeight/2));
            for(int j = 0; j < cells[i].length; j++)
            {
                gc.strokeText("" +j,posX + (cellWidth * j) + (cellWidth/2), posY -10);
                cells[i][j].draw(gc);
            }
        }
    }

    public int getGridId(){
        return GridID;
    }
}


