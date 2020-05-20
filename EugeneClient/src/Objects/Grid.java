package Objects;

import Networking.NetworkSingleton;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import sun.nio.ch.Net;

import javax.imageio.IIOException;
import java.io.IOException;

public class Grid extends BaseGameObject {
    private class Cell extends BaseGameObject implements IClickable
    {
        private int xGridPos;
        private int yGridPos;

        private static final int cellWidth = 32;
        private static final int cellHeight = 32;

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
            this.sprite = new Image("land.png");
            System.out.println("clicked cell at: " + this.getPosX()+ " " + this.getPosY());
            try{
                NetworkSingleton.getInstance().sendData("E");
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
                cells[i][j] = new Cell(i, j,new Image("sea.png"), this.getPosX(), this.getPosY());
            }
        }
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


