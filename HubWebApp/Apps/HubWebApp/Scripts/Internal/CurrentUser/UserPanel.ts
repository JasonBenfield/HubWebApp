import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
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

    constructor(private readonly hubApi: HubAppApi, private readonly view: UserPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }

}