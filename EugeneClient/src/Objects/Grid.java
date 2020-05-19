package Objects;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

public class Grid extends BaseGameObject {
    private class Cell extends BaseGameObject
    {
        private int xGridPos;
        private int yGridPos;
        private static final int cellWidth = 32;
        private static final int cellHeight = 32;

        public Cell(double xGridPos, double yGridPos, Image sprite) {
            super(xGridPos * cellWidth, yGridPos * cellHeight);
            this.sprite = sprite;
        }

        @Override
        public void draw(GraphicsContext gc) {
            drawImage(gc, xGridPos * cellWidth, yGridPos * cellHeight);
        }
    }

    public Grid(double posY, double posX) {
        super(posY, posX);
        cells = new Cell[16][16];
        for(int i = 0; i < cells.length; i++)
        {
            for (int j = 0; j < cells.length; j++)
            {
                cells[i][j] = new Cell(i, j,new Image("yoink.png"));
            }
        }
    }

    private Cell[][] cells;

    @Override
    public void draw(GraphicsContext gc) {
        super.draw(gc);
        for(int i = 0; i < cells.length; i++)
        {
            for(int j = 0; j < cells.length; j++)
            {
                cells[i][j].draw(gc);
            }
        }
    }
}
