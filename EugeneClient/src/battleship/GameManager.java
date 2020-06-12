package battleship;

import Networking.NetworkSingleton;
import Objects.Grid;

public final class GameManager{
    GameView gv;
    private static GameManager INSTANCE;
   public Grid playerfield;
    public Grid enemyfield;

    public GameManager(){
    }
    public void setGameView(GameView gv){
        this.gv = gv;
    }

    public void setPlayerfield(Grid playerfield) {
        this.playerfield = playerfield;
    }

    public void setEnemyfield(Grid enemyfield) {
        this.enemyfield = enemyfield;
    }

    public static GameManager getInstance() {
        if(INSTANCE == null) {
            INSTANCE = new GameManager();
        }

        return INSTANCE;
    }

    public enum State{
        LOBBY,
        LOADING,
        PLAYERTURN,
        ENEMYTURN,
        END
    }
    private State state = State.PLAYERTURN;

    public void fireShot(int player, int posY, int posX){
        if(state == State.PLAYERTURN){
        NetworkSingleton.getInstance().getC().fireShot(player,posX,posY);
            System.out.println("fired");
//            state = State.ENEMYTURN;
        }
    }

    public void getShot(int player, int posY, int posX){
        state = State.PLAYERTURN;
        System.out.println(player + " got shot at " + posY + " " + posX );
        enemyfield.getCell(posY,posX).changeTile();


    }
    public void testevent(int player, int posx, int posy){

    }
}

