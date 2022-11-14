import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Apis } from '../Apis';
import { MainMenuPanel } from '../MainMenuPanel';
import { ChangePasswordPanel } from './ChangePasswordPanel';
import { MainPageView } from './MainPageView';
import { UserEditPanel } from './UserEditPanel';
import { UserPanel } from './UserPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly userPanel: UserPanel;
    private readonly userEditPanel: UserEditPanel;
    private readonly changePasswordPanel: ChangePasswordPanel;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(hubApi, this.view.mainMenuPanel)
        );
        this.userPanel = this.panels.add(
            new UserPanel(hubApi, this.view.userPanel)
        );
        this.userEditPanel = this.panels.add(new UserEditPanel(hubApi, this.view.userEditPanel));
        this.changePasswordPanel = this.panels.add(new ChangePasswordPanel(hubApi, this.view.changePasswordPanel));
        this.userPanel.refresh();
        this.activateUserPanel();
    }

    private async activateUserPanel() {
        this.panels.activate(this.userPanel);
        const result = await this.userPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
        else if (result.editRequested) {
            this.userEditPanel.setUser(result.editRequested.user);
            this.activateUserEditPanel();
        }
        else if (result.changePasswordRequested) {
            this.activateChangePasswordPanel();
        }
    }

    private async activateUserEditPanel() {
        this.panels.activate(this.userEditPanel);
        const result = await this.userEditPanel.start();
        if (result.saved) {
            this.userPanel.setUser(result.saved.user);
            this.activateUserPanel();
        }
        else if (result.canceled) {
            this.activateUserPanel();
        }
    }

    private async activateChangePasswordPanel() {
        this.panels.activate(this.changePasswordPanel);
        const result = await this.changePasswordPanel.start();
        if (result.done) {
            this.activateUserPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateUserPanel();
        }
    }
}
new MainPage();