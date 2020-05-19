package battleship;

import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.input.DragEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import sceneswitcher.IEventPane;
import sceneswitcher.ViewController;

public class LoginView extends GridPane implements IEventPane
{
    Button btn_switch;
    TextField txt_ip, txt_port;
    Label lbl_ip, lbl_port;


    public LoginView()
    {
        lbl_port = new Label("server port");
        lbl_ip = new Label("server adress");
        txt_ip = new TextField();
        txt_port = new TextField();

        btn_switch = new Button("login");

        btn_switch.setOnAction(click -> {
            ViewController.show("GameView", txt_ip.getText(),txt_port.getText());
        });
        this.add(lbl_ip,0,0);
        this.add(lbl_port,1,0);
        this.add(txt_ip,0,1);
        this.add(txt_port,1,1);
        this.add(btn_switch,0,2);
    }

    @Override
    public void onFocusGained(Object... a_data)
    {

    }

    @Override
    public void onFocusLost()
    {

    }

    @Override
    public void onStop() {

    }

    @Override
    public void onClick(MouseEvent a_event) {

    }

}