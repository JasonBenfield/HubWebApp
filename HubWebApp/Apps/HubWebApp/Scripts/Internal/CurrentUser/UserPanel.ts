import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { UserPanelView } from "./UserPanelView";

interface IResult {
    readonly menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}


export class UserPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly userName: TextComponent;
    private readonly personName: TextComponent;
    private readonly email: TextComponent;
    private readonly alert: MessageAlert;

    constructor(private readonly hubApi: HubAppApi, private readonly view: UserPanelView) {
        this.userName = new TextComponent(view.userName);
        this.personName = new TextComponent(view.personName);
        this.email = new TextComponent(view.email);
        this.alert = new MessageAlert(view.alert);
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    async refresh() {
        const user = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.CurrentUser.GetUser()
        );
        this.userName.setText(user.UserName.DisplayText);
        this.personName.setText(user.Name.DisplayText);
        this.email.setText(user.Email);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }

}