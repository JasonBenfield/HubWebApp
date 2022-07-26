import { Awaitable } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Awaitable";
import { Command } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/Command";
import { LogEntryQueryPanelView } from "./LogEntryQueryPanelView";

interface IResult {
    menuRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class LogEntryQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();

    constructor(private readonly view: LogEntryQueryPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}