package battleship;

import javafx.application.Application;
import javafx.stage.Stage;
import sceneswitcher.View;
import sceneswitcher.ViewController;


public class Main extends Application {


    @Override
    public void start(Stage primaryStage) throws Exception
    {
//
        ViewController.setStage(primaryStage);
        ViewController.addView(new View(new LoginView(), 200, 200), "LoginView");
        ViewController.addView(new View(new GameView(), 1000, 1000), "GameView");
        ViewController.show("LoginView");
    }

    @Override
    public void stop() throws Exception
    {
        ViewController.stop();
    }




    public static void main(String[] args) {
        launch(args);
    }
}
