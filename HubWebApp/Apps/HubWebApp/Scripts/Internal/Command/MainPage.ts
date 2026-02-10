import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { CommandPanel } from './CommandPanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly menuPanel: MainMenuPanel;
    private readonly commandPanel: CommandPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.menuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        this.commandPanel = this.panels.add(
            new CommandPanel(this.hubClient, view.commandPanel)
        );
        const commandID = Url.current().query.getNumberValue("CommandID");
        if (commandID) {
            this.commandPanel.setCommandID(commandID);
            this.commandPanel.refresh();
            this.activateCommandPanel();
        }
        else {
            this.hubClient.Home.Index.open({});
        }
    }

    private async activateMenuPanel() {
        this.panels.activate(this.menuPanel);
        const result = await this.menuPanel.start();
        if (result.back) {
            this.activateCommandPanel();
        }
    }

    private async activateCommandPanel() {
        this.panels.activate(this.commandPanel);
        const result = await this.commandPanel.start();
        if (result.menu) {
            this.activateMenuPanel();
        }
    }
}
new MainPage(new MainPageView());